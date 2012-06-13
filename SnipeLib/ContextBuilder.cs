using System.Linq;
using Snipe;

namespace SnipeLib
{
    public interface IContextBuilder
    {
        void BuildContext(Context context);
    }

    public class ContextBuilder : IContextBuilder
    {
        private readonly IApplicationHost _applicationHost;
        private readonly IBlockWriter _blockWriter;

        public ContextBuilder(IApplicationHost applicationHost, IBlockWriter blockWriter)
        {
            _applicationHost = applicationHost;
            _blockWriter = blockWriter;
        }

        public void BuildContext(Context context)
        {
            _applicationHost.CreateDirectory(context.Namespace);
            _applicationHost.WriteFile(string.Format(@"{0}\{1}.cs", context.Namespace, context.MemberName), Build(context));
        }

        private string Build(Context context)
        {
            _blockWriter.AppendLine(@"using System;");
            _blockWriter.AppendLine(@"using NUnit.Framework;");
            _blockWriter.AppendLine();
            _blockWriter.AppendLine(string.Format(@"namespace {0}", context.Namespace));
            _blockWriter.WrapWithBraces(() => WriteContext(context));

            return _blockWriter.Flush();
        }

        private void WriteContext(Context context)
        {
            _blockWriter.AppendLine(string.Format(@"public class {0}", context));

            _blockWriter.WrapWithBraces(() => context.Scenarios
                                     .SelectMany(s => s.Givens)
                                     .Union(context.Scenarios.SelectMany(s => s.Whens))
                                     .Distinct()
                                     .ToList()
                                     .ForEach(WriteGivenWhen));
        }

        private void WriteGivenWhen(SpecPart specPart)
        {
            _blockWriter.AppendLine(string.Format("protected void {0}()", specPart));
            _blockWriter.WrapWithBraces(() => _blockWriter.AppendLine("// not implemented."));
        }
    }
}