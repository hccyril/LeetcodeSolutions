using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2023/11/26 WC373-D
// 比赛时没做出来，比赛后才想到思路
internal class P2949统计美丽子字符串II
{
    // 设子串长度为L，可知(L/2)^2 % k == 0
    // 因此对于k的每个因子，L/2必须包含至少一半
    // 最后用同余原理快速统计
    public long BeautifulSubstrings(string s, int k)
    {
        long ans = 0L;
        int p = 2;
        foreach (var g in k.GetFactors().GroupBy(t => t))
        {
            int c = g.Count() + 1 >> 1;
            while (c-- > 0) p *= g.Key;
        }
        Dictionary<(int, int), int> di = new();
        int d = 0;
        di[(0, p - 1)] = 1;
        static bool IsVowel(char c) => c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u';
        for (int i = 0; i < s.Length; ++i)
        {
            d += IsVowel(s[i]) ? 1 : -1;
            int m = i % p;
            if (di.ContainsKey((d, m)))
                ans += di[(d, m)]++;
            else
                di[(d, m)] = 1;
        }
        
        return ans;
    }

    internal static void Run()
    {
        long ans1 = new P2949统计美丽子字符串II().BeautifulSubstrings("baeyh", 2);
        Console.WriteLine("ans1=" + ans1);
        long ans2 = new P2949统计美丽子字符串II().BeautifulSubstrings("abba", 1);
        Console.WriteLine("ans2=" + ans2);
    }
}
