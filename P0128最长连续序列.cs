using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium 2021/08/20
    // 后来做了721后才发现这题用的技术跟 并查集 是同一原理
    class P0128最长连续序列
    {
        int max = 0;
        class Counter
        {
            public int Start { get => Origin.start; set => Origin.start = value; }
            public int End { get => Origin.end; set => Origin.end = value; }
            int start;
            int end;
            public Counter next { private get; set; }
            public Counter Origin => next?.Origin ?? this;
            public int Count => End - Start + 1;
        }
        public int LongestConsecutive(int[] nums)
        {
            Dictionary<int, Counter> dic = new Dictionary<int, Counter>();
            foreach (var n in nums)
            {
                if (!dic.ContainsKey(n))
                {
                    Counter c = null;
                    if (dic.ContainsKey(n - 1))
                    {
                        c = dic[n - 1];
                        c.End = n;
                    }
                    if (dic.ContainsKey(n + 1))
                    {
                        if (c == null)
                        {
                            c = dic[n + 1];
                            c.Start = n;
                        }
                        else
                        {
                            var next = dic[n + 1];
                            next.Start = c.Start;
                            c.Origin.next = next;
                        }
                    }
                    if (c == null) c = new Counter
                    {
                        Start = n,
                        End = n
                    };
                    dic.Add(n, c);
                    if (c.Count > max) max = c.Count;
                }
            }
            return max;
        }
    }
}
