using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// medium, 2024/8/21 Daily
// 二分+位运算
// 本题演示AllBitsCount方法的使用
internal class P3007价值和小于等于K的最大数字
{
    long Calc(long n, int x)
    {
        ++n;
        long b = 1L, ans = 0L;
        for (int i = 0; i <= 62; ++i)
        {
            if (b > n) break;
            if ((i + 1) % x == 0)
                ans += n.CountAllBit(i);
            b <<= 1;
        }
        return ans;
    }

    public long FindMaximumNumber(long k, int x)
    {
        long l = 1L, r = 2L;
        while (Calc(r, x) <= k)
        {
            l = r;
            r <<= 1;
        }
        --r;
        while (l < r)
        {
            long m = l + (r - l + 1 >> 1);
            if (Calc(m, x) <= k)
                l = m;
            else
                r = m - 1;
        }
        return l;
    }

    internal static void Run()
    {
        long k = 9; int x = 1;
        var sln = new P3007价值和小于等于K的最大数字();
        Console.WriteLine("ans=" + sln.FindMaximumNumber(k, x));
    }
}
