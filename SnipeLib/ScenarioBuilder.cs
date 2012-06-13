using System.Linq;
using Snipe;

namespace SnipeLib
{
    public interface IScenarioBuilder
    {
        void BuildScenario(Scenario scenario, Context context);
    }

    public class ScenarioBuilder : IScenarioBuilder
    {
        private readonly IApplicationHost _applicationHost;
        private readonly IBlockWriter _blockWriter;

        public ScenarioBuilder(IApplicationHost applicationHost, IBlockWriter blockWriter)
        {
            _applicationHost = applicationHost;
            _blockWriter = blockWriter;
        }

        public void BuildScenario(Scenario scenario, Context context)
        {
            _applicationHost.WriteFile(string.Format(@"{0}\{1}.cs", context.Namespace, scenario), Build(scenario, context));
        }

        private string Build(Scenario scenario, Context context)
        {

            _blockWriter.AppendLine(@"using System;");
            _blockWriter.AppendLine(@"using NUnit.Framework;");
            _blockWriter.AppendLine();
            _blockWriter.AppendLine(string.Format(@"namespace {0}", context.Namespace));

            _blockWriter.WrapWithBraces(() =>
            {
                _blockWriter.AppendLine(@"[TestFixture]");
                _blockWriter.AppendLine(string.Format(@"public class {0} : {1}", scenario, context));

                _blockWriter.WrapWithBraces(() =>
                                                {
                                                    WriteContextSetup(scenario);
                                                    WriteContextTearDown();
                                                    scenario.Thens.ToList().ForEach(WriteThen);
                                                });
            });

            return _blockWriter.Flush();
        }

        private void WriteContextTearDown()
        {
            _blockWriter.AppendLine(@"[TestFixtureTearDown]");
            _blockWriter.AppendLine(@"protected void AfterAll()");
            _blockWriter.WrapWithBraces(() => { });
        }

        private void WriteContextSetup(Scenario scenario)
        {
            _blockWriter.AppendLine(@"[TestFixtureSetUp]");
            _blockWriter.AppendLine(@"protected void BeforeAll()");
            _blockWriter.WrapWithBraces(() => scenario.Givens
                                     .Union(scenario.Whens)
                                     .ToList()
                                     .ForEach(s => _blockWriter.AppendLine(string.Format("{0}();", s))));
        }

        private void WriteThen(SpecPart then)
        {
            _blockWriter.AppendLine(@"[Test]");
            _blockWriter.AppendLine(string.Format(@"public void {0}()", then));

            _blockWriter.WrapWithBraces(() => _blockWriter.AppendLine("Assert.Fail(\"Not implemented.\");"));
        }
    }
}