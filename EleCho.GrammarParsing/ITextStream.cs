using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleCho.GrammarParsing
{
    public interface ITextStream
    {
        int Position { get; }

        int Read();
        int Peek(int offset);
    }
}
