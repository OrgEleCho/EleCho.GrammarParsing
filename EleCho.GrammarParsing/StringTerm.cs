using EleCho.GrammarParsing.Tokenizing;

namespace EleCho.GrammarParsing
{
    public class StringTerm : TerminalTerm
    {
        public StringTerm(string name) : base(name, TokenKind.String, null) { }
    }
}
