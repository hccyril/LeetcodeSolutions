using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard
    // 区间动态规划
    internal class P1563石子游戏V
    {
        int N;
        int[,] dp;
        int[] sums;
        int Sum(int i, int j) => sums[j] - (i > 0 ? sums[i - 1] : 0);
        int Play(int i, int j)
        {
            if (i == j) return 0;
            else if (dp[i, j] > 0) return dp[i, j];

            for (int k = i; k < j; k++)
            {
                int left = Sum(i, k), right = Sum(k + 1, j);
                if (left == right)
                    dp[i, j] = Math.Max(dp[i, j], Math.Max(left + Play(i, k), right + Play(k + 1, j)));
                else if (left > right)
                    dp[i, j] = Math.Max(dp[i, j], right + Play(k + 1, j));
                else
                    dp[i, j] = Math.Max(dp[i, j], left + Play(i, k));
            }

            return dp[i, j];
        }
        public int StoneGameV(int[] stoneValue)
        {
            N = stoneValue.Length; if (N == 1) return 0;
            dp = new int[N, N];
            for (int i = 1; i < N; i++) stoneValue[i] += stoneValue[i - 1];
            sums = stoneValue;
            return Play(0, N - 1);
        }
    }
}
