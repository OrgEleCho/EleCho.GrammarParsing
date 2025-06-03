namespace EleCho.GrammarParsing.Parsing
{
    public abstract class Parser
    {
        public Parser(Grammar grammar)
        {
            Grammar = grammar ?? throw new ArgumentNullException(nameof(grammar));
        }

        public Grammar Grammar { get; }

        public abstract ParseTree Parse(ITokenStream tokenStream);
    }
}
