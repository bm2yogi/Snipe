using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Snipe
{
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

        public static string ReplaceIllegalCharacters(this string word)
        {
            const string pattern = "^.*[`~!@#$%^&*()-=+,./<>?;':\"[]\\{}|].*$";
            return Regex.Replace(word, pattern, string.Empty);
        }
    }
}