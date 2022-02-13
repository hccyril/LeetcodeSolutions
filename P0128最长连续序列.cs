using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium 
    // ver1: 2021/8/20, OOP, 后来做了721后才发现这题用的技术跟 并查集 是同一原理
    // ver2: 2022/2/10 用 真 并查集 重做
    class P0128最长连续序列
    {
        // ver2 2022/2/10
        public int LongestConsecutive(int[] nums)
        {
            if (nums?.Any() != true) return 0; // 又来这套！！！
            UnionFind uni = new(nums.Length);

            Dictionary<int, int> dic = new();
            for (int i = 0; i < nums.Length; ++i) dic[nums[i]] = i;
            // 不能这样写，nums[i]有重复值 = =|||
            //var dic = Enumerable.Range(0, nums.Length).ToDictionary(i => nums[i]);

            foreach (int i in Enumerable.Range(0, nums.Length))
                if (dic.ContainsKey(nums[i] - 1))
                    uni.Union(dic[nums[i] - 1], i);
            return Enumerable.Range(0, nums.Length).Select(i => nums[i] - nums[uni.Find(i)] + 1).Max();
        }

        // ver1
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
        public int LongestConsecutive_ver1(int[] nums)
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
