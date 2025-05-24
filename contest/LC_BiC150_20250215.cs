using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest;

// 150双周赛 by main acct
// 80/1591  15  01:13:26    A-00:04:06    B-00:19:52    C-TLE    D-01:08:26*1
// D题幸好有KMP模板可以套，C题用之前的模板超时了，所以用差分不行，还需要线段树扫描线方法
internal class LC_BiC150_20250215
{
    #region Problem A
    public int SumOfGoodNumbers(int[] nums, int k)
    {
        int n = nums.Length, sm = 0;
        for (int i = 0; i < n; ++i)
        {
            bool ok = true;
            if (i - k >= 0 && nums[i - k] >= nums[i]) ok = false;
            if (i + k < n && nums[i + k] >= nums[i]) ok = false;
            if (ok) sm += nums[i];
        }
        return sm;
    }
    #endregion

    #region Problem B
    /*
    class Solution:
        def separateSquares(self, squares: List[List[int]]) -> float:
            cn = Counter()
            sm = 0
            for y, y, l in squares:
                cn[y] += l
                cn[y + l] -= l
                sm += l * l
            w = 0
            half = sm / 2
            y0 = 0
            area = 0
            for y in sorted(cn):
                if w > 0:
                    if area + w * (y - y0) >= half:
                        return y0 + (half - area) / w
                    area += w * (y - y0)
                w += cn[y]
                y0 = y
            return -1
     **/
    #endregion

    #region Problem C
    public double SeparateSquares(int[][] squares)
    {
        List<(int, int, int, int)> li = new();
        foreach (var rec in squares)
        {
            int x1 = rec[0], y1 = rec[1], l = rec[2], x2 = x1 + l, y2 = y1 + l;
            li.Add((y1, 1, x1, x2 - 1));
            li.Add((y2, -1, x1, x2 - 1));
        }
        li.Sort();

        InSet ins = new();
        int y0 = li.First().Item1;
        double area = 0.0;
        foreach ((int y, int val, int s, int e) in li)
        {
            if (y > y0)
            {
                area += (double)ins.Length * (y - y0);
                y0 = y;
            }
            ins.Update(new() { start = s, end = e, val = val });
        }

        ins = new();
        y0 = li.First().Item1;
        double half = area / 2;
        area = 0.0;
        foreach ((int y, int val, int s, int e) in li)
        {
            if (y > y0)
            {
                area += (double)ins.Length * (y - y0);
                y0 = y;
                if (area >= half)
                {
                    return y0 - (area - half) / ins.Length;
                }
            }
            ins.Update(new() { start = s, end = e, val = val });
        }
        return -1;
    }

