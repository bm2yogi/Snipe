using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Snipe.Tests.Parsing_Files
{
    [TestFixture]
    public class ParsingFilesContext
    {
        private string[] _theSpecFile;
        private SpecFileParser _theParser;
        private SpecFile _theResult;

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            given_a_valid_specFile();
            when_it_does_its_thing();
        }

        private void given_a_valid_specFile()
        {
            _theSpecFile = ValidSpecFile.ToArray();
        }

        private void when_it_does_its_thing()
        {
            _theParser = new SpecFileParser(_theSpecFile);
            _theResult = _theParser.SpecFile;
        }

        [TestFixtureTearDown]
        public void AfterAll()
        {

        }

        [Test]
        public void then_it_should_load_the_contexts()
        {
            Assert.IsNotEmpty(_theResult.Contexts);
        }

        [Test]
        public void then_the_context_value_should_equal_the_text()
        {
            Assert.AreEqual(_theResult.Contexts["parsingspecificationfiles"].MemberName,("ParsingSpecificationFilesContext"));
        }

        [Test]
        public void then_it_should_load_all_the_scenarios()
        {
            Assert.AreEqual(_theResult.Scenarios.Count(), 2);
        }

        [Test]
        public void then_the_scenario_memberNames_should_be_underscore_spaced()
        {
            Assert.AreEqual(_theResult.Scenarios["parsingavalidspecfile"].MemberName, ("Parsing_a_valid_spec_file"));
            Assert.AreEqual(_theResult.Scenarios["parsinganinvalidspecfile"].MemberName, ("Parsing_an_invalid_spec_file"));
        }

        public IEnumerable<string> ValidSpecFile
        {
            get
            {
                yield return "Context: Parsing Specification Files";
                yield return "";
                yield return "Scenario: Parsing a valid spec file";
                yield return "";
                yield return "Given a valid specification file";
                yield return "Given a bright sunny day";
                yield return "When the file is parsed";
                yield return "Then it should make everyone happy";
                yield return "Then it should bring about world peace";
                yield return "";
                yield return "Scenario: Parsing an invalid spec file";
                yield return "";
                yield return "Given an invalid specification file";
                yield return "Given a bright sunny day";
                yield return "When the file is parsed";
                yield return "Then it should make everyone sad";
                yield return "Then it should bring about world war three";
            }
        }
    }

    public class SpecFileParser
    {
        private readonly SpecFile _specFile;
        private readonly IEnumerable<string> _specFileData;

        public SpecFileParser(IEnumerable<string> specFileData)
        {
            _specFile = new SpecFile();
            _specFileData = specFileData;
            Parse();
        }

        private void Parse()
        {
            _specFileData
                .Select(line => new SpecLine(line))
                .ToList()
                .ForEach(ParseLine);
        }

        public SpecFile SpecFile
        {
            get { return _specFile; }
        }

        private void ParseLine(SpecLine specLine)
        {

            if (IsContext(specLine))
            {
                AddContext(specLine);
            }

            if (IsScenario(specLine))
            {
                AddScenario(specLine);
            }
        }

        private static bool IsScenario(SpecLine specLine)
        {
            return specLine.FirstWord.StartsWith("Scenario");
        }

        private static bool IsContext(SpecLine specLine)
        {
            return specLine.FirstWord.StartsWith("Context");
        }

        private void AddContext(SpecLine specLine)
        {
            if (_specFile.Contexts.ContainsKey(specLine.Key)) return;

            _specFile.Contexts.Add(specLine.Key, new Context(specLine));
        }

        private void AddScenario(SpecLine specLine)
        {
            if (_specFile.Scenarios.ContainsKey(specLine.Key)) return;

            _specFile.Scenarios.Add(specLine.Key, new Scenario(specLine));
        }
    }

    public class SpecLine
    {
        public string FirstWord = "";
        public string Key = "";
        public string[] Text = new string[]{};

        public SpecLine(string line)
        {
            if (String.IsNullOrEmpty(line.Trim())) return;

            var words = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            FirstWord = words.First();
            Text = words.Skip(1).ToArray();
            Key = Text.Aggregate((current, next) => current + next).ToLowerInvariant();
        }
    }


    public class SpecFile
    {
        public Dictionary<string, Context> Contexts { get; set; }
        public Dictionary<string, Scenario> Scenarios { get; set; }

        public SpecFile()
        {
            Contexts = new Dictionary<string, Context>();
            Scenarios = new Dictionary<string, Scenario>();
        }
    }

    public class Context
    {
        private readonly SpecLine _specLine;
        private readonly string _memberName;

        public Context(SpecLine specLine)
        {
            _specLine = specLine;
            _memberName =
            _memberName = _specLine.Text.Aggregate((c, n) => c + n) + "Context";
        }
        
        public string MemberName
        {
            get { return _memberName; }
        }
    }

    public class Scenario
    {
        private readonly SpecLine _specLine;
        private readonly string _memberName;

        public Scenario(SpecLine specLine)
        {
            _specLine = specLine;
            _memberName = _specLine.Text.Aggregate((c, n) => c + "_" + n);
        }

        public string MemberName { get { return _memberName; } }
    }
}