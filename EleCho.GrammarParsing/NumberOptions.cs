using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleCho.GrammarParsing
{
    /// <summary>
    /// 十进制数字选项
    /// </summary>
    public class NumberOptions
    {
        /// <summary>
        /// 允许浮点数, 例如 123.45, 0.001
        /// </summary>
        public bool AllowFloat { get; set; } = true;

        /// <summary>
        /// 允许科学计数法 (例如 1.23e4)
        /// </summary>
        public bool AllowScientificNotation { get; set; } = true;

        /// <summary>
        /// 允许以点符号开头的数字 (例如 .123)
        /// </summary>
        public bool AllowStartsWithDot { get; set; } = true;

        /// <summary>
        /// 默认的十进制数字选项
        /// </summary>
        public static NumberOptions Default { get; } = new NumberOptions();
    }
}
