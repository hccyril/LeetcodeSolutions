using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // 这题可以跟第44题对比看 //

    /*
     * 给你一个字符串 s 和一个字符规律 p，请你来实现一个支持 '.' 和 '*' 的正则表达式匹配。

'.' 匹配任意单个字符
'*' 匹配零个或多个前面的那一个元素
所谓匹配，是要涵盖 整个 字符串 s的，而不是部分字符串。

 
示例 1：

输入：s = "aa" p = "a"
输出：false
解释："a" 无法匹配 "aa" 整个字符串。
示例 2:

输入：s = "aa" p = "a*"
输出：true
解释：因为 '*' 代表可以匹配零个或多个前面的那一个元素, 在这里前面的元素就是 'a'。因此，字符串 "aa" 可被视为 'a' 重复了一次。
示例 3：

输入：s = "ab" p = ".*"
输出：true
解释：".*" 表示可匹配零个或多个（'*'）任意字符（'.'）。
示例 4：

输入：s = "aab" p = "c*a*b"
输出：true
解释：因为 '*' 表示零个或多个，这里 'c' 为 0 个, 'a' 被重复一次。因此可以匹配字符串 "aab"。
示例 5：

输入：s = "mississippi" p = "mis*is*p*."
输出：false
 

提示：

0 <= s.length <= 20
0 <= p.length <= 30
s 可能为空，且只包含从 a-z 的小写字母。
p 可能为空，且只包含从 a-z 的小写字母，以及字符 . 和 *。
保证每次出现字符 * 时，前面都匹配到有效的字符

来源：力扣（LeetCode）
链接：https://leetcode-cn.com/problems/regular-expression-matching
著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。
     * */
    class P0010正则表达式匹配
    {
        class Exp
        {
            public char c;
            public char x; // the char when *
            public Exp(char c, char x = ' ')
            {
                if (x == '*')
                {
                    this.c = '*';
                    this.x = c;
                }
                else
                    this.c = c;
            }

            public bool Match(char c)
            {
                if (this.c == '*') return MatchChar(c, this.x);
                return MatchChar(c, this.c);
            }

            public static bool MatchChar(char c, char p)
            {
                return p == '.' || p == c;
            }
        }

        List<Exp> pattern = new List<Exp>();
        string word;

        /// <summary>
        /// 在英文版里提交回溯就通过了(1.8s)，但是中文版里超时
        /// 因此用了个字典避免重复计算，最后运行时间变为80ms
        /// </summary>
        HashSet<int> wrongKeys = new HashSet<int>();
        int MakeKey(int s, int p)
        {
            return s * 1000 + p;
        }

        bool Wrong(int s, int p)
        {
            wrongKeys.Add(MakeKey(s, p));
            return false;
        }

        bool Rec(int i_s, int i_p)
        {
            if (wrongKeys.Contains(MakeKey(i_s, i_p))) return false;
            if (i_s >= word.Length && i_p >= pattern.Count)
                return true;
            if (i_p >= pattern.Count)
                return Wrong(i_s, i_p);

            Exp p = pattern[i_p];

            // before match, try if current * can skip
            if (p.c == '*' && Rec(i_s, i_p + 1))
                return true;

            // try match
            if (i_s >= word.Length) return Wrong(i_s, i_p);
            char c = word[i_s];
            bool match = p.Match(c);
            if (!match) return Wrong(i_s, i_p);

            // if match == true
            if (Rec(i_s + 1, i_p + 1)) return true;
            if (p.c == '*')
                if (Rec(i_s + 1, i_p)) return true;

            // all tried fail
            return Wrong(i_s, i_p);
        }

        public bool IsMatch(string s, string p)
        {
            word = s;
            for (int i = 0; i < p.Length; ++i)
            {
                if (p[i] == '*') continue;
                if (i < p.Length - 1 && p[i + 1] == '*')
                    pattern.Add(new Exp(p[i], '*'));
                else
                    pattern.Add(new Exp(p[i]));
            }

            return Rec(0, 0);
        }
    }
}
