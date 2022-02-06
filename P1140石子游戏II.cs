using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P1140石子游戏II // 记忆化搜索=动态规划
    {
        int[] sums;
        int?[,] dp;
        int Play(int i, int M)
        {
            if (sums[i] == 0) return 0;
            if (dp[i, M] == null)
            {
                dp[i, M] = 0;
                for (int X = 1; i + X < sums.Length && X <= (M << 1)/*(2 * M)*/; ++X)
                {
                    int stone = sums[i] - Play(i + X, Math.Max(X, M));
                    if (stone > dp[i, M].Value) dp[i, M] = stone;
                }
            }
            return dp[i, M].Value;
        }
        public int StoneGameII(int[] piles)
        {
            sums = new int[piles.Length + 1];
            sums[sums.Length - 1] = 0;
            for (int i = piles.Length - 1; i >= 0; --i)
                sums[i] = piles[i] + sums[i + 1];
            dp = new int?[piles.Length, piles.Length + 1];
            return Play(0, 1);
        }
    }
}
