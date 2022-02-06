using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, dijkstra // TODO interested 2021/11/20
    class P1514概率最大的路径
    {
        public double MaxProbability(int n, int[][] edges, double[] succProb, int start, int end)
        {
            // init
            List<int>[] dests = new List<int>[n];
            Dictionary<int, double> path = new(); // key=start << 14 | end
            for (int i = 0; i < edges.Length; ++i)
            {
                int s = edges[i][0], t = edges[i][1];
                if (dests[s] == null) dests[s] = new List<int>();
                dests[s].Add(t);
                path.Add((s << 14) | t, succProb[i]);
                if (dests[t] == null) dests[t] = new List<int>();
                dests[t].Add(s);
                path.Add((t << 14) | s, succProb[i]);
            }
            Heap<(int s, double p)> heap = new((a, b) => a.p > b.p);
            heap.Push((start, 1.0));
            HashSet<int> hs = new();

            // dijkstra
            while (heap.Any())
            {
                (int s, double p) = heap.Pop();
                if (s == end) return p;
                if (hs.Add(s))
                {
                    if (dests[s] == null) continue;
                    foreach (var t in dests[s]) if (!hs.Contains(t))
                            heap.Push((t, p * path[(s << 14) | t]));
                }
            }
            return 0;
        }
    }
}
