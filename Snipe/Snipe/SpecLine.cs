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
}