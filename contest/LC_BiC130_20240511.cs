using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest;

// 状态不好，这场比赛总体做得巨慢
// 317 / 2602	呱呱编程实验室 	12	0:53:05	 0:04:55	 0:23:58	 0:53:05	
internal class LC_BiC130_20240511
{
    #region Problem A
    // 第一题就写了很久，而且感谢测试用例不杀之恩，差点就贡献WA了
    public bool SatisfiesConditions(int[][] grid)
    {
        int m = grid.Length, n = grid[0].Length;
        for (int i = 0; i < m; ++i)
            for (int j = 0; j < n; ++j)
            {
                if (i < m - 1 && grid[i + 1][j] != grid[i][j]) return false;
                if (j < n - 1 && grid[i][j + 1] == grid[i][j]) return false;
            }
        return true;
    }
    #endregion

    #region Problem B
    // 一开始就审错题了，居然去算直线距离，你以为是圆么。。
    // 同样感谢测试用例，及时纠正错误
    public int MaxPointsInsideSquare(int[][] points, string s)
    {
        SortedSet<(int, int)> ss = new();
        for (int i = 0; i < s.Length; ++i)
        {
            ss.Add((Math.Max(Math.Abs(points[i][0]), Math.Abs(points[i][1])), i));
        }
        int c = 0, ans = 0;
        int lastd = -1;
        bool dup = false;
        int hm = 0;
        foreach ((int d, int i) in ss)
        {
            if (d > lastd)
            {
                ans += c;
                c = 0;
                lastd = d;
            }
            int b = 1 << (s[i] - 'a');
            if ((hm & b) != 0)
            {
                dup = true;
                break;
            }
            else
            {
                ++c;
                hm |= b;
            }
        }
        if (!dup) ans += c;
        return ans;
    }
    #endregion

    #region Problem C
    // 在循环里面把j写成i了，后来要调试才发现！
    public int MinimumSubstringsInPartition(string s)
    {
        int n = s.Length;
        int[] dp = new int[n];
        dp[0] = 1;
        int[] cnt = new int[26];
        for (int i = 1; i < n; ++i)
        {
            Array.Fill(cnt, 0);
            cnt[s[i] - 'a'] = 1;
            int maxc = 1, k = 1, cn = 1;
            dp[i] = dp[i - 1] + 1;
            for (int j = i - 1; j >= 0; --j)
            {
                ++cn;
                if (cnt[s[j] - 'a'] == 0) ++k;
                maxc = Math.Max(maxc, ++cnt[s[j] - 'a']);
                if (cn == k * maxc)
                    dp[i] = Math.Min(dp[i], 1 + (j > 0 ? dp[j - 1] : 0));
            }
        }
        return dp[^1];
    }
    #endregion

    #region Problem D
    // 最终问题转换成该区间内每个位（1，2，4...）各有多少个，最后算出结果等于2^x，然后用快速幂得到结果
    // 数从1到n总共有多少个1
    public long GetBitCount(long n)
    {
        long b = 1L, sm = n + 1 >> 1;
        int c = 1;
        while ((b << 1) <= n)
        {
            ++c;
            b <<= 1;

            long d = n + 1 >> c + 1, m = n + 1 & (1 << c + 1) - 1;
            sm += (d << c) + (m > b ? m - b : 0);
        }
        return sm;
    }

    // TODO 给定1的总个数c，求最大的n，使得1到n的总bitCount <= c

    // 套了三层娃，完全不够时间写（本来前三题就已经花了过多时间）
    public int[] FindProductsOfElements(long[][] queries)
    {
        foreach (var qp in queries)
        {
            // TODO
        }
        throw new NotImplementedException();
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
        LC_BiC130_20240511 sln = new();

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
        var s = "fabccddg";

        return MinimumSubstringsInPartition(s);
    }

    int RunTestE()
    {
        return 0;
    }
    #endregion
}
