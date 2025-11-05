using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest;

// 本场比赛用WXM账号使用Python完成
// 排名：114/2497 WXM 13 00:42:40 A-00:05:56 B-00:09:52 C-N/A D-00:42:40
// 全场112人AK，好在先做D题
// 第三题在数据量明显可以用DP的情况下非要用贪心，结果陷进去了（详情见Python）
// C#代码为赛后重写
internal class LC_WC0425_20241124
{
    #region Problem A
    // 模拟
    // 坑1：i < n - l + 1 不是 i < n - l
    // 坑2：j = i + l 不是 j = i + 1
    public int MinimumSumSubarray(IList<int> nums, int l, int r)
    {
        int ans = -1, n = nums.Count;
        for (int i = 0; i < n - l + 1; ++i)
            for (int j = i + l; j <= n && j <= i + r; ++j)
            {
                int sm = nums.Skip(i).Take(j - i).Sum();
                if (sm > 0)
                    ans = ans < 0 ? sm : Math.Min(ans, sm);
            }
        return ans;
    }
    #endregion

    #region Problem B
    // 本题用Python的Counter可直接求解
    //def isPossibleToRearrange(self, s: str, t: str, k: int) -> bool:
    //    n = len(s)
    //    d = len(s) // k
    //    c1 = Counter(s[i:i + d] for i in range(0, n, d))
    //    c2 = Counter(t[i:i + d] for i in range(0, n, d))
    //    return c1 == c2
    #endregion

    #region Problem C
    // 多维度DP，类似01背包，用从后往前的更新方法，可以避免创建两个dp数组
    public int MinArraySum(int[] nums, int k, int op1, int op2)
    {
        int[,] dp = new int[op1 + 1, op2 + 1];
        for (int i = 0; i <= op1; ++i)
            for (int j = 0; j <= op2; ++j)
                dp[i, j] = -1;
        dp[0, 0] = 0;
        foreach (int x0 in nums)
        {
            int x1 = x0 > 1 ? x0 + 1 >> 1 : -1,
                x2 = x0 >= k ? x0 - k : -1,
                x3 = x0 >= 2 * k - 1 ? (x0 + 1 >> 1) - k :
                    x0 > k + 1 ? x0 - k + 1 >> 1 : -1;
            for (int p1 = op1; p1 >= 0; --p1)
                for (int p2 = op2; p2 >= 0; --p2)
                {
                    if (dp[p1, p2] >= 0)
                        dp[p1, p2] += x0;
                    if (p1 > 0 && x1 >= 0 && dp[p1 - 1, p2] >= 0)
                        dp[p1, p2] = dp[p1, p2] < 0 ? dp[p1 - 1, p2] + x1 : Math.Min(dp[p1, p2], dp[p1 - 1, p2] + x1);
                    if (p2 > 0 && x2 >= 0 && dp[p1, p2 - 1] >= 0)
                        dp[p1, p2] = dp[p1, p2] < 0 ? dp[p1, p2 - 1] + x2 : Math.Min(dp[p1, p2], dp[p1, p2 - 1] + x2);
                    if (p1 > 0 && p2 > 0 && x3 >= 0 && dp[p1 - 1, p2 - 1] >= 0)
                        dp[p1, p2] = dp[p1, p2] < 0 ? dp[p1 - 1, p2 - 1] + x3 : Math.Min(dp[p1, p2], dp[p1 - 1, p2 - 1] + x3);
                }
        }
        int ans = -1;
        for (int i = 0; i <= op1; ++i)
            for (int j = 0; j <= op2; ++j)
                if (dp[i, j] >= 0)
                    ans = ans < 0 ? dp[i, j] : Math.Min(ans, dp[i, j]);
        return ans;
    }
    #endregion

    #region Problem D
    // Tree DP
    // dp[i][0]: 当前根节点连接最多k个儿子时子树的最大值
    // dp[i][1]: 当前根节点连接最多(k-1)个儿子时子树的最大值
    public long MaximizeSumOfWeights(int[][] edges, int k)
    {
        int n = edges.Length + 1;
        var tg = edges.WeightedTreeGraph();

        // DP
        long[,] dp = new long[n, 2];

        // DFS
        const int DFS_ROOT = 0;
        Stack<(int Node, int Child)> dfsStk = new();
        int i = DFS_ROOT, childIndex = 0;
        while (true)
        {
            if (childIndex == tg[i].Count)
            {
                int parent = dfsStk.Any() ? dfsStk.Peek().Node : -1;

                long d1 = 0L, 
                    d0 = tg[i].Where(t => t.Item1 != parent).Select(t => dp[t.Item1, 0]).Sum();
                var sa = tg[i]
                    .Where(t => t.Item1 != parent && dp[t.Item1, 0] - dp[t.Item1, 1] - t.Item2 < 0)
                    .Select(t => dp[t.Item1, 0] - dp[t.Item1, 1] - t.Item2)
                    .OrderBy(t => t)
                    .Take(k)
                    .ToArray();
                d1 = d0 -= sa.Sum();
                if (sa.Length == k)
                    d1 += sa[^1];
                dp[i, 0] = d0; dp[i, 1] = d1;

                if (dfsStk.Any())
                {
                    (i, childIndex) = dfsStk.Pop();
                    continue;
                }
                else break;
            }

            (int nextIndex, _) = tg[i][childIndex];

            if (dfsStk.Any() && dfsStk.Peek().Node == nextIndex)
            {
                ++childIndex;
                continue;
            }
            else
            {   // dfs next
                dfsStk.Push((i, childIndex + 1));
                (i, childIndex) = (nextIndex, 0);
            }
        }
        return dp[0, 0];
    }
    static readonly string RefMethod = nameof(GraphEX.TreeDfs2);

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
        LC_WC0425_20241124 sln = new();

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
