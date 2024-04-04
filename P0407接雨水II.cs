using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, P0042接雨水的延伸(单调栈？) // 2021/11/03 Daily
// 2024/2/17 finished
// 不需要BFS，直接排序然后依次处理就好，但是并查集的处理有不少细节要注意
class P0407接雨水II 
{
    public int TrapRainWater(int[][] heightMap)
    {
        int m = heightMap.Length, n = heightMap[0].Length;
        List<(int, int, int)> li = new(m * n);
        for (int i = 0; i < m; ++i)
            for (int j = 0; j < n; ++j)
                li.Add((heightMap[i][j], i, j));
        li.Sort();
        UnionFind uni = new(m * n);
        (int, int)[] a = new (int, int)[m * n]; // (h, count)
        int water = 0;
        foreach ((int h, int i, int j) in li)
        {
            int id = i * n + j;
            a[uni[id]] = heightMap.IsEdge(i, j) ? (-1, 1) : (h, 1);
            foreach ((int ni, int nj) in heightMap.FourDir(i, j))
            {
                int nid = ni * n + nj;
                if (a[uni[nid]].Item2 == 0) continue; // 对方节点还未访问时，不做任何处理
                if (h >= heightMap[ni][nj] && !uni.Check(id, nid))
                {
                    (int h1, int c1) = a[uni[id]];
                    (int h0, int c0) = a[uni[nid]];
                    uni.Union(nid, id);
                    if (h0 >= 0) // h0==-1表示贴着边线，接不到水
                    {
                        water += (h - h0) * c0;
                        a[uni[id]] = (h, c1 + c0); // WA原因：当h1<0时这个赋值是不正确的
                    }

                    // ** WA fix **
                    if (h0 < 0 || h1 < 0)
                        a[uni[id]] = (-1, 1);
                    //else
                    //{
                    //    a[uni[id]] = (-1, 1);
                    //}
                }
            }
            //if (heightMap.IsEdge(i, j) || heightMap.FourDir(i, j).Any(t => a[uni[t.ni * n + t.nj]].Item1 < 0))
            //    a[uni[id]] = (-1, 1);
        }
        return water;
    } 


    internal static void Run()
    {
        // sample test case (ans=4)
        //string s = "[[1,4,3,1,3,2],[3,2,1,3,2,4],[2,3,3,2,3,1]]";

        // WA: my=14, expected=12
        var s = "[[5,8,7,7],[5,2,1,5],[7,1,7,1],[8,9,6,9],[9,8,9,9]]";

        var sln = new P0407接雨水II();
        var input = s.ToTestInput<int[][]>();
        Console.WriteLine("ans=" + sln.TrapRainWater(input));
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
