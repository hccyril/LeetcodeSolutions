using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2024/3/22 Daily
// rating 2581
// BFS超时，后采用SortedSet优化，构建单调队列
internal class P2617网格图中最少访问的格子数
{
    const int MAX = 0xFFFFFF;

    class MonoQueue
    {
        SortedSet<(int, int)> ss = new(); // (steps, -index)

        private void PopLeft()
        {
            if (ss.Any())
            {
                (int s, int i) = ss.Min; i = -i;
                ss.Remove(ss.Min);
                while (ss.Any())
                {
                    (int t, int j) = ss.Min; j = -j;
                    if (j > i && t > s) break;
                    else ss.Remove(ss.Min);
                }
            }
        }
        internal int GetMinSteps(int index)
        {
            if (ss.Any())
            {
                (int s, int i) = ss.Min;
                while (-i < index)
                {
                    PopLeft();
                    if (ss.Any()) (s, i) = ss.Min;
                    else return MAX;
                }
                return s;
            }
            return MAX;
        }

        internal void Append(int s, int i)
        {
            if (s > 0 && s < MAX)
                ss.Add((s, -i));
        }
    }

    // version 2, AC (1163ms)
    public int MinimumVisitedCells(int[][] grid)
    {
        int m = grid.Length, n = grid[0].Length;
        if (m == 1 && n == 1) return 1;
        if (grid[0][0] == 0) return -1;
        int[,] a = new int[m, n]; a[0, 0] = 1;

        MonoQueue[] cs = new MonoQueue[n];

        // init first row
        int p = 2, ps = grid[0][0], ns = 0;
        for (int j = 0; j < n; ++j) cs[j] = new();
        cs[0].Append(2, grid[0][0]);
        for (int j = 1; j < n; ++j)
        {
            if (j > ps)
            {
                (p, ps, ns) = (p + 1, ns, 0);
            }

            if (j <= ps)
            {
                a[0, j] = p;
                ns = Math.Max(ns, Math.Min(n - 1, j + grid[0][j]));
                cs[j].Append(a[0, j] + 1, grid[0][j]);
            }
            else
            {
                break;
            }
        }
        p = 2; ps = grid[0][0]; ns = 0;

        for (int i = 1; i < m; ++i)
        {
            if (i > ps)
            {
                (p, ps, ns) = (p + 1, ns, 0);
            }
            if (i <= ps)
            {
                a[i, 0] = p;
                ns = Math.Max(ns, Math.Min(m - 1, i + grid[i][0]));
            }

            MonoQueue ra = new();
            if (a[i, 0] > 0)
                ra.Append(a[i, 0] + 1, grid[i][0]);

            for (int j = 1; j < n; ++j)
            {
                a[i, j] = Math.Min(ra.GetMinSteps(j), cs[j].GetMinSteps(i));
                ra.Append(a[i, j] + 1, j + grid[i][j]);
                cs[j].Append(a[i, j] + 1, i + grid[i][j]);
            }
        }

        int ans = a[m - 1, n - 1];
        return ans > 0 && ans < MAX ? ans : -1;
    }

    // BFS, US accept (5932ms); CN TLE
    public int MinimumVisitedCells_TLE(int[][] grid)
    {
        int m = grid.Length, n = grid[0].Length;
        if (m == 1 && n == 1) return 1;
        int[,] a = new int[m, n]; a[0, 0] = 1;
        Queue<(int, int)> qu = new();
        qu.Enqueue((0, 0));
        while (qu.Any())
        {
            (int i, int j) = qu.Dequeue();
            int r = Math.Min(i + grid[i][j], m - 1);
            if (r == m - 1 && j == n - 1)
                return a[i, j] + 1;
            for (int k = i + 1; k <= r; ++k)
                if (a[k, j] == 0)
                {
                    a[k, j] = a[i, j] + 1;
                    qu.Enqueue((k, j));
                }
            r = Math.Min(j + grid[i][j], n - 1);
            if (r == n - 1 && i == m - 1)
                return a[i, j] + 1;
            for (int k = j + 1; k <= r; ++k)
                if (a[i, k] == 0)
                {
                    a[i, k] = a[i, j] + 1;
                    qu.Enqueue((i, k));
                }
        }
        return -1;
    }

    internal static void Run()
    {
        var sln = new P2617网格图中最少访问的格子数();
        var grid = "[[1,0,4,5],[6,6,3,0]]".ToTestInput<int[][]>();
        Console.WriteLine(sln.MinimumVisitedCells(grid));
    }
}
