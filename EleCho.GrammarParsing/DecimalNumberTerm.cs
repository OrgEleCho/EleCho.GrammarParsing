using System.Diagnostics.CodeAnalysis;

namespace EleCho.GrammarParsing
{
    /// <summary>
    /// 十进制数字术语
    /// </summary>
    public class DecimalNumberTerm : Term
    {
        public DecimalNumberTerm(string name, DecimalNumberOptions options) : base(name)
        {
            Options = options;
        }

        public DecimalNumberOptions Options { get; }

        public override bool Parse(ITextSource textSource, ParseOptions options, [NotNullWhen(true)] out ParseTreeNode? node)
        {
            node = null;

            // 存储初始位置，用于在失败时恢复
            int startPosition = textSource.Position;

            // 检查是否为空
            if (textSource.Current == -1)
            {
                return false;
            }

            // 处理符号部分
            bool hasSign = false;
            if (Options.AllowSign && (textSource.Current == '+' || textSource.Current == '-'))
            {
                hasSign = true;
                textSource.Seek(textSource.Position + 1);

                // 如果只有符号没有数字，则失败
                if (textSource.Current == -1)
                {
                    textSource.Seek(startPosition);
                    return false;
                }
            }

            // 初始化跟踪变量
            bool hasDigits = false;
            bool hasDot = false;
            bool hasExponent = false;

            // 处理整数部分或者允许以点开头的情况
            if (char.IsDigit((char)textSource.Current))
            {
                hasDigits = true;

                // 消费所有数字
                while (textSource.Current != -1 && char.IsDigit((char)textSource.Current))
                {
                    textSource.Seek(textSource.Position + 1);
                }
            }

            // 处理小数点和小数部分
            if (textSource.Current == '.')
            {
                // 如果没有前导数字，检查是否允许以点开头
                if (!hasDigits && !Options.AllowStartsWithDot)
                {
                    textSource.Seek(startPosition);
                    return false;
                }

                // 检查是否允许浮点数
                if (!Options.AllowFloat)
                {
                    // 如果已经有数字，则成功解析为整数
                    if (hasDigits)
                    {
                        // 解析成功，返回整数部分
                        node = new ParseTreeNode(this, [], textSource, startPosition, textSource.Position); // 具体的node赋值由你来做
                        return true;
                    }
                    textSource.Seek(startPosition);
                    return false;
                }

                hasDot = true;
                textSource.Seek(textSource.Position + 1);

                // 解析小数部分的数字
                bool hasFractionalDigits = false;
                while (textSource.Current != -1 && char.IsDigit((char)textSource.Current))
                {
                    hasFractionalDigits = true;
                    textSource.Seek(textSource.Position + 1);
                }

                // 如果有小数点但没有任何数字（整数部分和小数部分），则失败
                if (!hasDigits && !hasFractionalDigits)
                {
                    textSource.Seek(startPosition);
                    return false;
                }

                // 如果有小数点，则至少需要整数部分或小数部分有数字
                hasDigits = hasDigits || hasFractionalDigits;
            }

            // 处理科学记数法部分
            if (Options.AllowScientificNotation &&
                (textSource.Current == 'e' || textSource.Current == 'E'))
            {
                // 如果前面没有任何数字，则失败
                if (!hasDigits)
                {
                    textSource.Seek(startPosition);
                    return false;
                }

                hasExponent = true;
                textSource.Seek(textSource.Position + 1);

                // 处理指数部分的符号
                if (textSource.Current == '+' || textSource.Current == '-')
                {
                    textSource.Seek(textSource.Position + 1);
                }

                // 处理指数部分的数字
                bool hasExponentDigits = false;
                while (textSource.Current != -1 && char.IsDigit((char)textSource.Current))
                {
                    hasExponentDigits = true;
                    textSource.Seek(textSource.Position + 1);
                }

                // 如果有E但没有指数部分的数字，则失败
                if (!hasExponentDigits)
                {
                    textSource.Seek(startPosition);
                    return false;
                }
            }

            // 检查是否有任何有效的数字部分
            if (!hasDigits)
            {
                textSource.Seek(startPosition);
                return false;
            }

            // 解析成功
            node = new ParseTreeNode(this, [], textSource, startPosition, textSource.Position); // 具体的node赋值由你来做
            return true;
        }
    }
}
