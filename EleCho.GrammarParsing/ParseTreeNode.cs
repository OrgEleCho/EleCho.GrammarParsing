using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleCho.GrammarParsing
{
    public class ParseTreeNode
    {
        private string? _cachedText;

        public ParseTreeNode(Term term, IReadOnlyList<ParseTreeNode> children, ITextSource textSource, int startPosition, int endPosition)
        {
            Term = term ?? throw new ArgumentNullException(nameof(term));
            Children = children ?? throw new ArgumentNullException(nameof(children));
            TextSource = textSource ?? throw new ArgumentNullException(nameof(textSource));
            StartPosition = startPosition;
            EndPosition = endPosition;
        }

        public Term Term { get; }
        public IReadOnlyList<ParseTreeNode> Children { get; }
        public ITextSource TextSource { get; }
        public int StartPosition { get; }
        public int EndPosition { get; }

        public string Text => _cachedText ??= TextSource.GetText(StartPosition, EndPosition);

        public override string ToString()
        {
            return $"{Term.Name} {{ ChildCount: {Children.Count} }}";
        }
    }

    public class ParseTree
    {
        public ParseTree(ParseTreeNode root)
        {
            Root = root ?? throw new ArgumentNullException(nameof(root));
        }

        public ParseTreeNode Root { get; }

        public override string ToString()
        {
            return $"ParseTree(Root: {Root.Term.Name}, Start: {Root.StartPosition}, End: {Root.EndPosition})";
        }
    }
}
