using System.Collections;

namespace EleCho.GrammarParsing
{
    public class TermRule : IReadOnlyList<TermSequence>
    {
        public IReadOnlyList<TermSequence> Matches { get; }

        public TermRule(IEnumerable<TermSequence> termSequences)
        {
            Matches = termSequences?.ToArray() ?? throw new ArgumentNullException(nameof(termSequences));
        }

        public TermSequence this[int index] => Matches[index];

        public int Count => Matches.Count;

        public IEnumerator<TermSequence> GetEnumerator()
        {
            return Matches.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Matches).GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join(" | ", Matches);
        }


        public static implicit operator TermRule(Term term)
        {
            return new TermRule([new TermSequence([term])]);
        }

        public static TermRule operator +(TermRule rule, Term term)
        {
            var sequences = rule.Matches.ToArray();

            return new TermRule([.. sequences[0..^1], new TermSequence([.. sequences[^1], term])]);
        }

        public static TermRule operator |(TermRule rule1, TermRule rule2)
        {
            return new TermRule([.. rule1, .. rule2]);
        }

        public static TermRule operator |(TermRule rule, Term term)
        {
            return rule | (TermRule)term;
        }
    }
}
