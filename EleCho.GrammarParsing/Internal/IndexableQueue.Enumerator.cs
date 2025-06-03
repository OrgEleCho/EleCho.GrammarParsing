using System.Collections;

namespace EleCho.GrammarParsing.Internal
{
    public partial class IndexableQueue<T>
    {
        public class Enumerator : IEnumerator<T>
        {
            private readonly IndexableQueue<T> _queue;
            private readonly int _originVersion;
            private int _index;

            public Enumerator(IndexableQueue<T> queue)
            {
                _originVersion = queue._version;
                _index = -1;
                _queue = queue;
            }

            public T Current => _queue[_index];

            object? IEnumerator.Current => Current;

            public void Dispose()
            {
                // nothing to do
                Reset();
            }

            public bool MoveNext()
            {
                if (_originVersion != _queue._version)
                {
                    throw new InvalidOperationException("Collection modified");
                }

                _index++;
                return _index < _queue.Count;
            }

            public void Reset()
            {
                _index = -1;
            }
        }
    }
}
