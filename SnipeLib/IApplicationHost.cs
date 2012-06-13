namespace SnipeLib
{
    public interface IApplicationHost
    {
        void CreateDirectory(string path);
        void WriteFile(string path, string content);
        string[] ReadFile(string path);
        void ConsoleOut(string message);
    }
}