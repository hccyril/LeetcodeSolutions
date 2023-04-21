using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2023/3/5
    // dijkstra, 做完了才知道用官方题解全用并查集
    // rating 2765
    internal class P0803打砖块
    {
        public int[] HitBricks(int[][] grid, int[][] hits)
        {
            int[] ans = new int[hits.Length];
            SortedSet<(int, int, int)> hp = new();
            for (int i = 0; i < hits.Length; ++i)
                if (grid[hits[i][0]][hits[i][1]] == 1)
                    grid[hits[i][0]][hits[i][1]] = ~i;
            for (int i = 0; i < grid.Length; ++i)
                for (int j = 0; j < grid[i].Length; ++j)
                    if (grid[i][j] == 1)
                        grid[i][j] = ~0xFFFFFF;
            for (int j = 0; j < grid[0].Length; ++j)
                if (grid[0][j] != 0)
                    hp.Add((grid[0][j], 0, j));
            while (hp.Any())
            {
                (int p, int i, int j) = hp.Min(); hp.Remove(hp.Min);
                if (p.WillDrop())
                {
                    int ps = ~p;
                    if ((i, j) != (hits[ps][0], hits[ps][1]))
                        ++ans[ps];
                }
                foreach ((int ni, int nj) in grid.FourDir(i, j))
                {
                    if (grid[ni][nj] < 0)
                    {
                        if (grid[ni][nj] < p)
                            grid[ni][nj] = p;
                        hp.Add((grid[ni][nj], ni, nj));
                    }
                }
                grid[i][j] = 2;
            }
            return ans;
        }
    }

    internal static class P0803Extensions
    {
        public static bool WillDrop(this int p) => p > -40001;
    }
}
