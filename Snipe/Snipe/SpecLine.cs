using System;
using System.Collections.Generic;
using System.Linq;

namespace Snipe
{
    public class SpecLine
    {
        public string FirstWord = "";
        public string Key = "";
        public string[] Text = new string[] { };

        public SpecLine(string line)
        {
            if (String.IsNullOrEmpty(line.Trim())) return;

            var words = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            FirstWord = words.First();
            Text = words.Skip(1).ToArray();
            Key = Text.Aggregate((current, next) => current + next).ToLowerInvariant();
        }
    }

    public class SpecPartFactory
    {
        public const string Context = "context";
        public const string Scenario = "scenario";
        public const string Given = "given";
        public const string When = "when";
        public const string Then = "then";

        private static readonly Dictionary<string, Func<IEnumerable<string>, ISpecPart>> Map
            = new Dictionary<string, Func<IEnumerable<string>, ISpecPart>>
                  {
                      {Context, x => new Context(x)},
                      {Scenario, x => new Scenario(x)},
                      {Given, x => new Given(x)},
                      {When, x => new When(x)},
                      {Then, x => new Then(x)}
                  };
        
        public static ISpecPart Create(string line)
        {
            if (string.IsNullOrEmpty(line.Trim())) return null;

            var words = line.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            var firstWord = words.First();
            var text = words.Skip(1);
            
            return !Map.ContainsKey(firstWord) ? Map[firstWord](text) : null;
        }
    }
}