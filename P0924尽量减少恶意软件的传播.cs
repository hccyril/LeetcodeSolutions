using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/4/29
    // rank: 1869
    // 并查集
    internal class P0924尽量减少恶意软件的传播
    {
        public int MinMalwareSpread(int[][] graph, int[] initial)
        {
            UnionFind uni = new(graph.Length);
            for (int i = 0; i < graph.Length - 1; i++)
                for (int j = i + 1; j < graph.Length; j++)
                    if (graph[i][j] == 1)
                        uni.Union(i, j);
            var dic = Enumerable.Range(0, graph.Length)
                .GroupBy(i => uni.Find(i))
                .ToDictionary(g => g.Key, g => g.Count());
            var qry = initial
                .GroupBy(i => uni.Find(i))
                .Where(g => g.Count() == 1)
                .OrderByDescending(g => dic[g.Key])
                .ThenBy(g => g.First())
                .Select(g => g.First());
            return qry.Any() ? qry.First() : initial.Min();
        }
    }
}
