using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SnipeLib;

namespace Snipe
{
    public interface ISnipeConsole
    {
        void Run(string [] args);
    }

    public class SnipeConsole : ISnipeConsole
    {
        private string _in;
        private string _out;
        private readonly IApplicationHost _applicationHost;
        private readonly IContextSpecBuilder _contextSpecBuilder;

        public SnipeConsole(IApplicationHost applicationHost, IContextSpecBuilder contextSpecBuilder)
        {
            _applicationHost = applicationHost;
            _contextSpecBuilder = contextSpecBuilder;
        }

        public void Run(string [] args)
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

        private void Execute()
        {
            _applicationHost.ConsoleOut(string.Format("Input: {0}", _in));
            _applicationHost.ConsoleOut(string.Format("Output: {0}", _out));

            BuildSpecFile();
        }

        private void BuildSpecFile()
        {
            var specLines = _applicationHost.ReadFile(_in);
            var parser = new SpecFileParser(specLines);
            _contextSpecBuilder.Build(parser.SpecFile);
        }

        private void WriteError(string message)
        {
            _applicationHost.ConsoleOut(message);
        }

        private void ParseArgs(IList<string> args)
        {
            if (args.Count != 2) throw new ApplicationException("The wrong number of parameters were provided.");

            _in = (Regex.Match(args[0], ":").Success) ? args[0].Split(':')[1] : args[0];
            _out = (Regex.Match(args[1], ":").Success) ? args[1].Split(':')[1] : args[1];
        }

        private  void ShowHelp()
        {
            _applicationHost.ConsoleOut("");
            _applicationHost.ConsoleOut("usage: snipe [in:]<specFilePath> [out:]<classFilePath>.");
            _applicationHost.ConsoleOut("");
            _applicationHost.ConsoleOut("\tspecFilePath:\tThe path and filename of the specification file to parse. This file is expected to be in the Gerhkin syntax.");
            _applicationHost.ConsoleOut("\tclassFilePath:\tThe path and filename of the parsed test class file.");
        }
    }
}