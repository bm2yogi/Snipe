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
            Assert.AreEqual("ParsingSpecificationFilesContext", _theResult.Contexts["parsingspecificationfiles"].MemberName);
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

        [Test]
        public void then_it_should_load_all_the_givens()
        {
            Assert.AreEqual(3, _theResult.Givens.Count());
        }

        [Test]
        public void then_it_should_load_all_the_whens()
        {
            Assert.AreEqual(1, _theResult.Whens.Count());
        }

        [Test]
        public void then_it_should_load_all_the_thens()
        {
            Assert.AreEqual(4, _theResult.Thens.Count());
        }

        public IEnumerable<string> ValidSpecFile
        {
            get
            {
                yield return "Context: Parsing Specification Files";
                yield return "";
                yield return "Scenario: Parsing a  valid spec file";
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
}