using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard
    class P0629K个逆序对数组
    {
        // 动态规划
        int[,] DP(int n, int k)
        {
            int[,] dp = new int[n, k + 1];
            for (int i = 0; i < n; ++i) dp[i, 0] = 1;
            for (int j = 1; j <= k; ++j)
            {
                dp[0, j] = 0;
                for (int i = 1; i < n; ++i)
                {
                    dp[i, j] = dp[i - 1, j];
                    for (int c = 0; c < i; ++c)
                    {
                        int a = i - c; // count of new pairs
                        int r = j - a; // count of prev
                        if (r >= 0)
                            dp[i, j] = dp[i, j].Add(dp[i - 1, r]);
                    }
                }
            }
            return dp;
        }
        public int KInversePairs(int n, int k)
        {
            return DP(n, k)[n - 1, k];
        }

        internal static void Run()
        {
            var dp = new P0629K个逆序对数组().DP(3, 1);
            for (int i = 0; i < 2; ++i)
            {
                for (int j = 0; j < 3; ++j)
                    Console.Write(dp[j, i] + " ");
                Console.WriteLine();
            }
        }
    }
}
