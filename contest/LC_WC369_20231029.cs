using System.Drawing;
using System.Security.Cryptography;

namespace ConsoleCore1.contest;

// 做完12先做4，结果最后3不够时间完成
internal class LC_WC369_20231029
{
    #region Problem A
    public int SolveA(int x)
    {
        return x;
    }
    #endregion

    #region Problem B
    public int SolveB(int x)
    {
        return x;
    }
    #endregion

    #region Problem C
    /* 巨失败！这么常规的DP推了这么久没做出来（比赛中在最后剩15分钟情况下未能做出来）
    def minIncrementOperations(self, nums: List[int], k: int) -> int:
        n = len(nums)
        d1 = [0] * n
        for i, x in enumerate(nums):
            d = max(0, k - x)
            if i > 2:
                d += min(d1[i-3:i])
            d1[i] = d
        #print(d1)
        return min(d1[-1:-4:-1])
     * */
    #endregion

    #region Problem D

    public int MaximumPoints(int[][] edges, int[] coins, int k)
    {
        int n = coins.Length;
        List<int>[] ug = new List<int>[n];
        for (int i = 0; i < n; ++i) ug[i] = new();
        foreach (var ed in edges)
        {
            int a = ed[0], b = ed[1];
            ug[a].Add(b);
            ug[b].Add(a);
        }

        List<int>[] dp = new List<int>[n];
        for (int i = 0; i < n; ++i) dp[i] = new();

        void DpDfs(int i, int p)
        {
            var sums = new List<int>();
            foreach (int j in ug[i])
                if (j != p)
                {
                    DpDfs(j, i);
                    for (int l = 0; l < dp[j].Count; ++l)
                        if (l >= sums.Count)
                            sums.Add(dp[j][l]);
                        else
                            sums[l] += dp[j][l];
                }
            int c = coins[i];
            for (int f = 0; f < 100; ++f)
            {
                int dp0 = f < sums.Count ? sums[f] : 0, dp1 = f + 1 < sums.Count ? sums[f + 1] : 0;
                int ds = c - k + dp0;
                c >>= 1;
                ds = Math.Max(ds, c + dp1);
                if (ds > 0)
                    dp[i].Add(ds);
                else break;
            }
            for (int j = dp[i].Count - 2; j >= 0; --j)
            {
                if (dp[i][j + 1] <= 0) dp[i].RemoveAt(j + 1);
                else if (dp[i][j] < dp[i][j + 1]) dp[i][j] = dp[i][j + 1];
            }
        }
        DpDfs(0, -1);
        // debug
        //for (int i = 0; i < n; ++i)
        //    Console.WriteLine("{0} : {1}", i, string.Join(" ", dp[i]));
        return dp[0].Count > 0 ? dp[0][0] : 0;
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
        char p = 'D';
        LC_WC369_20231029 sln = new();

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
        /*
        [[0,1],[0,2],[0,3],[2,4],[5,4],[6,0],[4,7],[8,5]]
[2,3,10,0,0,2,7,3,9]
2
        */
        var sedges = "[[0,1],[0,2],[0,3],[2,4],[5,4],[6,0],[4,7],[8,5]]";
        var edges = sedges.ToTestInput<int[][]>();
        int[] coins = { 2, 3, 10, 0, 0, 2, 7, 3, 9 };
        int k = 2;
        return MaximumPoints(edges, coins, k);
    }

    int RunTestE()
    {
        return 0;
    }
    #endregion
}
