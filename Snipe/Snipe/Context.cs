using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Snipe
{
    public abstract class SpecPart
    {
        protected SpecLine SpecLine;

        protected SpecPart(SpecLine specLine)
        {
            SpecLine = specLine;
        }

        public abstract string MemberName { get; }
    }

    public class Context : SpecPart
    {
        public Context(SpecLine specLine) : base(specLine){}

        public override string MemberName
        {
            get { return SpecLine.Text.PascalCase() + "Context"; }
        }
    }

    public class Scenario : SpecPart
    {
        public Scenario(SpecLine specLine) : base(specLine){}

        public override string MemberName
        {
            get { return SpecLine.Text.JoinWithUnderscores(); }
        }
    }

    public class Given : SpecPart
    {
        public Given(SpecLine specLine) : base(specLine){}

        public override string MemberName
        {
            get { return SpecLine.Text.JoinWithUnderscores(); }
        }
    }

    public static class Extensions
    {
        public static string JoinWithUnderscores(this IEnumerable<string> words)
        {
            return words.Aggregate((c, n) => c + "_" + n);
        }

        public static string PascalCase(this IEnumerable<string> words)
        {
            return words.Select(s=>s.InitialCap()).Aggregate((c, n) => c + n);
        }

        public static string InitialCap (this string word)
        {
            var firstLetter = word.First().ToString(CultureInfo.InvariantCulture).ToUpperInvariant();
            var rest = word.Substring(1);
            return firstLetter + rest;
        }
    }
}