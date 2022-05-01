using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/4/27 US Daily
    // 并查集
    internal class P1202交换字符串中的元素
    {
        class CharStruct
        {
            List<char> list = new();
            int ind = -1;
            public void Add(char c) => list.Add(c);
            public char Next()
            {
                if (ind < 0)
                {
                    list.Sort();
                    ind = 0;
                }
                return list[ind++];
            }
        }
        public string SmallestStringWithSwaps(string s, IList<IList<int>> pairs)
        {
            UnionFind uni = new(s.Length);
            foreach (var p in pairs)
                uni.Union(p[0], p[1]);
            Dictionary<int, CharStruct> dic = new();
            for (int i = 0; i < s.Length; ++i)
            {
                int gp = uni.Find(i);
                dic.TryAdd(gp, new());
                var item = dic[gp];
                item.Add(s[i]);
            }
            return string.Join("", Enumerable.Range(0, s.Length).Select(i => dic[uni.Find(i)].Next()));
        }
    }
}
