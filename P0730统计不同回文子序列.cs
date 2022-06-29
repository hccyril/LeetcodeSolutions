using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/6/10 Daily
    // DP，做了一整天 (愁 = =)
    internal class P0730统计不同回文子序列
    {
        public int CountPalindromicSubsequences(string s)
        {
            if (s.Length <= 2) return s.Length; // 最后提交时没加这句，又贡献了一次WA!

            // 后序优化：改成Dictionary<(int,int,int), int>可以节省一部分空间
            int[,,] dp = new int[s.Length, s.Length, 4];
            for (int i = 0; i < s.Length; ++i)
                for (int j = 0; j < s.Length; ++j)
                    for (int k = 0; k < 4; ++k)
                        dp[i, j, k] = -1;

            int DpDfs(int start, int end, int c = -1)
            {
                /*/DEBUG*/Console.WriteLine("DpDfs: {0} {1} {2}", start, end, c);

                if (start >= end) return start == end && (c < 0 || s[start] - 'a' == c) ? 1 : 0;
                //if (start >= end) return start == end && s[start] - 'a' == c ? 1 : 0; // 再次贡献了一个WA

                if (c < 0)
                {
                    int ans = 0;
                    for (int i = 0; i < 4; ++i)
                    {
                        ans = ans.Add(DpDfs(start, end, i));
                        /*/DEBUG*/Console.WriteLine("Ans: ({0} {1} {2}) = {3}", start, end, i, dp[start, end, i]);
                    }
                    /*/DEBUG*/Console.WriteLine("Sum: ({0} {1}) = {2}", start, end, ans);
                    return ans;
                }
                else 
                {
                    if (dp[start, end, c] >= 0) return dp[start, end, c];
                    char ch = (char)('a' + c);
                    if (s[start] == ch && s[end] == ch)
                    {
                        return dp[start, end, c] = DpDfs(start + 1, end - 1).Add(2); // aa 及 a
                    }
                    else
                    {
                        (int si, int ei) = (s.IndexOf(ch, start, end - start + 1), s.LastIndexOf(ch, end, end - start + 1));
                        if (si >= 0 && ei >= start)
                            return dp[start, end, c] = DpDfs(si, ei, c);
                        return dp[start, end, c] = 0;
                    }
                }
            }

            return DpDfs(0, s.Length - 1);
        }

        internal static void Run()
        {
            //输出：104860361
            //解释：共有 3104860382 个不同的非空回文子序列，104860361 对 109 + 7 取模后的值。
            string s = "abcdabcdabcdabcdabcdabcdabcdabcddcbadcbadcbadcbadcbadcbadcbadcba";

            var sln = new P0730统计不同回文子序列();
            string s1 = "bccb";
            string s2 = "aaa"; // 又WA了！答案是3，我的是2
            int ans = sln.CountPalindromicSubsequences(s2);
            Console.WriteLine(nameof(P0730统计不同回文子序列) + ": " + ans);
        }
    }
}
