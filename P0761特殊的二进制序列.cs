using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/8/8 Daily
    // 2023/3/31 补做
    // 1-(, 0-), 这题等价于一道括号匹配题目
    internal class P0761特殊的二进制序列
    {
        public string MakeLargestSpecial(string s)
        {
            if (s.Length <= 2) return s;
            int c = 0, b = 0;
            List<string> ls = new();
            for (int i = 0; i < s.Length; ++i)
            {
                if (i == s.Length - 1 && !ls.Any())
                    return $"1{MakeLargestSpecial(s.Substring(1, s.Length - 2))}0";

                c += s[i] == '1' ? 1 : -1;
                if (c == 0)
                {
                    ls.Add(MakeLargestSpecial(s.Substring(b, i + 1 - b)));
                    b = i + 1;
                }
            }
            return string.Join("", ls.OrderByDescending(t => t));
        }
    }
}
