﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace ConsoleCore1;

// hard, 2023/6/24 -> 2024/2/10
// dp[i,map,in,out]四维dp，map为第i行的房间安排状况，最大为3^5
internal class P1659最大化网格幸福感
{
    // ver3 - 轮廓线DP (2024.4.13)
    // AC (289ms)
    public int GetMaxGridHappiness(int m, int n, int ic, int ec)
    {
        int mn = 1 << (n << 1), fm = mn - 1;
        Dictionary<(int, int, int), int> dp0 = new(), dp1 = new();
        dp0[(0, 0, 0)] = 0;

        // moved to Common
        //bool Update(Dictionary<(int, int, int), int> dp, int b, int ip, int ep, int val)
        //    => (!dp.TryGetValue((b, ip, ep), out var v0) || val > v0) && (dp[(b, ip, ep)] = val) >= 0;
        int Neighbor(int me, int you) => you switch
        {
            1 => me == 2 ? -10 : 40,
            2 => me == 2 ? -60 : -10,
            _ => 0 
        };

        for (int i = 0; i < m; ++i)
            for (int j = 0; j < n; ++j)
            {
                dp1.Clear();
                foreach (((int b, int ip, int ep), int val) in dp0)
                {
                    // #1 不住人
                    int b1 = b << 2 & fm;
                    dp1.UpdateBigger((b << 2 & fm, ip, ep), val); 
                    //Update(dp1, b << 2 & fm, ip, ep, val);

                    // #2 i人(10b=2)
                    if (ip < ic)
                    {
                        int v1 = 120;
                        if (i > 0) v1 += Neighbor(2, b >> (n - 1 << 1) & 3);
                        if (j > 0) v1 += Neighbor(2, b & 3);
                        dp1.UpdateBigger((b << 2 & fm | 2, ip + 1, ep), val + v1);
                        //Update(dp1, b << 2 & fm | 2, ip + 1, ep, val + v1);
                    }

                    // #3 e人(01)
                    if (ep < ec)
                    {
                        int v1 = 40;
                        if (i > 0) v1 += Neighbor(1, b >> (n - 1 << 1) & 3);
                        if (j > 0) v1 += Neighbor(1, b & 3);
                        dp1.UpdateBigger((b << 2 & fm | 1, ip, ep + 1), val + v1);
                        //Update(dp1, b << 2 & fm | 1, ip, ep + 1, val + v1);
                    }
                }
                (dp0, dp1) = (dp1, dp0);
            }
        return dp0.Values.Max();
    }

    // 状态压缩&解压缩
    static int Pack(IList<int> a, int i, int e, int r = 0) => Enumerable.Range(0, a.Count).Select(t => a[t] << (t << 1)).Sum() | i << 16 | e << 20 | r << 24;
    static (int[] a, int i, int e, int r) Unpack(int p, int n)
    {
        int[] a = new int[n];
        for (int t = 0; t < n; ++t)
            a[t] = p >> (t << 1) & 3;
        int i = p >> 16 & 15, e = p >> 20 & 15, r = p >> 24;
        return (a, i, e, r);
    }

    // 计算幸福指数 （p1和p2为邻居，1为内向 2为外向）
    static int Score(int p1, int p2)
        => (p1 | p2) switch
        {
            1 => p1 == p2 ? -60 : 0,
            2 => p1 == p2 ? 40 : 0,
            3 => -10,
            _ => 0
        };

    // DFS枚举当前行所有的状态
    static IEnumerable<(int[], int, int)> EnumStatus(int n, int qi, int qe)
    {
        int[] quota = { n, qi, qe };
        int[] ans = new int[n], used = new int[3];
        int i = 0, j = 0;
        while (j < 3 || i > 0)
        {
            if (i == n)
            {
                yield return (ans, used[1], used[2]);
                j = 3;
            }
            if (j == 3)
            {
                --used[ans[--i]];
                j = ans[i] + 1;
            }
            else
            {
                if (used[j] < quota[j])
                {
                    ans[i++] = j;
                    ++used[j];
                    j = 0;
                }
                else
                {
                    ++j;
                }
            }
        }
    }

    // 状态压缩（方法2）
    static int Mask(int[] a) => Enumerable.Range(0, a.Length).Select(t => a[t] << (t << 1)).Sum();
    static int[] Unmask(int p, int n)
    {
        int[] a = new int[n];
        for (int t = 0; t < n; ++t)
            a[t] = p >> (t << 1) & 3;
        return a;
    }

    static Dictionary<int, int> innerD = new();
    static Dictionary<(int, int), int> interD = new();

