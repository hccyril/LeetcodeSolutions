using System;
using System.Collections.Generic;

namespace ConsoleCore1;

// hard, 2024/7/7 WC405-D
// AC自动机
internal class P3213最小代价构造字符串
{
    public int MinimumCost(string target, string[] words, int[] costs)
    {
        var di = new Dictionary<string, int>();
        int m = words.Length;
        for (int i = 0; i < m; ++i)
        {
            string w = words[i]; int c = costs[i];
            if (!di.TryAdd(w, c))
                di[w] = Math.Min(c, di[w]);
        }
        AcAuto ac = new(di.Keys);
        Span<int> dp = stackalloc int[target.Length + 1];
        dp.Fill(999999999); dp[0] = 0;
        for (int i = 1; i <= target.Length; ++i)
        {
            var x = target[i - 1];
            foreach (var w in ac.Query(x))
            {
                int c = di[w];
                dp[i] = Math.Min(dp[i], dp[i - w.Length] + c);
            }
        }
        return dp[^1] >= 999999999 ? -1 : dp[^1];
    }

    internal static void Run()
    {
        var sln = new P3213最小代价构造字符串();
        string target = "abcdef";
        string[] words = { "abdef", "abc", "d", "def", "ef" };
        int[] costs = { 100, 1, 1, 10, 5 };
        Console.WriteLine(sln.MinimumCost(target, words, costs));
    }
}
