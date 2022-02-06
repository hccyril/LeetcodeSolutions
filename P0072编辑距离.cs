using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0072编辑距离
    {
        public int MinDistance(string word1, string word2)
        {
            int m = word1.Length, n = word2.Length;
            int[,] dp = new int[m + 1, n + 1];
            for (int i = 0; i <= m; ++i)
            {
                for (int j = 0; j <= n; ++j)
                {
                    if (i == 0) dp[i, j] = j;
                    else if (j == 0) dp[i, j] = i;
                    else // i > 0 && j > 0
                    {
                        // 假设1：i是多出来的 d1 = dp[i - 1, j] + 1;
                        // 假设2：j是多出来的 d2 = dp[i, j - 1] + 1;
                        var d12 = Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1);
                        // 假设3：i应该与j匹配
                        var d3 = (word1[i - 1] == word2[j - 1] ? 0 : 1) + dp[i - 1, j - 1];

                        // 三者取最小值
                        dp[i, j] = Math.Min(d12, d3);
                    }
                }
            }
            return dp[m, n];
        }
    }
}
