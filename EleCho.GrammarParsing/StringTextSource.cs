using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleCho.GrammarParsing
{
    public class StringTextStream : ITextStream
    {
        public StringTextStream(string text)
        {
            Text = text;
        }

        public string Text { get; }

        public int Position { get; private set; }

        public int Peek(int offset)
        {
            var absolutePosition = Position + offset;
            if (absolutePosition < Text.Length)
            {
                return Text[absolutePosition];
            }

            return -1;
        }

        public int Read()
        {
            if (Position >= Text.Length)
            {
                return -1;
            }

            var result = Text[Position];
            Position++;

            return result;
        }
    }
}
