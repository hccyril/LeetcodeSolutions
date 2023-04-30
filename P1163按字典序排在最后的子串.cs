using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2023/4/24 Daily
    // rating只有18xx，也就是人均会用KMP??
    internal class P1163按字典序排在最后的子串
    {
        public string LastSubstring(string s)
        {
            var next = s.Kmp();
            int i = 0, j = -1;
            for (int k = 1; k < s.Length; ++k)
            {
                int comp = s[k].CompareTo(s[i + j + 1]);
                //Console.WriteLine($"i={i} j={j} k={k} (comp {s[k]} {s[i + j + 1]})={comp}");
                if (comp > 0)
                {
                    i = k - j - 1;
                    if (j >= 0)
                    {
                        j = next[j];
                        --k;
                    }
                }
                else if (comp < 0)
                {
                    if (j >= 0)
                    {
                        j = next[j];
                        --k;
                    }
                }
                else // equal
                {
                    ++j;
                }
            }
            return s.Substring(i);
        }

        // test case:
        // cacacb
        // ans="cb"
    }
}
