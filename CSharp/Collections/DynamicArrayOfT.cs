namespace Collections
{
    internal class DynamicArray<T>
        where T : IEquatable<T>
    {
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                    throw new IndexOutOfRangeException();

                return _items[index];
            }
            set
            {
                if (index < 0 || index >= _count)
                    throw new IndexOutOfRangeException();

                _items[index] = value;
            }
        }

        public int Count => _count;

        public int Capacity => _items.Length;

        private const int DEFAULT_SIZE = 4;
        private T[] _items = new T[DEFAULT_SIZE];
        private int _count;

        public void Add(T item)
        {
            // if (1. 아이템을 추가할 공간 (Capacity) 이 충분한지 확인)
            // {
            // 2. 충분하지않으면 2배짜리 배열 만들고, 기존 배열 데이터 복제
            // 3. 새로 만든 배열로 참조 변경.
            // }
            // count 인덱스에 아이템 삽입. 
            // count++

            if (_count >= _items.Length)
            {
                T[] tmp = new T[_count * 2];
                Array.Copy(_items, tmp, _count);
                _items = tmp;
            }
            _items[_count++] = item;
        }

        public int FindIndex(Predicate<T> match)
        {
            // for 문을 돌린다 
            // {
            //     i 번째 아이템이 match 조건 맞는지 본다
            //     만약 맞으면 i 를 반환한다.
            // }
            // -1 반환한다

            for (int i = 0; i < _count; i++)
            {
                if (match.Invoke(_items[i]))
                    return i;
            }
            return -1;
        }

        public T Find(Predicate<T> match)
        {
            for (int i = 0; i < _count; i++)
            {
                if (match.Invoke(_items[i]))
                    return _items[i];
            }
            return default(T);
        }

        public void RemoveAt(int index)
        {
            // for 루프를 index 부터 count - 1 까지 순회
            // 전부 한 칸씩 앞으로 당김 
            // _count--;

            for (int i = index; i < _count - 1; i++)
            {
                _items[i] = _items[i + 1];
            }
            _count--;
        }

        public bool Remove(T item)
        {
            int index = FindIndex(x => x.Equals(item));

            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }

            return false;
        }
    }
}
