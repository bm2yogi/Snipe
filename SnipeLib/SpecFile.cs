using System.Collections.Generic;

namespace Snipe
{
    public interface ISpecFile
    {
        Dictionary<string, Context> Contexts { get; set; }
    }

    public class SpecFile : ISpecFile
    {
        public Dictionary<string, Context> Contexts { get; set; }

        public SpecFile()
        {
            Contexts = new Dictionary<string, Context>();
        }
    }
}