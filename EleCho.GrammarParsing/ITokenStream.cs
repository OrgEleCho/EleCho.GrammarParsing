using EleCho.GrammarParsing.Tokenizing;

namespace EleCho.GrammarParsing
{
    public interface ITokenStream
    {
        int Position { get; }

        Token Read();
        Token Peek(int offset);
    }
}
