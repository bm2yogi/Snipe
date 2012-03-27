using System.Collections.Generic;
using System.Linq;

namespace Snipe
{
    public class SpecFileParser
    {
        private readonly SpecFile _specFile;
        private readonly IEnumerable<string> _specFileData;

        public SpecFileParser(IEnumerable<string> specFileData)
        {
            _specFile = new SpecFile();
            _specFileData = specFileData;
            Parse();
        }

        private void Parse()
        {
            _specFileData
                .Select(line => new SpecLine(line))
                .ToList()
                .ForEach(ParseLine);
        }

        public SpecFile SpecFile
        {
            get { return _specFile; }
        }

        private void ParseLine(SpecLine specLine)
        {
            AddContext(specLine);
            AddScenario(specLine);
            AddGiven(specLine);
        }

        private static bool IsContext(SpecLine specLine)
        {
            return specLine.FirstWord.StartsWith("Context");
        }

        private static bool IsScenario(SpecLine specLine)
        {
            return specLine.FirstWord.StartsWith("Scenario");
        }

        private static bool IsGiven(SpecLine specLine)
        {
            return specLine.FirstWord.StartsWith("Given");
        }

        private void AddContext(SpecLine specLine)
        {
            if (IsContext(specLine) && !_specFile.Contexts.ContainsKey(specLine.Key))

                _specFile.Contexts.Add(specLine.Key, new Context(specLine));
        }

        private void AddScenario(SpecLine specLine)
        {
            if (IsScenario(specLine) && !_specFile.Scenarios.ContainsKey(specLine.Key))

                _specFile.Scenarios.Add(specLine.Key, new Scenario(specLine));
        }

        private void AddGiven(SpecLine specLine)
        {
            if (IsGiven(specLine) && !_specFile.Givens.ContainsKey(specLine.Key))

                _specFile.Givens.Add(specLine.Key, new Given(specLine));
        }
    }
}