using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // dijkstra / bfs 最短路径变体
    class P1293网格中的最短路径
    {
        int[][] G;
        Queue<int[]> qu = new Queue<int[]>();
        HashSet<int> hs = new HashSet<int>();

        int Key(int[] node) => Key(node[0], node[1], node[2]);
        int Key(int i, int j, int k) => (k << 12) | (i << 6) | j;

        void CheckIn(int[] node)
        {
            qu.Enqueue(node);
            hs.Add(Key(node));
        }

        static readonly int[] d1 = { 1, 0 }, d2 = { -1, 0 }, d3 = { 0, 1 }, d4 = { 0, -1 };
        static readonly int[][] darr = { d1, d2, d3, d4 };
        IEnumerable<int[]> FindPaths(int i, int j, int k)
        {
            foreach (var di in darr)
            {
                int ti = i + di[0], tj = j + di[1];
                if (ti >= 0 && ti < G.Length && tj >= 0 && tj < G[0].Length)
                    if (k >= G[ti][tj] && !hs.Contains(Key(ti, tj, k - G[ti][tj])))
                        yield return di;
            }
        }

        public int ShortestPath(int[][] grid, int k)
        {
            G = grid;
            if (grid[0][0] == 1 && k == 0) return -1;
            if (grid.Length == 1 && grid[0].Length == 1) return k >= grid[0][0] ? 0 : -1;
            int[] node = { 0, 0, k - grid[0][0], 0 };
            CheckIn(node);
            while (qu.Any())
            {
                node = qu.Dequeue();
                int i = node[0], j = node[1], l = node[3]; k = node[2];
                foreach (var di in FindPaths(i, j, k))
                {
                    int[] next = { i + di[0], j + di[1], k - G[i + di[0]][j + di[1]], l + 1 };
                    if (next[0] == G.Length - 1 && next[1] == G[0].Length - 1)
                        return next[3];
                    CheckIn(next);
                }
            }
            return -1;
        }
    }
}
