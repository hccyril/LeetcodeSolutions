using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest;

// 309 / 36767	hccyril 	15	0:42:44	 0:02:18	 0:11:45	N/A	 0:42:44
// 用Python和C++完成，C#代码为赛后重写
internal class LC_WC0408_20240728
{
    #region Problem A
    /**
     * @brief T1 判断是否可以赢得数字游戏
     * 根据题意把2种方案都算出来，判断是否有其中一种可以赢
     */
    public bool CanAliceWin(int[] nums)
    {
        int sm = 0, a1 = 0, a2 = 0;
        foreach (int x in nums)
        {
            sm += x;
            if (x < 10) a1 += x;
            if (x >= 10 && x < 100) a2 += x;
        }
        return a1 > sm - a1 || a2 > sm - a2;
    }
    #endregion

    #region Problem B
    /**
     * @brief T2 统计不是特殊数字的数字数量
     * 留意满足题目要求的数必须是一个质数的平方，通过质数筛得到质数表然后枚举判断即可
     */
    public int NonSpecialCount(int l, int r)
    {
        int nans = 0;
        (r + 1).FactorTable();
        for (int i = 2; i * i <= r; ++i)
        {
            if (i.IsPrime())
            {
                int ns = i * i;
                if (ns >= l && ns <= r) nans++;
            }
        }
        return r - l + 1 - nans;
    }
    #endregion

    #region Problem C
    /**
     * @brief T3 统计 1 显著的字符串的数量
     * 关键技巧：题目要求1的数量大于0的数量的平方，所以当0的数量大于sqrt(n)时，肯定找不到任何解了
     * 然后我们发现sqrt(n)最大只有200，因此完全可以逐个枚举求解
     */

    // 求解子问题：求0的数量为k个时，1的数量大于等于k*k的子串数量
    // 子串转化成数组，并且连续1的数量存在第一个位置，例如 "00111011" -> [0 0 3 1 1 0 2 1]
    int Solve(List<int> a, int k)
    {
        int ans = 0, i = 0, n0 = 0, n1 = 0,
            t = k == 0 ? 1 : k * k; // t: 必须包含1的最少个数（也就是0的个数的平方）#特殊情况：0个0时也至少得有一个1，不能是空串
        for (int j = 0; j < a.Count; ++j)
        {
            if (a[j] > 0)
                ++n1;
            else
                ++n0;
            while (n0 > k)
            {
                if (a[i] > 0)
                    --n1;
                else
                    --n0;
                ++i;
            }
            if (n0 == k && i <= j)
            {
                int hi = n1, lo = n1 - a[i];
                if (lo < t) lo = t;
                if (hi >= lo)
                    ans += hi - lo + 1;
            }
        }
        return ans;
    }

    // 枚举0的个数从0到sqrt(n)，逐个用滑动窗口求解
    public int NumberOfSubstrings(string s)
    {
        int n = s.Length;
        List<int> a = new();
        int i = -1;
        for (int j = 0; j < n; ++j)
        {
            char c = s[j];
            if (c == '0')
            {
                a.Add(0);
                i = j;
            }
            else
            {
                a.Add(1);
                if (i < 0 || a[i] == 0) i = j;
                else ++a[i];
            }
        }
        int ans = 0;
        for (int k = 0; k * k < n; ++k)
            ans += Solve(a, k);
        return ans;
    }
    #endregion

    #region Problem D
    bool IsTouch(int[] c1, int[] c2)
    {
        long x1 = c1[0], y1 = c1[1], r1 = c1[2], x2 = c2[0], y2 = c2[1], r2 = c2[2],
            xd = x1 < x2 ? x2 - x1 : x1 - x2,
            yd = y1 < y2 ? y2 - y1 : y1 - y2;
        return xd * xd + yd * yd <= (r1 + r2) * (r1 + r2);
    }
    public bool CanReachCorner(int X, int Y, int[][] circles)
    {
        int n = circles.Length;
        UnionFind uni = new(n + 4);
        for (int i = 0; i < n; ++i)
        {
            int x = circles[i][0], y = circles[i][1], r = circles[i][2];
            if (x <= r || Y - y <= r) uni.Union(n + 1, i);
            if (X - x <= r || y <= r) uni.Union(n + 2, i);
            for (int j = 0; j < i; ++j)
                if (IsTouch(circles[i], circles[j]))
                    uni.Union(j, i);
        }
        return !uni.Check(n + 1, n + 2);
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
        LC_WC0408_20240728 sln = new();

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
        string s = "0"; 
            // "00011"; // exp: 5
        return NumberOfSubstrings(s);
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
