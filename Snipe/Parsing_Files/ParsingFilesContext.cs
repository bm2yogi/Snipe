using System.Collections.Generic;
using System.Linq;

namespace Snipe.Tests
{
    public class ParsingFilesContext
    {
        private string[] _theSpecFile;
        private SpecFileParser _theParser;
        protected SpecFile _theResult;

        public IEnumerable<string> ValidSpecFile
        {
            get
            {
                yield return "Context: Parsing Specification Files";
                yield return "";
                yield return "Scenario: Parsing a  valid spec file";
                yield return "";
                yield return "Given a valid specification file";
                yield return "Given a bright sunny day";
                yield return "When the file is parsed";
                yield return "Then it should make everyone happy";
                yield return "Then it should bring about world peace";
                yield return "Then unicorns should return to the wild";
                yield return "";
                yield return "Scenario: Parsing an invalid spec file";
                yield return "";
                yield return "Given an invalid specification file";
                yield return "Given a bright sunny day";
                yield return "When the file is parsed";
                yield return "Then it should make everyone sad";
                yield return "Then it should bring about world war three";
            }
        }

        protected void given_a_valid_specFile()
        {
            _theSpecFile = Enumerable.ToArray<string>(ValidSpecFile);
        }

        protected void when_it_does_its_thing()
        {
            _theParser = new SpecFileParser(_theSpecFile);
            _theResult = _theParser.SpecFile;
        }
    }
}