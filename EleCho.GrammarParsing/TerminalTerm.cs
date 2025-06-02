using System.Diagnostics.CodeAnalysis;

namespace EleCho.GrammarParsing
{
    public class TerminalTerm : Term
    {
        public string Text { get; }

        public TerminalTerm(string name, string text) : base(name)
        {
            Text = text;
        }

        public override bool Parse(ITextSource textSource, ParseOptions options, [NotNullWhen(true)] out ParseTreeNode? node)
        {
            var startPosition = textSource.Position;

            for (int i = 0; i < Text.Length; i++)
            {
                textSource.Seek(startPosition + i);

                if (textSource.Current != Text[i])
                {
                    node = null;
                    return false;
                }
            }

            // seek to next char after the terminal text
            textSource.Seek(startPosition + Text.Length);
            node = new ParseTreeNode(this, [], textSource, startPosition, textSource.Position);
            return true;
        }
    }
}
