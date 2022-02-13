using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/2/12 Daily
    // 回溯
    internal class P1020飞地的数量
    {
        IEnumerable<(int i, int j)> Edges(int m, int n)
        {
            for (int i = 0; i < m; ++i)
                if (i == 0 || i == m - 1)
                {
                    for (int j = 0; j < n; ++j) yield return (i, j);
                }
                else
                {
                    yield return (i, 0);
                    yield return (i, n - 1);
                }
        }
        IEnumerable<(int ni, int nj)> FourDir(int[][] mx, int i, int j)
        {
            if (i > 0) yield return (i - 1, j);
            if (i < mx.Length - 1) yield return (i + 1, j);
            if (j > 0) yield return (i, j - 1);
            if (j < mx[i].Length - 1) yield return (i, j + 1);
        }
        void Dfs(int[][] grid, int i, int j)
        {
            grid[i][j] = 2;
            foreach ((int ni, int nj) in FourDir(grid, i, j))
            {
                if (grid[ni][nj] == 1) Dfs(grid, ni, nj);
            }
        }
        public int NumEnclaves(int[][] grid)
        {
            int m = grid.Length, n = grid[0].Length;
            foreach ((int i, int j) in Edges(m, n))
            {
                if (grid[i][j] == 1)
                {
                    Dfs(grid, i, j);
                }
            }
            return grid.Sum(r => r.Count(c => c == 1));
        }
    }
}