    static int InnerScore(int[] a, int mask)
    {
        if (innerD.TryGetValue(mask, out int result)) return result;

        int score = 0, n = a.Length;
        for (int t = 1; t < n; ++t)
            score += Score(a[t - 1], a[t]);

        return innerD[mask] = score;
    }

    static int InterScore(int[] a0, int m0, int[] a1, int m1)
    {
        if (m1 < m0) (m0, m1) = (m1, m0);
        if (interD.TryGetValue((m0, m1), out int result)) return result;

        int n = a0.Length, score = 0;
        for (int t = 0; t < n; ++t)
            score += Score(a0[t], a1[t]);

        return interD[(m0, m1)] = score;
    }

    // ver2 - 看完官方题解后发现思路基本一样，但官解用记忆化回溯，尝试自己写
    // 基本跟官方题解完全一样了，本地执行也不慢，但提交上去就超时，想不明白
    public int GetMaxGridHappiness_DP2(int m, int n, int ic, int ec)
    {
        Dictionary<(int, int, int, int), int> di = new();

        int DpDfs(int row, int[] a0, int premask, int qi, int qe)
        {
            if (row == m) return 0;
            int ans = 0;
            if (di.TryGetValue((row, premask, qi, qe), out ans))
                return ans;

            foreach ((int[] a1, int ti, int te) in EnumStatus(n, qi, qe))
            {
                int score = ti * 120 + te * 40, mask = Mask(a1);

                score += InnerScore(a1, mask) + InterScore(a0, premask, a1, mask);

                score += DpDfs(row + 1, a1, mask, qi - ti, qe - te);

                ans = Math.Max(ans, score);
            }

            return di[(row, premask, qi, qe)] = ans;

        }
        return DpDfs(0, new int[n], 0, ic, ec);
    }

    // 还是超时！
    //public int GetMaxGridHappiness(int m, int n, int ic, int ec)
    //{
    //    Dictionary<int, int> di = new();

    //    int DpDfs(int row, int[] a0, int qi, int qe)
    //    {
    //        if (row == m) return 0;
    //        int premask = Pack(a0, qi, qe, row), ans = 0;
    //        if (di.TryGetValue(premask, out ans))
    //            return ans;

    //        foreach ((int[] a1, int ti, int te) in EnumStatus(n, qi, qe))
    //        {
    //            int score = ti * 120 + te * 40;
    //            for (int t = 1; t < n; ++t)
    //                score += Score(a1[t - 1], a1[t]);
    //            for (int t = 0; t < n; ++t)
    //                score += Score(a0[t], a1[t]);

    //            score += DpDfs(row + 1, a1, qi - ti, qe - te);

    //            ans = Math.Max(ans, score);
    //        }

    //        return di[premask] = ans;

    //    }
    //    return DpDfs(0, new int[n], ic, ec);
    //}

    // ver1 DP - 也不算很慢但放到判题平台上就是超时
    public int GetMaxGridHappiness_DP1(int m, int n, int ic, int ec)
    {
        Dictionary<int, int> d0 = new();
        d0[0] = 0;
        int ans = 0;

        while (m-- > 0)
        {
            // DP
            Dictionary<int, int> d1 = new();
            foreach ((int k0, int v0) in d0)
            {
                (int[] a0, int i0, int e0, _) = Unpack(k0, n);
                foreach ((int[] a1, int it1, int et1) in EnumStatus(n, ic - i0, ec - e0))
                {
                    int i1 = i0 + it1, e1 = e0 + et1;
                    if (i1 <= ic && e1 <= ec)
                    {
                        int v1 = v0 + it1 * 120 + et1 * 40;
                        for (int t = 1; t < n; ++t)
                            v1 += Score(a1[t - 1], a1[t]);
                        for (int t = 0; t < n; ++t)
                            v1 += Score(a0[t], a1[t]);
                        int k1 = Pack(a1, i1, e1);
                        if (!d1.TryGetValue(k1, out var v1p) || v1 > v1p)
                            d1[k1] = v1;
                        ans = Math.Max(ans, v1);
                    }
                }
            }
            d0 = d1;
        }

        return ans;
    }

    internal static void Run()
    {
        var sln = new P1659最大化网格幸福感();
        //Console.WriteLine("ANS=" + sln.GetMaxGridHappiness(2, 3, 1, 2));

        // TLE - 2328ms -> 765ms (ans=1120)
        //Console.WriteLine("ANS=" + sln.GetMaxGridHappiness(4, 5, 6, 5));

        // 打表(打表全做出来大约24秒）
        List<int> ans = new();
        for (int m = 1; m <= 5; ++m)
            for (int n = 1; n <= 5; ++n)
                for (int i = 0; i <= 6; ++i)
                    for (int e = 0; e <= 6; ++e)
                    {
                        ans.Add(sln.GetMaxGridHappiness(m, n, i, e));
                        Console.WriteLine($"{m} {n} {i} {e} => {ans[^1]}");
                    }

        Console.WriteLine("static int[] TA = {" + string.Join(",", ans) + "};");
    }

