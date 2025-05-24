using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2025/2/10 Daily // 2022/1/4 Daily
// rating 2566
// 博弈论, 拓扑排序+DP
internal class P0913猫和老鼠
{
    // Done 2025/2/11
    // https://leetcode.cn/submissions/detail/598411211/
    public int CatMouseGame(int[][] graph)
    {
        int n = graph.Length;
        int[,,] dp = new int[n, n, 2];
        const int Mo = 0, Ca = 1;
        Queue<(int, int, int)> qu = new();
        for (int i = 0; i < n; i++)
        {
            for (int j = 1; j < n; j++)
            {
                if (i == 0)
                {
                    dp[0, j, Mo] = dp[0, j, Ca] = Mo;
                    qu.Enqueue((0, j, Ca));
                }
                else if (i == j)
                {
                    dp[i, j, Mo] = dp[i, j, Ca] = Ca;
                    qu.Enqueue((i, j, Mo));
                    qu.Enqueue((i, j, Ca));
                }
                else
                {
                    dp[i, j, Mo] = ~graph[i].Length;
                    dp[i, j, Ca] = ~graph[j].Where(k => k != 0).Count();
                }
            }
        }

        while (qu.Any())
        {
            (int i, int j, int role) = qu.Dequeue();
            int result = dp[i, j, role];
            if (role == Mo)
            {
                foreach (int k in graph[j])
                    if (k != 0 && dp[i, k, Ca] < 0)
                    {
                        if (result == Ca)
                        {
                            dp[i, k, Ca] = Ca;
                            qu.Enqueue((i, k, Ca));
                        }
                        else // result == Mo
                        {
                            if (++dp[i, k, Ca] == -1)
                            {
                                dp[i, k, Ca] = Mo;
                                qu.Enqueue((i, k, Ca));
                            }
                        }
                    }
            }
            else // role == cat
            {
                foreach (int k in graph[i])
                    if (dp[k, j, Mo] < 0)
                    {
                        if (result == Mo)
                        {
                            dp[k, j, Mo] = Mo;
                            qu.Enqueue((k, j, Mo));
                        }
                        else // result == Ca
                        {
                            if (++dp[k, j, Mo] == -1)
                            {
                                dp[k, j, Mo] = Ca;
                                qu.Enqueue((k, j, Mo));
                            }
                        }
                    }
            }
        }

        int ans = dp[1, 2, Mo];
        ans = ans < 0 ? 0 : ans + 1;
        return ans;
    }

    // 用记忆化回溯没法处理节点的先后顺序问题，会导致WA
    public int CatMouseGame_1_WA(int[][] graph)
    {
        int n = graph.Length;
        int[,,] dp = new int[n, n, 2];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                dp[i, j, 0] = dp[i, j, 1] = -1;
            }
        }

        const int Mo = 0, Ca = 1, Draw = 2;
        int DpDfs(int i, int j, int r)
        {
            if (i == 0) return dp[i, j, r] = Mo;
            if (i == j) return dp[i, j, r] = Ca;
            if (dp[i, j, r] != -1) return dp[i, j, r];
            dp[i, j, r] = Draw;
            if (r == Mo)
            {
                bool catWin = true;
                foreach (int k in graph[i])
                {
                    int res = DpDfs(k, j, Ca);
                    if (res == Mo) return dp[i, j, r] = Mo;
                    if (res != Ca) catWin = false;
                }
                return dp[i, j, r] = catWin ? Ca : Draw;
            }
            else
            {
                bool mouseWin = true;
                foreach (int k in graph[j])
                {
                    if (k == 0) continue;
                    int res = DpDfs(i, k, Mo);
                    if (res == Ca) return dp[i, j, r] = Ca;
                    if (res != Mo) mouseWin = false;
                }
                return dp[i, j, r] = mouseWin ? Mo : Draw;
            }
        }
        return (DpDfs(1, 2, Mo) + 1) % 3;
    }

    internal static void Run()
    {
        // 0
        //int[][] graph = new int[][]
        //{
        //    new int[] {2, 5},
        //    new int[] {3},
        //    new int[] {0, 4, 5},
        //    new int[] {1, 4, 5},
        //    new int[] {2, 3},
        //    new int[] {0, 2, 3}
        //};

        // 1 not 0
        var graph = "[[5,7,9],[3,4,5,6],[3,4,5,8],[1,2,6,7],[1,2,5,7,9],[0,1,2,4,8],[1,3,7,8],[0,3,4,6,8],[2,5,6,7,9],[0,4,8]]".ToTestInput<int[][]>();

        Console.WriteLine(new P0913猫和老鼠().CatMouseGame(graph));
    }
}
