using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/11/14 Daily
    // 难度分19xx，但是做了一上午，错了好几次！！！
    // n=30, should be backtrack
    internal class P0805数组的均值分割
    {
        // 方法二，换一种思路，枚举所有的和
        // 终于过了，把两个HashSet都改成了Dictionary
        // small: 维护组成n这个和至少要多少个数，否则判断不出全部数加在一起的情况
        // big: 维护还剩下多少个可以用，否则会出现x既算进n里又在big里供比较的“分身”情况
        public bool SplitArraySameAverage(int[] nums)
        {
            if (nums.Length <= 1) return false;
            int avg = nums.Sum();
            for (int i = 0; i < nums.Length; ++i)
                nums[i] = nums[i] * nums.Length - avg;
            Array.Sort(nums);
            if (nums.Any(t => t == 0)) return true;
            Dictionary<int, int> big = new(), small = new();
            foreach (var x in nums.Where(t => t > 0))
                big[x] = big.ContainsKey(x) ? big[x] + 1 : 1;
            foreach (var x in nums)
            {
                if (x > 0) --big[x];
                foreach (var s in small.Keys.Append(0).ToArray())
                {
                    int n = s + x, c = s == 0 ? 1 : small[s] + 1;
                    if (n < 0)
                    {
                        if (big.ContainsKey(-n) && big[-n] > 0 && c < nums.Length - 1) return true;
                        if (!small.ContainsKey(n) || small[n] > c)
                            small[n] = c;
                    }
                }
            }
            return false;
        }

        // 方法一，优化了两次还是超时TLE
        public bool SplitArraySameAverage_TLE(int[] nums)
        {
            if (nums.Length == 1) return false;
            Array.Sort(nums);
            Rsi avg = new(nums.Sum(), nums.Length);

            // 优化2：保证比平均小的数占少数
            int cnt = nums.Count(t => avg > t);
            if (cnt > (nums.Length >> 1))
                return SplitArraySameAverage(nums.Select(t => avg.Sum - t).ToArray());

            bool Dfs(int i, Rsi r)
            {
                if (i == nums.Length - 1 && r.N == nums.Length - 1) return false; // 不能全选
                for (int j = i; j < nums.Length; ++j)
                {
                    if (j > i && nums[j] == nums[i]) continue; // 优化1：前面没选时后面不能选重复的
                    Rsi t = r + nums[j];
                    int comp = t.CompareTo(avg);
                    if (comp == 0) return true;
                    else if (comp < 0)
                    {
                        if (Dfs(j + 1, t)) return true;
                    }
                    else break;
                }
                return false;
            }
            return Dfs(0, new(0, 0));
        }

        internal static void Run()
        {

            // 方法1：超时（18s）
            //int[] input = { 33, 86, 88, 78, 21, 76, 19, 20, 88, 76, 10, 25, 37, 97, 58, 89, 65, 59, 98, 57, 50, 30, 58, 5, 61, 72, 23, 6 };

            // 方法二 WA
            int[] input = { 17, 3, 7, 12, 1 };
            Console.WriteLine("ans: {0}", new P0805数组的均值分割().SplitArraySameAverage(input));
        }
    }

    // 有理数
    class Rsi : IComparable<Rsi>
    {
        int a, b;

        public int Sum { get; private set; }

        public int N { get; private set; }

        public Rsi(int sum, int n)
        {
            this.Sum = sum; this.N = n;
            Re();
        }

        public void Add(int x) => Add(x, 1);

        public void Add(int x, int c)
        {
            Sum += x; N += c;
            Re();
        }

        public static Rsi operator+(Rsi a, int x) => new Rsi(a.Sum + x, a.N + 1);

        public static bool operator <(Rsi a, int x) => a.a < x * a.b;
        public static bool operator >(Rsi a, int x) => a.a > x * a.b;

        //public static bool operator ==(Rsi x, Rsi y) => x.CompareTo(y) == 0;

        //public static bool operator !=(Rsi x, Rsi y) => x.CompareTo(y) != 0;

        void Re()
        {
            a = Sum; b = N;
            if (a > 0 && b > 1)
            {
                int g = Gcd(a, b);
                a /= g; b /= g;
            }
        }

        static int Gcd(int a, int b) => b != 0 ? Gcd(b, a % b) : a;

        public int CompareTo(Rsi y) => (a * y.b).CompareTo(y.a * b);
    }

    // test cases:
    // 优化1：ans=false
    //      [60,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30]
    // 优化2：
    //      [33,86,88,78,21,76,19,20,88,76,10,25,37,97,58,89,65,59,98,57,50,30,58,5,61,72,23,6]

    /** 完整test cases
[1,2,3,4,5,6,7,8]
[3,1]
[17,3,7,12,1]
[60,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30]
[33,86,88,78,21,76,19,20,88,76,10,25,37,97,58,89,65,59,98,57,50,30,58,5,61,72,23,6]
     * */
}
