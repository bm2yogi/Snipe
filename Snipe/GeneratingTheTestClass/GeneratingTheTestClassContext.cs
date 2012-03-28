using System.Collections.Generic;
using System.IO;
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
        private string _testClassPath;

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            given_a_parsed_specfile();
            when_a_test_classFile_is_generated();
        }

        [TestFixtureTearDown]
        public void AfterAll()
        {
            Delete_generated_file();
        }

        private void Delete_generated_file()
        {
            if (File.Exists(_testClassPath)) File.Delete(_testClassPath);
        }

        private void given_a_parsed_specfile()
        {
            _theSpecFile = ValidSpecFile;
        }

        private void when_a_test_classFile_is_generated()
        {
            _theSpecBuilder = new SpecBuilder(_theSpecFile);
            _theSpecBuilder.Build();
            _testClassPath = _theSpecBuilder.SpecPath;
            _theTestClass = (File.Exists(_testClassPath)) ? File.ReadAllLines(_theSpecBuilder.SpecPath) : new [] {""};
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
        public void then_it_should_create_a_test_base_class_for_the_context()
        {
            
        }


    }

    public class SpecBuilder
    {
        private readonly ISpecFile _theSpecFile;

        public SpecBuilder(ISpecFile theSpecFile)
        {
            _theSpecFile = theSpecFile;
        }

        public void Build()
        {
        }

        public string SpecPath
        {
            get { return ""; }
        }
    }
}