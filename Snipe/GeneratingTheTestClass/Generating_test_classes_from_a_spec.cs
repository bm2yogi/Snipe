using System;
using NUnit.Framework;

namespace Snipe.Tests.GeneratingTheTestClass
{
    [TestFixture]
    public class When_generating_test_classes_from_a_spec : GeneratingTheTestClassContext
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
}