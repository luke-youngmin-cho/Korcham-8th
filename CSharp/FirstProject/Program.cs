namespace FirstProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 0x43A32345
            A a1 = new A();
            A a2 = new A();
            a1.DoSomething();
            a2.DoSomething();

            B b1 = new B();
            b1.num2 = 1;
            b1.num3 = 2;
            //A a3 = b1; // 안됨
        }
    }

    public class A
    {
        public static int num1;
        public int num2;

        public void DoSomething()
        {
            Console.WriteLine(num2);
        }
    }

    public class B
    {
        public static int num1;
        public int num2;
        public ulong num3;

        public void DoSomething()
        {
            Console.WriteLine(num2);
        }
    }
}
