using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 回溯
    internal class P1219黄金矿工
    {
        int max = 0;
        IEnumerable<(int ni, int nj)> Move(int[][] grid, int i, int j)
        {
            if (i > 0) yield return (i - 1, j);
            if (i < grid.Length - 1) yield return (i + 1, j);
            if (j > 0) yield return (i, j - 1);
            if (j < grid[i].Length - 1) yield return (i, j + 1);
        }
        void Dfs(int[][] grid, int i, int j, int collected)
        {
            int val = grid[i][j];
            max = Math.Max(max, collected += val);
            grid[i][j] = 0;
            foreach ((int ni, int nj) in Move(grid, i, j))
                if (grid[ni][nj] > 0)
                    Dfs(grid, ni, nj, collected);
            grid[i][j] = val;
        }
        public int GetMaximumGold(int[][] grid)
        {
            max = 0;
            for (int i = 0; i < grid.Length; ++i)
                for (int j = 0; j < grid[i].Length; ++j)
                    if (grid[i][j] > 0)
                        Dfs(grid, i, j, 0);
            return max;
        }
    }
}
