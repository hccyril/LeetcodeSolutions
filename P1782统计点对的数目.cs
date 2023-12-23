using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2023/8/23 Daily
    // rank: 2457
    // 统计问题，关键是根据入参数量级找到哪里应该排序和二分
    internal class P1782统计点对的数目
    {
        public int[] CountPairs(int n, int[][] edges, int[] queries)
        {
            var cnt = new Counter<int>();
            foreach (var ed in edges)
            {
                if (ed[0] > ed[1]) (ed[0], ed[1]) = (ed[1], ed[0]);
                int a = ed[0], b = ed[1];
                ++cnt[a]; ++cnt[b];
            }

            bool Comp(int a, int b) => (cnt[a] - cnt[b]) switch
            {
                > 0 => true,
                0 => a > b,
                _ => false
            };

            List<(int, int)>[] la = new List<(int, int)>[n + 1];
            for (int i = 0; i < la.Length; ++i)
                la[i] = new();

            foreach (var ed in edges.OrderBy(e => e[0]).ThenBy(e => e[1]))
            {
                int a = ed[0], b = ed[1];
                if (!Comp(a, b)) (a, b) = (b, a);
                if (la[a].Any() && la[a][^1].Item1 == b)
                    la[a][^1] = (la[a][^1].Item1, la[a][^1].Item2 + 1);
                else
                    la[a].Add((b, 1));
            }

            var sort = Enumerable.Range(1, n).Select(i => (cnt[i], i)).ToArray();
            Array.Sort(sort);

            int Ans(int q)
            {
                int c = 0;
                for (int i = sort.Length - 1; i > 0 && sort[i].Item1 + sort[i - 1].Item1 > q; --i)
                {
                    (int s, int t) = sort[i];
                    c += i - ~Array.BinarySearch(sort, (q - s + 1, 0));
                    foreach ((int b, int d) in la[t])
                        if (cnt[t] + cnt[b] > q && cnt[t] + cnt[b] - d <= q)
                            --c;
                }
                return c;
            }

            return queries.Select(q => Ans(q)).ToArray();
        }

        internal static void Run()
        {
            int n = 8;
            var edges = "[[3,2],[5,7],[4,2],[6,2],[5,6],[7,5],[5,2],[5,1],[5,4],[7,2],[3,5],[6,2],[6,4],[3,4],[8,6],[4,8],[1,4],[2,7],[7,8]]".ToTestInput<int[][]>();
            //int[] qrs = { 7, 2, 3, 3, 4, 1, 4, 0, 2, 1, 1, 4, 0, 7, 10, 0, 0 };
            int[] qrs = { 10 }; // ans=5, my=6
            var sln = new P1782统计点对的数目();
            Console.WriteLine("ans=" + sln.CountPairs(n, edges, qrs).FirstOrDefault());
        }
    }
}
