namespace Collections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[10];
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }

            DynamicArray da1 = new DynamicArray();
            da1.Add(3);
            da1.Add(5);

            for (int i = 0; i < da1.Count; i++)
            {
                Console.WriteLine(da1[i]);
            }

            if (da1.Remove(3))
            {
                Console.WriteLine("Removed 3");
            }

            DynamicArray<ulong> ulongDA = new DynamicArray<ulong>();
            ulongDA.Add(5);
            ulongDA.Remove(5);

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
        }
    }


    public class Dummy : object
    {
        public void SayHi(int count, float num) { }
    }
}
