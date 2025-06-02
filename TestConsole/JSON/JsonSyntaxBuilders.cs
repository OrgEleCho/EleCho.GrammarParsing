using EleCho.GrammarParsing.AST;

namespace TestConsole.JSON
{
    public static class JsonSyntaxBuilders
    {
        public static SyntaxBuilder Object { get; } = (parseTreeNode, children) =>
        {

        };

        public static SyntaxBuilder Array { get; } = (parseTreeNode, children) =>
        {

        };

        public static SyntaxBuilder True { get; } = (_, _) => JsonBoolean.True;
        public static SyntaxBuilder False { get; } = (_, _) => JsonBoolean.False;
        public static SyntaxBuilder Null { get; } = (_, _) => JsonNull.Instance;
    }
}
