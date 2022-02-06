using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, P0042接雨水的延伸(单调栈？) // TODO 2021/11/03 Daily
    class P0407接雨水II : P0042接雨水
    {
        public int TrapRainWater(int[][] heightMap)
        {
            throw new NotImplementedException();
        } 

        // 官方题解2-BFS
        public int TrapRainWater_OfficialDemo2(int[][] heightMap)
        {
            int m = heightMap.Length;
            int n = heightMap[0].Length;
            int[] dirs = { -1, 0, 1, 0, -1 };
            int maxHeight = 0;

            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    maxHeight = Math.Max(maxHeight, heightMap[i][j]);
                }
            }
            int[,] water = new int[m, n];
            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    water[i, j] = maxHeight;
                }
            }

            Queue<int[]> qu = new Queue<int[]>();
            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    if (i == 0 || i == m - 1 || j == 0 || j == n - 1)
                    {
                        if (water[i, j] > heightMap[i][j])
                        {
                            water[i, j] = heightMap[i][j];
                            qu.Enqueue(new int[] { i, j });
                        }
                    }
                }
            }

            while (qu.Count > 0)
            {
                int[] curr = qu.Dequeue();
                int x = curr[0];
                int y = curr[1];
                for (int i = 0; i < 4; ++i)
                {
                    int nx = x + dirs[i], ny = y + dirs[i + 1];
                    if (nx < 0 || nx >= m || ny < 0 || ny >= n)
                    {
                        continue;
                    }
                    if (water[x, y] < water[nx, ny] && water[nx, ny] > heightMap[nx][ny])
                    {
                        water[nx, ny] = Math.Max(water[x, y], heightMap[nx][ny]);
                        qu.Enqueue(new int[] { nx, ny });
                    }
                }
            }

            int res = 0;
            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    res += water[i, j] - heightMap[i][j];
                }
            }
            return res;
        }

    }
}
