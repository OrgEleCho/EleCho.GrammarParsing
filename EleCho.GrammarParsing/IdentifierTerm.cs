using System.Diagnostics.CodeAnalysis;

namespace EleCho.GrammarParsing
{
    public class IdentifierTerm : Term
    {
        public IdentifierTerm(string name, IdentifierOptions options) : base(name)
        {
            Options = options ?? new IdentifierOptions();
        }

        public IdentifierOptions Options { get; }
        
        public override bool Parse(ITextSource textSource, ParseOptions options, [NotNullWhen(true)] out ParseTreeNode? node)
        {
            node = null;

            // 保存起始位置，以便失败时恢复
            int startPosition = textSource.Position;

            // 检查是否为空
            if (textSource.Current == -1)
            {
                return false;
            }

            // 检查第一个字符
            // C#标识符必须以字母或下划线开头
            if (!IsValidIdentifierStart(textSource.Current))
            {
                return false;
            }

            // 消费第一个字符
            textSource.Seek(textSource.Position + 1);

            // 消费后续的标识符字符（字母、数字、下划线）
            while (textSource.Current != -1 && IsValidIdentifierPart(textSource.Current))
            {
                textSource.Seek(textSource.Position + 1);
            }

            // 成功解析
            node = new ParseTreeNode(this, [], textSource, startPosition, textSource.Position); // 具体的node赋值由你来做
            return true;
        }

        private bool IsValidIdentifierStart(int c)
        {
            if (c == '_')
                return true;

            if (Options.AllowUnicode)
            {
                // 使用C#的字符类判断，支持Unicode字母
                return char.IsLetter((char)c);
            }
            else
            {
                // 只允许ASCII字母
                return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
            }
        }

        private bool IsValidIdentifierPart(int c)
        {
            if (c == '_')
                return true;

            if (char.IsDigit((char)c))
                return true;

            if (Options.AllowUnicode)
            {
                // 允许Unicode字母和数字
                return char.IsLetterOrDigit((char)c);
            }
            else
            {
                // 只允许ASCII字母和数字
                return (c >= 'A' && c <= 'Z') ||
                       (c >= 'a' && c <= 'z') ||
                       (c >= '0' && c <= '9');
            }
        }
    }
}
