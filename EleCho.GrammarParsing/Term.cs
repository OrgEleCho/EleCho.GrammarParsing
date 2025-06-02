using System.Diagnostics.CodeAnalysis;
using EleCho.GrammarParsing.AST;

namespace EleCho.GrammarParsing
{
    public abstract class Term : IParsable
    {
        public string Name { get; }
        public SyntaxBuilder? SyntaxBuilder { get; set; }

        public Term(string name)
        {
            Name = name;
        }

        public abstract bool Parse(ITextSource textSource, ParseOptions options, [NotNullWhen(true)] out ParseTreeNode? node);

        public static TermRule operator +(Term term1, Term term2)
        {
            return new TermRule([new TermSequence([term1, term2])]);
        }

        public static TermRule operator +(Term term, string text)
        {
            return new TermRule([new TermSequence([term, new TerminalTerm(text, text)])]);
        }

        public static TermRule operator +(string text, Term term)
        {
            return new TermRule([new TermSequence([new TerminalTerm(text, text), term])]);
        }

        public static TermRule operator |(Term term1, Term term2)
        {
            return ((TermRule)term1) | ((TermRule)term2);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
