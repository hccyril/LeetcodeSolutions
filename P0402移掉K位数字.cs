using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/2/18 US Daily
    internal class P0402移掉K位数字
    {
        const string 相似题目 = nameof(P0321拼接最大数);

        // ver2 - 未实现，参考评论：
        // 思路，从左到右，找第一个比后面大的字符，删除，清零，k次扫描。

        // ver1 - AC but slow
        string BuildNum(string num, int i, int count)
        {
            if (count <= 0) return "";
            for (int j = i + 1; num[i] > '0' && j < num.Length - count + 1; ++j)
                if (num[j] < num[i])
                    i = j;
            return num[i] + BuildNum(num, i + 1, count - 1);
        }

        public string RemoveKdigits(string num, int k, int i = 0)
        {
            string ans = BuildNum(num, 0, num.Length - i - k);
            ans = ans.TrimStart('0');
            return ans == "" ? "0" : ans;
        }
    }
}
