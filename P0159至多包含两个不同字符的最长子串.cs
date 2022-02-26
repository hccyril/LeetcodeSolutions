using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /* 159. 至多包含两个不同字符的最长子串
     * 给定一个字符串 s ，找出 至多 包含两个不同字符的最长子串 t ，并返回该子串的长度。

    示例 1:
    输入: "eceba"
    输出: 3
    解释: t 是 "ece"，长度为3。

    示例 2:
    输入: "ccaabbb"
    输出: 5
    解释: t 是 "aabbb"，长度为5。


     * */
    // medium, plus, 2022/2/25 VIP
    // 灵活运用 ++x 与 x++ 的例子
    internal class P0159至多包含两个不同字符的最长子串
    {
        public int LengthOfLongestSubstringTwoDistinct(string s)
        {
            int max = 0, count = 0;
            int[] arr = new int[256];
            for (int l = 0, r = 0; r < s.Length; ++r)
            {
                if (arr[s[r]]++ == 0) ++count;
                while (count > 2)
                    if (--arr[s[l++]] == 0) --count;
                max = Math.Max(max, r - l + 1);
            }
            return max;
        }
    }
}
