namespace EleCho.GrammarParsing
{
    public class ParseOptions
    {
        public bool IgnoreWhitespace { get; set; } = true;

        public static ParseOptions Default { get; } = new ParseOptions();
    }
}
