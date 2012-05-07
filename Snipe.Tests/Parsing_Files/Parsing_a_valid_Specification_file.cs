using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Snipe.Tests.ParsingFiles
{
    [TestFixture]
    public class When_parsing_a_valid_Specification_file : ParsingFilesContext
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
        public void it_should_have_one_context()
        {
            Assert.AreEqual(1, TheResult.Contexts.Count);
        }

        private Context TheFirstContext
        {
            get { return TheResult.Contexts.First().Value; }
        }

        [Test]
        public void the_context_memberName_should_be_PascalCased()
        {
            Assert.AreEqual("ParsingSpecificationFilesContext", TheFirstContext.MemberName);
        }

        [Test]
        public void the_first_context_should_have_two_scenarios()
        {
            Assert.AreEqual(2, TheFirstContext.Scenarios.Count());
        }

        [Test]
        public void the_scenario_memberNames_should_be_underscore_spaced()
        {
            Assert.AreEqual(TheFirstContext.Scenarios.First().MemberName, ("Parsing_a_valid_spec_file"));
            Assert.AreEqual(TheFirstContext.Scenarios.Skip(1).First().MemberName, ("Parsing_an_invalid_spec_file"));
        }

        [Test]
        public void the_generated_class_and_membernames_should_be_stripped_of_illegal_characters()
        {
            const string pattern = "^[`~!@#$%^&*()-=+,./<>?;':\"[]\\{}|]*$";
            var specParts = TheFirstContext.Scenarios.SelectMany(sc => sc.Givens.Union(sc.Whens.Union(sc.Thens)));
            Assert.IsFalse(specParts.Any(sc => Regex.IsMatch(sc.MemberName, pattern)));
        }

        [Test]
        public void the_first_scenario_should_have_2_givens()
        {
            Assert.AreEqual(2, TheFirstContext.Scenarios.First().Givens.Count());
        }

        [Test]
        public void the_second_scenario_should_have_2_givens()
        {
            Assert.AreEqual(2, TheFirstContext.Scenarios.Skip(1).First().Givens.Count());
        }

        [Test]
        public void the_first_scenario_should_have_1_when()
        {
            Assert.AreEqual(1, TheFirstContext.Scenarios.First().Whens.Count());
        }

        [Test]
        public void the_second_scenario_should_have_1_when()
        {
            Assert.AreEqual(1, TheFirstContext.Scenarios.Skip(1).First().Whens.Count());
        }

        [Test]
        public void the_first_scenario_should_have_3_thens()
        {
            Assert.AreEqual(3, TheFirstContext.Scenarios.First().Thens.Count());
        }

        [Test]
        public void the_second_scenario_should_have_3_thens()
        {
            Assert.AreEqual(3, TheFirstContext.Scenarios.Skip(1).First().Thens.Count());
        }
    }
}