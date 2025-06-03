namespace EleCho.GrammarParsing
{
    public sealed class NonTerminalTerm : Term
    {
        private HashSet<TerminalTerm>? _lookAheadSet;

        public NonTerminalTerm(string name) : base(name)
        {
        }

        public NonTerminalTerm(string name, NonTerminalTermRule? rule) : this(name)
        {
            Rule = rule;
        }

        public NonTerminalTermRule? Rule { get; set; }

        public IReadOnlySet<TerminalTerm> GetLookAheadSet()
        {
            if (_lookAheadSet is not null)
            {
                return _lookAheadSet;
            }

            if (Rule is null)
            {
                throw new InvalidOperationException("No rule");
            }

            _lookAheadSet = new HashSet<TerminalTerm>();
            foreach (var branch in Rule)
            {
                foreach (var terminalTerm in branch.GetLookAheadSet())
                {
                    _lookAheadSet.Add(terminalTerm);
                }
            }

            return _lookAheadSet;
        }
    }


}
