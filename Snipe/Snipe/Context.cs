using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Snipe
{
    public interface ISpecPart
    {
        string Key { get; }
        string MemberName { get; }
    }

    public class Context : ISpecPart
    {
        private readonly SpecLine _specLine;
        
        public Context(SpecLine specLine)
        {
            _specLine = specLine;
        }

        public string Key
        {
            get { return _specLine.Text.Concatenate(); }
        }

        public virtual string MemberName
        {
            get { return _specLine.Text.PascalCase() + "Context"; }
        }
    }

    public class Scenario : ISpecPart
    {
        private readonly SpecLine _specLine;

        public Scenario(SpecLine specLine)
        {
            _specLine = specLine;
        }

        public string Key
        {
            get { return _specLine.Text.Concatenate(); }
        }

        public virtual string MemberName
        {
            get { return _specLine.Text.ConcatenateWithUnderscores(); }
        }
    }

    public class Given : ISpecPart
    {

        private readonly SpecLine _specLine;

        public Given(SpecLine specLine)
        {
            _specLine = specLine;
        }

        public string Key
        {
            get { return _specLine.Text.Concatenate(); }
        }

        public virtual string MemberName
        {
            get { return _specLine.Text.ConcatenateWithUnderscores(); }
        }
    }

    public class When : ISpecPart
    {
        private readonly SpecLine _specLine;

        public When(SpecLine specLine)
        {
            _specLine = specLine;
        }

        public string Key
        {
            get { return _specLine.Text.Concatenate(); }
        }

        public virtual string MemberName
        {
            get { return _specLine.Text.ConcatenateWithUnderscores(); }
        }
    }

    public class Then : ISpecPart
    {
        private readonly SpecLine _specLine;

        public Then(SpecLine specLine)
        {
            _specLine = specLine;
        }

        public string Key
        {
            get { return _specLine.Text.Concatenate(); }
        }

        public virtual string MemberName
        {
            get { return _specLine.Text.ConcatenateWithUnderscores(); }
        }
    }

    public static class Extensions
    {
        public static string ConcatenateWithUnderscores(this IEnumerable<string> words)
        {
            return words.Aggregate((c, n) => c + "_" + n);
        }

        public static string Concatenate(this IEnumerable<string> words)
        {
            return words.Aggregate((c, n) => c + n);
        }

        public static string PascalCase(this IEnumerable<string> words)
        {
            return words.Select(s => s.InitialCap()).Concatenate();
        }

        public static string InitialCap(this string word)
        {
            var firstLetter = word.First().ToString(CultureInfo.InvariantCulture).ToUpperInvariant();
            var rest = word.Substring(1);
            return firstLetter + rest;
        }
    }
}