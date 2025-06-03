// See https://aka.ms/new-console-template for more information
using EleCho.GrammarParsing;
using EleCho.GrammarParsing.Parsing;
using EleCho.GrammarParsing.Tokenizing;
using TestConsole;
using TestConsole.JSON;

var jsonGrammar = new JsonGrammar();
var textSource = new StringTextStream(
    """
    [
      { "abc": "234" },
      123
    ]
    """);

var tokenizer = new Tokenizer(jsonGrammar);
var tokenStream = tokenizer.Tokenize(textSource);

//List<Token> tokens = new List<Token>();
//while (tokenStream.Read() is { Kind: not TokenKind.EndOfFile } token)
//{
//    tokens.Add(token);
//}

//return;

var parser = new RecursiveDescentParser(jsonGrammar);
var parseTree = parser.Parse(tokenStream);

Console.WriteLine("QWQ");