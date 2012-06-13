using System;
using System.Text;

namespace SnipeLib
{
    public interface IBlockWriter
    {
        string Flush();
        void AppendLine(string line = "");
        void WrapWithBraces(Action writeBlock);
    }

    public class BlockWriter : IBlockWriter
    {
        private int _indentLevel;
        private readonly StringBuilder _builder;

        public BlockWriter()
        {
            _builder = new StringBuilder();
        }

        public string Flush()
        {
            var result = _builder.ToString();
            _builder.Clear();
            return result;
        }

        public void AppendLine(string line = "")
        {
            var tabs = String.Empty.PadLeft(_indentLevel, '\t');
            _builder.AppendLine(string.Format("{0}{1}", tabs, line));
        }

        public void WrapWithBraces(Action writeBlock)
        {
            AppendLine(@"{");
            Indent();
            writeBlock();
            Outdent();
            AppendLine(@"}");

            AppendLine();
        }

        private void Indent()
        {
            _indentLevel++;
        }

        private void Outdent()
        {
            _indentLevel--;
        } 
    }
}