using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleCho.GrammarParsing.Tokenizing
{
    public class TokenizeOptions
    {
        public bool IgnoreWhiteSpace { get; set; } = true;



        public static TokenizeOptions Default { get; } = new TokenizeOptions();
    }
}
