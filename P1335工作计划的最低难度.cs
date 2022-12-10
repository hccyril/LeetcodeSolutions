using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/10/16 US Daily
    // DP 标准的二维DP，但是改了好几版才过= =
    internal class P1335工作计划的最低难度
    {
        public int MinDifficulty(int[] jobDifficulty, int d)
        {
            int n = jobDifficulty.Length;
            if (d > n) return -1;

            //int[] ps = new int[n];
            //for (int i = 0; i < n; i++) 
            //    ps[i] = jobDifficulty[i] + (i > 0 ? ps[i - 1] : 0);
            //int rs(int a, int b) => ps[b] - (a > 0 ? ps[a - 1] : 0);
            Dictionary<(int, int), int> rd = new();
            for (int i = 0; i < n; ++i)
            {
                int ma = jobDifficulty[i];
                for (int j = i; j < n; ++j)
                    rd[(i, j)] = ma = Math.Max(ma, jobDifficulty[j]);
            }

            int[,] dp = new int[n, d + 1];
            for (int i = 0; i < n; ++i)
                for (int j = 0; j <= d; ++j)
                    dp[i, j] = -1;
            for (int i = 0; i < n; ++i)
            {
                for (int j = 1; j <= i + 1 && j <= d; ++j)
                {
                    if (j == 1) dp[i, j] = rd[(0, i)];
                    else for (int k = i; k >= j - 1; --k)
                    {
                        int c = rd[(k, i)] + (k > 0 && j > 1 ? dp[k - 1, j - 1] : 0);
                        dp[i, j] = dp[i, j] >= 0 ? Math.Min(dp[i, j], c) : c;
                    }
                }
            }
            return dp[n - 1, d];
        }

        internal static void Run()
        {
            int[] a = { 6, 5, 4, 3, 2, 1 };
            int d = 2;
            Console.WriteLine(new P1335工作计划的最低难度().MinDifficulty(a, d));
        }
    }
}
