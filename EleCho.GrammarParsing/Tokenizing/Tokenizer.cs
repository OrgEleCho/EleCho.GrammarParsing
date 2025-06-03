namespace EleCho.GrammarParsing.Tokenizing
{
    public partial class Tokenizer
    {
        public Tokenizer(Grammar grammar)
        {
            Grammar = grammar;
        }

        public Grammar Grammar { get; }
        
        public ITokenStream Tokenize(ITextStream textStream)
        {
            return new TokenStream(Grammar, textStream);
        }
    }
}
