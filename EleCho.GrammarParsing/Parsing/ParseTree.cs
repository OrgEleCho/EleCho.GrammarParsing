namespace EleCho.GrammarParsing.Parsing
{
    public class ParseTree
    {
        public ParseTree(ParseTreeNode root)
        {
            Root = root ?? throw new ArgumentNullException(nameof(root));
        }

        public ParseTreeNode Root { get; }
    }
}
