using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EleCho.GrammarParsing
{
    public interface ITextSource
    {
        int Current { get; }
        int Position { get; }

        void Seek(int position);
    }
}
