using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest;

// 2024/6/30 US Site
// AK: 445 / 31897	hccyril 	19	1:17:28	 0:06:51	 0:38:54	 0:38:02	 1:12:28  1
internal class LC_WC0404_20240630
{
    #region Problem A
    public int MaxHeightOfTriangle(int red, int blue)
    {
        int r = red, b = blue, ans = 0;
        for (int i = 1; i < 200; ++i)
        {
            if (r >= i)
            {
                r -= i;
                ans = Math.Max(ans, i);
                (r, b) = (b, r);
            }
            else break;
        }
        r = blue; b = red;
        for (int i = 1; i < 200; ++i)
        {
            if (r >= i)
            {
                r -= i;
                ans = Math.Max(ans, i);
                (r, b) = (b, r);
            }
            else break;
        }
        return ans;
    }
    #endregion

    #region Problem B
    // same with problem c, with k == 2
    #endregion

    #region Problem C
    public int MaximumLength(int[] nums, int k = 2)
    {
        var dp = Enumerable.Range(0, k).Select(_ => new Dictionary<int, int>()).ToArray();
        HashSet<int> first = new();
        int ans = 0;
        foreach (int x in nums.Select(t => t % k))
        {
            if (dp[x].Any())
            {
                foreach ((int f, int c) in dp[x])
                {
                    dp[f].TryAdd(x, 0);
                    dp[f][x] = Math.Max(dp[f][x], dp[x][f] + 1);
                    ans = Math.Max(ans, dp[f][x]);
                }
            }
            foreach (int f in first)
            {
                dp[f].TryAdd(x, 0);
                dp[f][x] = Math.Max(dp[f][x], 2);
                ans = Math.Max(ans, dp[f][x]);
            }
            first.Add(x);
        }
        return ans;
    }
    #endregion

    #region Problem D
    public int MinimumDiameterAfterMerge(int[][] edges1, int[][] edges2)
    {
        int n = edges1.Length + 1, m = edges2.Length + 1;
        var tg1 = edges1.TreeGraph(n);
        var tg2 = edges2.TreeGraph(m);
        int d1 = tg1.TreeDiameter(), d2 = tg2.TreeDiameter();
        //Console.WriteLine("d1={0} d2={1}", d1, d2);
        int m1 = d1 + 1 >> 1, m2 = d2 + 1 >> 1;
        return Math.Max(m1 + m2 + 1, Math.Max(d1, d2));
    }
    //public int MinimumDiameterAfterMerge(int[][] edges1, int[][] edges2)
    //{
    //    int n = edges1.Length + 1, m = edges2.Length + 1;
    //    var tg1 = edges1.TreeGraph(n);
    //    var tg2 = edges2.TreeGraph(m);
    //    int d1 = tg1.Diameter(), d2 = tg2.Diameter();
    //    int m1 = d1 + 1 >> 1, m2 = d2 + 1 >> 2;
    //    return Math.Max(m1 + m2, Math.Max(d1, d2));
    //}
    #endregion

    #region Problem E
    public int SolveE(int x)
    {
        return x;
    }
    #endregion

    #region Run Test
    internal static int Run()
    {
        char p = 'D';
        LC_WC0404_20240630 sln = new();

        return p switch
        {
            'A' => sln.RunTestA(),
            'B' => sln.RunTestB(),
            'C' => sln.RunTestC(),
            'D' => sln.RunTestD(),
            'E' => sln.RunTestE(),
            _ => -1
        };
    }

    int RunTestA()
    {
        return 0;
    }

    int RunTestB()
    {
        return 0;
    }

    int RunTestC()
    {
        return 0;
    }

    int RunTestD()
    {
        return 0;
    }

    int RunTestE()
    {
        return 0;
    }
    #endregion
}
