namespace SnipeLib
{
    public interface IFileSystem
    {
        void CreateDirectory(string path);
        void WriteFile(string path, string content);
        string[] ReadFile(string path);
    }

    public class SnipeFileSystem: IFileSystem
    {
        public void CreateDirectory(string path)
        {
            System.IO.Directory.CreateDirectory(path);
        }

        public void WriteFile(string path, string content)
        {
            System.IO.File.WriteAllText(path, content);
        }

        public string [] ReadFile(string path)
        {
            return System.IO.File.ReadAllLines(path);
        }
    }
}