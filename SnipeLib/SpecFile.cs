using System.Collections.Generic;

namespace Snipe
{
    public interface ISpecFile
    {
        Dictionary<string, Context> Contexts { get; set; }
        Dictionary<string, Scenario> Scenarios { get; set; }
        Dictionary<string, Given> Givens { get; set; }
        Dictionary<string, When> Whens { get; set; }
        Dictionary<string, Then> Thens { get; set; }
    }

    public class SpecFile : ISpecFile
    {
        public Dictionary<string, Context> Contexts { get; set; }
        public Dictionary<string, Scenario> Scenarios { get; set; }
        public Dictionary<string, Given> Givens { get; set; }
        public Dictionary<string, When> Whens { get; set; }
        public Dictionary<string, Then> Thens { get; set; }

        public SpecFile()
        {
            Contexts = new Dictionary<string, Context>();
            Scenarios = new Dictionary<string, Scenario>();
            Givens = new Dictionary<string, Given>();
            Whens = new Dictionary<string, When>();
            Thens = new Dictionary<string, Then>();
        }
    }
}