using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /* 639. 解码方法 II (Hard, DP)
     * 
一条包含字母 A-Z 的消息通过以下的方式进行了编码：

'A' -> 1
'B' -> 2
...
'Z' -> 26
除了上述的条件以外，现在加密字符串可以包含字符 '*'了，字符'*'可以被当做1到9当中的任意一个数字。

给定一条包含数字和字符'*'的加密信息，请确定解码方法的总数。

同时，由于结果值可能会相当的大，所以你应当对109 + 7取模。（翻译者标注：此处取模主要是为了防止溢出）

示例 1 :

输入: "*"
输出: 9
解释: 加密的信息可以被解密为: "A", "B", "C", "D", "E", "F", "G", "H", "I".
示例 2 :

输入: "1*"
输出: 9 + 9 = 18（翻译者标注：这里1*可以分解为1,* 或者当做1*来处理，所以结果是9+9=18）
说明 :

输入的字符串长度范围是 [1, 105]。
输入的字符串只会包含字符 '*' 和 数字'0' - '9'。

来源：力扣（LeetCode）
链接：https://leetcode-cn.com/problems/decode-ways-ii
著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。
     * */
    class P0639解码方法II
    {
        public static void Run()
        {
            var p = new P0639解码方法II();
            Console.WriteLine(p.NumDecodings("*"));
            Console.WriteLine(p.NumDecodings("1*"));
            Console.WriteLine(p.NumDecodings("2*"));
        }

        public int NumDecodings(string s)
        {
            int dp0 = 1, dp1 = 1, dp = 0;
            for (int i = 0; i < s.Length; ++i)
            {
                dp = 0;
                int n;
                // 单位数1-9，与当前最末尾字符进行匹配
                for (n = 1; n < 10; ++n)
                {
                    if (CodeMatch(n, s[i]))
                        dp = dp.Add(dp1);
                }
                if (i > 0)
                {
                    // 双位数10-26，与当前最末尾2位字符进行匹配
                    for (n = 10; n <= 26; ++n)
                    {
                        if (CodeMatch(n, s[i - 1], s[i]))
                        {
                            dp = dp.Add(dp0);
                        }
                    }
                }
                dp0 = dp1; dp1 = dp;
            }
            return dp;
        }

        private bool CodeMatch(int n, char c)
        {   // *就匹配1-9，否则就两者完全一致
            if (c == '*') return n >= 1 && n <= 9;
            else return c - '0' == n;
        }

        private bool CodeMatch(int n, char c1, char c2)
        {   // 把个位和十位拆开分别匹配就行了
            int n1 = n / 10, n2 = n % 10;
            return CodeMatch(n1, c1) && CodeMatch(n2, c2);
        }
    }
}
