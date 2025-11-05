using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2025/3/8 Daily
// rating 2561
internal class P2234花园的最大总美丽值
{
    // AC，其实线性枚举就好了
    public long MaximumBeauty(int[] flowers, long newFlowers, int target, int full, int partial)
    {
        Array.Sort(flowers);
        int n = flowers.Length;

        if (flowers[0] >= target)
            return (long)n * full;

        long ans = 0L;
        if (flowers.Select(f => f < target ? (long)(target - f) : 0L).Sum() <= newFlowers)
            ans = (long)n * full;

        int i = 0, j = n;
        long k = newFlowers;
        while (j > 1)
        {
            int m = Math.Max(target - flowers[j - 1], 0);
            if (k >= m)
            {
                k -= m;
                --j;
            }
            else break;
        }
        for (int pt = flowers[0]; pt < target; ++pt)
        {
            k -= i;
            while (k < 0 && j < n)
                k += Math.Max(target - flowers[j++], 0);

            while (i < j && flowers[i] <= pt)
            {
                k -= pt - flowers[i++];
                while (k < 0 && j < n)
                    k += Math.Max(target - flowers[j++], 0);
            }

            if (k >= 0)
                ans = Math.Max(ans, (long)pt * partial + (long)full * (n - j));
            else
                break;
        }
        return ans;
    }

    // ver1 WA
    // 证实了数据并不是山脉数组
    public long MaximumBeauty_WA(int[] flowers, long newFlowers, int target, int full, int partial)
    {
        Array.Sort(flowers);
        int n = flowers.Length;

        if (flowers[0] >= target)
            return (long)n * full;

        long GetBeauty(int pt)
        {
            int i = 0, j = n;
            long k = newFlowers;
            while (i < n && flowers[i] < pt)
            {
                k -= pt - flowers[i];
                if (k < 0) return 0L;
                ++i;
            }
            while (j > 1)
            {
                int f = Math.Max(flowers[j - 1], pt);
                k -= Math.Max(target - f, 0);
                if (k < 0) break;
                else --j;
            }
            return (long)pt * partial + (long)(n - j) * full;
        }

        int l = flowers[0], r = target - 1;
        while (r - l > 2)
        {
            int d = (r - l) / 3;
            long b1 = GetBeauty(l + d), b2 = GetBeauty(r - d);
            if (b1 == 0) // 即便加了这句也还是WA
                r = r - d - 1;
            else if (b1 == b2)
                (l, r) = (l + d, r - d);
            else if (b1 < b2)
                l = l + d + 1;
            else
                r = r - d - 1;
        }

        long ans = Enumerable.Range(l, r - l + 1).Select(GetBeauty).Max();
        if (flowers.Select(f => f < target ? (long)(target - f) : 0L).Sum() <= newFlowers)
        {
            long ans2 = (long)n * full;
            if (ans2 > ans) ans = ans2;
        }
        return ans;
    }
}
