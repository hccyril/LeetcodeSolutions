using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleCore1;

// 2024/1/20 难题挑战
// rating: 2428
// 未能在45分钟内做完，估计用时50分钟左右
internal class P2867统计树中的合法路径数目
{
    public long CountPaths(int n, int[][] edges)
    {
        var tg = edges.TreeGraph(n + 1);
        n.FactorTable();
        long ans = 0L;

        (long n1, long n0) Dfs(int i, int p)
        {
            var a = tg[i].Where(j => j != p)
                .Select(j => Dfs(j, i))
                .ToArray();

            // solve TLE
            long t0 = a.Select(t => t.n0).Sum(),
                t1 = a.Select(t => t.n1).Sum();

            // TLE - 需要优化
            //for (int j1 = 0; j1 < a.Length - 1; ++j1)
            //    for (int j2 = j1 + 1; j2 < a.Length; ++j2)
            //    {
            //        if (i.IsPrime())
            //        {
            //            ans += a[j1].n0 * a[j2].n0;
            //        }
            //        else
            //        {
            //            ans += a[j1].n1 * a[j2].n0 + a[j1].n0 * a[j2].n1;
            //        }
            //    }
            long nt1 = 0L, nt0 = 0L;
            if (i.IsPrime()) ++nt1;
            else ++nt0;

            long ps = 0L;
            foreach ((long n1, long n0) in a)
            {
                if (i.IsPrime())
                {
                    ps += n0 * (t0 - n0); // solve TLE
                    ans += n0;
                    nt1 += n0;
                }
                else
                {
                    ans += n1 * (t0 - n0); // solve TLE
                    ans += n1;
                    nt0 += n0;
                    nt1 += n1;
                }
            }
            if (i.IsPrime()) ans += ps / 2; // solve TLE
            return (nt1, nt0);
        }
        Dfs(1, -1);
        return ans;
    }

    internal static void Run()
    {
        var sln = new P2867统计树中的合法路径数目();
        int n = 5;
        var edges = "[[1, 2], [1, 3], [2, 4], [2, 5]]".ToTestInput<int[][]>();
        Console.WriteLine(sln.CountPaths(n, edges)); 
    }
}
