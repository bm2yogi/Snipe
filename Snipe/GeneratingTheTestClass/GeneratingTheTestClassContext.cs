using System.Collections.Generic;
using System.IO;
using Moq;

namespace Snipe.Tests
{
    public class GeneratingTheTestClassContext
    {
        private ISpecFile _theSpecFile;
        private SpecBuilder _theSpecBuilder;
        private IEnumerable<string> _theTestClass;
        protected string _testClassPath;

        protected ISpecFile ValidSpecFile
        {
            get
            {
                var mockSpecFile = new Mock<ISpecFile>();
                return mockSpecFile.Object;
            }
        }

        protected void given_a_parsed_specfile()
        {
            _theSpecFile = ValidSpecFile;
        }

        protected void when_a_test_classFile_is_generated()
        {
            _theSpecBuilder = new SpecBuilder(_theSpecFile);
            _theSpecBuilder.Build();
            _testClassPath = _theSpecBuilder.SpecPath;
            _theTestClass = (File.Exists(_testClassPath)) ? File.ReadAllLines(_theSpecBuilder.SpecPath) : new [] {""};
        }
    }
}