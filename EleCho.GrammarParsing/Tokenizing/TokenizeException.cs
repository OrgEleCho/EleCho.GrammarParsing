using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleCho.GrammarParsing.Tokenizing
{
    public class TokenizeException : Exception
    {
        public TokenizeException(string message) : base(message) { }
    }
}
