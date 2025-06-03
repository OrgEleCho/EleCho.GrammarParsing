using EleCho.GrammarParsing.Tokenizing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EleCho.GrammarParsing
{
    public class Grammar
    {
        public NonTerminalTerm? Root { get; set; }


        public TokenizeOptions TokenizeOptions { get; set; } = TokenizeOptions.Default;
        public NumberOptions NumberOptions { get; set; } = NumberOptions.Default;
    }


}
