using System.Diagnostics.CodeAnalysis;

namespace EleCho.GrammarParsing
{
    public class StringTerm : Term
    {
        public StringTerm(string name) : base(name)
        {
            Options = new StringOptions();
        }

        public StringTerm(string name, StringOptions options) : base(name)
        {
            Options = options ?? new StringOptions();
        }

        public StringOptions Options { get; }

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

            // 字符串必须以引号开始
            if (textSource.Current != '"')
            {
                return false;
            }

            // 跳过开始的引号
            textSource.Seek(textSource.Position + 1);

            bool escaped = false;

            // 循环直到遇到结束引号或EOF
            while (textSource.Current != -1)
            {
                int currentChar = textSource.Current;

                // 处理转义序列
                if (escaped)
                {
                    // 已经处于转义状态，消费这个字符
                    escaped = false;
                    textSource.Seek(textSource.Position + 1);
                    continue;
                }

                // 检查是否进入转义模式
                if (Options.AllowEscape && currentChar == Options.EscapeChar)
                {
                    escaped = true;
                    textSource.Seek(textSource.Position + 1);
                    continue;
                }

                // 如果是结束引号，完成解析
                if (currentChar == '"')
                {
                    // 消费结束引号
                    textSource.Seek(textSource.Position + 1);

                    // 成功解析
                    node = new ParseTreeNode(this, [], textSource, startPosition, textSource.Position); // 具体的node赋值由你来做
                    return true;
                }

                // 消费普通字符
                textSource.Seek(textSource.Position + 1);
            }

            // 如果到达这里，说明遇到了EOF但没有找到结束引号，解析失败
            textSource.Seek(startPosition);
            return false;
        }
    }
}
