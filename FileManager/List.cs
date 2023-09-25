using System;
using System.Collections;

namespace Collections
{
    public class List<T> : IEnumerable
    {
        public int Length => _array.Length;

        private T[] _array;

        public List()
        {
            _array = new T[0];
        }

        public List(int length)
        {
            if (length < 0)
            {
                throw new ArgumentException($"List length {length} is invalid");
            }

            _array = new T[length];
        }

        public List(T[] array)
        {
            _array = array;
        }

        public T this[int index]
        {
            get => _array[index];
            set => _array[index] = value;
        }

        public void Push(T element)
        {
            T[] temp = new T[_array.Length + 1];
            _array.CopyTo(temp, 0);
            _array = temp;
            _array[_array.Length - 1] = element;
        }

        public void Pop()
        {
            T[] temp = new T[_array.Length - 1];
            
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = _array[i];
            }

            _array = temp;
        }

        public bool Contains(T element)
        {
            foreach(T el in _array)
            {
                if (el.Equals(element))
                {
                    return true;
                }
            }

            return false;
        }

        public T[] ToArray()
        {
            return _array;
        }

        public void Concat(List<T> list)
        {
            T[] toConcat = list.ToArray();
            T[] temp = new T[_array.Length + toConcat.Length];
            _array.CopyTo(temp, 0);
            toConcat.CopyTo(temp, _array.Length);
            _array = temp;
        }

        public void Sort(Func<T, T, bool> sortMethod)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                for (int j = 0; j < _array.Length - 1; j++)
                {
                    if (sortMethod(_array[j], _array[j + 1]))
                    {
                        T temp = _array[j + 1];
                        _array[j + 1] = _array[j];
                        _array[j] = temp;
                    }
                }
            }
        }

        public List<T> Filter(Func<T, bool> filterMethod)
        {
            List<T> result = new List<T>();

            for (int i = 0; i < _array.Length; i++)
            {
                if (!filterMethod(_array[i]))
                {
                    result.Push(_array[i]);
                }
            }

            return result;
        }

        public IEnumerator GetEnumerator()
        {
            return new ListEnumerator<T>(_array);
        }
    }
}
