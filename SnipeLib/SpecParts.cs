using System;
using System.Collections.Generic;
using System.Linq;

namespace Snipe
{
    public class SpecPartFactory
    {
        private const string Context = "context";
        private const string Feature = "feature";
        private const string Scenario = "scenario";
        private const string Given = "given";
        private const string When = "when";
        private const string Then = "then";
        
        public static SpecPart Create(string line)
        {
            if (String.IsNullOrEmpty(line.Trim())) return null;

            line = line
                .Trim()
                .ToLowerInvariant()
                .ReplaceIllegalCharacters();

            var words = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var firstWord = words.First().ToLowerInvariant();

            if (firstWord.StartsWith(Context)) return new Context(words);
            if (firstWord.StartsWith(Feature)) return new Context(words);
            if (firstWord.StartsWith(Scenario)) return new Scenario(words);
            if (firstWord.StartsWith(Given)) return new Given(words);
            if (firstWord.StartsWith(When)) return new When(words);
            if (firstWord.StartsWith(Then)) return new Then(words);
            
            return null;
        }
    }

    public abstract class SpecPart
    {
        public string Key { get; set; }
        public string MemberName { get; set; }

        public override string ToString()
        {
            return MemberName;
        }

        public override bool Equals(object obj)
        {
            return this.Key.Equals(((SpecPart) obj).Key);
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }
    }

    public class Context : SpecPart
    {
        public Context(IEnumerable<string> line)
        {
            Key = line.Skip(1).Concatenate();
            Namespace = line.Skip(1).PascalCase();
            MemberName = Namespace +"Context";
            Scenarios = new List<Scenario>();
        }

        public string Namespace { get; set; }

        public IList<Scenario> Scenarios { get; set; }
    }
    public class Scenario : SpecPart {

        public Scenario(IEnumerable<string> line)
        {
            Key = line.Skip(1).Concatenate();
            MemberName = line.Skip(1).ConcatenateWithUnderscores().InitialCap();
            Givens = new List<SpecPart>();
            Whens = new List<SpecPart>();
            Thens = new List<SpecPart>();
        }

        public IList<SpecPart> Givens { get; set; }
        public IList<SpecPart> Whens { get; set; }
        public IList<SpecPart> Thens { get; set; }
    }
    public class Given : SpecPart {
        public Given(IEnumerable<string> line)
        {
            Key = line.Concatenate();
            MemberName = line.ConcatenateWithUnderscores().InitialCap();
        }
     }
    public class When : SpecPart {
        public When(IEnumerable<string> line)
        {
            Key = line.Concatenate();
            MemberName = line.ConcatenateWithUnderscores().InitialCap();
        }
     }
    public class Then : SpecPart {
        public Then(IEnumerable<string> line)
        {
            Key = line.Skip(1).Concatenate();
            MemberName = line.Skip(1).ConcatenateWithUnderscores().InitialCap();
        }
     }
}