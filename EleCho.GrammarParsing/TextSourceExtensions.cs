using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleCho.GrammarParsing
{
    public static class TextSourceExtensions
    {
        public static void SeekNext(this ITextSource textSource)
        {
            textSource.Seek(textSource.Position + 1);
        }

        public static void SkipWhitespace(this ITextSource textSource)
        {
            while (char.IsWhiteSpace((char)textSource.Current))
            {
                textSource.SeekNext();
            }
        }

        public static string GetText(this ITextSource textSource, int startPosition, int endPosition)
        {
            var originPosition = textSource.Position;

            StringBuilder sb = new StringBuilder();

            textSource.Seek(startPosition);
            while (textSource.Position < endPosition)
            {
                sb.Append((char)textSource.Current);

                textSource.SeekNext();
            }

            textSource.Seek(originPosition);

            return sb.ToString();
        }
    }
}
