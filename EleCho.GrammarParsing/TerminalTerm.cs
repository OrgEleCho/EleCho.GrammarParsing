using EleCho.GrammarParsing.Tokenizing;

namespace EleCho.GrammarParsing
{
    public class TerminalTerm : Term
    {
        public TerminalTerm(string name, TokenKind requiredTokenKind, string? requiredTokenText) : base(name)
        {
            RequiredTokenKind = requiredTokenKind;
            RequiredTokenText = requiredTokenText;
        }

        public TokenKind RequiredTokenKind { get; }
        public string? RequiredTokenText { get; }
    }


}
