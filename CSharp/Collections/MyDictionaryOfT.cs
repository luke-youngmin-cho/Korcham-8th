using System.Collections;

namespace Collections
{
    public struct KeyValuePair<T, K>
    {
        public KeyValuePair(T key, K value)
        {
            this.Key = key;
            this.Value = value;
        }

        public T Key;
        public K Value;
    }


    internal class MyDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        public TValue this[TKey key]
        {
            get
            {
                return Find(key);
            }
            set
            {
                Entry entry = GetEntry(key);
                if (entry != null)
                {
                    entry.Value = value;
                }
                else
                {
                    throw new Exception($"{key} 값을 찾을 수 없습니다.");
                }
            }
        }


        public class Entry
        {
            public Entry Next;
            public TKey Key;
            public TValue Value;
        }
        private Entry[] _entries = new Entry[DEFAULT_SIZE];
        private const int DEFAULT_SIZE = 100;

        public void Add(TKey key, TValue value)
        {
            uint hashCode = (uint)key.GetHashCode();
            uint index = hashCode % DEFAULT_SIZE;

            Entry entry = _entries[index];
            if (entry == null)
            {
                _entries[index] = new Entry { Next = null, Key = key, Value = value };
            }
            else
            {
                if (entry.Key.Equals(key))
                    throw new Exception($"{key} 는 이미 존재합니다. 중복 키를 허용할 수없습니다.");

                while (entry.Next != null)
                {
                    entry = entry.Next;

                    if (entry.Key.Equals(key))
                        throw new Exception($"{key} 는 이미 존재합니다. 중복 키를 허용할 수없습니다.");
                }

                entry.Next = new Entry { Next = null, Key = key, Value = value };
            }
        }

        private Entry GetEntry(TKey key)
        {
            uint hashCode = (uint)key.GetHashCode();
            uint index = hashCode % DEFAULT_SIZE;

            Entry entry = _entries[index];

            do
            {
                if (entry != null)
                {
                    if (entry.Key.Equals(key))
                    {
                        return entry;
                    }

                    entry = entry.Next;
                }
            }
            while (entry != null);

            return null;
        }

        public TValue Find(TKey key)
        {
            uint hashCode = (uint)key.GetHashCode();
            uint index = hashCode % DEFAULT_SIZE;

            Entry entry = _entries[index];

            do
            {
                if (entry != null)
                {
                    if (entry.Key.Equals(key))
                    {
                        return entry.Value;
                    }

                    entry = entry.Next;
                }
            }
            while (entry != null);

            return default(TValue);
        }
        
        public bool TryGetValue(TKey key, out TValue value)
        {
            uint hashCode = (uint)key.GetHashCode();
            uint index = hashCode % DEFAULT_SIZE;

            Entry entry = _entries[index];

            do
            {
                if (entry != null)
                {
                    if (entry.Key.Equals(key))
                    {
                        value = entry.Value;
                        return true;
                    }

                    entry = entry.Next;
                }
            }
            while (entry != null);

            value = default(TValue);
            return false;
        }

        public bool Remove(TKey key)
        {
            uint hashCode = (uint)key.GetHashCode();
            uint index = hashCode % DEFAULT_SIZE;

            Entry entry = _entries[index];
            Entry prev = entry;
            do
            {
                if (entry != null)
                {
                    if (entry.Key.Equals(key))
                    {
                        // 충돌이 없었다면
                        if (prev == entry)
                            _entries[index] = null;
                        else
                            prev.Next = entry.Next;

                        return true;
                    }

                    prev = entry;
                    entry = entry.Next;
                }
            }
            while (entry != null);

            return false;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            public Enumerator(MyDictionary<TKey, TValue> dictionary)
            {
                _dictionary = dictionary;
                _validIndex = 0;
                _entry = null;
                _current = default;
            }

            public KeyValuePair<TKey, TValue> Current => _current;

            object IEnumerator.Current => _current;

            private MyDictionary<TKey, TValue> _dictionary;
            private int _validIndex; // 버킷 배열의 인덱스
            private Entry _entry;
            private KeyValuePair<TKey, TValue> _current;


            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                int index = _validIndex;
                while (index < DEFAULT_SIZE)
                {
                    if (_entry != null)
                    {
                        if (_entry.Next != null)
                        {
                            _entry = _entry.Next;
                            _current = new KeyValuePair<TKey, TValue> { Key = _entry.Key, Value = _entry.Value };
                            return true;
                        }
                        else
                        {
                            ++index;
                        }
                    }

                    if (_dictionary._entries[index] != null)
                    {
                        _validIndex = index;
                        _entry = _dictionary._entries[index];
                        _current = new KeyValuePair<TKey, TValue> { Key = _entry.Key, Value = _entry.Value };
                        return true;
                    }

                    _entry = null;
                    ++index;
                }

                return false;
            }

            public void Reset()
            {
                _validIndex = 0;
                _entry = null;
                _current = default;
            }
        }
    }
}
