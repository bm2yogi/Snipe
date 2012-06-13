using System;
using System.IO;
using SnipeLib;

namespace Snipe
{
    public class SnipeApplicationHost: IApplicationHost
    {
        public void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public void WriteFile(string path, string content)
        {
            File.WriteAllText(path, content);
        }

        public string [] ReadFile(string path)
        {
            return File.ReadAllLines(path);
        }

        public void ConsoleOut(string message)
        {
            Console.WriteLine(message);
        }
    }
}