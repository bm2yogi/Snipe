using System;
using System.Linq;
using System.Text;

namespace Snipe
{
    public class ContextSpecBuilder
    {
        private readonly Context _context;
        private readonly ISpecFile _theSpecFile;
        private readonly StringBuilder _builder = new StringBuilder();

        public ContextSpecBuilder(ISpecFile theSpecFile)
        {
            _theSpecFile = theSpecFile;
            _context = _theSpecFile.Contexts.First().Value;
        }

        public void Build()
        {
            WriteContext();
            _context.Scenarios.ToList().ForEach(WriteScenario);
            Output = _builder.ToString();
        }

        public string Output { get; set; }

        private void WriteContext()
        {
            _builder.AppendLine(string.Format(@"public class {0}", _context));

            WrapWithBraces(() => _context.Scenarios
                                     .SelectMany(s => s.Givens)
                                     .Union(_context.Scenarios.SelectMany(s => s.Whens))
                                     .Distinct()
                                     .ToList()
                                     .ForEach(WriteGivenWhen));
        }

        private void WriteScenario(Scenario scenario)
        {
            _builder.AppendLine(@"[TestFixture]");
            _builder.AppendLine(string.Format(@"public class {0} : {1}", scenario, _context));

            WrapWithBraces(() =>
                               {
                                   WriteContextSetup();
                                   WriteContextTearDown();
                                   scenario.Thens.ToList().ForEach(WriteThen);
                               });
        }

        private void WriteContextTearDown()
        {
            _builder.AppendLine(@"[TestFixtureTearDown]");
            _builder.AppendLine(@"protected void AfterAll()");
            WrapWithBraces(() => { });
        }

        private void WriteContextSetup()
        {
            _builder.AppendLine(@"[TestFixtureSetUp]");
            _builder.AppendLine(@"protected void BeforeAll()");
            WrapWithBraces(() => { });
        }

        private void WriteGivenWhen(SpecPart specPart)
        {
            _builder.AppendLine(string.Format("protected void {0}()", specPart));
            WrapWithBraces(() => { });
        }

        private void WriteThen(SpecPart then)
        {
            _builder.AppendLine(@"[Test]");
            _builder.AppendLine(string.Format(@"public void {0}()", then));

            WrapWithBraces(() => _builder.AppendLine("Assert.Fail(\"Not implemented.\")"));
        }

        private void WrapWithBraces(Action writeBlock)
        {
            _builder.AppendLine(@"{");
            writeBlock();
            _builder.AppendLine(@"}");
            _builder.AppendLine();
        }
    }
}