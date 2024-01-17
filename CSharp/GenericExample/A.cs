using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericExample
{
    // Generic type
    // "컴파일타임"에 사용되는 경우가 있는 타입을 정의하도록 하는 문법
    internal class A<T>
    {
        public T Num1;
        public T Num2;

        public void PrintAll()
        {
            Console.WriteLine(Num1);
            Console.WriteLine(Num2);
        }

        public void PrintType<K>()
        {
            Console.WriteLine(typeof(K));
        }

        public TResult DoSomething<TResult, T>(T param1)
        {
            return default(TResult);
        }
    }

    public class B<A,C,D,E,F,G>
        where A : class
        where C : Enum
    {
    }

    internal class AOfInt
    {
        public int Num1;
        public int Num2;

        public void PrintAll()
        {
            Console.WriteLine(Num1);
            Console.WriteLine(Num2);
        }

        public void PrintTypeOfUlong()
        {
            Console.WriteLine(typeof(ulong));
        }

        public void PrintTypeOfUshort()
        {
            Console.WriteLine(typeof(ushort));
        }
    }

    internal class AOfFloat
    {
        public float Num1;
        public float Num2;

        public void PrintAll()
        {
            Console.WriteLine(Num1);
            Console.WriteLine(Num2);
        }
    }
}