    static int[] TA = { 0, 40, 40, 40, 40, 40, 40, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 0, 40, 120, 120, 120, 120, 120, 120, 150, 150, 150, 150, 150, 150, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 0, 40, 120, 200, 200, 200, 200, 120, 160, 230, 230, 230, 230, 230, 240, 260, 260, 260, 260, 260, 260, 240, 260, 260, 260, 260, 260, 260, 240, 260, 260, 260, 260, 260, 260, 240, 260, 260, 260, 260, 260, 260, 240, 260, 260, 260, 260, 260, 260, 0, 40, 120, 200, 280, 280, 280, 120, 160, 240, 310, 310, 310, 310, 240, 270, 340, 340, 340, 340, 340, 300, 320, 340, 340, 340, 340, 340, 300, 320, 340, 340, 340, 340, 340, 300, 320, 340, 340, 340, 340, 340, 300, 320, 340, 340, 340, 340, 340, 0, 40, 120, 200, 280, 360, 360, 120, 160, 240, 320, 390, 390, 390, 240, 280, 350, 420, 420, 420, 420, 360, 380, 400, 420, 420, 420, 420, 360, 380, 400, 420, 420, 420, 420, 360, 380, 400, 420, 420, 420, 420, 360, 380, 400, 420, 420, 420, 420, 0, 40, 120, 120, 120, 120, 120, 120, 150, 150, 150, 150, 150, 150, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 0, 40, 120, 200, 320, 320, 320, 120, 160, 230, 300, 320, 320, 320, 240, 260, 280, 300, 320, 320, 320, 240, 260, 280, 300, 320, 320, 320, 240, 260, 280, 300, 320, 320, 320, 240, 260, 280, 300, 320, 320, 320, 240, 260, 280, 300, 320, 320, 320, 0, 40, 120, 200, 320, 400, 520, 120, 160, 240, 320, 430, 500, 520, 240, 280, 350, 420, 480, 500, 520, 360, 380, 400, 460, 480, 500, 520, 360, 380, 440, 460, 480, 500, 520, 360, 380, 440, 460, 480, 500, 520, 360, 380, 440, 460, 480, 500, 520, 0, 40, 120, 200, 320, 400, 520, 120, 160, 240, 320, 440, 520, 630, 240, 280, 360, 440, 550, 610, 680, 360, 400, 470, 530, 590, 660, 680, 480, 500, 520, 570, 640, 660, 680, 480, 500, 520, 570, 640, 660, 680, 480, 500, 520, 570, 640, 660, 680, 0, 40, 120, 200, 320, 400, 520, 120, 160, 240, 320, 440, 520, 640, 240, 280, 360, 440, 560, 640, 750, 360, 400, 480, 560, 670, 730, 790, 480, 520, 590, 650, 710, 770, 840, 600, 620, 640, 690, 710, 770, 840, 600, 620, 640, 690, 710, 770, 840, 0, 40, 120, 200, 200, 200, 200, 120, 160, 230, 230, 230, 230, 230, 240, 260, 260, 260, 260, 260, 260, 240, 260, 260, 260, 260, 260, 260, 240, 260, 260, 260, 260, 260, 260, 240, 260, 260, 260, 260, 260, 260, 240, 260, 260, 260, 260, 260, 260, 0, 40, 120, 200, 320, 400, 520, 120, 160, 240, 320, 430, 500, 520, 240, 280, 350, 420, 480, 500, 520, 360, 380, 400, 460, 480, 500, 520, 360, 380, 440, 460, 480, 500, 520, 360, 380, 440, 460, 480, 500, 520, 360, 380, 440, 460, 480, 500, 520, 0, 40, 120, 200, 320, 400, 520, 120, 160, 240, 320, 440, 520, 630, 240, 280, 360, 440, 550, 630, 740, 360, 400, 470, 550, 660, 720, 780, 480, 520, 580, 640, 700, 760, 780, 600, 610, 620, 640, 700, 760, 780, 600, 610, 620, 640, 700, 760, 780, 0, 40, 120, 200, 320, 400, 520, 120, 160, 240, 320, 440, 520, 640, 240, 280, 360, 440, 560, 640, 760, 360, 400, 480, 560, 670, 750, 870, 480, 520, 600, 680, 780, 850, 960, 600, 640, 710, 780, 830, 890, 960, 720, 740, 760, 810, 870, 890, 960, 0, 40, 120, 200, 320, 400, 520, 120, 160, 240, 320, 440, 520, 640, 240, 280, 360, 440, 560, 640, 760, 360, 400, 480, 560, 680, 760, 880, 480, 520, 600, 680, 800, 880, 990, 600, 640, 720, 800, 910, 990, 1100, 720, 760, 830, 910, 1020, 1080, 1130, 0, 40, 120, 200, 280, 280, 280, 120, 160, 240, 310, 310, 310, 310, 240, 270, 340, 340, 340, 340, 340, 300, 320, 340, 340, 340, 340, 340, 300, 320, 340, 340, 340, 340, 340, 300, 320, 340, 340, 340, 340, 340, 300, 320, 340, 340, 340, 340, 340, 0, 40, 120, 200, 320, 400, 520, 120, 160, 240, 320, 440, 520, 630, 240, 280, 360, 440, 550, 610, 680, 360, 400, 470, 530, 590, 660, 680, 480, 500, 520, 570, 640, 660, 680, 480, 500, 520, 570, 640, 660, 680, 480, 500, 520, 570, 640, 660, 680, 0, 40, 120, 200, 320, 400, 520, 120, 160, 240, 320, 440, 520, 640, 240, 280, 360, 440, 560, 640, 760, 360, 400, 480, 560, 670, 750, 870, 480, 520, 600, 680, 780, 850, 960, 600, 640, 710, 780, 830, 890, 960, 720, 740, 760, 810, 870, 890, 960, 0, 40, 120, 200, 320, 400, 520, 120, 160, 240, 320, 440, 520, 640, 240, 280, 360, 440, 560, 640, 760, 360, 400, 480, 560, 680, 760, 880, 480, 520, 600, 680, 800, 880, 990, 600, 640, 720, 800, 910, 990, 1100, 720, 760, 840, 920, 1020, 1080, 1140, 0, 40, 120, 200, 320, 400, 520, 120, 160, 240, 320, 440, 520, 640, 240, 280, 360, 440, 560, 640, 760, 360, 400, 480, 560, 680, 760, 880, 480, 520, 600, 680, 800, 880, 1000, 600, 640, 720, 800, 920, 1000, 1120, 720, 760, 840, 920, 1040, 1120, 1230, 0, 40, 120, 200, 280, 360, 360, 120, 160, 240, 320, 390, 390, 390, 240, 280, 350, 420, 420, 420, 420, 360, 380, 400, 420, 420, 420, 420, 360, 380, 400, 420, 420, 420, 420, 360, 380, 400, 420, 420, 420, 420, 360, 380, 400, 420, 420, 420, 420, 0, 40, 120, 200, 320, 400, 520, 120, 160, 240, 320, 440, 520, 640, 240, 280, 360, 440, 560, 640, 750, 360, 400, 480, 560, 670, 730, 790, 480, 520, 590, 650, 710, 770, 840, 600, 620, 640, 690, 710, 770, 840, 600, 620, 640, 690, 710, 770, 840, 0, 40, 120, 200, 320, 400, 520, 120, 160, 240, 320, 440, 520, 640, 240, 280, 360, 440, 560, 640, 760, 360, 400, 480, 560, 680, 760, 880, 480, 520, 600, 680, 800, 880, 990, 600, 640, 720, 800, 910, 990, 1100, 720, 760, 830, 910, 1020, 1080, 1130, 0, 40, 120, 200, 320, 400, 520, 120, 160, 240, 320, 440, 520, 640, 240, 280, 360, 440, 560, 640, 760, 360, 400, 480, 560, 680, 760, 880, 480, 520, 600, 680, 800, 880, 1000, 600, 640, 720, 800, 920, 1000, 1120, 720, 760, 840, 920, 1040, 1120, 1230, 0, 40, 120, 200, 320, 400, 520, 120, 160, 240, 320, 440, 520, 640, 240, 280, 360, 440, 560, 640, 760, 360, 400, 480, 560, 680, 760, 880, 480, 520, 600, 680, 800, 880, 1000, 600, 640, 720, 800, 920, 1000, 1120, 720, 760, 840, 920, 1040, 1120, 1240 };
    static Dictionary<(int, int, int, int), int> dt = new();
    static void Init()
    {
        int x = 0;
        for (int m = 1; m <= 5; ++m)
            for (int n = 1; n <= 5; ++n)
                for (int i = 0; i <= 6; ++i)
                    for (int e = 0; e <= 6; ++e)
                        dt[(m, n, i, e)] = TA[x++];
    }

    public int GetMaxGridHappiness_DT(int m, int n, int ic, int ec)
    {
        if (!dt.Any()) Init();
        return dt[(m, n, ic, ec)];
    }
}
