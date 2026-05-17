using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest;

// 第428场周赛 by hccyril，2024/12/15 https://leetcode.com/contest/weekly-contest-428/
// 23902, 79 AK
//# rank      score   time        A           B           C           D
//# 775/23902  11    01:25:02    00:08:58    N/A          N/A     01:25:02
//# 做完A之后做D，做完D之后剩下不到5分钟，BC都没时间做
//# D题估计很难（全球101人做出来），后续补充难度分
// 比赛时AD均用python完成，本代码为赛后重做
internal class LC_WC0428_20241215
{
    #region Problem A
    public int ButtonWithLongestTime(int[][] events)
    {
        int n = events.Length;
        var cn = new Counter<int>();
        cn[events[0][0]] = events[0][1];
        for (int i = 1; i < n; ++i)
            cn[events[i][0]] = Math.Max(cn[events[i][0]], events[i][1] - events[i - 1][1]);
        return cn.Items
            .OrderByDescending(t => t.Value)
            .ThenBy(t => t.Key)
            .Select(t => t.Key)
            .First();
    }
    #endregion

    #region Problem B
    // treedfs * 2
    public double MaxAmount(string initialCurrency, IList<IList<string>> pairs1, double[] rates1, IList<IList<string>> pairs2, double[] rates2)
    {
        void Dfs(StarGraph<string, double> g, string cur, string parent, Dictionary<string, double> map)
        {
            double val = map[cur];
            foreach ((string nt, double w) in g[cur])
                if (nt != parent)
                {
                    map[nt] = val * w;
                    Dfs(g, nt, cur, map);
                }
        }

        Dictionary<string, double> map2 = new();
        StarGraph<string, double> g2 = new();
        for (int i = 0; i < pairs2.Count; ++i)
        {
            g2.AddEdge(pairs2[i][0], pairs2[i][1], 1.0 / rates2[i]);
            g2.AddEdge(pairs2[i][1], pairs2[i][0], rates2[i]);
        }
        map2[initialCurrency] = 1.0;
        Dfs(g2, initialCurrency, "", map2);

        Dictionary<string, double> map1 = new();
        StarGraph<string, double> g1 = new();
        for (int i = 0; i < pairs1.Count; ++i)
        {
            g1.AddEdge(pairs1[i][0], pairs1[i][1], rates1[i]);
            g1.AddEdge(pairs1[i][1], pairs1[i][0], 1.0 / rates1[i]);
        }
        map1[initialCurrency] = 1.0;
        Dfs(g1, initialCurrency, "", map1);
        return map1.Select(t => map2.TryGetValue(t.Key, out double v) ? t.Value * v : 0.0).Max();
    }
    #endregion

    #region Problem C
    // LCP问题，可用Z函数求解
    public int BeautifulSplits(int[] nums)
    {
        int n = nums.Length;
        var z0 = nums.ZFun();
        int ans = 0;
        for (int i = 1; i < n - 1; ++i)
        {
            int m = n - i;
            var z = nums[i..].ZFun();
            for (int j = 1; j < m; ++j)
                if (j >= i && z0[i] >= i || z[j] >= j)
                    ++ans;
        }
        return ans;
    }

    // WA1:[1,1,0,1,3,2,2,2]
    // WA2:[0,0,0,0,2,2,0,1,2,1,2]
    public int BeautifulSplits_WA(int[] nums)
    {
        int n = nums.Length;
        var z0 = nums.ZFun();
        int ans = 0;
        for (int i = 1; i < n - 1; ++i)
        {
            if (z0[i] >= i)
            {
                ans += n - (i << 1);
            }
            else
            {
                int m = n - i;
                var z = nums[i..].ZFun();
                for (int j = 1; j < m - 1; ++j)
                    if (z[j] >= j)
                        ++ans;
            }
        }
        return ans;
    }
    #endregion

    #region Problem D
    // 最终good时，所有字母的出现次数要么为0，要么为t，t是可以枚举的
    // 给定t，求最小操作次数，用DP
    // d0[i]: 前(i+1)个字符good，且最后一个字母调整到0个
    // dt[i]: 前(i+1)个字符good，且最后一个字母调整到t个
    // 关键点：触发操作3当且仅当前一个字母可以减少，而且当前字母可以增加，因此这是一个被动技能而且不会触发连招
    //      因此，计算操作3的数量可以用贪心策略，使得这成为了本题解题关键
    // 时间复杂度O(26*T)，T为最大出现次数减最小出现次数
    public int MakeStringGood(string s)
    {
        Span<int> a = stackalloc int[26];
        foreach (char c in s)
            a[c - 'a']++;
        int mi = s.Length, ma = 0;
        for (int i = 0; i < 26; ++i)
        {
            mi = Math.Min(mi, a[i]);
            ma = Math.Max(ma, a[i]);
        }
        int ans = (ma - mi) * 26;
        for (int t = mi; t <= ma; ++t)
        {
            int d0 = a[0], dt = Math.Abs(a[0] - t);
            for (int i = 1; i < 26; ++i)
            {
                int e0 = Math.Min(d0, dt) + a[i];
                int et = Math.Min(d0, dt) + Math.Abs(a[i] - t);
                if (a[i - 1] > t && a[i] < t)
                    et = Math.Min(et, dt + t - (a[i] + Math.Min(a[i - 1] - t, t - a[i])));
                if (a[i - 1] > 0 && a[i] < t)
                    et = Math.Min(et, d0 + t - (a[i] + Math.Min(a[i - 1], t - a[i])));
                (d0, dt) = (e0, et);
            }
            ans = Math.Min(ans, Math.Min(d0, dt));
        }
        return ans;
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
        LC_WC0428_20241215 sln = new();

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
        return 1215;
    }

    int RunTestE()
    {
        return 0;
    }
    #endregion
}
