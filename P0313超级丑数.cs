using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, DP 
    // ver2 2022/2/5 HFS (Heap-BFS)
    // ver1 2021/08/09 用自己的方法还没做出来
    class P0313超级丑数
    {
        #region ver2 - HFS
        public int NthSuperUglyNumber(int n, int[] primes)
        {
            if (n == 1) return 1;
            Array.Sort(primes);
            Heap<(int num, int i)> hp = new((a, b) => a.num < b.num);
            hp.Push((primes[0], 0));
            int count = 1;
            while (hp.Any())
            {
                (int num, int i) = hp.Pop();
                if (++count == n) return num;
                if (num <= int.MaxValue / primes[i]) hp.Push((num * primes[i], i));
                else if (i < primes.Length - 1)
                {
                    num /= primes[i++];
                    if (num <= int.MaxValue / primes[i]) hp.Push((num * primes[i], i));
                }
            }
            return -1;
        }
        #endregion 

        #region ver1 - DP - TODO
        const int MAX = 2147483647;
        int N;
        int[] Primes;
        int[,] dp, pow;
        bool Overflow_i(int index, int y) => Pow_i(index, y) < 0;
        //{
        //    int n = MAX;
        //    //for (int i = 0; i < y; ++i) n /= x;
        //    //return n > 0;
        //}
        int Pow_i(int index, int y)
        {
            if (pow[index, y] != 0) return pow[index, y];
            if (y < 0) return 1;
            if (y == 0) return pow[index, y] = 1;
            int prime = Primes[index];
            int prev = Pow_i(index, y - 1);
            if (prev < 0 || MAX / prime < prev) return pow[index, y] = -1;
            return pow[index, y] = prime * prev;
        }
        // dp[i,k] = 小于等于(Primes[i])^k的丑数有多少个
        int Count(int i, int pow)
        {
            if (dp[i, pow] > 0) return dp[i, pow];
            if (pow <= 0) return dp[i, 0] = 1;

            int prime = Primes[i];
            int count = 0;

            // continue from here (2021/11/17 做了很久又突然不想做了)

            return dp[i, pow] = count;
        }
        // 从第i个prime开始，顺序为n（0-index）的丑数是多少
        int Calc(int i, int index)
        {
            if (index < 0) return 1;
            int n = index + 1;
            int pow = 1;
            for (; !Overflow_i(i, pow) && Count(i, pow) <= n; ++pow) ;
            return Pow_i(i, pow - 1) * Calc(i + 1, index - Count(i, pow - 1));
        }
        public int NthSuperUglyNumber_ver1(int n, int[] primes)
        {
            N = n;
            Array.Reverse(primes); Primes = primes;
            dp = new int[primes.Length, 32];
            pow = new int[primes.Length, 32];
            return Calc(0, n - 1);
        }
        #endregion 

        ///// <summary>
        ///// 官方题解
        ///// </summary>
        //int Official(int n, int[] primes)
        //{
        //    int[] dp = new int[n + 1];
        //    dp[1] = 1;
        //    int m = primes.Length;
        //    int[] pointers = new int[m];
        //    Array.Fill(pointers, 1);
        //    for (int i = 2; i <= n; i++)
        //    {
        //        int[] nums = new int[m];
        //        int minNum = int.MaxValue;
        //        for (int j = 0; j < m; j++)
        //        {
        //            nums[j] = dp[pointers[j]] * primes[j];
        //            minNum = Math.Min(minNum, nums[j]);
        //        }
        //        dp[i] = minNum;
        //        for (int j = 0; j < m; j++)
        //        {
        //            if (minNum == nums[j])
        //            {
        //                pointers[j]++;
        //            }
        //        }
        //    }
        //    return dp[n];
        //}
    }
}
