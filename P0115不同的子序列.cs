using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0115不同的子序列
    {
        public int NumDistinct(string s, string t)
        {
            int[,] dp = new int[s.Length, t.Length];
            for (int si = 0; si < s.Length; ++si)
            {
                for (int ti = 0; ti < t.Length; ++ti)
                {
                    if (ti > si || ti > 0 && dp[si - 1, ti - 1] == 0) break;
                    if (si > 0) dp[si, ti] = dp[si - 1, ti];
                    if (s[si] == t[ti])
                    {
                        if (ti == 0) dp[si, ti]++;
                        else dp[si, ti] += dp[si - 1, ti - 1];
                    }
                }
            }
            return dp[s.Length - 1, t.Length - 1];
        }

        internal static void Run()
        {
            Console.WriteLine(new P0115不同的子序列().NumDistinct("ddd", "dd"));
        }
    }
}
