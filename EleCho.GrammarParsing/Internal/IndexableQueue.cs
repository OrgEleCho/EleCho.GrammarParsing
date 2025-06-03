using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EleCho.GrammarParsing.Internal
{
    public partial class IndexableQueue<T> : IReadOnlyList<T>
    {
        private T[] _buffer;
        private int _head;
        private int _count;
        private int _version;

        public IndexableQueue(int capacity)
        {
            _buffer = new T[capacity];
            _head = 0;
            _count = 0;
        }

        public IndexableQueue() : this(4) { }

        public int Count => _count;

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                return _buffer[(_head + index) % _buffer.Length];
            }
        }

        public void Enqueue(T value)
        {
            if (_count == _buffer.Length)
            {
                // grow
                var newBuffer = new T[Math.Max(_buffer.Length * 2, 4)];
                CopyTo(newBuffer, 0);

                if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
                {
                    Array.Clear(_buffer);
                }

                _buffer = newBuffer;
                _head = 0;
            }

            _buffer[(_head + _count) % _buffer.Length] = value;
            _count++;
            _version++;
        }

        public T Dequeue()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException("No item");
            }

            var value = _buffer[_head];

            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                _buffer[_head] = default!;
            }

            _head = (_head + 1) % _buffer.Length;
            _count--;
            _version++;

            return value;
        }

        public void CopyTo(T[] array, int index)
        {
            if (_count == 0)
            {
                return;
            }

            if (array.Length - index < _count)
            {
                throw new ArgumentException("No enough space to copy");
            }

            int tail = (_head + _count) % _buffer.Length;
            if (tail > _head)
            {
                Array.Copy(_buffer, _head, array, index, _count);
            }
            else
            {
                var segment1Count = _buffer.Length - _head;
                Array.Copy(_buffer, _head, array, index, segment1Count);
                Array.Copy(_buffer, 0, array, segment1Count + index, tail);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
