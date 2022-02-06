using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P1406石子游戏III
    {
        int[] stones;
        int[] sums;
        int?[] dp;

        int Play(int i)
        {
            if (i >= stones.Length) return 0;
            if (dp[i] == null)
            {
                dp[i] = stones[i] + sums[i + 1] - Play(i + 1);
                if (i + 1 < stones.Length)
                {
                    int choice2 = stones[i] + stones[i + 1] + sums[i + 2] - Play(i + 2);
                    if (choice2 > dp[i].Value) dp[i] = choice2;
                }
                if (i + 2 < stones.Length)
                {
                    int choice3 = stones[i] + stones[i + 1] + stones[i + 2] + sums[i + 3] - Play(i + 3);
                    if (choice3 > dp[i].Value) dp[i] = choice3;
                }
            }
            return dp[i].Value;
        }

        public string StoneGameIII(int[] stoneValue)
        {
            stones = stoneValue;
            sums = new int[stones.Length + 1];
            sums[stones.Length] = 0;
            for (int i = stones.Length - 1; i >= 0; --i)
                sums[i] = stones[i] + sums[i + 1];
            dp = new int?[stones.Length];
            int Alice = Play(0);
            int Bob = stones.Sum() - Alice;
            return Alice == Bob ? "Tie" : Alice > Bob ? "Alice" : "Bob";
        }
    }
}
