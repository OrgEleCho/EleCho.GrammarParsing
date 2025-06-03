using EleCho.GrammarParsing.Tokenizing;

namespace EleCho.GrammarParsing
{
    public class SymbolTerm : TerminalTerm
    {
        public SymbolTerm(string name, string? requiredTokenText) : base(name, TokenKind.Symbol, requiredTokenText)
        {
        }
    }
}
