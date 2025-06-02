using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleCho.GrammarParsing.AST
{
    public delegate Syntax SyntaxBuilder(ParseTreeNode parseTreeNode, IReadOnlyList<Syntax> children);
}
