using EleCho.GrammarParsing.Tokenizing;

namespace EleCho.GrammarParsing
{
    public class IdentifierTerm : TerminalTerm
    {
        public IdentifierTerm(string name, string? requiredTokenText) : base(name, TokenKind.Identifier, requiredTokenText)
        {
        }
    }


}
