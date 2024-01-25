using System.Diagnostics;
using System.Linq;

namespace SortAlgorithms.cs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int[] arr =
            Enumerable.Repeat(0, 10000000)
                      .Select(x => random.Next(0, 1000))
                      .ToArray();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            //ArraySort.BubbleSort(arr); // 10만개, 25000 ms
            //ArraySort.SelectionSort(arr); // 10만개, 6300 ms
            //ArraySort.InsertionSort(arr); // 10만개, 4000 ms
            //ArraySort.RecursiveMergeSort(arr); // 1000 만개, 1800 ms / 중복이 거의 없을때 2200 ms
            //ArraySort.MergeSort(arr); // 1000 만개, 1750 ms / 중복이 거의 없을때 2200 ms
            ArraySort.RecursiveQuickSort(arr); // 1000만개 ??? / 중복이 거의 없을때 1800 ms
            //ArraySort.QuickSort(arr); // 1000 만개 146000 , 중복이 거의 없을때 1700 ms
            //ArraySort.HeapSort(arr); // 1000 만개 2500 ms 중복이 거의 없을때 3300 ms

            sw.Stop();
            Console.WriteLine($"ElapsedTime : {sw.ElapsedMilliseconds}");

            //for (int i = 0; i < arr.Length; i++)
            //{
            //    Console.Write($"{arr[i]},");
            //}
        }
    }
}
