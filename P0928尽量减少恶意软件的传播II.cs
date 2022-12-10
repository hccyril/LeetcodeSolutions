using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/11/14
    // 并查集
    internal class P0928尽量减少恶意软件的传播II
    {
        public int MinMalwareSpread(int[][] graph, int[] initial)
        {
            int min_count = int.MaxValue, min_i = -1;
            foreach (int ban in initial)
            {
                UnionFind uni = new(graph.Length);
                for (int i = 0; i < graph.Length; ++i)
                    for (int j = i + 1; j < graph.Length; ++j)
                        if (graph[i][j] == 1 && i != ban && j != ban)
                            uni.Union(i, j);

                var di = Enumerable.Range(0, graph.Length)
                    .GroupBy(i => uni.Find(i))
                    .ToDictionary(g => g.Key, g => g.Count());

                int infected = initial.Where(i => i != ban)
                    .GroupBy(i => uni.Find(i))
                    .Select(g => di[g.Key])
                    .Sum();

                if (infected < min_count)
                {
                    min_count = infected;
                    min_i = ban;
                }
            }
            return min_i;
        }
    }
}
