using System;
using System.Linq;
using System.Text;

namespace Snipe
{
    public class ContextSpecBuilder
    {
        private int _indentLevel = 0;
        private readonly Context _context;
        private Scenario _currentScenario;
        private readonly ISpecFile _theSpecFile;
        private readonly StringBuilder _builder = new StringBuilder();

        public ContextSpecBuilder(ISpecFile theSpecFile)
        {
            _theSpecFile = theSpecFile;
            _context = _theSpecFile.Contexts.First().Value;
            _currentScenario = _context.Scenarios.First();
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
            AppendLine(string.Format(@"public class {0}", _context));

            WrapWithBraces(() => _context.Scenarios
                                     .SelectMany(s => s.Givens)
                                     .Union(_context.Scenarios.SelectMany(s => s.Whens))
                                     .Distinct()
                                     .ToList()
                                     .ForEach(WriteGivenWhen));
        }

        private void WriteScenario(Scenario scenario)
        {
            _currentScenario = scenario;

            AppendLine(@"[TestFixture]");
            AppendLine(string.Format(@"public class {0} : {1}", scenario, _context));

            WrapWithBraces(() =>
                               {
                                   WriteContextSetup();
                                   WriteContextTearDown();
                                   scenario.Thens.ToList().ForEach(WriteThen);
                               });
        }

        private void WriteContextTearDown()
        {
            AppendLine(@"[TestFixtureTearDown]");
            AppendLine(@"protected void AfterAll()");
            WrapWithBraces(() => { });
        }

        private void WriteContextSetup()
        {
            AppendLine(@"[TestFixtureSetUp]");
            AppendLine(@"protected void BeforeAll()");
            WrapWithBraces(() => _currentScenario.Givens
                                     .Union(_currentScenario.Whens)
                                     .ToList()
                                     .ForEach(s => AppendLine(string.Format("{0}();", s))));
        }

        private void WriteGivenWhen(SpecPart specPart)
        {
            AppendLine(string.Format("protected void {0}()", specPart));
            WrapWithBraces(() => AppendLine("// not implemented."));
        }

        private void WriteThen(SpecPart then)
        {
            AppendLine(@"[Test]");
            AppendLine(string.Format(@"public void {0}()", then));

            WrapWithBraces(() => AppendLine("Assert.Fail(\"Not implemented.\")"));
        }

        private void AppendLine(string line="")
        {
            var tabs = String.Empty.PadLeft(_indentLevel, '\t');
            _builder.AppendLine(string.Format("{0}{1}", tabs, line));
        }

        private void WrapWithBraces(Action writeBlock)
        {
            AppendLine(@"{");
                Indent();
                writeBlock();
                Outdent();
            AppendLine(@"}");

            AppendLine();
        }

        private void Indent()
        {
            _indentLevel++;
        }

        private void Outdent()
        {
            _indentLevel--;
        }
    }
}