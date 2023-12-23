using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

internal class P2048下一个更大的数值平衡数
{
    // 《中等题》2023/12/9 Daily
    // n只有1e6，所以数字只会有1-6，可以枚举子集（只有64种情况）
    // 对于每个子集先统计位数，如果位数不够或者位数大于等于len(n)+2，肯定不是最优解
    // 如果位数等于len(n)+1，说明一定比n大，则只测试最小的那个
    // 如果位数等于len(n)，使用回溯找出最小的满足要求的数
    public int NextBeautifulNumber(int n)
    {
        ++n; // 严格大于n == 大于等于n+1
        var na = n.EnumDigits().ToArray(); // n.ToString().Select(c => c - '0').ToArray();
        int nlen = na.Length; //n.DigitCount(); //n.ToString().Length;
        int[] bits = { 0, 1, 2, 4, 8, 16, 32 };
        int ans = -1;

        void Dfs(int i, int[] cnt, int pre)
        {
            if (i == na.Length)
            {
                if (ans < 0 || pre < ans) ans = pre;
                return;
            }
            int c = na[i];
            for (int d = 1; d <= 6; ++d)
                if (cnt[d] > 0)
                    if (d == c)
                    {
                        --cnt[d];
                        Dfs(i + 1, cnt, pre * 10 + d);
                        ++cnt[d];
                    }
                    else if (d > c)
                    {
                        int t = pre * 10 + d;
                        --cnt[d];
                        for (int r = 1; r <= 6; ++r)
                            for (int rc = 0; rc < cnt[r]; ++rc)
                                t = t * 10 + r;
                        if (ans < 0 || t < ans) ans = t;
                        ++cnt[d];
                        return;
                    }
        }

        for (int m = 0; m < 64; ++m)
        {
            int l = Enumerable.Range(1, 6).Where(d => (m & bits[d]) != 0).Sum();
            if (l == nlen)
            {
                Dfs(0, Enumerable.Range(0, 7).Select(d => (m & bits[d]) != 0 ? d : 0).ToArray(), 0);
            }
            else if (l == nlen + 1)
            {
                int t = 0;
                foreach (int d in Enumerable.Range(1, 6).Where(d => (m & bits[d]) != 0))
                    for (int i = 0; i < d; ++i)
                        t = t * 10 + d;
                if (ans < 0 || t < ans) ans = t;
            }
        }
        return ans;
    }

    internal static void Run()
    {
        var sln = new P2048下一个更大的数值平衡数();
        int n = 620883; // 3000;
        Console.WriteLine("P2048 n={0} ans={1}", n, sln.NextBeautifulNumber(n));
    }
        
}
