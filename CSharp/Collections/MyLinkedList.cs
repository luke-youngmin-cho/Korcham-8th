﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    class MyLinkedListNode<T>
        where T : IEquatable<T>
    {
        public T Value;
        public MyLinkedListNode<T> Prev;
        public MyLinkedListNode<T> Next;

        public MyLinkedListNode(T value)
        {
            this.Value = value;
        }
    }

    internal class MyLinkedList<T> : IEnumerable<T>
        where T : IEquatable<T>
    {
        public MyLinkedListNode<T> First => _first;

        public MyLinkedListNode<T> Last => _last;

        public int Count => _count;


        private MyLinkedListNode<T> _first;
        private MyLinkedListNode<T> _last;
        private int _count;


        public MyLinkedListNode<T> Find(Predicate<T> match)
        {
            // While 문에서 First 부터 탐색 시작 ~ 현재 탐색중인 노드의 다음노드가 없을때 까지
            // 순회중에 조건에 맞는 노드 찾으면 해당 노드반환
            // 못찾았으면 null 반환

            MyLinkedListNode<T> tmp = _first;
            while (tmp != null)
            {
                if (match.Invoke(tmp.Value))
                    return tmp;

                tmp = tmp.Next;
            }

            return null;
        }

        public MyLinkedListNode<T> FindLast(Predicate<T> match)
        {
            MyLinkedListNode<T> tmp = _last;
            while (tmp != null)
            {
                if (match.Invoke(tmp.Value))
                    return tmp;

                tmp = tmp.Prev;
            }

            return null;
        }

        public void AddFirst(T value)
        {
            // value 값의 새 노드를 만든다.
            // if ( first 가 있음 )
            //      새 노드의 Next 를 first 로,
            //      first 의 Prev 를 새 노드로.
            // else 
            //      last 를 새 노드로 ( 이게 최초 삽입 이기 떄문에 last 가 null 인 상황 )
            //
            // first 를 새 노드로 갱신 
            // count++

            MyLinkedListNode<T> tmp = new MyLinkedListNode<T>(value);

            if (_first != null)
            {
                tmp.Next = _first;
                _first.Prev = tmp;
            }
            else
            {
                _last = tmp;
            }

            _first = tmp;
            _count++;
        }

        public void AddLast(T value)
        {
            // value 값의 새 노드를 만든다.
            // if ( last 가 있음 )
            //      새 노드의 Prev 를 last 로,
            //      last 의 Next 를 새 노드로.
            // else 
            //      first 를 새 노드로 ( 이게 최초 삽입 이기 떄문에 first 가 null 인 상황 )
            //
            // last 를 새 노드로 갱신 
            // count++

            MyLinkedListNode<T> tmp = new MyLinkedListNode<T>(value);

            if (_last != null)
            {
                tmp.Prev = _last;
                _last.Next = tmp;
            }
            else
            {
                _first = tmp;
            }

            _last = tmp;
            _count++;
        }

        public void AddBefore(MyLinkedListNode<T> node, T value)
        {
            // 새 노드를 만든다.
            // if (기준노드.Prev 가 있으면)
            // {
            //      기준노드.Prev.Next = 새 노드
            //      새 노드.Prev = 기준노드.Prev
            // }
            // else (... 기준노드가 first 일때는 first 를 갱신해야함)
            // {
            //      first = 새 노드
            // }
            //
            // 기준 노드.Prev = 새 노드
            // 새 노드.Next = 기준 노드
            // count++

            MyLinkedListNode<T> tmp = new MyLinkedListNode<T>(value);
            if (node.Prev != null)
            {
                node.Prev.Next = tmp;
                tmp.Prev = node.Prev;
            }
            else
            {
                _first = tmp;
            }

            node.Prev = tmp;
            tmp.Next = node;
            _count++;
        }


        public void AddAfter(MyLinkedListNode<T> node, T value)
        {
            // 새 노드를 만든다.
            // if (기준노드.Next 가 있으면)
            // {
            //      기준노드.Next.Prev = 새 노드
            //      새 노드.Next = 기준노드.Next
            // }
            // else (... 기준노드가 last 일때는 last 를 갱신해야함)
            // {
            //      last = 새 노드
            // }
            //
            // 기준 노드.Next = 새 노드
            // 새 노드.Prev = 기준 노드
            // count++

            MyLinkedListNode<T> tmp = new MyLinkedListNode<T>(value);
            if (node.Next != null)
            {
                node.Next.Prev = tmp;
                tmp.Next = node.Next;
            }
            else
            {
                _last = tmp;
            }

            node.Next = tmp;
            tmp.Prev = node;
            _count++;
        }

        public bool Remove(T value)
        {
            return Remove(Find(x => x.Equals(value)));
        }

        public bool RemoveLast(T value)
        {
            return Remove(FindLast(x => x.Equals(value)));
        }

        public bool Remove(MyLinkedListNode<T> node)
        {
             // if node 가 null 
             // return false
             //
             // if (Prev 있으면) 
             //     node.Prev.Next 를 node.Next 로
             // else ( 없으면 지우려는게 first 다)
             //     first = node.Next
             //
             // if (Next 있으면)
             //     node.Next.Prev 를 node.Prev 로
             // else ( 없으면 지우려는게 last 다 )
             //     last = node.Prev
             //
             // count--
             // return true

            if (node == null)
                return false;

            if (node.Prev != null)
                node.Prev.Next = node.Next;
            else
                _first = node.Next;

            if (node.Next != null)
                node.Next.Prev = node.Prev;
            else
                _last = node.Prev;

            _count--;
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public struct Enumerator : IEnumerator<T>
        {
            public Enumerator(MyLinkedList<T> list)
            {
                _list = list;
                _node = list._first;
                _current = default(T);
            }


            public T Current => _current;

            object IEnumerator.Current => _current;

            private MyLinkedList<T> _list;
            private MyLinkedListNode<T> _node;
            private T _current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (_node == null)
                    return false;

                _current = _node.Value;
                _node = _node.Next;
                return true;
            }

            public void Reset()
            {
                _node = _list._first;
                _current = default(T);
            }
        }
    }
}
