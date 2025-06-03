using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EleCho.GrammarParsing;

namespace TestConsole.JSON
{
    public class JsonGrammar : Grammar
    {
        public JsonGrammar()
        {
            var leftBrace = new SymbolTerm("LeftBrace", "{");
            var rightBrace = new SymbolTerm("LeftBrace", "}");

            var leftBracket = new SymbolTerm("LeftBrace", "[");
            var rightBracket = new SymbolTerm("LeftBrace", "]");

            var colon = new SymbolTerm("Colon", ":");
            var comma = new SymbolTerm("Comma", ",");

            var jsonValue = new NonTerminalTerm("JsonValue");
            var jsonObject = new NonTerminalTerm("JsonObject");
            var jsonArray = new NonTerminalTerm("JsonArray");

            var jsonKeyValuePair = new NonTerminalTerm("JsonKeyValuePair");
            var jsonObjectItems = new NonTerminalTerm("JsonObjectItems");
            var jsonArrayItems = new NonTerminalTerm("JsonArrayItems");

            var jsonNumber = new NumberTerm("JsonNumber");
            var jsonString = new StringTerm("JsonString");
            var jsonTrue = new IdentifierTerm("JsonTrue", "true");
            var jsonFalse = new IdentifierTerm("JsonFalse", "false");
            var jsonNull = new IdentifierTerm("JsonNull", "null");

            jsonValue.Rule = jsonObject | jsonArray | jsonString | jsonNumber | jsonTrue | jsonFalse | jsonNull;
            jsonObject.Rule = leftBrace + jsonObjectItems + rightBrace | leftBrace + rightBrace;
            jsonArray.Rule = leftBracket + jsonArrayItems + rightBracket | leftBracket + rightBracket;

            jsonKeyValuePair.Rule = jsonString + colon + jsonValue;
            jsonObjectItems.Rule = jsonKeyValuePair + comma + jsonObjectItems | jsonKeyValuePair;
            jsonArrayItems.Rule = jsonValue + comma + jsonArrayItems | jsonValue;

            Root = jsonValue;
        }
    }
}
