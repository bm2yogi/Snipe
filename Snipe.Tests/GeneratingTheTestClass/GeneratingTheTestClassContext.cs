using System.Collections.Generic;

namespace Snipe.Tests
{
    public class GeneratingTheTestClassContext
    {
        private ISpecFile _theParsedSpecFile;
        private ContextSpecBuilder _theContextSpecBuilder;
        protected string Output;

        protected void given_a_parsed_specfile()
        {
            _theParsedSpecFile = ParsedSpecFile();
        }

        protected void when_a_test_classFile_is_generated()
        {
            _theContextSpecBuilder = new ContextSpecBuilder(_theParsedSpecFile);
            _theContextSpecBuilder.Build();
            Output = _theContextSpecBuilder.Output;
        }

        public IEnumerable<string> ValidSpecFile
        {
            get
            {
                yield return "Context: Parsing Specification Files";
                yield return "";
                yield return "Scenario: When parsing a  valid spec file";
                yield return "";
                yield return "Given a valid specification file";
                yield return "Given a bright sunny day";
                yield return "When the file is parsed";
                yield return "Then it should make everyone happy";
                yield return "Then it should bring about world peace";
                yield return "Then unicorns should return to the wild";
                yield return "";
                yield return "Scenario: When parsing an invalid spec file";
                yield return "";
                yield return "Given an invalid specification file";
                yield return "Given a bright sunny day";
                yield return "When the file is parsed";
                yield return "Then it should make everyone sad";
                yield return "Then it should bring about world war three";
            }
        }

        protected SpecFile ParsedSpecFile()
        {
            return new  SpecFileParser(ValidSpecFile).SpecFile;
        }

    }
}