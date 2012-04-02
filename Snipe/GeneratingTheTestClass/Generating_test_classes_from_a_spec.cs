using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Snipe.Tests
{
    [TestFixture]
    public class Generating_test_classes_from_a_spec : GeneratingTheTestClassContext
    {
        [TestFixtureSetUp]
        public void BeforeAll()
        {
            given_a_parsed_specfile();
            when_a_test_classFile_is_generated();
        }

        [TestFixtureTearDown]
        public void AfterAll()
        {
        }

        [Test]
        public void it_should_look_like_this()
        {
            Console.Write(Output);
        }
    }

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
                _theSpecFile.Givens.Values.ToList().ForEach(WriteGiven);
                _theSpecFile.Whens.Values.ToList().ForEach(WriteWhen);
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
            _builder.AppendLine();
            _builder.AppendLine("public void AfterAll()");
            WrapWithBraces(() => { });
        }

        private void WriteContextSetup()
        {
            _builder.AppendLine();
            _builder.AppendLine("public void BeforeAll()");
            WrapWithBraces(() => { });
        }

        private void WriteGiven(Given given)
        {
            _builder.AppendLine();
            _builder.AppendLine(string.Format("public void {0}()", given.MemberName));
            WrapWithBraces(() => { });
        }

        private void WriteWhen(When when)
        {
            _builder.AppendLine();
            _builder.AppendLine(string.Format("public void {0}()", when.MemberName));
            WrapWithBraces(() => { });
        }

        private void WriteThen(Then then)
        {
            _builder.AppendLine();
            _builder.AppendLine(@"[Test]");
            _builder.AppendLine(string.Format(@"public void {0}()", then.MemberName));

            WrapWithBraces(() => _builder.AppendLine(@"Assert.Fail(""Not implemented."""));
        }

        private void WrapWithBraces(Action writeBlock)
        {
            _builder.AppendLine(@"{");
            writeBlock();
            _builder.AppendLine(@"}");
        }
    }
}