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
        }
    }
}
