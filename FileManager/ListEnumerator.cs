using System.Collections;
using System;

namespace Collections
{
    public class ListEnumerator<T> : IEnumerator
    {
        private T[] _array;
        private int _position = -1;

        object IEnumerator.Current => Current;

        public T Current
        {
            get
            {
                return _array[_position];
            }
        }

        public ListEnumerator(T[] array)
        {
            _array = array;
        }

        public bool MoveNext()
        {
            return ++_position < _array.Length;
        }

        public void Reset()
        {
            _position = -1;
        }
    }
}
