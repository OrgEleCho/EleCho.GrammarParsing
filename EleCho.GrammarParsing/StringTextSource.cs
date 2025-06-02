namespace EleCho.GrammarParsing
{
    public sealed class StringTextSource : ITextSource
    {
        public StringTextSource(string text)
        {
            Text = text;
        }

        public string Text { get; }
        public int Current
        {
            get
            {
                if (Position < 0 || Position >= Text.Length)
                {
                    return -1;
                }

                return Text[Position];
            }
        }

        public int Position { get; private set; }

        public void Seek(int position)
        {
            Position = position;
        }

        public static implicit operator StringTextSource(string text)
        {
            return new StringTextSource(text);
        }
    }
}
