using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2023/2/10 US Daily
    // BFS
    internal class P1162地图分析
    {
        public int MaxDistance(int[][] grid)
        {
            int maxd = 0;
            int[,] di = new int[grid.Length, grid[0].Length];
            Queue<(int, int)> qu = new();
            for (int i = 0; i < grid.Length; ++i)
                for (int j = 0; j < grid[i].Length; ++j)
                    if (grid[i][j] == 1)
                    {
                        di[i, j] = 0;
                        qu.Enqueue((i, j));
                    }
                    else di[i, j] = -1;
            while (qu.Any())
            {
                (int i, int j) = qu.Dequeue();
                foreach ((int ni, int nj) in grid.FourDir(i, j))
                    if (di[ni, nj] < 0)
                    {
                        maxd = Math.Max(maxd, di[ni, nj] = di[i, j] + 1);
                        qu.Enqueue((ni, nj));
                    }
            }
            return maxd;
        }
    }
}
