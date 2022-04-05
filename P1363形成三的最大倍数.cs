using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/3/30
    // WC177-D, rank: 1823
    // 不是DP，而是贪心法
    internal class P1363形成三的最大倍数
    {
        // ver2: 针对ver1 WA的地方进行了优化，AC
        public string LargestMultipleOfThree(int[] digits)
        {
            Array.Sort(digits);
            Array.Reverse(digits);
            int r1 = 0, r2 = 0;
            foreach (var d in digits)
                if (d % 3 == 1) ++r1;
                else if (d % 3 == 2) ++r2;
            int c1 = r1 % 3, c2 = r2 % 3;
            int min = Math.Min(c1, c2);
            c1 -= min; c2 -= min;
            if (c1 > 0)
            {
                if (c1 == 2 && r2 >= 2) --r2;
                else r1 -= c1;
            }
            else if (c2 > 0)
            {
                if (c2 == 2 && r1 >= 2) --r1;
                else r2 -= c2;
            }
            StringBuilder sb = new();
            foreach (var d in digits)
            {
                if (d % 3 == 1)
                {
                    if (r1-- > 0) sb.Append(d);
                }
                else if (d % 3 == 2)
                {
                    if (r2-- > 0) sb.Append(d);
                }
                else sb.Append(d);
            }
            string s = sb.ToString();
            if (s == "") return s;
            s = s.TrimStart('0');
            if (s == "") s = "0";
            return s;
        }

        // ver1: WA:
        // input: [2,2,1,1,1]
        // my ans: "111"
        // expected: "2211"
        public string LargestMultipleOfThree_WA(int[] digits)
        {
            Array.Sort(digits);
            Array.Reverse(digits);
            int r1 = 0, r2 = 0;
            foreach (var d in digits)
                if (d % 3 == 1) ++r1;
                else if (d % 3 == 2) ++r2;
            int c1 = r1 % 3, c2 = r2 % 3;
            int min = Math.Min(c1, c2);
            c1 -= min; c2 -= min;
            if (c1 > 0) r1 -= c1;
            else if (c2 > 0) r2 -= c2;
            StringBuilder sb = new();
            foreach (var d in digits)
            {
                if (d % 3 == 1)
                {
                    if (r1-- > 0) sb.Append(d);
                }
                else if (d % 3 == 2)
                {
                    if (r2-- > 0) sb.Append(d);
                }
                else sb.Append(d);
            }
            string s = sb.ToString();
            if (s == "") return s;
            s = s.TrimStart('0');
            if (s == "") s = "0";
            return s;
        }
    }
}
