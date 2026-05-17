using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// medium, 20260507 Daily
// rating 2187
internal class P3660跳跃游戏IX
{
    // 上一个版本还是WA，果然还是要用到二分查找
    // AC(终于)
    public int[] MaxValue(int[] nums)
    {
        int n = nums.Length, max_i = 0;
        UnionFind uni = new(n);
        int[] ans = nums.ToArray();
        List<(int, int)> m_stk = new();
        for (int i = 0; i < n; ++i)
        {
            if (nums[i] > nums[max_i])
            {
                max_i = i;
            }
            else if (nums[i] < nums[max_i])
            {
                uni.Union(max_i, i);
            }
            while (m_stk.Count > 0 && m_stk[^1].Item1 >= nums[i])
                m_stk.RemoveAt(m_stk.Count - 1);
            m_stk.Add((nums[i], i));
        }
        int BinarySearch(int x)
        {
            int f = m_stk.BinarySearch((x, 0));
            if (f < 0) f = ~f;
            return --f;
        }
        for (int i = n - 1; i >= 0; --i)
        {
            int f = BinarySearch(nums[i]);
            if (f >= 0)
            {
                int min_i = m_stk[f].Item2;
                if (min_i > i)
                {
                    ans[uni[i]] = Math.Max(ans[uni[i]], ans[uni[min_i]]);
                    uni.Union(i, min_i);
                }
            }
        }
        for (int i = 0; i < n; ++i)
            ans[i] = ans[uni[i]];

        return ans;
    }

    // 换了个思路，用并查集
    public int[] MaxValue_WA2(int[] nums)
    {
        int n = nums.Length, max_i = 0, min_i = n - 1;
        UnionFind uni = new(n);
        int[] ans = nums.ToArray();
        for (int i = 0; i < n; ++i)
        {
            if (nums[i] > nums[max_i])
            {
                max_i = i;
            }
            else if (nums[i] < nums[max_i])
            {
                uni.Union(max_i, i);
            }
        }
        for (int i = n - 1; i >= 0; --i)
        {
            if (nums[i] < nums[min_i])
            {
                min_i = i;
            }
            else if (nums[i] > nums[min_i])
            {
                ans[uni[i]] = Math.Max(ans[uni[i]], ans[uni[min_i]]); // fix WA
                uni.Union(i, min_i);
                //ans[uni[i]] = Math.Max(ans[uni[i]], ans[i]); // WA
            }
        }
        //Console.WriteLine(string.Join(" ", ans));
        //Console.WriteLine(string.Join(" ", uni.parent));
        for (int i = 0; i < n; ++i)
            ans[i] = ans[uni[i]];

        return ans;
    }

    // 这题还是有点坑，用了单调栈+二分查找，写了三个版本还是没写对
    // 现在看可能方向都错了，需要重新思考一下
    public int[] MaxValue_WA(int[] nums)
    {
        int n = nums.Length, ma = 0;
        int[] ans = nums.Select(x => ma = Math.Max(ma, x)).ToArray();
        List<int> m_stk = new();
        for (int i = 0; i < n; ++i)
        {
            while (m_stk.Any() && nums[m_stk[^1]] >= nums[i])
                m_stk.RemoveAt(m_stk.Count - 1);
            m_stk.Add(i);
        }
        // Console.WriteLine(string.Join(" ", m_stk));
        // Console.WriteLine(string.Join(" ", ans));
        for (int i = 0; i < n; ++i)
        {
            int x = nums[i], l = 0, r = m_stk.Count - 1;
            while (l < r)
            {
                int m = l + r + 1 >> 1;
                if (m_stk[m] <= i)
                    l = m + 1;
                else if (x <= nums[m_stk[m]])
                    r = m - 1;
                else
                    l = m;
            }
            // Console.WriteLine("i = {0} l = {1}", i, l);
            if (l >= 0 && l < m_stk.Count && m_stk[l] > i && x > nums[m_stk[l]])
                ans[i] = Math.Max(ans[i], ans[m_stk[l]]);
            //if (i > 0)
            //ans[i] = Math.Max(ans[i], ans[i - 1]);
        }
        return ans;
    }

    internal static void Run()
    {
        var sln = new P3660跳跃游戏IX();
        int[] a = { 2, 3, 1 };
        //int[] a = { 9, 30, 16, 6, 36, 9 }; // expected ans: [36,36,36,36,36,36]
        var ans = sln.MaxValue(a);
        Console.WriteLine(string.Join(" ", ans));
    }
}