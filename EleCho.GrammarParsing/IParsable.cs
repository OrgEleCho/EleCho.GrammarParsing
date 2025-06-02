using System.Diagnostics.CodeAnalysis;

namespace EleCho.GrammarParsing
{
    public interface IParsable
    {
        bool Parse(ITextSource textSource, ParseOptions options, [NotNullWhen(true)] out ParseTreeNode? node);
    }
}
