using EleCho.GrammarParsing.Internal;
using EleCho.GrammarParsing.Parsing;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Xml.Linq;

namespace EleCho.GrammarParsing.Tokenizing
{
    public partial class Tokenizer
    {
        public class TokenStream : ITokenStream
        {
            private int _position;
            private IndexableQueue<Token> _tokenBuffer;

            public TokenStream(Grammar grammar, ITextStream textStream)
            {
                Grammar = grammar;
                TextStream = textStream;

                _tokenBuffer = new IndexableQueue<Token>();
            }

            public Grammar Grammar { get; }
            public ITextStream TextStream { get; }

            private string ReadNumber()
            {
                var reader = TextStream;

                int cur;
                cur = reader.Peek(0);

                StringBuilder sb = new StringBuilder();
                sb.Append((char)cur);
                reader.Read();  // skip the first char. 跳过第一个字符

                while (cur != -1)
                {
                    cur = reader.Peek(0);
                    if (cur >= '0' && cur <= '9')
                    {
                        reader.Read();   // skip the char. 跳过字符
                        sb.Append((char)cur);
                    }
                    else
                    {
                        break;
                    }
                }

                if (Grammar.NumberOptions.AllowFloat && cur == '.')
                {
                    reader.Read();   // skip the char. 跳过字符
                    sb.Append((char)cur);
                    while (cur != -1)
                    {
                        cur = reader.Peek(0);
                        if (cur >= '0' && cur <= '9')
                        {
                            reader.Read();   // skip the char. 跳过字符
                            sb.Append((char)cur);
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                if (Grammar.NumberOptions.AllowStartsWithDot && (cur == 'e' || cur == 'E'))
                {
                    sb.Append((char)cur);

                    cur = reader.Peek(0);
                    if (cur == '+' || cur == '-')
                    {
                        reader.Read();   // skip the first '+' or '-'. 跳过第一个'+'或'-'
                        sb.Append((char)cur);
                    }

                    while (cur != -1)
                    {
                        reader.Peek(0);
                        if (cur >= '0' && cur <= '9')
                        {
                            reader.Read();   // skip the char. 跳过字符
                            sb.Append((char)cur);
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                return sb.ToString();
            }

            private string ReadIdentifier()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append((char)TextStream.Read());

                while (true)
                {
                    var peek = TextStream.Peek(0);
                    if (peek < 0 || !char.IsLetterOrDigit((char)peek))
                    {
                        break;
                    }

                    TextStream.Read();
                    sb.Append((char)peek);
                }

                return sb.ToString();
            }

            private string ReadString()
            {
                var reader = TextStream;

                int cur;
                cur = reader.Read();   // skip the first '"' 跳过第一个双引号

                bool escape = false;
                StringBuilder sb = new StringBuilder();
                while (cur != -1)
                {
                    cur = reader.Read();
                    if (escape)
                    {
                        escape = false;
                        sb.Append(cur switch
                        {
                            '0' => '\0',
                            'a' => '\a',
                            'b' => '\b',
                            't' => '\t',
                            'r' => '\r',
                            'f' => '\f',
                            'n' => '\n',
                            'v' => '\v',
                            _ => cur
                        });
                    }
                    else
                    {
                        if (cur == '\\')
                        {
                            escape = true;
                        }
                        else if (cur == '"')
                        {
                            // " is already readed, so we need to read the next char, 双引号已经被读取了, 所以我们不需要读取下一个字符
                            return sb.ToString();
                        }
                        else
                        {
                            sb.Append((char)cur);
                        }
                    }
                }

                throw new InvalidOperationException("Unexpected end of stream");
            }

            private string ReadSymbol()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append((char)TextStream.Read());

                while (true)
                {
                    var peek = TextStream.Peek(0);
                    if (peek < 0 || 
                        char.IsLetterOrDigit((char)peek) ||
                        char.IsWhiteSpace((char)peek))
                    {
                        break;
                    }

                    TextStream.Read();
                    sb.Append((char)peek);
                }

                return sb.ToString();
            }

            internal int SkipWhiteSpace()
            {
                var reader = TextStream;

                int cur;
                while (true)
                {
                    cur = reader.Peek(0);  // skip the whitespace. 跳过空白
                    if (char.IsWhiteSpace((char)cur))
                    {
                        reader.Read();
                    }
                    else
                    {
                        break;
                    }
                }

                return cur;
            }

            private bool ReadNextTokenFromTextStream()
            {
                if (Grammar.TokenizeOptions.IgnoreWhiteSpace)
                {
                    SkipWhiteSpace();
                }

                var peek = TextStream.Peek(0);
                if (peek < 0)
                {
                    return false;
                }

                var peekChar = (char)peek; 
                
                if (peekChar == '"')
                {
                    var startPosition = TextStream.Position;
                    var numberText = ReadString();

                    _tokenBuffer.Enqueue(new Token(TokenKind.String, TextStream, numberText, startPosition, TextStream.Position));
                    return true;
                }
                else if (char.IsAsciiDigit(peekChar))
                {
                    var startPosition = TextStream.Position;
                    var numberText = ReadNumber();

                    _tokenBuffer.Enqueue(new Token(TokenKind.Number, TextStream, numberText, startPosition, TextStream.Position));
                    return true;
                }
                else if (char.IsLetter(peekChar) || peekChar == '_')
                {
                    var startPosition = TextStream.Position;
                    var numberText = ReadIdentifier();

                    _tokenBuffer.Enqueue(new Token(TokenKind.Identifier, TextStream, numberText, startPosition, TextStream.Position));
                    return true;
                }
                else
                {
                    var startPosition = TextStream.Position;
                    var numberText = ReadSymbol();

                    _tokenBuffer.Enqueue(new Token(TokenKind.Symbol, TextStream, numberText, startPosition, TextStream.Position));
                    return true;
                }
            }

            public int Position => _position;

            public Token Read()
            {
                if (_tokenBuffer.Count == 0)
                {
                    if (!ReadNextTokenFromTextStream())
                    {
                        return Token.EndOfFile(TextStream);
                    }
                }

                _position++;
                return _tokenBuffer.Dequeue();
            }

            public Token Peek(int offset)
            {
                if (offset < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(offset));
                }

                while (_tokenBuffer.Count <= offset)
                {
                    if (!ReadNextTokenFromTextStream())
                    {
                        return Token.EndOfFile(TextStream);
                    }
                }

                return _tokenBuffer[offset];
            }
        }
    }
}
