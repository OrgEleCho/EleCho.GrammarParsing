using EleCho.GrammarParsing.AST;

namespace EleCho.GrammarParsing
{
    public class ParseTree
    {
        public ParseTree(ParseTreeNode root)
        {
            Root = root ?? throw new ArgumentNullException(nameof(root));
        }

        public ParseTreeNode Root { get; }

        public SyntaxTree BuildSyntaxTree()
        {
            return new SyntaxTree(Root.BuildSyntax());
        }

        public override string ToString()
        {
            return $"ParseTree(Root: {Root.Term.Name}, Start: {Root.StartPosition}, End: {Root.EndPosition})";
        }
    }
}
