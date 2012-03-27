using System.Collections.Generic;

namespace Snipe
{
    public class SpecFile
    {
        public Dictionary<string, Context> Contexts { get; set; }
        public Dictionary<string, Scenario> Scenarios { get; set; }
        public Dictionary<string, Given> Givens { get; set; }

        public SpecFile()
        {
            Contexts = new Dictionary<string, Context>();
            Scenarios = new Dictionary<string, Scenario>();
            Givens = new Dictionary<string, Given>();
        }
    }
}