using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EleCho.GrammarParsing;

namespace TestConsole
{
    public class JsonGrammar : Grammar
    {
        public JsonGrammar()
        {
            var leftBrace = Terminal("LeftBrace", "{");
            var rightBrace = Terminal("LeftBrace", "}");

            var leftBracket = Terminal("LeftBrace", "[");
            var rightBracket = Terminal("LeftBrace", "]");

            var colon = Terminal("Colon", ":");
            var comma = Terminal("Comma", ",");

            var jsonValue = NonTerminal("JsonValue");
            var jsonObject = NonTerminal("JsonObject");
            var jsonArray = NonTerminal("JsonArray");

            var jsonKeyValuePair = NonTerminal("JsonKeyValuePair");
            var jsonObjectItems = NonTerminal("JsonObjectItems");
            var jsonArrayItems = NonTerminal("JsonArrayItems");

            var jsonNumber = DecimalNumber("JsonNumber");
            var jsonString = String("JsonString");
            var jsonTrue = Terminal("JsonTrue", "true");
            var jsonFalse = Terminal("JsonFalse", "false");
            var jsonNull = Terminal("JsonNull", "null");

            jsonValue.Rule = jsonObject | jsonArray | jsonString | jsonNumber | jsonTrue | jsonFalse | jsonNull;
            jsonObject.Rule = leftBrace + jsonObjectItems + rightBrace | leftBrace + rightBrace;
            jsonArray.Rule = leftBracket + jsonArrayItems + rightBracket | leftBracket + rightBracket;

            jsonKeyValuePair.Rule = jsonString + colon + jsonValue;
            jsonObjectItems.Rule = jsonKeyValuePair + comma + jsonObjectItems | jsonKeyValuePair;
            jsonArrayItems.Rule = jsonValue + comma + jsonArrayItems | jsonValue;

            Root = jsonObject;
        }
    }
}
