using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0076最小覆盖子串
    {
        class Counter
        {
            int[] arr = new int[52];
            public int Count { get; private set; }
            int Index(char c)
            {
                if (c >= 'a') return c - 71; // c - 'a' + 26
                else return c - 65;
            }
            public void Add(char c)
            {
                arr[Index(c)]++;
                Count++;
            }

            public int CountChar(char c)
            {
                int i = Index(c);
                if (arr[i] > 0) Count--;
                arr[i]--;
                return Count;
            }

            public int UncountChar(char c)
            {
                int i = Index(c);
                arr[i]++;
                if (arr[i] > 0) Count++;
                return Count;
            }
        }

        public string MinWindow(string s, string t)
        {
            int[] ans = { -1, -1 };
            int min = 0xFFFFFF;
            Counter c = new Counter();
            foreach (var tc in t) c.Add(tc);
            int head = 0, tail = 0;
            while (head < s.Length)
            {
                while (c.Count > 0 && head < s.Length) c.CountChar(s[head++]);
                while (c.Count == 0)
                {
                    if (head - tail < min)
                    {
                        min = head - tail;
                        ans = new int[] { tail, head - tail };
                    }
                    c.UncountChar(s[tail++]);
                }
            }
            return ans[0] == -1 ? "" : s.Substring(ans[0], ans[1]);
        }
    }
}
