using System.Linq;
using NUnit.Framework;

namespace Snipe.Tests
{
    [TestFixture]
    public class Parsing_a_valid_Specification_file : ParsingFilesContext
    {
        [TestFixtureSetUp]
        public void BeforeAll()
        {
            given_a_valid_specFile();
            when_it_does_its_thing();
        }

        [TestFixtureTearDown]
        public void AfterAll()
        {

        }

        [Test]
        public void it_should_load_the_contexts()
        {
            Assert.IsNotEmpty(_theResult.Contexts);
        }

        [Test]
        public void the_context_value_should_equal_the_text()
        {
            Assert.AreEqual("ParsingSpecificationFilesContext", _theResult.Contexts["parsingspecificationfiles"].MemberName);
        }

        [Test]
        public void it_should_load_all_the_scenarios()
        {
            Assert.AreEqual(_theResult.Scenarios.Count(), 2);
        }

        [Test]
        public void the_scenario_memberNames_should_be_underscore_spaced()
        {
            Assert.AreEqual(_theResult.Scenarios["parsingavalidspecfile"].MemberName, ("Parsing_a_valid_spec_file"));
            Assert.AreEqual(_theResult.Scenarios["parsinganinvalidspecfile"].MemberName, ("Parsing_an_invalid_spec_file"));
        }

        [Test]
        public void it_should_load_all_the_givens()
        {
            Assert.AreEqual(3, _theResult.Givens.Count());
        }

        [Test]
        public void it_should_load_all_the_whens()
        {
            Assert.AreEqual(1, _theResult.Whens.Count());
        }

        [Test]
        public void it_should_load_all_the_thens()
        {
            Assert.AreEqual(4, _theResult.Thens.Count());
        }
    }
}