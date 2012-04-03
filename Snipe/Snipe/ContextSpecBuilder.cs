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
            _builder.AppendLine(string.Format(@"public class {0}", _context.MemberName));

            WrapWithBraces(() =>
                               {
                                   _context.Scenarios.SelectMany(s => s.Givens).ToList().ForEach(WriteGiven);
                                   _context.Scenarios.SelectMany(s => s.Whens).ToList().ForEach(WriteWhen);
                               });
        }

        private void WriteScenario(Scenario scenario)
        {
            _builder.AppendLine(@"[TestFixture]");
            _builder.AppendLine(string.Format(@"public class {0} : {1}", scenario.MemberName, _context.MemberName));

            WrapWithBraces(() =>
                               {
                                   WriteContextSetup();
                                   WriteContextTearDown();
                                   scenario.Thens.ForEach(WriteThen);
                               });
        }

        private void WriteContextTearDown()
        {
            _builder.AppendLine("protected void AfterAll()");
            WrapWithBraces(() => { });
        }

        private void WriteContextSetup()
        {
            _builder.AppendLine("protected void BeforeAll()");
            WrapWithBraces(() => { });
        }

        private void WriteGiven(Given given)
        {
            _builder.AppendLine(string.Format("protected void {0}()", given.MemberName));
            WrapWithBraces(() => { });
        }

        private void WriteWhen(When when)
        {
            _builder.AppendLine(string.Format("protected void {0}()", when.MemberName));
            WrapWithBraces(() => { });
        }

        private void WriteThen(Then then)
        {
            _builder.AppendLine(@"[Test]");
            _builder.AppendLine(string.Format(@"public void {0}()", then.MemberName));

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