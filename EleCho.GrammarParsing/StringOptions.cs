namespace EleCho.GrammarParsing
{
    public class StringOptions
    {
        /// <summary>
        /// 允许转义字符
        /// </summary>
        public bool AllowEscape { get; set; } = true;

        /// <summary>
        /// 转移字符
        /// </summary>
        public char EscapeChar { get; set; } = '\\';

        /// <summary>
        /// 默认的字符串选项实例
        /// </summary>
        public static StringOptions Default => new StringOptions();
    }
}
