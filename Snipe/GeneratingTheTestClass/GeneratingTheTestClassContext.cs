using System.Collections.Generic;
using NUnit.Framework;
using Moq;

namespace Snipe.Tests
{
    [TestFixture]
    public class GeneratingTheTestClassContext
    {
        private ISpecFile _theSpecFile;
        private SpecBuilder _theSpecBuilder;
        private IEnumerable<string> _theTestClass;

        [TestFixtureSetUp]
        public void SetupContext()
        {
            given_a_parsed_specfile();
            when_a_test_classFile_is_generated();
        }

        private void given_a_parsed_specfile()
        {
            _theSpecFile = ValidSpecFile;
        }

        private void when_a_test_classFile_is_generated()
        {
            _theSpecBuilder = new SpecBuilder(_theSpecFile);
            _theTestClass = _theSpecBuilder.Spec;
        }

        protected ISpecFile ValidSpecFile
        {
            get
            {
                var mockSpecFile = new Mock<ISpecFile>();
                return mockSpecFile.Object;
            }
        }

        [Test]
        public void then_it_should_do_something_here()
        {
            Assert.IsNotNull(_theTestClass);
        }


    }

    public class SpecBuilder
    {
        private readonly ISpecFile _theSpecFile;

        public SpecBuilder(ISpecFile theSpecFile)
        {
            _theSpecFile = theSpecFile;
        }

        public IEnumerable<string> Spec
        {
            get { yield return ""; }
        }
    }
}