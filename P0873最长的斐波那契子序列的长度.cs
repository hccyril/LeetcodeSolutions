using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/2/7 也算是dp
    // 《剑指 Offer II 093. 最长斐波那契数列》：https://leetcode-cn.com/problems/Q91FMA/
    internal class P0873最长的斐波那契子序列的长度
    {
        public int LenLongestFibSubseq(int[] arr)
        {
            int maxLen = 0;
            Dictionary<int, int> dic = new();
            for (int i = 0; i < arr.Length; ++i) dic[arr[i]] = i;
            int[][] dp = new int[arr.Length][];
            for (int i = 1; i < arr.Length; ++i)
            {
                dp[i] = new int[i];
                for (int j = 0; j < i; ++j)
                {
                    int a = arr[i] - arr[j]; // 要注意必须 a < arr[j] 才行
                    if (a < arr[j] && dic.ContainsKey(a))
                    {
                        int k = dic[a];
                        maxLen = Math.Max(maxLen, dp[i][j] = dp[j][k] + 1);
                    }
                    else dp[i][j] = 2;
                }
            }
            return maxLen >= 3 ? maxLen : 0;
        }

        internal static void Run()
        {
            int[] input = { 1, 2, 3, 4, 5, 6, 7, 8 };
            int result = new P0873最长的斐波那契子序列的长度().LenLongestFibSubseq(input);
            Console.WriteLine("LenLongestFibSubseq=" + result);
        }
    }
}