    // 比赛时写的，TLE
    public double SeparateSquares_TLE(int[][] squares)
    {
        int OPEN = 0, CLOSE = 1;
        int[][] events = new int[squares.Length * 2][];
        int t = 0;
        foreach (int[] rec in squares)
        {
            int x1 = rec[0], y1 = rec[1], l = rec[2], x2 = x1 + l, y2 = y1 + l;
            events[t++] = new int[] { y1, OPEN, x1, x2 };
            events[t++] = new int[] { y2, CLOSE, x1, x2 };
        }

        Array.Sort(events, (a, b) => a[0].CompareTo(b[0]));

        List<int[]> active = new List<int[]>();
        int cur_y = events[0][0];
        decimal total_area = 0;
        foreach (int[] evt in events)
        {
            int y = evt[0], typ = evt[1], x1 = evt[2], x2 = evt[3];

            long query = 0;
            int cur = -1;
            foreach (int[] xs in active)
            {
                cur = Math.Max(cur, xs[0]);
                query += Math.Max(xs[1] - cur, 0);
                cur = Math.Max(cur, xs[1]);
            }

            total_area += query * (y - cur_y);

            if (typ == OPEN)
            {
                active.Add(new int[] { x1, x2 });
                active.Sort((a, b) => a[0].CompareTo(b[0]));
            }
            else
            {
                for (int i = 0; i < active.Count; ++i)
                    if (active[i][0] == x1 && active[i][1] == x2)
                    {
                        active.RemoveAt(i);
                        break;
                    }
            }

            cur_y = y;
        }

        double half_area = (double)total_area / 2;
        total_area = 0;
        foreach (int[] evt in events)
        {
            int y = evt[0], typ = evt[1], x1 = evt[2], x2 = evt[3];

            long query = 0;
            int cur = -1;
            foreach (int[] xs in active)
            {
                cur = Math.Max(cur, xs[0]);
                query += Math.Max(xs[1] - cur, 0);
                cur = Math.Max(cur, xs[1]);
            }

            total_area += query * (y - cur_y);
            if ((double)total_area >= half_area)
            {
                return y - ((double)total_area - half_area) / query;
            }

            if (typ == OPEN)
            {
                active.Add(new int[] { x1, x2 });
                active.Sort((a, b) => a[0].CompareTo(b[0]));
            }
            else
            {
                for (int i = 0; i < active.Count; ++i)
                    if (active[i][0] == x1 && active[i][1] == x2)
                    {
                        active.RemoveAt(i);
                        break;
                    }
            }

            cur_y = y;
        }
        return -1;
    }
    #endregion

    #region Problem D
    // AC with 1 WA
    // just use SearchAllKmp
    public int ShortestMatchingSubstring(string s, string p)
    {
        int mi = s.Length + 1;
        if (p == "**") return 0;
        else if (p.StartsWith('*') && p.EndsWith('*') || p.StartsWith("**") || p.EndsWith("**"))
        {
            p = p.TrimStart('*').TrimEnd('*');
            int[] dp = s.SearchAllKmp(p);
            return dp.Any(t => t == p.Length) ? p.Length : -1;
        }
        else if (p.Contains("**") || p.StartsWith('*') || p.EndsWith('*'))
        {
            var sp = p.Split('*').Where(t => !string.IsNullOrEmpty(t)).ToArray();
            string a = sp[0], b = sp[1];
            int i = 0, k = 0, n = s.Length;
            int[] d1 = s.SearchAllKmp(a),
                d2 = s.SearchAllKmp(b);
            int l = -1, r = -1;
            for (int j = 0; j < n; ++j)
            {
                if (d2[j] == b.Length)
                {
                    while (i + a.Length <= j)
                    {
                        if (d1[i] == a.Length)
                        {
                            l = i;
                        }
                        ++i;
                    }
                    if (l >= 0)
                    {
                        r = j + b.Length - 1;
                        mi = Math.Min(mi, r - l + 1);
                    }
                }
            }
        }
        else
        {
            var sp = p.Split('*');
            string a = sp[0], b = sp[1], c = sp[2];
            int i = 0, k = 0, n = s.Length;
            int[] d1 = s.SearchAllKmp(a),
                d2 = s.SearchAllKmp(b),
                d3 = s.SearchAllKmp(c);
            int l = -1, r = -1;
            for (int j = 0; j < n; ++j)
            {
                if (d2[j] == b.Length)
                {
                    while (i + a.Length <= j)
                    {
                        if (d1[i] == a.Length)
                        {
                            l = i;
                        }
                        ++i;
                    }
                    if (l >= 0)
                    {
                        if (r < 0 || r < j + b.Length)
                        {
                            r = -1;
                            if (j + b.Length > k) k = j + b.Length;
                            while (k + c.Length <= n)
                            {
                                if (d3[k] == c.Length)
                                {
                                    r = k;
                                    break;
                                }
                                ++k;
                            }
                        }
                    }
                    if (l >= 0 && r >= 0)
                    {
                        mi = Math.Min(mi, r + c.Length - l);
                    }
                }
            }
        }
        return mi <= s.Length ? mi : -1;
    }
    #endregion

}
