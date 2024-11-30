using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// 2024/9/29 140双周赛-D
// DP+前后缀分解+KMP+ZFunction (Z函数范例题)
// 关于Z函数参考：https://oi-wiki.org/string/z-func/
internal class P3303第一个几乎相等子字符串的下标
{
    public int MinStartingIndex(string s, string pattern)
    {
        int[] dp1 = s.SearchAllKmp(pattern),
            dp2 = string.Join("", s.Reverse()).SearchAllKmp(string.Join("", pattern.Reverse()));
        dp2 = dp2.Reverse().ToArray();
        for (int i = 0; i <= s.Length - pattern.Length; ++i)
            if (dp1[i] + dp2[i + pattern.Length - 1] >= pattern.Length - 1)
                return i;
        return -1;
    }
}
