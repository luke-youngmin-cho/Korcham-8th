using System.Collections; // C# 의 object 타입 기반의 자료구조들을 포함하는 namespace
using System.Collections.Generic; // C# 의 generic 타입 기반의 자료구조들을 포함하는 namespace
using KeyValurPair = System.Collections.Generic.KeyValuePair;

namespace Collections
{
    internal class Program
    {
        static IEnumerator ToastRoutine()
        {
            yield return "인덕션 가열 시작";
            yield return "인덕션에 팬 올림";
            yield return "팬에 버터 두름";
            yield return "팬에 빵 올림";
            yield return "토스트 완성";
        }

        static IEnumerable<string> ToastRoutinable()
        {
            yield return "인덕션 가열 시작";
            yield return "인덕션에 팬 올림";
            yield return "팬에 버터 두름";
            yield return "팬에 빵 올림";
            yield return "토스트 완성";
        }

        static void Main(string[] args)
        {
            #region Dynamic Array (동적 배열)
            int[] arr = new int[10];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = 0;
                Console.WriteLine(arr[i]);
            }

            // object (Object class) 는 C#의 모든 타입의 기반타입.
            object obj = 1; // Boxing , 값 타입을 object 타입으로 참조하기위해서 힙 영역에 object 타입 객체를 만듬.  
            obj = "안녕"; // 기반타입참조 (Upcasting)
            string str = (string)obj; // 하위타입변환 (Downcasting)
            obj = 4.5f;
            float num = (float)obj; // Unboxing, object 타입에서 원래 값타입을 읽음.
            object obj2 = obj;
            Type type = typeof(int);
            Type dummyType = typeof(Dummy);
            Dummy dummy = new Dummy();
            dummy.SayHi(1, 3);

            DynamicArray da1 = new DynamicArray();
            da1.Add(3);
            da1.Add(5);

            for (int i = 0; i < da1.Count; i++)
            {
                Console.WriteLine(da1[i]);
                da1[i] = 3;
            }

            if (da1.Remove(3))
            {
                Console.WriteLine("Removed 3");
            }

            DynamicArray<ulong> ulongDA = new DynamicArray<ulong>();
            ulongDA.Add(5);
            ulongDA.Remove(5);
            ulongDA.Add(3);
            ulongDA.Add(6);
            ulongDA.Add(1);

            // using 구문 : IDisposable 객체의 Dispose() 호출을 보장하는 구문
            using (IEnumerator<ulong> ulongDAEnum = ulongDA.GetEnumerator())
            {
                while (ulongDAEnum.MoveNext())
                {
                    Console.WriteLine(ulongDAEnum.Current);
                }
                ulongDAEnum.Reset();
            }

            // IEnumerable 객체에 대해 순회하는 구문.
            foreach (ulong item in ulongDA)
            {
                Console.WriteLine(item);
            }

            IEnumerator toastRoutine = ToastRoutine();
            while (toastRoutine.MoveNext())
            {
                Console.WriteLine(toastRoutine.Current);
            }

            foreach (var item in ToastRoutinable())
            {
                Console.WriteLine(item);
            }

            //------ ArrayList ------
            ArrayList arrayList = new ArrayList();
            arrayList.Add(1);
            arrayList.Add("Luke");

            List<ulong> ulongs = new List<ulong>();
            ulongs.Add(5);
            int index = ulongs.FindIndex(x => x > 3);
            if (index >= 0)
            {
                Console.WriteLine($"{ulongs[index]} is bigger than 3");
                ulongs.RemoveAt(index);
            }
            #endregion

            #region Linked List (연결 리스트)
            MyLinkedList<ushort> myLinkedList = new MyLinkedList<ushort>();
            myLinkedList.AddLast(5);
            myLinkedList.AddLast(2);
            MyLinkedListNode<ushort> nodeBiggerThan1 = myLinkedList.FindLast(x => x > 1);
            myLinkedList.AddBefore(nodeBiggerThan1, 10);

            foreach (var item in myLinkedList)
            {
                Console.WriteLine(item);
            }

            //------ LinkedList ---------
            LinkedList<string> nameList = new LinkedList<string>();
            nameList.AddLast("Luke");
            nameList.AddFirst("Carl");
            LinkedListNode<string> nameNode = nameList.Find("Luke");
            nameList.AddAfter(nameNode, "Clerk");

            #endregion

            #region Dictionary (해시 테이블)
            MyDictionary<string, int> gradeTable = new MyDictionary<string, int>();
            int value;
            if (gradeTable.TryGetValue("철수", out value))
            {
                Console.WriteLine($"철수의 점수는 : {value}");
            }

            gradeTable.Add("철수", 90);
            gradeTable.Add("영희", 70);
            foreach (var item in gradeTable)
            {
                Console.WriteLine($"{item.Key} 의 점수 : {item.Value}");
            }

            Dictionary<ulong, string> nickNames = new Dictionary<ulong, string>();
            nickNames.Add(12309213123, "철수");
            nickNames.Add(42984712894, "영희");
            foreach (System.Collections.Generic.KeyValuePair<ulong, string> item in nickNames)
            {
            }

            foreach (var keys in nickNames.Keys)
            {
            }

            foreach (var values in nickNames.Values)
            {
            }
            #endregion

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(0);
            queue.Enqueue(5);
            queue.Dequeue();
            queue.Peek();

            Stack<int> stack = new Stack<int>();
            stack.Push(0);
            stack.Push(5);
            stack.Pop();
            stack.Peek();

            Random random = new Random();
            List<int> list = new List<int>(Enumerable.Repeat(0, 20)
                                                     .Select(x => random.Next(0, 10)));

            var filtered =
                from item in list
                where item > 5
                orderby item descending
                select item;

            foreach (var item in filtered)
            {
                Console.WriteLine(item);
            }

            filtered =
                list.Where(item => item > 5)
                    .OrderByDescending(item => item);
        }
    }


    public class Dummy : object
    {
        public void SayHi(int count, float num) { }
    }

    public class A
    {
        public int num = 1;
    }

    public class B : A
    {
        new public int num = 2; // 같은 이름의 기반타입 멤버 숨김
        // 숨긴다는건 기반타입 멤버도 메모리에 할당은 하되, 업캐스팅 하지 않고서는 접근이 안된다는걸 의미함.
    }

    public class Test
    {
        public void Main()
        {
            A a = new A();
            Console.WriteLine(a.num); // ... 1
            B b = new B();
            Console.WriteLine(b.num); // ... 2

            A a2 = new B();
            Console.WriteLine(a2.num); // ... 1
            Console.WriteLine((a2 as B)?.num); // ... 2
        }
    }

    
}
