﻿using System.Linq;
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
        public void it_should_have_one_context()
        {
            Assert.AreEqual(1, _theResult.Contexts.Count);
        }

        private Context TheFirstContext
        {
            get { return _theResult.Contexts.First().Value; }
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
        public void it_should_load_all_the_givens()
        {
            Assert.AreEqual(2, TheFirstContext.Scenarios.First().Givens.Count());
            Assert.AreEqual(2, TheFirstContext.Scenarios.Skip(1).First().Givens.Count());
        }

        [Test]
        public void it_should_load_all_the_whens()
        {
            Assert.AreEqual(1, TheFirstContext.Scenarios.First().Whens.Count());
            Assert.AreEqual(1, TheFirstContext.Scenarios.Skip(1).First().Whens.Count());
        }

        [Test]
        public void it_should_load_all_the_thens()
        {
            Assert.AreEqual(3, TheFirstContext.Scenarios.First().Thens.Count());
            Assert.AreEqual(2, TheFirstContext.Scenarios.Skip(1).First().Thens.Count());
        }
    }
}