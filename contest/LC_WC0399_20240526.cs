using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest;

// https://leetcode.cn/contest/weekly-contest-399/
// 115 / 3423	呱呱编程实验室 	20	1:21:42	 1:06:37	 0:21:00	 1:21:42	 1:04:30
// D题8分，应该是第一次在比赛中做出较难的线段树 // 更新：D题难度分2697，应该是在比赛中做出来的最难题
internal class LC_WC0399_20240526
{
    #region Problem A
    // C题的简化版，暴力求解
    public int NumberOfPairs(int[] nums1, int[] nums2, int k)
    {
        int ans = 0;
        for (int i = 0; i < nums1.Length; ++i)
            for (int j = 0; j < nums2.Length; ++j)
                if (nums1[i] % (nums2[j] * k) == 0)
                    ++ans;
        return ans;
    }
    #endregion

    #region Problem B
    // 简单题
    /*
    def compressedString(self, word: str) -> str:
        ans = ""
        for k, g in groupby(word):
            c = sum(1 for _ in g)
            while c > 9:
                ans = ans + "9" + k
                c -= 9
            if c:
                ans = ans + str(c) + k
        return ans
     */
    #endregion

    #region Problem C
    // 最后时刻才做出来
    /*
    def numberOfPairs(self, nums1: List[int], nums2: List[int], k: int) -> int:
        cn = Counter()
        for y in nums1:
            if y % k == 0:
                x = y // k
                for t in range(1, int(sqrt(x)) + 1):
                    if x % t == 0:
                        cn[t] += 1
                        if t * t != x:
                            cn[x // t] += 1
        return sum(cn[y] for y in nums2)
     */
    #endregion

    #region Problem D
    // 线段树，每个区间维护4个信息：无所谓，最左边必须是0，最右边必须是0，左右都必须是0
    (long t00, long t01, long t10, long t11)[] a;

    void Update(int ind, int x, int i, int l, int r)
    {
        if (l == r)
        {
            a[i].t01 = a[i].t10 = a[i].t00 = 0;
            a[i].t11 = x > 0 ? x : 0;
            return;
        }
        else if (l > r) return;
        int m = l + r >> 1;
        if (ind <= m) Update(ind, x, i << 1, l, m);
        else
        {
            Update(ind, x, i << 1 | 1, m + 1, r);
        }
        int j = i << 1, k = i << 1 | 1;
        a[i].t00 = Math.Max(a[j].t00 + a[k].t10, a[j].t01 + a[k].t00);
        a[i].t01 = Math.Max(a[j].t00 + a[k].t11, a[j].t01 + a[k].t01);
        a[i].t10 = Math.Max(a[j].t10 + a[k].t10, a[j].t11 + a[k].t00);
        a[i].t11 = Math.Max(a[j].t10 + a[k].t11, a[j].t11 + a[k].t01);
    }

    public int MaximumSumSubsequence(int[] nums, int[][] queries)
    {
        int n = nums.Length, m = queries.Length;
        a = new (long t00, long t01, long t10, long t11)[n << 2 | 1];

        for (int i = 0; i < n; ++i)
            Update(i, nums[i], 1, 0, n - 1);

        long sm = 0;
        foreach (var v in queries)
        {
            int j = v[0], y = v[1];
            Update(j, y, 1, 0, n - 1);
            sm = (sm + a[1].t11) % 1000000007;
        }

        return (int)sm;
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
        LC_WC0399_20240526 sln = new();

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
