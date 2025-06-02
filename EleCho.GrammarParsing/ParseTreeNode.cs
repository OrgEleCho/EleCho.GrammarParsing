using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EleCho.GrammarParsing.AST;

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

        public Syntax BuildSyntax()
        {
            if (Term.SyntaxBuilder is null)
            {
                return new DefaultSyntax(this, Children.Select(c => c.BuildSyntax()).ToArray());
            }

            return Term.SyntaxBuilder.Invoke(this, Children.Select(c => c.BuildSyntax()).ToArray());
        }

        public override string ToString()
        {
            return $"{Term.Name} {{ ChildCount: {Children.Count} }}";
        }
    }
}
