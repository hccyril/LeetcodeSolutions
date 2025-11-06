using System;
using System.Linq;
namespace ConsoleCore1;

// hard, 2023/7/10 practice
// 挑战成功，用时38分42秒
// 转换成0-1背包问题，背包从小到大去装，能装完则返回True
internal class P1655分配重复整数
{
    /// <summary>
    /// 01背包（根据模板修改），将每个顾客需求看作一件物品，重量和价值均为a[i]
    /// </summary>
    /// <param name="c">背包容量</param>
    /// <param name="a">原数组，即quantity</param>
    /// <param name="mp">状态压缩，第i位为1表示第i个物品之前已经装入背包了</param>
    /// <returns>返回（最大价值，装入物品的状压）</returns>
    (int, int) Knapsack(int c, int[] a, int mp)
    {
        int n = a.Length;
        int[] ia = Enumerable.Range(0, n).Where(i => (mp & 1 << i) == 0).ToArray();
        int m = ia.Length;
        (int, int)[] dp = new (int, int)[c + 1];
        for (int i = 0; i < m; ++i)
            for (int j = c; j >= a[ia[i]]; --j)
            {
                if (a[ia[i]] + dp[j - a[ia[i]]].Item1 > dp[j].Item1)
                    dp[j] = (a[ia[i]] + dp[j - a[ia[i]]].Item1, dp[j - a[ia[i]]].Item2 | 1 << ia[i]);
            }
        return dp[^1];
    }

    // 将nums进行Counter计算，结果看作不超过50个背包
    // 将背包从小到大进行0-1背包计算，最后能全部装完返回true
    public bool CanDistribute(int[] nums, int[] quantity)
    {
        int[] cn = new int[1001];
        foreach (int x in nums)
            ++cn[x];
        int[] ca = Enumerable.Range(1, 1000).Where(i => cn[i] > 0).Select(i => cn[i]).OrderBy(t => t).ToArray();
        int m = quantity.Length, fm = (1 << m) - 1, mp = 0;
        foreach (int c in ca)
        {
            (int v, int s) = Knapsack(c, quantity, mp);
            mp |= s;
            if (mp == fm) return true;
        }
        return false;
    }

    internal static void Run()
    {
        int[] a = { 1, 2, 3, 3 },
              b = { 2 };
        var sln = new P1655分配重复整数();
        var r = sln.CanDistribute(a, b);
        Console.WriteLine("r=" + r);
    }
}

