using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/5/13
    // rank: 2576
    // DP (n,k,c,x) = L (前n个字符删除k个，后缀为连续x个c）时最短长度为L
    internal class P1531压缩字符串II
    {
        (int, int) Plus((int, int) t) => t.Item1 switch
        {
            1 or 9 or 99 => (t.Item1 + 1, t.Item2 + 1),
            _ => (t.Item1 + 1, t.Item2)
        };

        Dictionary<(char, int), int>[,] dp;

        public int GetLengthOfOptimalCompression(string s, int k)
        {
            if (k >= s.Length) return 0;

            dp = new Dictionary<(char, int), int>[s.Length, k + 1];

            for (int i = 0; i < s.Length; ++i)
                for (int j = 0; j <= k; ++j)
                    dp[i, j] = new();
            dp[0, 0].Add((s[0], 1), 1);
            for (int i = 1; i < s.Length; ++i)
            {
                ((char c, int x), int l) = dp[i - 1, 0].First();
                (x, l) = s[i] == s[i - 1] ? Plus((x, l)) : (1, l + 1);
                dp[i, 0].Add((s[i], x), l); // 写错啦！！我X!!!
                // dp[i, 0].Add((c, x), l);
            }

            for (int j = 1; j <= k; ++j)
                for (int i = j; i < s.Length; ++i)
                {
                    // remove s[i]
                    foreach ((var key, var val) in dp[i - 1, j - 1])
                        dp[i, j].Add(key, val);

                    // keep s[i]
                    if (i == j)
                        dp[i, j][(s[i], 1)] = 1;
                    else
                        foreach (((char c, int x), int l) in dp[i - 1, j])
                        {
                            (int a, int b) = s[i] == c ? Plus((x, l)) : (1, l + 1);
                            if (!dp[i, j].ContainsKey((s[i], a)) || dp[i, j][(s[i], a)] > b)
                                dp[i, j][(s[i], a)] = b;
                        }
                }
            return dp[s.Length - 1, k].Values.Min();
        }

        // ver2: WA2
        //(int, int) Plus((int, int) t) => t.Item1 switch
        //{
        //    1 or 9 or 99 => (t.Item1 + 1, t.Item2 + 1),
        //    _ => (t.Item1 + 1, t.Item2)
        //};

        //Dictionary<(char, int), int>[,] dp;

        //public int GetLengthOfOptimalCompression(string s, int k)
        //{
        //    if (k >= s.Length) return 0;

        //    dp = new Dictionary<(char, int), int>[s.Length, k + 1];

        //    for (int i = 0; i < s.Length; ++i)
        //        for (int j = 0; j <= k; ++j)
        //            dp[i, j] = new();
        //    dp[0, 0].Add((s[0], 1), 1);
        //    for (int i = 1; i < s.Length; ++i)
        //    {
        //        ((char c, int x), int l) = dp[i - 1, 0].First();
        //        (x, l) = s[i] == s[i - 1] ? Plus((x, l)) : (1, l + 1);
        //        dp[i, 0].Add((c, x), l);
        //    }

        //    for (int j = 1; j <= k; ++j)
        //        for (int i = j; i < s.Length; ++i)
        //        {
        //            // remove s[i]
        //            foreach ((var key, var val) in dp[i - 1, j - 1])
        //                dp[i, j].Add(key, val);

        //            // keep s[i]
        //            if (i == j)
        //                dp[i, j][(s[i], 1)] = 1;
        //            else
        //                foreach (((char c, int x), int l) in dp[i - 1, j])
        //                {
        //                    (int a, int b) = s[i] == c ? Plus((x, l)) : (1, l + 1);
        //                    if (!dp[i, j].ContainsKey((s[i], a)) || dp[i, j][(s[i], a)] > b)
        //                        dp[i, j][(s[i], a)] = b;
        //                }
        //        }
        //    return dp[s.Length - 1, k].Values.Min();
        //}

        // ver1: WA1
        //(int, int) Plus((int, int) t) => t.Item1 switch
        //{
        //    1 or 9 or 99 => (t.Item1 + 1, t.Item2 + 1),
        //    _ => (t.Item1 + 1, t.Item2)
        //};

        //SortedList<char, (int, int)>[,] dp;

        //public int GetLengthOfOptimalCompression(string s, int k)
        //{
        //    if (k >= s.Length) return 0;

        //    dp = new SortedList<char, (int, int)>[s.Length, k + 1];

        //    for (int i = 0; i < s.Length; ++i)
        //        for (int j = 0; j <= k; ++j)
        //            dp[i, j] = new();
        //    dp[0, 0].Add(s[0], (1, 1));
        //    for (int i = 1; i < s.Length; ++i)
        //        dp[i, 0].Add(s[i], s[i] == s[i - 1] ? Plus(dp[i - 1, 0][s[i]]) : (1, dp[i - 1, 0].First().Value.Item2 + 1));

        //    for (int j = 1; j <= k; ++j)
        //        for (int i = j; i < s.Length; ++i)
        //        {
        //            // remove s[i]
        //            foreach ((var key, var val) in dp[i - 1, j - 1])
        //                dp[i, j].Add(key, val);

        //            // keep s[i]
        //            if (i == j)
        //            {
        //                dp[i, j][s[i]] = (1, 1);
        //            }
        //            else
        //            {
        //                int cnt = 0, len = s.Length;
        //                if (dp[i, j].ContainsKey(s[i]))
        //                    (cnt, len) = dp[i, j][s[i]];
        //                foreach ((var c, (int x, int l)) in dp[i - 1, j])
        //                {
        //                    (int a, int b) = s[i] == c ? Plus((x, l)) : (1, l + 1);
        //                    if (b < len || b == len && a > cnt)
        //                        (cnt, len) = (a, b);
        //                }
        //                dp[i, j][s[i]] = (cnt, len);
        //            }
        //        }
        //    return dp[s.Length - 1, k].Select(t => t.Value.Item2).Min();
        //}

        internal static void Run()
        {
            // base input
            //string s = "aaabcccd"; int k = 2;

            //// WA1: my: 13 exp: 12
            //string s = "aaabaaaabbbaaabbbbbbbbbaaababbbbabbbabbbbbaaaaabab"; int k = 8;

            // WA2: my: 3 exp: 4
            string s = "abbabaaa"; int k = 2;

            var sln = new P1531压缩字符串II();
            var ans = sln.GetLengthOfOptimalCompression(s, k);
            for (int i = 0; i < s.Length; ++i)
            {
                for (int j = 0; j <= k; ++j)
                {
                    Console.WriteLine($"[{i},{j}]: " + string.Join(" ", sln.dp[i, j].Select(t => $"({t.Key.Item1} {t.Key.Item2} {t.Value})")));
                }
                Console.WriteLine();
            }
            Console.WriteLine("ans=" + ans);
        }
    }
}
