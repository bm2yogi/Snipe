using System.IO;
using NUnit.Framework;

namespace Snipe.Tests
{
    [TestFixture]
    public class Generating_test_classes_from_a_spec : GeneratingTheTestClassContext
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
            Delete_generated_file();
        }

        private void Delete_generated_file()
        {
            if (File.Exists(_testClassPath)) File.Delete(_testClassPath);
        }

        [Test]
        public void it_should_create_a_test_base_class_for_the_context()
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