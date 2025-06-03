using EleCho.GrammarParsing.Tokenizing;
using System.Collections;

namespace EleCho.GrammarParsing
{

    public class NonTerminalTermRuleBranch : IReadOnlyList<Term>
    {
        private readonly Term[] _terms;
        private HashSet<TerminalTerm>? _lookAheadSet;

        public NonTerminalTermRuleBranch(IEnumerable<Term> terms)
        {
            if (terms is null)
            {
                throw new ArgumentNullException(nameof(terms));
            }

            foreach (var term in terms)
            {
                if (term is null)
                {
                    throw new ArgumentException("Collection contains null value", nameof(terms));
                }
            }

            _terms = terms.ToArray();
        }

        public Term this[int index] => ((IReadOnlyList<Term>)_terms)[index];

        public int Count => ((IReadOnlyCollection<Term>)_terms).Count;

        public IEnumerator<Term> GetEnumerator()
        {
            return ((IEnumerable<Term>)_terms).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _terms.GetEnumerator();
        }

        public IReadOnlySet<TerminalTerm> GetLookAheadSet()
        {
            if (_lookAheadSet is not null)
            {
                return _lookAheadSet;
            }

            _lookAheadSet = new HashSet<TerminalTerm>();
            if (Count > 0)
            {
                if (this[0] is TerminalTerm terminalTerm)
                {
                    _lookAheadSet.Add(terminalTerm);
                }
                else if (this[0] is NonTerminalTerm nonTerminalTerm)
                {
                    foreach (var terminalTermChild in nonTerminalTerm.GetLookAheadSet())
                    {
                        _lookAheadSet.Add(terminalTermChild);
                    }
                }
            }

            return _lookAheadSet;
        }
    }


}
