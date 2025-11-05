using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// medium, 2025/9/29 Daily
// rating 2130
// 多边形划分三角形，状态压缩，记忆化回溯 <--更正：状态压缩的做法是超时的
internal class P1039多边形三角剖分的最低得分
{
    public int MinScoreTriangulation(int[] values)
    {
        int n = values.Length;
        int[,] dp = new int[n, n];
        int DpDfs(int i, int j)
        {
            if (i + 1 == j) return 0;
            if (dp[i, j] > 0) return dp[i, j];
            for (int k = i + 1; k < j; ++k)
            {
                int s = DpDfs(i, k) + values[i] * values[k] * values[j] + DpDfs(k, j);
                if (dp[i, j] == 0 || dp[i, j] > s)
                    dp[i, j] = s;
            }
            return dp[i, j];
        }
        return DpDfs(0, n - 1);
    }

    // 超时原因：2^50种状态太多，状态压缩的做法不可行
    public int MinScoreTriangulation_TLE(int[] values)
    {
        int n = values.Length;
        if (n == 3)
            return values[0] * values[1] * values[2];
        Dictionary<long, int> di = new();

        int DpDfs(long mp, int m)
        {
            if (di.TryGetValue(mp, out var v))
                return v;
            if (m == 4) // 4边形只有2种划分而不是4种，所以特判计算
            {
                var a = Enumerable.Range(0, n)
                    .Where(i => (mp & 1L << i) == 0)
                    .Select(i => values[i])
                    .ToArray();
                return di[mp] = Math.Min(a[0] * a[1] * a[2] + a[2] * a[3] * a[0], a[1] * a[2] * a[3] + a[3] * a[0] * a[1]);
            }
            int minScore = int.MaxValue;
            for (int i = -1, j = -1, k = 0; i < n; ++k)
            {
                if ((mp & 1L << k % n) == 0)
                {
                    if (i >= 0 && j >= 0)
                    {
                        int s = values[i] * values[j % n] * values[k % n] + DpDfs(mp ^ 1L << j % n, m - 1);
                        minScore = Math.Min(minScore, s);
                    }
                    (i, j) = (j, k);
                }
            }
            return di[mp] = minScore;
        }

        return DpDfs(0L, n);
    }
}
