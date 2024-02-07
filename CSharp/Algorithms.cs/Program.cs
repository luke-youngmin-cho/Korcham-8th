namespace Algorithms.cs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Fibonacci fibonacci = new Fibonacci(30);
            Console.WriteLine(fibonacci[5]); // O(1)
            Console.WriteLine(fibonacci[16]); // O(1)
        }
    }
}
