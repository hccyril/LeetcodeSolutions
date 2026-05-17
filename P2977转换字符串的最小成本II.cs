using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2026/1/30 Daily
// WC377Q4 rating 2696
// AC自动机+Floyd+DP
internal class P2977转换字符串的最小成本II
{
    class HashSetPack
    {
        public HashSet<string> hs = [];
        public HashSet<string> ht = [];
    }

    // 版本2：优化Floyd部分
    public long MinimumCost(string source, string target, string[] original, string[] changed, int[] cost)
    {
        int n = source.Length, m = original.Length;

        // AC自动机
        AcAuto ac = new(original);

        // Floyd
        Dictionary<string, long> di = [];
        void Update(string a, string b, long c)
        {
            string key = $"{a}-{b}";
            if (!di.TryGetValue(key, out long value) || value > c)
                di[key] = c;
        }
        Dictionary<int, HashSetPack> mp = [];
        for (int i = 0; i < m; ++i)
        {
            string s = original[i], t = changed[i];
            long c = cost[i];
            int d = s.Length;
            if (mp.TryGetValue(d, out var p))
            {
                p.hs.Add(s);
                p.ht.Add(t);
            }
            else
            {
                p = new();
                p.hs.Add(s);
                p.ht.Add(t);
                mp[d] = p;
            }
            Update(s, t, c);
        }
        foreach ((int d, var p) in mp)
        {
            var hs = p.hs;
            var ht = p.ht;
            foreach (var t in hs.Intersect(ht))
                foreach (var u in hs)
                    if (u != t && di.TryGetValue($"{u}-{t}", out long cu))
                        foreach (var v in ht)
                            if (t != v && u != v && di.TryGetValue($"{t}-{v}", out long cv))
                                Update(u, v, cu + cv);
        }

        // DP
        Span<long> dp = stackalloc long[n];
        for (int i = 0; i < n; ++i)
        {
            dp[i] = long.MaxValue;
            if (source[i] == target[i])
                dp[i] = i > 0 ? dp[i - 1] : 0;
            foreach (var s in ac.Query(source[i]))
            {
                int d = s.Length;
                if ((d == i + 1 || dp[i - d] != long.MaxValue) && di.TryGetValue($"{s}-{target[(i - d + 1)..(i + 1)]}", out var c))
                {
                    dp[i] = Math.Min(dp[i], (d == i + 1 ? 0L : dp[i - d]) + c);
                }
            }
        }

        return dp[n - 1] == long.MaxValue ? -1 : dp[n - 1];
    }

    // 版本1：US能过，CN超时
    public long MinimumCost_TLE(string source, string target, string[] original, string[] changed, int[] cost)
    {
        int n = source.Length, m = original.Length;

        // AC自动机
        AcAuto ac = new(original);

        // Floyd
        Dictionary<string, long> di = [];
        void Update(string a, string b, long c)
        {
            string key = $"{a}-{b}";
            if (!di.TryGetValue(key, out long value) || value > c)
                di[key] = c;
        }
        HashSet<string> hs = [.. original], ht = [.. changed], hi = [.. hs.Intersect(ht)];
        for (int i = 0; i < m; ++i)
            Update(original[i], changed[i], cost[i]);
        foreach (var t in hi)
            foreach (var u in hs)
                foreach (var v in ht)
                    if (u != v && di.TryGetValue($"{u}-{t}", out long cu) && di.TryGetValue($"{t}-{v}", out long cv))
                        Update(u, v, cu + cv);

        // DP
        Span<long> dp = stackalloc long[n];
        for (int i = 0; i < n; ++i)
        {
            dp[i] = long.MaxValue;
            if (source[i] == target[i])
                dp[i] = i > 0 ? dp[i - 1] : 0;
            foreach (var s in ac.Query(source[i]))
            {
                int d = s.Length;
                if ((d == i + 1 || dp[i - d] != long.MaxValue) && di.TryGetValue($"{s}-{target[(i - d + 1)..(i + 1)]}", out var c))
                {
                    dp[i] = Math.Min(dp[i], (d == i + 1 ? 0L : dp[i - d]) + c);
                }
            }
        }

        return dp[n - 1] == long.MaxValue ? -1 : dp[n - 1];
    }
}
