using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest;

/**
 * WC427 - by 157*
 * 比赛晚半小时开始，只做了一题，最后一题尝试用SortedSet[Interval]完成但后来发现考虑少了一种情况
 * 原因：未考虑到下面有个区间“包住”上面区间的情况
 * 新思路：考虑用差分+树状数组（更新：其他题解确实用树状数组，但自己没想到，想到了一个分治的方法通过了）
 * */
internal class LC_WC0427_20241208
{
    #region Problem A
    /*
    def constructTransformedArray(self, nums: List[int]) -> List[int]:
        return [nums[(i + x) % len(nums)] for i, x in enumerate(nums)]
     * */
    #endregion

    #region Problem B
    // B与D一样
    // or brute-force
    /*
    def maxRectangleArea(self, points: List[List[int]]) -> int:
        max_area = -1
        for s in combinations(points, 4):
            xs, ys = set(p[0] for p in s), set(p[1] for p in s)
            if len(xs) == 2 and len(ys) == 2:
                a, b = xs
                if a > b: a, b = b, a
                c, d = ys
                if c > d: c, d = d, c
                if not any(a < x < b and c <= y <= d or a <= x <= b and c < y < d for x, y in points):
                    max_area = max(max_area, abs(a - b) * abs(c - d))
        return max_area
     * */
    #endregion

    #region Problem C
    // （赛后才看的题）
    // k与n/k必定有一个很小，可以进行枚举
    public long MaxSubarraySum(int[] nums, int k)
    {
        int n = nums.Length, p = n / k;
        // 方法一：枚举k
        long F1()
        {
            long max_sum = Enumerable.Range(0, k).Select(i => (long)nums[i]).Sum();
            for (int start = 0; start < k; ++start)
            {
                long current_sum = 0L;
                for (int j = start + k; j <= n; j += k)
                {
                    long sm = Enumerable.Range(j - k, k).Select(i => (long)nums[i]).Sum();
                    current_sum = current_sum > 0 ? current_sum + sm : sm;
                    max_sum = Math.Max(max_sum, current_sum);
                }
            }
            return max_sum;
        }
        // 方法二：枚举k的倍数
        long F2()
        {
            long max_sum = Enumerable.Range(0, k).Select(i => (long)nums[i]).Sum();
            for (int win_size = k; win_size <= n; win_size += k)
            {
                long win_sum = Enumerable.Range(0, win_size).Select(i => (long)nums[i]).Sum();
                max_sum = Math.Max(max_sum, win_sum);
                for (int j = win_size; j < n; ++j)
                {
                    win_sum += nums[j] - nums[j - win_size];
                    max_sum = Math.Max(max_sum, win_sum);
                }
            }
            return max_sum;
        }
        return k < p ? F1() : F2();
    }
    #endregion

    #region Problem D
    // 尝试课本上讲的分治法，结果=求解左边+求解右边+计算左右连接的部分
    // 中间部分，构建单调栈，同样的y只需考虑中轴线最近的两个点
    // 时间复杂度O(n * log(n) ** 2)
    public long MaxRectangleArea(int[] xCoord, int[] yCoord)
    {
        int n = xCoord.Length;
        var a = Enumerable.Range(0, n)
            .Select(i => ((int x, int y))(xCoord[i], yCoord[i]))
            .OrderBy(t => t.x)
            .ThenBy(t => t.y)
            .ToArray();

        long Dac(int start, int end)
        {
            if (end - start + 1 < 4 || a[start].x == a[end].x) return -1L;
            long max_area = -1L;
            int mid = start + end >> 1, mid_x = a[mid].x;

            if (mid_x < a[end].x)
            {   // fix WA: adjust mid so that points with same x(==mid_x) are at the same side
                int mid_up = end;
                while (mid < mid_up)
                {
                    int m = mid + mid_up + 1 >> 1;
                    if (a[mid].x == a[m].x) mid = m;
                    else mid_up = m - 1;
                }
            }
            else
            {   // fix stack overflow: there must be a valid division
                int mid_up = mid - 1; (mid, mid_x) = (start, mid_x - 1);
                while (mid < mid_up)
                {
                    int m = mid + mid_up + 1 >> 1;
                    if (a[mid].x <= mid_x) mid = m;
                    else mid_up = m - 1;
                }
                mid_x = a[mid].x;
            }

            // calc middle
            Stack<(int, int, int)> stk = new();
            int x1 = -1, x2 = -1, y0 = -1;
            foreach (int i in Enumerable.Range(start, end - start + 1)
                .OrderBy(i => a[i].y).Append(-1))
            {
                (int x, int y) = i < 0 ? (mid_x, 80000001) : a[i];
                if (y > y0)
                {
                    if (x1 >= 0 && x2 >= 0)
                    {
                        while (stk.Any())
                        {
                            (int xl, int xr, int yb) = stk.Peek();
                            if (x1 < xl && x2 > xr)
                                break;
                            else
                            {
                                if (x1 == xl && x2 == xr)
                                    max_area = Math.Max(max_area, (long)(x2 - x1) * (y0 - yb));
                                stk.Pop();
                            }
                        }

                        stk.Push((x1, x2, y0));
                    }
                    else if (x1 >= 0)
                    {
                        while (stk.Any() && stk.Peek().Item1 <= x1)
                            stk.Pop();
                    }
                    else if (x2 >= 0)
                    {
                        while (stk.Any() && stk.Peek().Item2 >= x2)
                            stk.Pop();
                    }
                    (x1, x2, y0) = (-1, -1, y);
                }

                if (x <= mid_x && (x1 < 0 || x > x1)) x1 = x;
                else if (x > mid_x && (x2 < 0 || x < x2)) x2 = x;
            }

            // divide
            max_area = Math.Max(max_area, Dac(start, mid));
            max_area = Math.Max(max_area, Dac(mid + 1, end));
            return max_area;
        }

        return Dac(0, n - 1);
    }

