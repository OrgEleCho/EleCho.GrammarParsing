// See https://aka.ms/new-console-template for more information
using EleCho.GrammarParsing;
using TestConsole;

var jsonGrammar = new JsonGrammar();
var textSource = new StringTextSource(
    """
    {
        "name": "John",
        "age": 30,
        "car": null,
        "isEmployed": true,
        "children": [
            {
                "name": "Jane",
                "age": 10
            },
            {
                "name": "Doe",
                "age": 5
            }
        ]
    }
    """);

var jsonParseTree = jsonGrammar.Parse(textSource, ParseOptions.Default);

var numberParseOK = new DecimalNumberTerm("QWQ", DecimalNumberOptions.Default).Parse((StringTextSource)"123.456", ParseOptions.Default, out var node);
var stringParseOK = new StringTerm("AWA", StringOptions.Default).Parse((StringTextSource)"\"Hello, World!\"", ParseOptions.Default, out var stringNode);



Console.WriteLine("QWQ");