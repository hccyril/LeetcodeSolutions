using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P1143最长公共子序列
    {
        public int LongestCommonSubsequence(string text1, string text2)
        {
            int[,] dp = new int[text1.Length, text2.Length];
            for (int i = 0; i < text1.Length; ++i)
                for (int j = 0; j < text2.Length; ++j)
                {
                    int cnt = text1[i] == text2[j] ? 1 : 0;
                    if (cnt > dp[i, j]) dp[i, j] = cnt;
                    if (i > 0 && dp[i - 1, j] > dp[i, j]) dp[i, j] = dp[i - 1, j];
                    if (j > 0 && dp[i, j - 1] > dp[i, j]) dp[i, j] = dp[i, j - 1];
                    if (i > 0 && j > 0 && dp[i - 1, j - 1] + cnt > dp[i, j]) dp[i, j] = dp[i - 1, j - 1] + cnt;
                }
            return dp[text1.Length - 1, text2.Length - 1];
        }
    }
}
