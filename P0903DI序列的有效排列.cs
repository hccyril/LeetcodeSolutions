using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, rank: 2433, 2022/7/17 
    // DP: dp[i][j]: 0..i的排列，且最后一个数字为次序j的时候的排列总数
    // 解释：因为调换以后数字会变，但次序不会，例如[0,1,3,4,5] 次序2的数就是a[2] == 3
    internal class P0903DI序列的有效排列
    {
        public const string 受启发题目 = "629. K个逆序对数组 https://leetcode.cn/problems/k-inverse-pairs-array/";

        public int NumPermsDISequence(string s)
        {
            int[] dp0 = new int[s.Length + 1], dp1 = new int[s.Length + 1];
            dp0[0] = 1;
            for (int i = 1; i <= s.Length; ++i)
            {
                Array.Fill(dp1, 0);
                var DI = s[i - 1];
                for (int j = 0; j < i; ++j)
                {
                    // D: 当前为j，下一个要比j小，所以可以选0..j（留意j也可以的）
                    // I: 当前为j，下一个要比j大，所以可以选j+1..i
                    var r = DI == 'D' ? Enumerable.Range(0, j + 1) : Enumerable.Range(j + 1, i - j);
                    foreach (int k in r)
                        dp1[k] = dp1[k].Add(dp0[j]);
                }
                (dp0, dp1) = (dp1, dp0);
            }
            int sum = 0;
            foreach (int n in dp0) sum = sum.Add(n);
            return sum;
        }
    }
}
