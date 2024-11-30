using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest;

// https://leetcode.com/contest/biweekly-contest-135
// 3211 / 32438	hccyril 	12	1:35:19	 0:02:24	 0:06:24	 1:25:19  2
// 本次比赛第4题做出来的不到30人(US Site)
internal class LC_BiC135_20240720
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
    // 一开始用python写的三分法，结果后来发现(1)有相同值,(2)有反例，局部有多个山谷
    // 最后想到用排序+树状数组，在最后一刻提交AC
    // 枚举d从0到k，右边大于d的组必定可以只转换一次，但左边的就要分情况，如果min(x, k-y) <= k-d，则可以只换一次，否则必须换2次
    // 最后加上树状数组给过了
    // 赛后很久才理解了别人用的差分方法
    public int MinChanges(int[] nums, int k)
    {
        int n = nums.Length;
        var a = Enumerable.Range(0, k + 1).Select(i => new SortedSet<(int, int)>()).ToArray();
        for (int i = 0, j = n - 1; i < j; ++i, --j)
        {
            int x = nums[i], y = nums[j];
            if (x > y) (x, y) = (y, x);
            int d = y - x;
            a[d].Add((x, i));
        }
        int p = n >> 1, ans = n, p0 = 0;
        Fenwick fw = new(k + 1);
        for (int d = 0; d <= k; ++d)
        {
            p -= a[d].Count;
            ans = Math.Min(ans, (p0 << 1) - fw.Sum(k - d) + p);
            p0 += a[d].Count;
            foreach ((int x, _) in a[d])
            {
                int y = x + d;
                fw.Update(Math.Min(x, k - y));
            }
        }
        return ans;
    }
    #endregion

    #region Problem D
    // 比赛时没时间看（T3做完已经没时间了），本题全网只有69人做出来，不知道是否是时间的问题
    // 但赛后想到可以用动态规划做
    // 20240721
    public long MaximumScore_1(int[][] grid)
    {
        int n = grid.Length;
        long[,] dp0 = new long[n, 4], dp = new long[n, 4];
        long MaxCell(long[,] d, int i, int b = 0) 
            => Enumerable.Range(0, 4)
            .Where(j => (j & b) == b)
            .Select(j => d[i, j])
            .Max();
        for (int i = 1; i < n; ++i)
        {
            if (i == 1)
            {
                dp[i, 0] = 0;
                dp[i, 1] = grid[0][i - 1];
                dp[i, 2] = grid[0][i];
                dp[i, 3] = 0;
            }
            else
            {
                dp[i, 0] = Math.Max(dp[i - 1, 0], dp[i - 1, 2]);
                dp[i, 1] = grid[0][i - 1] + MaxCell(dp, i - 2);
                dp[i, 2] = MaxCell(dp, i - 1, 1) + grid[0][i];
                dp[i, 3] = MaxCell(dp, i - 1, 1);
            }
        }
        for (int r = 1; r < n; ++r)
        {
            (dp0, dp) = (dp, dp0);
            for (int i = 1; i < n; ++i)
            {
                if (i == 1)
                {
                    dp[i, 0] = MaxCell(dp0, i);
                    dp[i, 1] = grid[0][i - 1] + MaxCell(dp0, i, 1);
                    dp[i, 2] = grid[0][i] + MaxCell(dp0, i, 2);
                    dp[i, 3] = MaxCell(dp0, i, 3);
                }
                else
                {
                    // TODO: 感觉没那么简单
                    //dp[i, 0] = Math.Max(dp[i - 1, 0], dp[i - 1, 2]);
                    //dp[i, 1] = grid[0][i - 1] + MaxCell(dp, i - 2);
                    //dp[i, 2] = Math.Max(dp[i - 1, 1], dp[i - 1, 3]) + grid[0][i];
                    //dp[i, 3] = Math.Max(dp[i - 1, 1], dp[i - 1, 3]);
                }
            }
        }
        return MaxCell(dp, n - 1);
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
        char p = 'C';
        LC_BiC135_20240720 sln = new();

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
        return 720;
    }

    int RunTestE()
    {
        return 0;
    }
    #endregion
}
