using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

internal class P2926平衡子序列的最大和
{
    // Hard, WC370-D, 2023/11/5
    // C++ map方法，C++已提交通过
    // 翻过来C#，在最大case用时24656ms，猜测ElementAt方法并没有优化，而是用线性查找
    public long MaxBalancedSubsequenceSum_TLE(int[] nums)
    {
        long ans = 0L;
        int n = nums.Length;
        int ma = nums.Max();
        if (ma < 0) return ma;
        int[] a = Enumerable.Range(0, n).Select(i => nums[i] - i).ToArray();
        SortedDictionary<int, long> sd = new();
        
        for (int i = 0; i < n; ++i)
        {
            if (nums[i] <= 0) continue;
            int x = a[i]; long val = nums[i];
            if (!sd.Any())
            {
                sd.Add(x, val);
                ans = Math.Max(ans, val);
            }
            else
            {
                int start = UpperBound(sd, x);
                if (start > 0)
                    val += sd.ElementAt(start - 1).Value;
                List<int> removeKeys = new();
                for (int end = start; end < sd.Count; ++end)
                {
                    var kv = sd.ElementAt(end);
                    if (kv.Value <= val)
                        removeKeys.Add(kv.Key);
                    else
                        break;
                }
                foreach (int key in removeKeys)
                    sd.Remove(key);
                ans = Math.Max(ans, sd[x] = val);
            }
        }

        return ans;
    }

    static int UpperBound(SortedDictionary<int, long> sd, int x)
    {
        if (sd.ElementAt(^1).Key <= x)
            return sd.Count;
        int l = 0, r = sd.Count - 1;
        while (l < r)
        {
            int m = l + r >> 1;
            if (sd.ElementAt(m).Key > x)
                r = m;
            else
                l = m + 1;
        }
        return l;
    }

    // 方法二：尝试用SortedSet重写，本地实测2秒多，比SortedDictionary更优，但是仍然超时
    public long MaxBalancedSubsequenceSum_2(int[] nums)
    {
        long ans = 0L;
        int n = nums.Length;
        int ma = nums.Max();
        if (ma < 0) return ma;
        int[] a = Enumerable.Range(0, n).Select(i => nums[i] - i).ToArray();
        SortedSet<(int, long)> ss = new();

        for (int i = 0; i < n; ++i)
        {
            if (nums[i] <= 0) continue;
            int x = a[i]; long val = nums[i];
            if (!ss.Any())
            {
                ss.Add((x, val));
                ans = Math.Max(ans, val);
            }
            else
            {
                SortedSet<(int, long)> lb = ss.GetViewBetween((-2147483648, 0L), (x + 1, 0L)),
                    ub = ss.GetViewBetween((x + 1, 0L), (2147483647, 0L));
                if (lb.Any())
                    val += lb.Max.Item2;

                List<(int, long)> removeKeys = new();
                foreach ((int k, long v) in ub)
                {
                    if (v <= val)
                        removeKeys.Add((k, v));
                    else
                        break;
                }

                foreach (var key in removeKeys)
                    ss.Remove(key);
                ss.Add((x, val));
                ans = Math.Max(ans, val);
            }
        }

        return ans;
    }

    // 参考：使用max树状数组
    public long MaxBalancedSubsequenceSum(int[] nums)
    {
        int n = nums.Length;
        int[] b = new int[n];
        for (int i = 0; i < n; i++)
        {
            b[i] = nums[i] - i;
        }
        Array.Sort(b);

        BIT t = new BIT(b.Length + 1);
        long ans = long.MinValue;
        for (int i = 0; i < n; i++)
        {
            // j 为 nums[i]-i 离散化后的值（从 1 开始）
            int j = Array.BinarySearch(b, nums[i] - i) + 1;
            long f = Math.Max(t.PreMax(j), 0) + nums[i];
            ans = Math.Max(ans, f);
            t.Update(j, f);
        }
        return ans;
    }

    internal static void Run()
    {
        var sln = new P2926平衡子序列的最大和();

        //int[] nums = { 3, 3, 5, 6 };
        //long ans = sln.MaxBalancedSubsequenceSum(nums);
        //Console.WriteLine("ans={0} expected={1}", ans, 14);

        int[] nums = Common.ReadArray(2926);
        Console.WriteLine("input size={0}", nums.Length);
        long ans = sln.MaxBalancedSubsequenceSum(nums);
        Console.WriteLine("ans={0} expected={1}", ans, "?");
    }
}

// 树状数组模板（维护前缀最大值）
class BIT
{
    readonly long[] tree;

    public BIT(int n)
        => tree = Enumerable.Range(0, n).Select(_ => long.MinValue).ToArray();

    public void Update(int i, long val)
    {
        for (; i < tree.Length; tree[i] = Math.Max(tree[i], val), i += i & -i) ;
    }

    public long PreMax(int i)
    {
        long res = long.MinValue;
        for (; i > 0; res = Math.Max(res, tree[i]), i &= i - 1) ;
        return res;
    }
}
