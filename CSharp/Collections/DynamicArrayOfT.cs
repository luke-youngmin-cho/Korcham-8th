using System.Collections;

namespace Collections
{
    internal class DynamicArray<T> : IEnumerable<T>
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

        // 책 읽어주는자 데려오기
        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        // inner structure ( 멤버 구조체 )
        // 반복자, 반복기 ( 책 읽어주는자 )
        public struct Enumerator : IEnumerator<T>
        {
            public Enumerator(DynamicArray<T> data)
            {
                _data = data; // 책 읽는자에게 책 쥐어주기
                _index = -1; // 책 표지 덮은상태
            }

            public T Current => _data[_index]; // 현재 페이지 읽기

            object IEnumerator.Current => _data[_index];

            private DynamicArray<T> _data; // 책
            private int _index; // 책의 현재 페이지


            public void Dispose()
            {
            }

            /// <summary>
            /// 다음페이지로 넘기기 시도
            /// </summary>
            /// <returns> true : 다음페이지로 넘어감. false : 다음페이지 없음 </returns>
            public bool MoveNext()
            {
                // 넘길수있는 다음페이지가 있으면 
                if (_index < _data.Count - 1)
                {
                    ++_index; // 다음 페이지로 넘김
                    return true;
                }

                return false;
            }

            /// <summary>
            /// 책 덮기
            /// </summary>
            public void Reset()
            {
                _index = -1;
            }
        }
    }
}
