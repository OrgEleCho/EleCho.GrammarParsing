namespace EleCho.GrammarParsing.AST
{
    public class DefaultSyntax : Syntax
    {
        public DefaultSyntax(ParseTreeNode parseTreeNode, IReadOnlyList<Syntax> children)
        {
            ParseTreeNode = parseTreeNode ?? throw new ArgumentNullException(nameof(parseTreeNode));
            Children = children ?? throw new ArgumentNullException(nameof(children));
        }

        public ParseTreeNode ParseTreeNode { get; }
        public IReadOnlyList<Syntax> Children { get; }
    }
}