    // 失败尝试，没有考虑后面区间完全包住前面区间的情况，具体见测试样例
    public long MaxRectangleArea_WA(int[] xCoord, int[] yCoord)
    {
        int n = xCoord.Length;
        var a = Enumerable.Range(0, n)
            .Select(i => ((int x, int y))(xCoord[i], yCoord[i]))
            .OrderBy(t => t.x)
            .ThenBy(t => t.y)
            .ToArray();
        long max_area = 0L;
        SortedSet<Interval> s = new();
        int j = 0;
        for (int i = 0; i < n; i = j)
        {
            while (j < n && a[j].x == a[i].x) ++j;
            // a[i] till a[j - 1] are same x points
            // 1. get area
            List<Interval> li = new();
            for (int k = i + 1; k < j; ++k)
            {
                Interval iv = new(a[k - 1].y, a[k].y); iv.val = a[k].x;
                if (s.TryGetValue(iv, out Interval i0) && i0.start == iv.start && i0.end == iv.end)
                {
                    max_area = Math.Max(max_area, (long)(iv.val - i0.val) * (iv.end - iv.start));
                    Console.WriteLine("x1 x2 y1 y2 = {0} {1} {2} {3} area={4}", i0.val, iv.val, iv.start, iv.end, max_area);
                }
                li.Add(iv);
            }
            // 2. remove old
            for (int k = i; k < j; ++k)
            {
                Interval ic = new(a[k].y);
                if (s.TryGetValue(ic, out Interval i0))
                    s.Remove(i0);
            }
            // 3. add new
            li.ForEach(t => s.Add(t));
        }
        return max_area;
    }
    #endregion

    #region Problem E
    public int SolveE(int x)
    {
        return x;
    }
    #endregion

    #region Run Test
    internal static int Run()
    {
        char p = 'D';
        LC_WC0427_20241208 sln = new();

        return p switch
        {
            'A' => sln.RunTestA(),
            'B' => sln.RunTestB(),
            'C' => sln.RunTestC(),
            'D' => sln.RunTestD(),
            'E' => sln.RunTestE(),
            _ => -1
        };
    }

    int RunTestA()
    {
        return 0;
    }

    int RunTestB()
    {
        return 0;
    }

    int RunTestC()
    {
        return 0;
    }

    int RunTestD()
    {
        // simple case 2
        //int[] x = { 1, 1, 3, 3, 2 };
        //int[] y = { 1, 3, 1, 3, 2 };
        // WA
        //int[] x = { 31, 14, 31, 14, 15, 74, 79, 79, 83, 11, 45, 16, 11, 77, 28, 61, 85, 7, 76, 90 };
        //int[] y = { 45, 74, 74, 45, 12, 51, 25, 15, 70, 65, 11, 82, 58, 33, 13, 28, 2, 7, 47, 54 };
        // WA2 expect=53
        //int[] x = { 1, 0, 1, 0, 55, 26, 41, 30, 29, 89, 81, 53, 74, 20, 50, 80, 13, 55, 67, 16 };
        //int[] y = { 92, 39, 39, 92, 57, 35, 94, 44, 17, 54, 50, 8, 75, 23, 98, 24, 90, 96, 2, 34 };
        // stack overflow (递归死循环)
        int[] x = { 9, 3, 9, 3, 3, 0, 98, 47, 50, 35, 87, 40, 3, 70, 30, 8, 19, 44, 87, 43 };
        int[] y = { 98, 68, 68, 98, 59, 33, 23, 34, 98, 58, 88, 79, 76, 85, 84, 56, 96, 36, 90, 19 };
        return (int)MaxRectangleArea(x, y);
    }

    int RunTestE()
    {
        return 0;
    }
    #endregion
}
