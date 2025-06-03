using EleCho.GrammarParsing.Tokenizing;

namespace EleCho.GrammarParsing.Parsing
{
    public class ParseTreeNode
    {
        public ParseTreeNode(Term term, IReadOnlyList<ParseTreeNode> children, Token? token)
        {
            Term = term ?? throw new ArgumentNullException(nameof(term));
            Children = children ?? throw new ArgumentNullException(nameof(children));
            Token = token;
        }

        public Term Term { get; }
        public IReadOnlyList<ParseTreeNode> Children { get; }
        public Token? Token { get; }
    }
}
