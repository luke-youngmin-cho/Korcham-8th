namespace GenericExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            A<int> aOfInt = new A<int>();
            A<float> aOfFloat = new A<float>();
            aOfInt.PrintType<ulong>();
            aOfInt.PrintType<ushort>();
        }
    }
}
