using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/6/8
    // 数学
    internal class P0466统计重复个数
    {
        // ver2 - AC
        //                 开头          循环体            余数
        // s1             start        end-start         r1=(n1-start) % (end-start)
        // s2            cn[start]     c2-cnt[start]     cnt[start+r1]
        public int GetMaxRepetitions(string s1, int n1, string s2, int n2)
        {
            int n = Math.Min(n1, 10000);
            int[] cnt = new int[n + 1];
            Dictionary<int, int> dic = new();

            int i2 = 0, c1 = -1, c2 = 0;
            for (int i = 0; i < n; ++i)
            {
                int start_i2 = i2;
                if (i > 0 && c2 > 0 && i2 == 0)
                {
                    c1 = i;
                    break;
                }
                foreach (var ch in s1)
                    if (ch == s2[i2])
                    {
                        if (++i2 == s2.Length)
                        {
                            i2 = 0; ++c2;
                        }
                    }
                if (i2 == start_i2 && c2 == 0) return 0;
                cnt[i + 1] = c2;
                if (i2 != 0 && dic.ContainsKey(i2))
                {
                    int start = dic[i2], end = i + 1;
                    int rest = n1, t2 = 0;
                    rest -= start; t2 += cnt[start];
                    t2 += (rest / (end - start)) * (c2 - cnt[start]);
                    rest %= end - start;
                    if (rest > 0)
                        t2 += cnt[start + rest];
                    return t2 / n2;
                }
                else
                {
                    dic[i2] = i + 1;
                }
            }
            if (c1 < 0) return c2 / n2;
            return (n1 / c1 * c2 + cnt[n1 % c1]) / n2;
        }

        // ver1 WA
        public int GetMaxRepetitions_ver1_WA(string s1, int n1, string s2, int n2)
        {
            int n = Math.Min(n1, 10000);
            int[] cnt = new int[n + 1];
            int i2 = 0, c1 = -1, c2 = 0;
            for (int i = 0; i < n; ++i)
            {
                int start_i2 = i2;
                if (i > 0 && c2 > 0 && i2 == 0)
                {
                    c1 = i;
                    break;
                }
                foreach (var ch in s1)
                    if (ch == s2[i2])
                    {
                        if (++i2 == s2.Length)
                        {
                            i2 = 0; ++c2;
                        }
                    }
                if (i2 == start_i2 && c2 == 0) return 0;
                cnt[i + 1] = c2;
            }
            if (c1 < 0) return c2 / n2;
            return (n1 / c1 * c2 + cnt[n1 % c1]) / n2;
        }

        // ver1 WA exp 2468 my 246
        // "niconiconi" 99981 "nico" 81
    }
}
