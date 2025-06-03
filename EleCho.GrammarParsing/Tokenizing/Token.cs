using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleCho.GrammarParsing.Tokenizing
{
    public record struct Token(TokenKind Kind, ITextStream Source, string Text, int Position, int EndPosition)
    {
        public static Token EndOfFile(ITextStream source)
        {
            return new Token(TokenKind.EndOfFile, source, string.Empty, source.Position, source.Position);
        }
    }
}
