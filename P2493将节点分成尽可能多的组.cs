using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2023/2/4
    // rank: 2415
    // 并查集找连通图，对每个连通图枚举每个顶点BFS
    internal class P2493将节点分成尽可能多的组
    {
        public int MagnificentSets(int n, int[][] edges)
        {
            // pre-process
            foreach (var e in edges)
            {
                e[0]--; e[1]--;
            }

            var dg = edges.UndirectedGraphNoLength(n);
            var uni = new UnionFind(n);
            foreach (var e in edges)
                uni.Union(e[0], e[1]);
            var gps = Enumerable.Range(0, n).GroupBy(i => uni.Find(i));

            int Calc(int[] li)
            {
                int ans = 0;
                foreach (var si in li)
                {
                    // BFS
                    Queue<int> qu = new();
                    Dictionary<int, int> di = new();
                    di[si] = 1;
                    qu.Enqueue(si);
                    while (qu.Any())
                    {
                        int i = qu.Dequeue();
                        if (di[i] > ans) ans = di[i];
                        foreach (int j in dg[i])
                        {
                            if (di.ContainsKey(j))
                            {
                                if (di[j] == di[i])
                                    return -1;
                            }
                            else
                            {
                                di[j] = di[i] + 1;
                                qu.Enqueue(j);
                            }
                        }
                    }
                }
                return ans;
            }

            int sm = 0;
            foreach (var g in gps)
            {
                int gn = Calc(g.ToArray());
                if (gn < 0) return -1;
                sm += gn;
            }
            return sm;
        }

        internal static void Run()
        {
            int n = 5;
            var instr = "[[1,2],[1,3],[2,4],[3,5],[2,5]]";
            var edges = JsonConvert.DeserializeObject<int[][]>(instr);
            var sln = new P2493将节点分成尽可能多的组();
            int ans = sln.MagnificentSets(n, edges);
            Console.WriteLine("2493 MagnificentSets ans = " + ans);
        }
    }
}
