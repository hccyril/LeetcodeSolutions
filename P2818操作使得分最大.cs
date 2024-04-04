using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// 2023/12/23 rating 2396
// 挑战失败：WA 3 + TLE 1，做到凌晨2点才通过
// 巨坑：没考虑到因子重复的情况，例如16的prime score也是1
// 巨坑2：沿用求素数的方法，循环到sqrt(n)就停止了，但对于本题来说要遍历完一直到n
internal class P2818操作使得分最大
{
    static int[] factor = Array.Empty<int>();
    static int[] FactorTable()
    {
        int n = 100000;
        var fl = new List<int>[n + 1];
        int i, j;
        for (i = 0; i <= n; ++i) fl[i] = new();
        for (i = 2; i <= n; fl[i].Add(2), i += 2) ;
        for (i = 3; i <= n; i++)
            if (!fl[i].Any())
                for (j = i; j <= n; fl[j].Add(i), j += i) ;
        return Enumerable.Range(0, n + 1).Select(i => fl[i].Count).ToArray();
    }
    public int MaximumScore(IList<int> nums, int k)
    {
        if (factor.Length == 0) 
            factor = FactorTable();
        int n = nums.Count, ans = 1;
        var sa = Enumerable.Range(0, n).Select(i => (nums[i], i)).OrderByDescending(t => t).ToArray();
        foreach ((int x, int i) in sa)
        {
            int l = 1, r = 1, s = factor[x];
            while (i - l >= 0 && factor[nums[i - l]] < s) ++l;
            while (i + r < n && factor[nums[i + r]] <= s) ++r;
            int p = Math.Min(l * r, k);
            k -= p;
            // 要用快速幂，否则会超时
            ans = ans.Multi(x.Pow(p)); //while (p-- > 0) ans = ans.Multi(x);
            if (k == 0) break;
        }
        return ans;
    }

    // BruteForce，校验用
    public int MaximumScore_BF(IList<int> nums, int k)
    {
        int GetFactor(int x)
        {
            int c = 0;
            for (int i = 2; i * i <= x; ++i) 
                if (x % i == 0)
                {
                    ++c;
                    while (x % i == 0) x /= i;
                }
            if (x > 1) ++c;
            return c;
        }
        if (factor.Length == 0) factor = FactorTable();
        int n = nums.Count;

        int GetP(int l, int r)
        {
            int maxf = -1, x = 0;
            for (int i = l; i <= r; ++i)
            {
                int f = GetFactor(nums[i]);
                if (f != factor[nums[i]])
                    Console.WriteLine("{0} {1} {2}", nums[i], f, factor[nums[i]]);
                if (f > maxf)
                {
                    maxf = f; // factor[nums[i]];
                    x = nums[i];
                }
            }
            return x;
        }
        List<(int, int, int)> li = new();
        for (int l = 0; l < n; ++l)
            for (int r = l; r < n; ++r)
                li.Add((GetP(l, r), l, r));
        int ans = 1;
        foreach ((int x, int l, int r) in li.OrderByDescending(t => t).Take(k))
        {
            //Console.WriteLine("{0} {1} {2}", x, l, r);
            ans = ans.Multi(x);
        }

        return ans;
    }

    internal static void Run()
    {
        var sln = new P2818操作使得分最大();

        //// expected: 912532739
        //int[] nums = { 13, 16, 12, 15, 12, 1, 13, 1, 18, 1 };
        //int k = 46;

        // expected: 531428126
        int[] nums = { 1, 75866, 1, 92619, 1334, 29568, 62581, 53130, 94710, 72816, 87780, 67830, 20930, 49559, 50505, 63669, 33660, 42252, 457, 17339, 13668, 73583, 94603, 82062, 1, 21090, 8101, 90146, 86195, 43839, 7460, 30690, 21661, 1, 1, 12680, 68710, 96288, 57558, 10920, 94613, 1, 59960, 1, 63389, 19264, 51409 };
        int k = 106;
        Console.WriteLine("ans=" + sln.MaximumScore(nums, k));
    }
}

/**
 * backup
    static int[] factor = Array.Empty<int>();
    static int[] FactorTable()
    {
        int n = 100000;
        factor = new int[n + 1];
        Array.Fill(factor, 1);
        factor[0] = factor[1] = 0;
        int i, j;
        for (i = 4; i <= n; ++factor[i], i += 2) ;
        for (i = 3; i * i <= n; i++)
            if (factor[i] == 1)
                for (j = i + i; j <= n; ++factor[j], j += i) ;
        return factor;
    }
    public int MaximumScore(IList<int> nums, int k)
    {
        if (factor.Length == 0) FactorTable();
        int n = nums.Count, ans = 1;
        var sa = Enumerable.Range(0, n).Select(i => (nums[i], i)).OrderByDescending(t => t).ToArray();
        foreach ((int x, int i) in sa)
        {
            int l = 1, r = 1, s = factor[x];
            while (i - l >= 0 && factor[nums[i - l]] < s) ++l;
            while (i + r < n && factor[nums[i + r]] <= s) ++r;
            int p = Math.Min(l * r, k);
            k -= p;
            while (p-- > 0) ans = ans.Multi(x);
            if (k == 0) break;
        }
        return ans;
    }

    // BruteForce，校验用
    public int MaximumScore_BF(IList<int> nums, int k)
    {
        if (factor.Length == 0) FactorTable();
        int n = nums.Count;

        int GetP(int l, int r)
        {
            int maxf = 0, x = 0;
            for (int i = l; i <= r; ++i)
                if (factor[nums[i]] > maxf)
                {
                    maxf = factor[nums[i]];
                    x = nums[i];
                }
            return x;
        }
        List<(int, int, int)> li = new();
        for (int l = 0; l < n; ++l)
            for (int r = l; r < n; ++r)
                li.Add((GetP(l, r), l, r));
        int ans = 1;
        foreach ((int x, int l, int r) in li.OrderByDescending(t => t).Take(k))
        {
            Console.WriteLine("{0} {1} {2}", x, l, r);
            ans = ans.Multi(x);
        }

        return ans;
    }

    internal static void Run()
    {
        var sln = new P2818操作使得分最大();
        // expected: 912532739
        int[] nums = { 13, 16, 12, 15, 12, 1, 13, 1, 18, 1 };
        int k = 46;
        Console.WriteLine("ans=" + sln.MaximumScore_BF(nums, k));
    }
*/