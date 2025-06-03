using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleCho.GrammarParsing.Internal
{
    internal static class TextStreamExtensions
    {
        public static bool Match(ITextStream textStream, string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (textStream.Peek(i) != text.Length)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
