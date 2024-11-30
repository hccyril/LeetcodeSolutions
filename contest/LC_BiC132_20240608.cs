using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest;

// Done in US site, AC, all done in C#
// Rank	Name	Score	Finish Time 	Q1 (3)	Q2 (4)	Q3 (5)	Q4 (7)
// 201 / 33718	hccyril 	19	1:10:00	0:04:58	0:19:00*1	1:05:00	1:04:18
internal class LC_BiC132_20240608
{
    #region Problem A
    // 直接用最笨的方法
    // 已经掌握了s[l..r]的写法
    // 留意i-1和i+1那里都要加括号，跟Python不一样
    public string ClearDigits(string s)
    {
        while (s.Any(c => '0' <= c && c <= '9'))
        {
            for (int i = 0; i < s.Length; ++i)
                if (s[i] >= '0' && s[i] <= '9')
                {
                    if (i > 0)
                    {
                        s = s[0..(i - 1)] + s[(i + 1)..];
                    }
                    else
                        s = s[1..];
                    break;
                }
        }
        return s;
    }
    #endregion

    #region Problem B
    // 跟【1535. 找出数组游戏的赢家】是同一道题（输出不同，这题是输出下标）
    // 轮到了maxskill就固定了冠军，不用再模拟了，只要模拟之前有没人连赢k场即可
    public int FindWinningPlayer(int[] skills, int k)
    {
        int maxskill = skills.Max(), wc = 0, j = 0;
        if (skills[0] == maxskill) return 0;
        for (int i = 1; i < skills.Length; ++i)
        {
            if (skills[i] == maxskill) return i;
            if (skills[i] > skills[j])
            {
                j = i;
                wc = 1;
                if (wc == k) return j;
            }
            else
            {
                ++wc;
                if (wc == k) return j;
            }
        }
        return -1;
    }
    #endregion

    #region Problem C
    // C和D完全一样，只是数据范围不同
    #endregion

    #region Problem D
    // 先对nums做离散化，从1e9 => 5000
    // dp[d, c] = len: 最后一个数是d，且有c个不同数对时，得到的长度为len
    // dp[d, c] = Max(
    //     (1) dp[d, c] + 1
    //     (2) max(dp[x, c - 1]) + 1 for all x != d
    // )
    // 实际操作中只用mx数组维护最大和第二大即可
    public int MaximumLength(int[] nums, int k)
    {
        int n = nums.Length;
        Dictionary<int, int> hs = new();
        foreach (int x in nums)
        {
            if (!hs.ContainsKey(x))
                hs[x] = hs.Count;
        }
        for (int i = 0; i < n; ++i)
            nums[i] = hs[nums[i]];
        int[,] dp = new int[hs.Count, k + 1];
        (int first, int d, int second)[] mx = new (int first, int c, int second)[k + 1];
        Array.Fill(mx, (-1, -1, -1));
        void UpdateMax(int c, int d, int val)
        {
            (int first, int d0, int second) = mx[c];
            if (val > first)
            {
                if (d == d0)
                    mx[c] = (val, d, second);
                else
                    mx[c] = (val, d, first);
            }
            else if (d != d0 && val > second)
            {
                mx[c] = (first, d0, val);
            }
        }
        int GetMax(int c, int d) // max: k == c and x != d
        {
            (int first, int d0, int second) = mx[c];
            return d != d0 ? first : second;
        }
        foreach (int d in nums)
        {
            for (int c = 0; c <= k; ++c)
            {
                // same d
                if (c == 0 || dp[d, c] > 0)
                    ++dp[d, c];
                // diff d
                if (c > 0)
                {
                    int m = GetMax(c - 1, d);
                    if (m > 0 && m + 1 > dp[d, c])
                        dp[d, c] = m + 1;
                }
                if (dp[d, c] > 0)
                    UpdateMax(c, d, dp[d, c]);
            }
        }
        return Enumerable.Range(0, k + 1).Select(c => mx[c].first).Max();
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
        LC_BiC132_20240608 sln = new();

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
        return 68;
    }

    int RunTestE()
    {
        return 0;
    }
    #endregion
}
