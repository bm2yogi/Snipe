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
            var specLines = _specFileData
                .Select(x => new SpecLine(x));

            Context currentContext = null;
            Scenario currentScenario = null;

            var enumerator = specLines.GetEnumerator();
            
            while (enumerator.MoveNext())
            {
                if (IsContext(enumerator.Current))
                {
                    currentContext = new Context(enumerator.Current);
                    _specFile.Contexts.Add(currentContext.Key, currentContext);
                }

                if (IsScenario(enumerator.Current))
                {
                    currentScenario = new Scenario(enumerator.Current);
                    if (currentContext == null) throw new ApplicationException("Scenario cannot precede Context in SpecFile.");
                    currentContext.Scenarios.Add(currentScenario);
                }

                if (IsGiven(enumerator.Current))
                {
                    if (currentScenario == null) throw new ApplicationException("Givens cannot precede Scenarios in SpecFile.");
                    currentScenario.Givens.Add(new Given(enumerator.Current));
                }

                if (IsWhen(enumerator.Current))
                {
                    if (currentScenario == null) throw new ApplicationException("Whens cannot precede Scenarios in SpecFile.");
                    currentScenario.Whens.Add(new When(enumerator.Current));
                }

                if (IsThen(enumerator.Current))
                {
                    if (currentScenario == null) throw new ApplicationException("Thens cannot precede Scenarios in SpecFile.");
                    currentScenario.Thens.Add(new Then(enumerator.Current));
                }
            }
        }

        public SpecFile SpecFile
        {
            get { return _specFile; }
        }

        private static bool IsContext(SpecLine specLine)
        {
            return specLine.FirstWord.ToLowerInvariant().StartsWith("context");
        }

        private static bool IsScenario(SpecLine specLine)
        {
            return specLine.FirstWord.ToLowerInvariant().StartsWith("scenario");
        }

        private static bool IsGiven(SpecLine specLine)
        {
            return specLine.FirstWord.ToLowerInvariant().StartsWith("given");
        }

        private static bool IsWhen(SpecLine specLine)
        {
            return specLine.FirstWord.ToLowerInvariant().StartsWith("when");
        }

        private static bool IsThen(SpecLine specLine)
        {
            return specLine.FirstWord.ToLowerInvariant().StartsWith("then");
        }

        //private void AddContext(SpecLine specLine)
        //{
        //    if (IsContext(specLine) && !_specFile.Contexts.ContainsKey(specLine.Key))

        //        _specFile.Contexts.Add(specLine.Key, new Context(specLine));
        //}

        //private void AddScenario(SpecLine specLine)
        //{
        //    if (IsScenario(specLine) && !_specFile.Scenarios.ContainsKey(specLine.Key))

        //        _specFile.Scenarios.Add(specLine.Key, new Scenario(specLine));
        //}

        //private void AddGiven(SpecLine specLine)
        //{
        //    if (IsGiven(specLine) && !_specFile.Givens.ContainsKey(specLine.Key))

        //        _specFile.Givens.Add(specLine.Key, new Given(specLine));
        //}

        //private void AddWhen(SpecLine specLine)
        //{
        //    if (IsWhen(specLine) && !_specFile.Whens.ContainsKey(specLine.Key))

        //        _specFile.Whens.Add(specLine.Key, new When(specLine));
        //}

        //private void AddThen(SpecLine specLine)
        //{
        //    if (IsThen(specLine) && !_specFile.Thens.ContainsKey(specLine.Key))

        //        _specFile.Thens.Add(specLine.Key, new Then(specLine));
        //}
    }

    public class SpecLineComparer : IEqualityComparer<SpecLine>
    {
        public bool Equals(SpecLine x, SpecLine y)
        {
            return x.Key.Equals(y.Key);
        }

        public int GetHashCode(SpecLine obj)
        {
            return obj.Key.GetHashCode();
        }
    }
}