using EleCho.GrammarParsing.Tokenizing;

namespace EleCho.GrammarParsing
{
    public class NumberTerm : TerminalTerm
    {
        public NumberTerm(string name) : base(name, TokenKind.Number, null) { }
    }
}
