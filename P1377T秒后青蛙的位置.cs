using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/4/23
    // simple BFS
    internal class P1377T秒后青蛙的位置
    {
        public double FrogPosition(int n, int[][] edges, int t, int target)
        {
            if (n == 1) return 1;
            var g = edges.UndirectedGraphNoLength();
            BitArray visit = new(n + 1);
            Queue<(int, int, double)> qu = new();
            qu.Enqueue((0, 1, 1.0));
            visit[1] = true;
            while (qu.Any())
            {
                (int time, int node, double prob) = qu.Dequeue();
                if (time >= t) return 0;
                int count = 0;
                if (g.ContainsKey(node)) count = g[node].Count(n => !visit[n]);
                if (count == 0) continue;
                double nextProb = prob / count;
                foreach (int next in g[node].Where(n => !visit[n]))
                {
                    if (next == target)
                    {
                        if (time + 1 == t || g[next].Count(n => !visit[n]) == 0) return nextProb;
                        else return 0;
                    }
                    visit[next] = true;
                    qu.Enqueue((time + 1, next, nextProb));
                }
            }
            return 0;
        }
    }
}
