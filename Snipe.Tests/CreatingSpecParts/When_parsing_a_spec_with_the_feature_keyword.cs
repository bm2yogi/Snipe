using NUnit.Framework;
using Sahara;

namespace Snipe.Tests.CreatingSpecParts
{
    [TestFixture]
    public class When_parsing_a_spec_with_the_context_keyword : CreatingSpecPartsContext
    {
        [TestFixtureSetUp]
        public void SetupContext()
        {
            Given_a_spec_that_uses_the_context_keyword();
            when_parsing_the_specline();
        }

        private void Given_a_spec_that_uses_the_context_keyword()
        {
            this.SpecLine = "Context: Some Very cool Feature.";
        }

        [Test]
        public void It_should_create_a_context_specPart()
        {
            this.TheSpecPart.MemberName.ShouldEqual("SomeVeryCoolFeatureContext");
        }

    }
    [TestFixture]
    public class When_parsing_a_spec_with_the_feature_keyword : CreatingSpecPartsContext
    {
        [TestFixtureSetUp]
        public void SetupContext()
        {
            Given_a_spec_that_uses_the_feature_keyword();
            when_parsing_the_specline();
        }

        private void Given_a_spec_that_uses_the_feature_keyword()
        {
            this.SpecLine = "Feature: Some Very cool Feature.";
        }

        [Test]
        public void It_should_create_a_context_specPart()
        {
            this.TheSpecPart.MemberName.ShouldEqual("SomeVeryCoolFeatureContext");
        }

    }
}