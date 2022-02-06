using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium
    // 区间动态规划
    internal class P1690石子游戏VII
    {
        int[] stones;
        int?[,] dp;
        int Play(int i, int j, int sum)
        {
            if (i >= j) return 0;
            if (dp[i, j] != null) return dp[i, j].Value;
            int si = sum - stones[i], sj = sum - stones[j];
            return (dp[i, j] = Math.Max(si - Play(i + 1, j, si), sj - Play(i, j - 1, sj))).Value;
        }
        public int StoneGameVII(int[] stones)
        {
            this.stones = stones;
            dp = new int?[stones.Length, stones.Length];
            int sum = stones.Sum();
            return Play(0, stones.Length - 1, sum);
        }

        // 贪心法：WA
        //public int StoneGameVII(int[] stones)
        //{
        //    int sum = stones.Sum();
        //    int i = 0, j = stones.Length - 1, score = 0;
        //    bool alice = false;
        //    while (i < j)
        //    {
        //        if (stones[i] - stones[i + 1] < stones[j] - stones[j - 1])
        //            sum -= stones[i++];
        //        else
        //            sum -= stones[j--];
        //        alice = !alice;
        //        if (alice) score += sum;
        //        else score -= sum;
        //    }
        //    return score;
        //}
    }
}
