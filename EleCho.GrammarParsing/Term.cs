namespace EleCho.GrammarParsing
{
    public abstract class Term
    {
        internal Term(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public static NonTerminalTermRule operator +(Term term1, Term term2)
        {
            return new NonTerminalTermRule([new NonTerminalTermRuleBranch([term1, term2])]);
        }

        public static NonTerminalTermRule operator +(Term term, string text)
        {
            return new NonTerminalTermRule([new NonTerminalTermRuleBranch([term, new SymbolTerm(text, text)])]);
        }

        public static NonTerminalTermRule operator +(string text, Term term)
        {
            return new NonTerminalTermRule([new NonTerminalTermRuleBranch([new SymbolTerm(text, text), term])]);
        }

        public static NonTerminalTermRule operator |(Term term1, Term term2)
        {
            return ((NonTerminalTermRule)term1) | ((NonTerminalTermRule)term2);
        }

        public override string ToString()
        {
            return Name;
        }
    }


}
