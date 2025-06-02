using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EleCho.GrammarParsing.AST;

namespace TestConsole.JSON
{
    public abstract class JsonValue : Syntax
    {

    }

    public class JsonObject : JsonValue
    {
        public JsonObject(IEnumerable<KeyValuePair<string, JsonValue>>)
    }

    public class JsonArray : JsonValue
    {

    }

    public class JsonBoolean : JsonValue
    {
        private JsonBoolean(bool value)
        {
            Value = value;
        }

        public bool Value { get; }

        public static JsonBoolean True { get; } = new JsonBoolean(true);
        public static JsonBoolean False { get; } = new JsonBoolean(false);
    }

    public class JsonNumber : JsonValue
    {
        public JsonNumber(double value)
        {
            Value = value;
        }

        public double Value { get; }
    }

    public class JsonNull : JsonValue
    {
        private JsonNull() { }

        public static JsonNull Instance { get; } = new JsonNull();
    }
}
