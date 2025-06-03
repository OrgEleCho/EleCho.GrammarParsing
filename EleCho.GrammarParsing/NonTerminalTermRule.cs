using System.Collections;

namespace EleCho.GrammarParsing
{
    public class NonTerminalTermRule : IReadOnlyList<NonTerminalTermRuleBranch>
    {
        private readonly NonTerminalTermRuleBranch[] _branches;

        public NonTerminalTermRule(IEnumerable<NonTerminalTermRuleBranch> branches)
        {
            if (branches is null)
            {
                throw new ArgumentNullException(nameof(branches));
            }

            foreach (var branch in branches)
            {
                if (branch is null)
                {
                    throw new ArgumentException("Collection contains null value", nameof(branches));
                }
            }

            _branches = branches.ToArray();
        }

        public NonTerminalTermRuleBranch this[int index] => ((IReadOnlyList<NonTerminalTermRuleBranch>)_branches)[index];

        public int Count => ((IReadOnlyCollection<NonTerminalTermRuleBranch>)_branches).Count;

        public IEnumerator<NonTerminalTermRuleBranch> GetEnumerator()
        {
            return ((IEnumerable<NonTerminalTermRuleBranch>)_branches).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _branches.GetEnumerator();
        }



        public override string ToString()
        {
            return string.Join(" | ", this);
        }


        public static implicit operator NonTerminalTermRule(Term term)
        {
            return new NonTerminalTermRule([new NonTerminalTermRuleBranch([term])]);
        }

        public static NonTerminalTermRule operator +(NonTerminalTermRule rule, Term term)
        {
            var sequences = rule.ToArray();

            return new NonTerminalTermRule([.. sequences[0..^1], new NonTerminalTermRuleBranch([.. sequences[^1], term])]);
        }

        public static NonTerminalTermRule operator |(NonTerminalTermRule rule1, NonTerminalTermRule rule2)
        {
            return new NonTerminalTermRule([.. rule1, .. rule2]);
        }

        public static NonTerminalTermRule operator |(NonTerminalTermRule rule, Term term)
        {
            return rule | (NonTerminalTermRule)term;
        }
    }


}
