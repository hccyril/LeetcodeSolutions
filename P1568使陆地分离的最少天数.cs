using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/4/16
    // rank: 2209
    // Tarjan算法
    internal class P1568使陆地分离的最少天数
    {
        // 可以证明最多只需要消除两块陆地（找到一个边角位然后把相邻的陆地删了）
        // 另外如果一开始就不连通，返回0
        // 所以题目变成了搜索有没1的情况，这种情况有两种可能：
        // （1）有一块陆地只有一个边跟大陆相连，那删除掉相邻的陆地就把这块陆地分割出来了（注意当总面积=2时不成立，已特殊处理）
        // （2）有一块“关键”陆地作为两块陆地的连接点，其邻边数必为2，搜索能否从一边走到另一边即可验证

        // ver2: Tarjan算法
        // grid标记：2：访问过；3：割点
        int Tarjan(int[][] grid)
        {
            for (int i = 0; i < grid.Length; i++)
                for (int j = 0; j < grid[i].Length; j++)
                    if (grid[i][j] > 0)
                    {
                        dfn = new(); low = new();
                        Dfs(grid, i, j);

#if DEBUG
                        Console.WriteLine("dfn:");
                        for (int ii = 0; ii < grid.Length; ++ii)
                        {
                            Console.WriteLine();
                            for (int jj = 0; jj < grid[ii].Length; ++jj)
                            {
                                int v = grid[ii][jj] == 0 ? 0 : dfn[(ii, jj)];
                                Console.Write(" " + v);
                            }
                        }

                        Console.WriteLine();
                        Console.WriteLine("low:");
                        for (int ii = 0; ii < grid.Length; ++ii)
                        {
                            Console.WriteLine();
                            for (int jj = 0; jj < grid[ii].Length; ++jj)
                            {
                                int v = grid[ii][jj] == 0 ? 0 : low[(ii, jj)];
                                Console.Write(" " + v);
                            }
                        }

                        Console.WriteLine();
                        Console.WriteLine("grid:");
                        for (int ii = 0; ii < grid.Length; ++ii)
                        {
                            Console.WriteLine();
                            for (int jj = 0; jj < grid[ii].Length; ++jj)
                            {
                                Console.Write(" " + grid[ii][jj]);
                            }
                        }
                        Console.WriteLine();
#endif

                        return grid.Any(r => r.Any(c => c > 2)) ? 1 : 2;
                    }
            return 0;
        }
        Dictionary<(int, int), int> dfn, low;
        int timestamp = 0;
        int Dfs(int[][] grid, int i, int j)
        {
            grid[i][j] = 2;
            low[(i, j)] = dfn[(i, j)] = ++timestamp;
            int branch = 0;
            foreach ((int ni, int nj) in grid.FourDir(i, j))
            {
                if (grid[ni][nj] == 1)
                {
                    ++branch;
                    int low_ret = Dfs(grid, ni, nj);
                    // tarjan定义：当子节点的low值比当前节点要大，表示沿着子节点的路径回不到祖先，当前节点为割点
                    // 注意：这个判断不适用于根节点（root i.e. dfn==1)
                    if (dfn[(i, j)] > 1 && low_ret >= dfn[(i, j)]) grid[i][j] = 3;

                    // 回溯时更新low的值
                    low[(i, j)] = Math.Min(low[(i, j)], low_ret);
                }
                else if (grid[ni][nj] > 1)
                {
                    // 目标节点已经访问过，更新low值
                    low[(i, j)] = Math.Min(low[(i, j)], dfn[(ni, nj)]);
                }
            }
            // 对于根节点，是否为割点的判断方式是有没有多个回溯分支
            if (dfn[(i, j)] == 1 && branch > 1)
                grid[i][j] = 3;

            return low[(i, j)];
        }

        // ver1: 优化的BFS
        int Bfs(int[][] grid)
        {
            for (int i = 0; i < grid.Length; i++)
                for (int j = 0; j < grid[i].Length; j++)
                {
                    // 0：水面不处理；2：已搜索过的双边陆地；这两种情况均跳过
                    if (grid[i][j] == 0) continue;
                    int conns = grid.FourDir(i, j).Count(p => grid[p.ni][p.nj] > 0);
                    if (conns == 1) return 1;
                    else if (conns == 2 && grid[i][j] != 2)
                    {
                        // 可以证明此时该点的两个连接点都为1，不会是2
                        grid[i][j] = 2;
                        var cp = grid.FourDir(i, j).Where(p => grid[p.ni][p.nj] > 0).ToArray();
                        // 将起点延伸到单边路径的尽头
                        while (grid.FourDir(cp[0].ni, cp[0].nj).Count(p => grid[p.ni][p.nj] > 0) == 2)
                        {
                            grid[cp[0].ni][cp[0].nj] = 2;
                            var qry = grid.FourDir(cp[0].ni, cp[0].nj).Where(p => grid[p.ni][p.nj] == 1);
                            if (qry.Any())
                            {
                                cp[0] = qry.First();
                            }
                            else break;
                        }
                        // 只有一种特殊情况：整个大陆是一个环，这样所有点的度都是2
                        // 错！可能有其他情况，总之不要直接返回
                        if (cp[0] == cp[1]) continue; // return 2;
                        // 将终点延伸到单边路径的尽头
                        while (grid.FourDir(cp[1].ni, cp[1].nj).Count(p => grid[p.ni][p.nj] > 0) == 2)
                        {
                            grid[cp[1].ni][cp[1].nj] = 2;
                            var qry = grid.FourDir(cp[1].ni, cp[1].nj).Where(p => grid[p.ni][p.nj] == 1);
                            if (qry.Any())
                            {
                                cp[1] = qry.First();
                            }
                            else break;
                        }

                        HashSet<(int, int)> visited = new();
                        Queue<(int, int)> qu = new();
                        visited.Add((i, j)); // 要加上当前点，表示搜索路径不能从这个点通过
                        visited.Add(cp[0]);
                        qu.Enqueue(cp[0]);
                        bool connected = false;
                        while (qu.Any() && !connected)
                        {
                            (int si, int sj) = qu.Dequeue();
                            foreach ((int ni, int nj) in grid.FourDir(si, sj))
                            {
                                if (grid[ni][nj] > 0 && !visited.Contains((ni, nj)))
                                {
                                    if ((ni, nj) == cp[1])
                                    {
                                        connected = true;
                                        break;
                                    }
                                    visited.Add((ni, nj));
                                    qu.Enqueue((ni, nj));
                                }
                            }
                        }
                        if (!connected) return 1;
                    }
                }
            return 2;
        }

        public int MinDays(int[][] grid)
        {
            if (!grid.IsConnected()) return 0;
            // 以下保证grid是连通的

            // 1的陆地的数量
            int area = grid.Sum(r => r.Sum());

            // 简单情况直接处理
            if (area == 1) return 1;
            else if (area == 2) return 2;
            else if (area == 3) return 1;

            // ver1: BFS
            //return Bfs(grid);
            // ver2: Tarjan
            return Tarjan(grid);
        }

        internal static void Run()
        {
            var sln = new P1568使陆地分离的最少天数();
            // case1: expected: 1
            var input = new int[][]
                {new int[] {1,1,0,1,1},
                 new int[] {1,1,1,1,1},
                 new int[] {1,1,0,1,1},
                 new int[] {1,1,1,1,1}}; //     new int[] {1,1,0,1,1}}; // 中间那个0或1分别是两个case

            // case2: expected: 1
            //var input = new int[][]
            //    {new int[] {1,1,1,1,1,1},
            //     new int[] {1,1,1,1,1,1},
            //     new int[] {0,1,1,1,1,1},
            //     new int[] {1,1,1,0,1,0},
            //     new int[] {1,1,0,0,1,1}};
            Console.WriteLine(sln.MinDays(input));
        }
    }

    public static partial class SolutionExtensions
    {
        // 是否全连通，有多于一块陆地或者全是水面返回false
        internal static bool IsConnected(this int[][] grid)
        {
            int si = -1, sj = -1;
            for (int i = 0; si < 0 && i < grid.Length; ++i)
                for (int j = 0; si < 0 && j < grid[i].Length; ++j)
                    if (grid[i][j] == 1)
                    {
                        si = i; sj = j;
                    }
            if (si < 0) return false;
            grid[si][sj] = 2;
            Queue<(int, int)> qu = new(); qu.Enqueue((si, sj));
            while (qu.Any())
            {
                (int i, int j) = qu.Dequeue();
                foreach ((int ni, int nj) in grid.FourDir(i, j))
                    if (grid[ni][nj] == 1)
                    {
                        grid[ni][nj] = 2;
                        qu.Enqueue((ni, nj));
                    }
            }
            bool disconnect = grid.Any(r => r.Any(c => c == 1));
            // 还原
            for (int i = 0; i < grid.Length; ++i)
                for (int j = 0; j < grid[i].Length; ++j)
                    if (grid[i][j] == 2)
                        grid[i][j] = 1;
            return !disconnect;
        }
    }
}
