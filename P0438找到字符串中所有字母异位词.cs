using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/2/2 US daily
    // 值得一做的字符串题目
    internal class P0438找到字符串中所有字母异位词
    {
        class Finder
        {
            const int complete = (1 << 26) - 1;
            public bool Found() => map == complete;
            int map;
            int[] arr = new int[26], match = new int[26];
            public Finder(string p)
            {
                foreach (char c in p)
                    ++match[c - 'a'];
                for (int i = 0; i < 26; ++i)
                    if (match[i] == 0)
                        map |= 1 << i;
            }
            public Finder Add(char c)
            {
                SetBit(c, 1);
                return this;
            }

            public Finder Remove(char c)
            {
                SetBit(c, -1);
                return this;
            }

            private void SetBit(char c, int inc)
            {
                int i = c - 'a';
                arr[i] += inc;
                int bit = 1 << i;
                if (arr[i] == match[i]) map |= bit;
                else if ((map & bit) != 0)
                    map ^= bit;
            }
        }
        public IList<int> FindAnagrams(string s, string p)
        {
            if (p.Length > s.Length) return new List<int>();
            IList<int> ans = new List<int>();
            Finder f = new(p);
            for (int i = 0; i < p.Length; ++i) f.Add(s[i]);
            if (f.Found()) ans.Add(0);
            for (int i = 1, j = p.Length; j < s.Length; ++i, ++j)
            {
                f.Remove(s[i - 1]).Add(s[j]);
                if (f.Found()) ans.Add(i);
            }
            return ans;
        }
    }
}
