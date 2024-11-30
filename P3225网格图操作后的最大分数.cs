using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// BiC135-D 20240720 
// rating 3027
// 2024/11/29 AC 第一次自主写出3000难度以上的题目（虽然也参考了题解）
internal class P3225网格图操作后的最大分数
{
    // 关键思路：设 b x y 为 j-1 j j+1 列的黑格子数目，若 b >= x <= y，此时根据贪心法，必定取 x = 0
    // 因此只需要分类讨论 x < y 和 x >= y 的情况即可
    // DP[j][x][y]: 当前第j列有x个黑格，预计j+1列有y个黑格，的最优解
    // mc_b[j][x]: 当前第j列有x个黑格，且大于j-1列的黑格（即x > b）时的缓存最大值
    // mc_w[j][x]: 当前第j列有x个黑格，且无论j-1列有多少黑格时的缓存最大值，此时的数值带上第j列可计算数值
    public long MaximumScore(int[][] grid)
    {
        int n = grid.Length;
        long ans = 0L;

        // init
        long[,] dp = new long[n + 1, n + 1], gt = new long[n, n];
        long[] mc_b = new long[n + 1], mc_w = new long[n + 1],
            mc1_b = new long[n + 1], mc1_w = new long[n + 1];

        // grid转成前缀和
        for (int j = 0; j < n; ++j)
            for (int i = 0; i < n; ++i)
                gt[i, j] = i == 0 ? grid[i][j] : gt[i - 1, j] + grid[i][j];

        // dp
        for (int j = 0; j < n - 1; ++j)
        {
            if (j == 0) // 对于第一列，当x<y时，x>0是没有意义的，因此只有x==0和x>y两种情况
            {
                for (int y = 1; y <= n; ++y)
                {
                    mc_w[y] = mc_b[y] = dp[0, y] = gt[y - 1, j];
                    ans = Math.Max(ans, dp[0, y]);
                }
                for (int x = 1; x <= n; ++x)
                    for (int y = 0; y < x; ++y)
                    {
                        dp[x, y] = gt[x - 1, j + 1] - (y == 0 ? 0 : gt[y - 1, j + 1]);
                        mc_w[y] = Math.Max(mc_w[y], dp[x, y]);
                        ans = Math.Max(ans, dp[x, y]);
                    }
                continue;
            }
            long bc = dp[0, 0]; // 见后#处注释
            for (int x = 0; x <= n; ++x)
                for (int y = 0; y <= n; ++y)
                {
                    if (x == 0)
                    {   // # dp1[0][y] = max(dp[b][0] + s(b, y)) for b in 0..n) 这个式子中只有dp[0][0]会跟dp1重叠，因此只缓存这一个数值而不是整个数组
                        dp[0, y] = 0L;
                        for (int b = 0; b <= n; ++b)
                        {
                            long d = b == 0 ? bc : dp[b, 0];
                            if (y > b)
                                d += gt[y - 1, j] - (b == 0 ? 0 : gt[b - 1, j]);
                            dp[0, y] = Math.Max(dp[0, y], d);
                        }
                        if (y > 0) mc1_b[y] = Math.Max(mc1_b[y], dp[x, y]); // [bug]第一次提交时漏了这句导致一个WA
                        mc1_w[y] = Math.Max(mc1_w[y], dp[x, y]);
                    }
                    else if (x < y) // therefore x > 0, y > 0
                    {
                        dp[x, y] = gt[y - 1, j] - gt[x - 1, j] + mc_b[x];
                        mc1_b[y] = Math.Max(mc1_b[y], dp[x, y]);
                        mc1_w[y] = Math.Max(mc1_w[y], dp[x, y]);
                    }
                    else // x >= y, x > 0, y >= 0
                    {
                        dp[x, y] = gt[x - 1, j + 1] - (y == 0 ? 0 : gt[y - 1, j + 1]) + mc_w[x];
                        mc1_w[y] = Math.Max(mc1_w[y], dp[x, y]);
                    }
                    ans = Math.Max(ans, dp[x, y]);
                }
            (mc_b, mc1_b) = (mc1_b, mc_b); Array.Fill(mc1_b, 0L);
            (mc_w, mc1_w) = (mc1_w, mc_w); Array.Fill(mc1_w, 0L);
        }

        return ans;
    }

    // test: answer=54
    // [[0,4,14,0,3],[0,0,0,15,7],[0,7,0,10,0],[0,0,0,0,0],[0,4,12,0,0]]
}
