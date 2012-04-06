using System;
using System.IO;

namespace Snipe
{
    class Program
    {
        private static string _in;
        private static string _out;

        static void Main(string[] args)
        {
            if (args.Length == 0) ShowHelp();
            ParseArgs(args);
            Execute();
        }

        private static void Execute()
        {
            Console.WriteLine("Input: {0}", _in);
            Console.WriteLine("Output: {0}", _out);

            BuildSpecFile();
        }

        private static void BuildSpecFile()
        {
            try
            {
                var specLines = File.ReadAllLines(_in);
                var parser = new SpecFileParser(specLines);
                var builder = new ContextSpecBuilder(parser.SpecFile);
                builder.Build();
                File.WriteAllText(_out, builder.Output);
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
                ShowHelp();
            }
        }

        private static void WriteError(string message)
        {
            Console.WriteLine(message);
        }

        private static void ParseArgs(string[] args)
        {
            try
            {
                _in = args[0].Split(':')[1];
                _out = args[1].Split(':')[1];
            }
            catch (Exception)
            {
                ShowHelp();
            }
        }

        private static void ShowHelp()
        {
            Console.WriteLine();
            Console.WriteLine("usage: you're doing it wrong.");
        }
    }
}
