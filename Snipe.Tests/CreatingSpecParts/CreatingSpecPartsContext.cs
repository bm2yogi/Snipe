using System.Linq;
using NUnit.Framework;

namespace Snipe.Tests.CreatingSpecParts
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

        protected void when_parsing_the_specline()
        {
            TheSpecPart = SpecPartFactory.Create(SpecLine);
        }

    }

    [TestFixture]
	public class Parsing_specLines_with_mixed_capitalization : CreatingSpecPartsContext
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
}

