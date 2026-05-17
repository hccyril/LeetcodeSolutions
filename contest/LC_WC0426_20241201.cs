using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest;

// 第426场周赛，2024/12/1 by 157* using Python3
// 1:15 AK (430 AK)
// rank      score   time        A           B           C           D
// 267/2446  18    01:14:41    00:06:19    00:17:35    01:14:41    00:49:14
// C#为赛后重写，重写时发现各题都有更简便写法，而且CD都不需要用换根DP
internal class LC_WC0426_20241201
{
    #region Problem A
    public int SmallestNumber(int n)
        => Enumerable.Range(1, 10).Select(t => (1 << t) - 1).First(t => t >= n);

    #endregion

    #region Problem B
    public int GetLargestOutlier(int[] nums)
    {
        HashSet<int> h1 = new(), h2 = new();
        int sm = nums.Sum();
        foreach (int x in nums)
            if (!h1.Add(x)) h2.Add(x);
        int ans = -1001;
        foreach (int x in nums)
        {
            int r = sm - x;
            if (r % 2 == 0)
            {
                int y = r / 2;
                if (x == y && h2.Contains(x) || x != y && h1.Contains(y))
                    ans = Math.Max(ans, x);
            }
        }
        return ans;
    }
    #endregion

    #region Problem C
    // 直接DFS遍历计算
    public int[] MaxTargetNodes(int[][] edges1, int[][] edges2, int k)
    {
        if (k == 0)
        {
            int[] ans = new int[edges1.Length + 1];
            Array.Fill(ans, 1);
            return ans;
        }
        int n1 = edges1.Length + 1, n2 = edges2.Length + 1;
        var tg1 = edges1.TreeGraph(n1);
        var tg2 = edges2.TreeGraph(n2);
        int Dfs(IList<int>[] tg, int i, int p, int lv, int limit)
        {
            if (lv == limit) return 1;
            int sm = 1;
            foreach (int j in tg[i])
                if (j != p)
                    sm += Dfs(tg, j, i, lv + 1, limit);
            return sm;
        }
        int m = Enumerable.Range(0, n2).Select(i => Dfs(tg2, i, -1, 0, k - 1)).Max();
        return Enumerable.Range(0, n1).Select(i => Dfs(tg1, i, -1, 0, k) + m).ToArray();
    }
    #endregion

    #region Problem D
    // 将树节点按黑白染色分别计数
    public int[] MaxTargetNodes(int[][] edges1, int[][] edges2)
    {
        int n1 = edges1.Length + 1, n2 = edges2.Length + 1;
        var tg1 = edges1.TreeGraph(n1);
        var tg2 = edges2.TreeGraph(n2);
        BitArray a1 = tg1.TreeBW(), a2 = tg2.TreeBW();
        int m = Enumerable.Range(0, n2).Count(i => a2[i]),
            m1 = Enumerable.Range(0, n1).Count(i => a1[i]),
            m0 = n1 - m1; 
        m = Math.Max(m, n2 - m);
        return Enumerable.Range(0, n1).Select(i => m + (a1[i] ? m1 : m0)).ToArray();
    }
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
        //char p = 'D';
        //LC_WC0426_20241201 sln = new();

        //return p switch
        //{
        //    'A' => sln.RunTestA(),
        //    'B' => sln.RunTestB(),
        //    'C' => sln.RunTestC(),
        //    'D' => sln.RunTestD(),
        //    'E' => sln.RunTestE(),
        //    _ => -1
        //};
        return -1;
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
