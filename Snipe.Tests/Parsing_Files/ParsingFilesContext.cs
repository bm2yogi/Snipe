using System.Collections.Generic;

namespace Snipe.Tests
{
    public class ParsingFilesContext
    {
        private IEnumerable<string> _theSpecFile;
        protected SpecFile TheResult;

        protected void given_a_valid_specFile()
        {
            _theSpecFile = ValidSpecFile;
        }

        protected void when_it_does_its_thing()
        {
            TheResult = new SpecFileParser(_theSpecFile).SpecFile;
        }

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
                yield return "Then it should not contain any of these characters `~!@#$%^&*()-=+,./<>?;':\"[]\\{}|";
            }
        }
    }
}