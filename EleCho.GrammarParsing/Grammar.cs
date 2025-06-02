namespace EleCho.GrammarParsing
{
    public class Grammar
    {
        public Term? Root { get; set; }



        public ParseTree Parse(ITextSource textSource, ParseOptions options)
        {
            if (Root is null)
            {
                throw new InvalidOperationException("No root term defined in the grammar.");
            }

            ArgumentNullException.ThrowIfNull(textSource, nameof(textSource));
            ArgumentNullException.ThrowIfNull(options, nameof(options));

            if (!Root.Parse(textSource, options, out var rootNode))
            {
                throw new ArgumentException($"Failed to parse the input text with the grammar. Current position: {textSource.Position}");
            }

            return new ParseTree(rootNode);
        }

        public static TerminalTerm Terminal(string name, string text)
        {
            return new TerminalTerm(name, text);
        }

        public static NonTerminalTerm NonTerminal(string name)
        {
            return new NonTerminalTerm(name);
        }

        public static DecimalNumberTerm DecimalNumber(string name)
        {
            return new DecimalNumberTerm(name, DecimalNumberOptions.Default);
        }

        public static StringTerm String(string name)
        {
            return new StringTerm(name, StringOptions.Default);
        }
    }
}
