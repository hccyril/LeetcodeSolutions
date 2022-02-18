using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/2/18
    // 欧拉回路 （贪心法）
    internal class P0753破解保险箱
    {
        public string CrackSafe(int n, int k)
        {
            int total = 1;
            for (int i = 0; i < n; ++i) total *= k;
            HashSet<string> hs = new();
            List<int> list = new();
            for (int i = 0; i < n; ++i) list.Add(0);
            StringBuilder ans = new();
            for (int i = 0; i < n - 1; ++i) ans.Append(0);
            while (hs.Count < total)
            {
                for (int i = k - 1; i >= 0; --i)
                {
                    list[list.Count - 1] = i;
                    string s = string.Join("", list);
                    if (hs.Add(s))
                    {
                        ans.Append(i);
                        break;
                    }
                }
                list.RemoveAt(0);
                list.Add(0);
            }
            return ans.ToString();
        }
    }
}
