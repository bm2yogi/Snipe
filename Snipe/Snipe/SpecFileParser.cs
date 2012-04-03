using System;
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
            var specParts = _specFileData
                .Select(SpecPartFactory.Create);

            Context currentContext = null;
            Scenario currentScenario = null;

            var enumerator = specParts.GetEnumerator();
            
            while (enumerator.MoveNext())
            {
                if (enumerator.Current is Context)
                {
                    currentContext = enumerator.Current as Context;
                    _specFile.Contexts.Add(currentContext.Key, currentContext);
                }

                if (enumerator.Current is Scenario)
                {
                    currentScenario = enumerator.Current as Scenario;
                    if (currentContext == null) throw new ApplicationException("Scenario cannot precede Context in SpecFile.");
                    currentContext.Scenarios.Add(currentScenario);
                }

                if (enumerator.Current is Given)
                {
                    if (currentScenario == null) throw new ApplicationException("Givens cannot precede Scenarios in SpecFile.");
                    currentScenario.Givens.Add(enumerator.Current);
                }

                if (enumerator.Current is When)
                {
                    if (currentScenario == null) throw new ApplicationException("Whens cannot precede Scenarios in SpecFile.");
                    currentScenario.Whens.Add(enumerator.Current);
                }

                if (enumerator.Current is Then)
                {
                    if (currentScenario == null) throw new ApplicationException("Thens cannot precede Scenarios in SpecFile.");
                    currentScenario.Thens.Add(enumerator.Current);
                }
            }
        }

        public SpecFile SpecFile
        {
            get { return _specFile; }
        }
    }
}