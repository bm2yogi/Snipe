using System;
using System.Collections.Generic;
using System.Reflection;
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
        private readonly IApplicationHost _applicationHost;
        private readonly IContextSpecBuilder _contextSpecBuilder;

        public SnipeConsole(IApplicationHost applicationHost, IContextSpecBuilder contextSpecBuilder)
        {
            _applicationHost = applicationHost;
            _contextSpecBuilder = contextSpecBuilder;
        }

        public void Run(string [] args)
        {
            ShowAppInfo();

            try
            {
                ParseArgs(args);
                Execute();
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
            }
        }

        private void ParseArgs(IList<string> args)
        {
            if (args.Count != 1) throw new ApplicationException("Please provide the path to your specification file.");

            _in = (Regex.Match(args[0], ":").Success) ? args[0].Split(':')[1] : args[0];
        }

        private void Execute()
        {
            _applicationHost.ConsoleOut(string.Format("Processing file '{0}'...", _in));
            _applicationHost.ConsoleOut(string.Format("Generating test classes..."));
            BuildSpecFile();
            _applicationHost.ConsoleOut(string.Format("Done."));
        }

        private void BuildSpecFile()
        {
            var specLines = _applicationHost.ReadFile(_in);
            var parser = new SpecFileParser(specLines);
            _contextSpecBuilder.Build(parser.SpecFile);
        }

        private void ShowAppInfo()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = assembly.GetName();
            _applicationHost.ConsoleOut("");
            _applicationHost.ConsoleOut(string.Format("{0} (version {1})",assemblyName.Name, assemblyName.Version));
            _applicationHost.ConsoleOut("");
        }

        private void WriteError(string message)
        {
            _applicationHost.ConsoleOut(message);
            ShowHelp();
        }

        private  void ShowHelp()
        {
            _applicationHost.ConsoleOut("");
            _applicationHost.ConsoleOut("Usage: Snipe <specFilePath>");
            _applicationHost.ConsoleOut("");
            _applicationHost.ConsoleOut("specFilePath:\tThe path and filename of the specification file to parse. This file is expected to be in the Gerhkin syntax.");
        }
    }
}