namespace EleCho.GrammarParsing.AST
{
    public class SyntaxTree
    {
        public SyntaxTree(Syntax root)
        {
            Root = root;
        }

        public Syntax Root { get; }
    }
}
