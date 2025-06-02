using System.Collections;

namespace EleCho.GrammarParsing
{
    public class TermSequence : IReadOnlyList<Term>
    {
        public IReadOnlyList<Term> Terms { get; }

        public int Count => Terms.Count;

        public Term this[int index] => Terms[index];

        public TermSequence(IEnumerable<Term> terms)
        {
            Terms = terms?.ToArray() ?? throw new ArgumentNullException(nameof(terms));
        }

        public IEnumerator<Term> GetEnumerator()
        {
            return Terms.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Terms).GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join(" + ", Terms);
        }
    }
}
