using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // 动态规划
    // 这题有优化的空间：从右下往左上倒推会简单很多
    class P0174地下城游戏
    {
        class HealthStruct : IComparable<HealthStruct>
        {
            public int max, hp;

            public int CompareTo(HealthStruct other)
                => hp == other.hp ? other.max - max : hp - other.hp;
        }
        public int CalculateMinimumHP(int[][] dungeon)
        {
            List<HealthStruct>[] dp = new List<HealthStruct>[dungeon[0].Length];
            for (int i = 0; i < dungeon.Length; ++i)
                for (int j = 0; j < dungeon[i].Length; ++j)
                {
                    int c = dungeon[i][j];
                    if (i == 0 && j == 0)
                        dp[j] = new List<HealthStruct>() { new HealthStruct { max = c, hp = Math.Max(1, 1 - c) } };
                    else if (dp[j] == null)
                        dp[j] = new List<HealthStruct>();

                    if (i > 0)
                        foreach (var hd in dp[j])
                        {
                            hd.max += c;
                            hd.hp = Math.Max(Math.Max(1, 1 - hd.max), hd.hp);
                        }
                    if (j > 0)
                        foreach (var hd1 in dp[j - 1])
                        {
                            var hd = new HealthStruct();
                            hd.max = hd1.max + c;
                            hd.hp = Math.Max(Math.Max(1, 1 - hd.max), hd1.hp);
                            dp[j].Add(hd);
                        }
                    // adjust
                    dp[j].Sort();
                    for (int k = dp[j].Count - 2; k >= 0; --k)
                        while (k < dp[j].Count - 1 && dp[j][k + 1].max <= dp[j][k].max)
                            dp[j].RemoveAt(k + 1);
                }
            return dp.Last().First().hp;
        }
    }
}
