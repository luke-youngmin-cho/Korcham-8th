namespace Algorithms.cs
{
    public class Fibonacci
    {
        public Fibonacci()
        {
            Get(1);
        }

        public Fibonacci(int n)
        {
            Get(n);
        }

        public int this[int n]
        {
            get
            {
                if (n < 0)
                    throw new IndexOutOfRangeException();

                if (n > _limit)
                    return Get(n);

                return _cache[n];
            }
        }


        private int[] _cache;
        private int _limit;

        public int Get(int n)
        {
            _limit = n;
            _cache = new int[n + 1];
            return F(n);
        }

        // F(n) = F(n - 1) + F(n - 2)
        private int F(int n)
        {
            // 1이 수열의 최솟값 이므로 ( 0번째 인덱스 값은 0, 1번째 인덱스 값은 1)
            if (n <= 1)
                return n;

            // 캐싱된 값이 있으면 계산안하고 바로 리턴
            if (_cache[n] > 0)
                return _cache[n];

            _cache[n] = F(n - 1) + F(n - 2);
            return _cache[n];
        }
    }
}
