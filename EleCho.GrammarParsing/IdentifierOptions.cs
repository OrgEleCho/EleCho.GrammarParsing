namespace EleCho.GrammarParsing
{
    public class IdentifierOptions
    {
        /// <summary>
        /// 允许 Unicode 字符
        /// </summary>
        public bool AllowUnicode { get; set; } = true;

        public static IdentifierOptions Default { get; } = new IdentifierOptions();
    }
}
