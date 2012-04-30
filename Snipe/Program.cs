using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Snipe
{
    class Program
    {
        private static string _in;
        private static string _out;

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }

            try
            {
                ParseArgs(args);
                Execute();
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
                ShowHelp();
            }
        }

        private static void Execute()
        {
            Console.WriteLine("Input: {0}", _in);
            Console.WriteLine("Output: {0}", _out);

            BuildSpecFile();
        }

        private static void BuildSpecFile()
        {
            var specLines = File.ReadAllLines(_in);
            var parser = new SpecFileParser(specLines);
            var builder = new ContextSpecBuilder(parser.SpecFile);
            builder.Build();
            File.WriteAllText(_out, builder.Output);
        }

        private static void WriteError(string message)
        {
            Console.WriteLine(message);
        }

        private static void ParseArgs(IList<string> args)
        {
            if (args.Count != 2) throw new ApplicationException("The wrong number of parameters were provided.");

            _in = (Regex.Match(args[0], ":").Success) ? args[0].Split(':')[1] : args[0];
            _out = (Regex.Match(args[1], ":").Success) ? args[1].Split(':')[1] : args[1];
        }

        private static void ShowHelp()
        {
            Console.WriteLine();
            Console.WriteLine("usage: snipe [in:]<specFilePath> [out:]<classFilePath>.");
            Console.WriteLine();
            Console.WriteLine("\tspecFilePath:\tThe path and filename of the specification file to parse. This file is expected to be in the Gerhkin syntax.");
            Console.WriteLine("\tclassFilePath:\tThe path and filename of the parsed test class file.");
        }
    }
}
