using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // 这题可以跟第10题对比看 //

    /*
     * 给定一个字符串 (s) 和一个字符模式 (p) ，实现一个支持 '?' 和 '*' 的通配符匹配。

'?' 可以匹配任何单个字符。
'*' 可以匹配任意字符串（包括空字符串）。
两个字符串完全匹配才算匹配成功。

说明:

s 可能为空，且只包含从 a-z 的小写字母。
p 可能为空，且只包含从 a-z 的小写字母，以及字符 ? 和 *。

来源：力扣（LeetCode）
链接：https://leetcode-cn.com/problems/wildcard-matching
著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。
     * */
    class P0044通配符匹配
    {
        HashSet<string> hs = new HashSet<string>();
        bool Match(string s, int si, string p, int pi)
        {
            string key = $"{si},{pi}";
            if (hs.Contains(key)) return false;
            bool b = true;
            try
            {
                if (si >= s.Length && (pi >= p.Length || pi == p.Length - 1 && p[pi] == '*'))
                    return b = true;
                else if (si >= s.Length || pi >= p.Length)
                    return b = false;

                char sc = s[si];
                char pc = p[pi];

                if (pc == '*')
                {
                    return b = Match(s, si + 1, p, pi + 1) || Match(s, si, p, pi + 1) || Match(s, si + 1, p, pi);
                }
                else if (pc == '?')
                    return b = Match(s, si + 1, p, pi + 1);
                else
                    return b = sc == pc ? Match(s, si + 1, p, pi + 1) : false;
            }
            finally
            {
                if (!b)
                {
                    hs.Add(key);
                }
            }

        }

        public bool IsMatch(string s, string p)
        {
            while (p.Contains("**")) p = p.Replace("**", "*");
            if (p == "") return s == "";
            if (s == "") return p == "*";

            return Match(s, 0, p, 0);
        }
    }
}
