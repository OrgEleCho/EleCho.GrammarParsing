using System.Diagnostics.CodeAnalysis;

namespace EleCho.GrammarParsing
{
    public class NonTerminalTerm : Term
    {
        public TermRule? Rule { get; set; }

        public NonTerminalTerm(string name) : base(name)
        {
        }

        public NonTerminalTerm(string name, TermRule rule) : base(name)
        {
            Rule = rule;
        }

        public override bool Parse(ITextSource textSource, ParseOptions options, [NotNullWhen(true)] out ParseTreeNode? node)
        {
            if (Rule is null)
            {
                node = null;
                return false;
            }

            var startPosition = textSource.Position;
            var children = new List<ParseTreeNode>();
            foreach (var match in Rule)
            {
                textSource.Seek(startPosition);

                bool success = true;
                children.Clear();
                foreach (var term in match)
                {
                    if (options.IgnoreWhitespace)
                    {
                        textSource.SkipWhitespace();
                    }

                    if (!term.Parse(textSource, options, out var childNode))
                    {
                        success = false;
                        break;
                    }

                    children.Add(childNode);
                }

                if (success)
                {
                    node = new ParseTreeNode(this, children, textSource, startPosition, textSource.Position);
                    return true;
                }
            }

            node = null;
            return false;
        }
    }
}
