using System.Linq;
using NUnit.Framework;

namespace Snipe.Tests
{
	public class CreatingSpecPartsContext
	{
	    protected string SpecLine;
	    protected SpecPart TheSpecPart;
	    protected Context TheContext;
	    protected string[] Spec;
	    protected SpecFile TheSpecFile;

	    protected void given_a_specLine_with_mixed_caps()
	    {
	        SpecLine = "gIVEn a SpeClINe wIth MixeD CaPS";
	    }
		
		protected void given_a_context_with_multiple_scenarios()
		{
		    Spec = new[]
		               {
		                   "Context: The Context",
		                   "Scenario: The first scenario",
		                   "",
		                   "Scenario: The second scenario",
		                   ""
		               };
		}
		
		protected void given_each_scenario_with_speclines_that_differ_only_in_capitalization()
		{
		    const string given = "Given the first specline";

		    Spec[2] = given.ToLowerInvariant();
		    Spec[4] = given.ToUpperInvariant();
		}

        protected void when_parsing_the_file()
        {
            var parser = new SpecFileParser(Spec);
            TheSpecFile = parser.SpecFile;
        }
		
		protected void when_parsing_the_specline()
		{
		    TheSpecPart = SpecPartFactory.Create(SpecLine);
		}
		
	}
	
	[TestFixture]
	public class specLines_with_mixed_capitalization : CreatingSpecPartsContext
	{
		[TestFixtureSetUp]
		protected void BeforeAll()
		{
			given_a_specLine_with_mixed_caps();
			when_parsing_the_specline();
		}
		
		[TestFixtureTearDown]
		protected void AfterAll()
		{
		}
		
		[Test]
		public void the_first_word_should_be_capitalized()
		{
            Assert.AreEqual(TheSpecPart.MemberName.ToUpperInvariant().First(), TheSpecPart.MemberName.First());
		}
		
		[Test]
		public void the_rest_of_the_spec_part_should_be_lowercase()
		{
		    var restOfMemberName = TheSpecPart.MemberName.Substring(1);
		    Assert.AreEqual(restOfMemberName.ToLowerInvariant(), restOfMemberName);
		}
		
	}

    [TestFixture]
    public class speclines_that_differ_only_in_capitalization : CreatingSpecPartsContext
    {
        [TestFixtureSetUp]
        protected void BeforeAll()
        {
            given_a_context_with_multiple_scenarios();
            given_each_scenario_with_speclines_that_differ_only_in_capitalization();
            when_parsing_the_file();
        }

        [TestFixtureTearDown]
        protected void AfterAll()
        {
        }

        [Test]
        public void only_one_matching_specpart_should_be_generated()
        {
            Assert.AreEqual(1, TheSpecFile.Contexts.Values.SelectMany(c => c.Scenarios.SelectMany(s => s.Givens)).Count());
        }
    }
}

