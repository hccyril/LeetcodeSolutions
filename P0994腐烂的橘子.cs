using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, BFS
    class P0994腐烂的橘子
    {
        public int OrangesRotting(int[][] grid)
        {
            int[] d1 = { 1, 0 }, d2 = { 0, 1 }, d3 = { -1, 0 }, d4 = { 0, -1 };
            int[][] directs = { d1, d2, d3, d4 };
            Queue<int> qu = new Queue<int>();
            for (int i = 0; i < grid.Length; ++i)
                for (int j = 0; j < grid[i].Length; ++j)
                    if (grid[i][j] == 2)
                        qu.Enqueue(i * 10 + j);
            while (qu.Any())
            {
                int i = qu.Dequeue(), j = i % 10; i /= 10;
                int m = grid[i][j] + 1;
                foreach (var di in directs)
                {
                    int i1 = i + di[0], j1 = j + di[1];
                    if (i1 >= 0 && i1 < grid.Length && j1 >= 0 && j1 < grid[0].Length && grid[i1][j1] == 1)
                    {
                        grid[i1][j1] = m;
                        qu.Enqueue(i1 * 10 + j1);
                    }
                }
            }
            // 装X失败：return grid.Any(r => r.Any(t => t == 1)) ? -1 : grid.Select(r => r.Max()).Max() - 2;    
            if (grid.Any(r => r.Any(t => t == 1))) return -1;
            int max = grid.Select(r => r.Max()).Max();
            return max > 1 ? max - 2 : max;
        }
    }
}
