using System.Linq;
using NUnit.Framework;

namespace Snipe.Tests
{
	public class CreatingSpecPartsContext
	{
	    protected string SpecLine;
	    protected SpecPart TheSpecPart;

	    protected void given_a_specLine_with_mixed_caps()
	    {
	        SpecLine = "gIVEn a SpeClINe wIth MixeD CaPS";
	    }
		
		protected void given_a_context_with_multiple_scenarios()
		{
			// not implemented.
		}
		
		protected void given_each_scenario_with_speclines_that_differ_only_in_capitalization()
		{
			// not implemented.
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
			Assert.Fail("Not implemented.");
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
			when_parsing_the_specline();
		}
		
		[TestFixtureTearDown]
		protected void AfterAll()
		{
		}
		
		[Test]
		public void only_one_matching_specpart_should_be_generated()
		{
			Assert.Fail("Not implemented.");
		}
		
	}
	
}

