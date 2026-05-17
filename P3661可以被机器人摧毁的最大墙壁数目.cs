using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 20260403 Daily
// rating 2525
// arrays DP
// 实现非常麻烦，陆陆续续写了一个上午
internal class P3661可以被机器人摧毁的最大墙壁数目
{
    public int MaxWalls(int[] robots, int[] distance, int[] walls)
    {
        int n = robots.Length, m = walls.Length;
        if (n == 1)
        {
            int i = robots[0], d = distance[0];
            return Math.Max(
                walls.Count(w => w <= i && i - w <= d),
                walls.Count(w => w >= i && w - i <= d));
        }
        var robs = robots
            .Zip(distance, (r, d) => ((int i, int l, int r))(r, r - d, r + d))
            .OrderBy(t => t.i)
            .ToArray();
        Array.Sort(walls);

        int wi = 0, max_left = 0, max_right = 0, prev_max = 0, cr = 0, co = 0;

        // robs[0]
        while (wi < m && walls[wi] < robs[0].i)
        {
            if (walls[wi] >= robs[0].l)
                ++max_left;
            ++wi;
        }
        if (wi < m && walls[wi] == robs[0].i)
        {
            ++max_left;
            ++max_right;
            ++wi;
        }
        while (wi < m && walls[wi] < robs[1].i)
        {
            if (walls[wi] <= robs[0].r)
                ++max_right;
            if (walls[wi] >= robs[1].l)
                ++cr;
            if (walls[wi] >= robs[1].l && walls[wi] <= robs[0].r)
                ++co;
            ++wi;
        }
        (prev_max, max_left) = (Math.Max(max_left, max_right), Math.Max(max_left + cr, max_right + cr - co));
        max_right = prev_max;

        // robs[1..n-2]
        for (int i = 1; i < n - 1; ++i)
        {
            if (wi < m && walls[wi] == robs[i].i)
            {
                ++max_left;
                ++max_right;
                ++wi;
            }

            cr = co = 0;
            while (wi < m && walls[wi] < robs[i + 1].i)
            {
                bool ltr = walls[wi] <= robs[i].r,
                    rtl = walls[wi] >= robs[i + 1].l;
                if (ltr)
                    max_right++;
                if (rtl) cr++;
                if (ltr && rtl) co++;
                wi++;
            }

            (prev_max, max_left) = (Math.Max(max_left, max_right), Math.Max(max_left + cr, max_right + cr - co));
            max_right = prev_max;
        }

        // robs[n-1]
        if (wi < m && walls[wi] == robs[^1].i)
        {
            ++max_left;
            ++max_right;
            ++wi;
        }
        while (wi < m && walls[wi] <= robs[^1].r)
        {
            ++max_right;
            ++wi;
        }

        // done
        return Math.Max(max_left, max_right);
    }

    // 偷懒让AI帮忙找到问题了
    public int MaxWalls_WA(int[] robots, int[] distance, int[] walls)
    {
        int n = robots.Length, m = walls.Length;
        if (n == 1)
        {
            int i = robots[0], d = distance[0];
            return Math.Max(
                walls.Count(w => w <= i && i - w <= d),
                walls.Count(w => w >= i && w - i <= d));
        }
        var robs = robots
            .Zip(distance, (r, d) => ((int i, int l, int r))(r, r - d, r + d))
            .OrderBy(t => t.i)
            .ToArray();
        Array.Sort(walls);

        int wi = 0, max_left = 0, max_right = 0, prev_max = 0, cl = 0, cr = 0, co = 0;

        // robs[0]
        while (wi < m && walls[wi] < robs[0].i)
        {
            if (walls[wi] >= robs[0].l)
                ++max_left;
            ++wi;
        }
        if (wi < m && walls[wi] == robs[0].i)
        {
            ++max_left; 
            ++max_right;
            ++wi;
        }
        while (wi < m && walls[wi] < robs[1].i)
        {
            if (walls[wi] <= robs[0].r)
                ++max_right;
            if (walls[wi] >= robs[1].l)
                ++cl;
            if (walls[wi] >= robs[1].l && walls[wi] <= robs[0].r)
                ++co;
            ++wi;
        }
        (prev_max, max_left) = (Math.Max(max_left, max_right), Math.Max(max_left + cl, max_right + cl - co));
        max_right = prev_max;

        // robs[1..n-2]
        for (int i = 1; i < n - 1; ++i)
        {
            if (wi < m && walls[wi] == robs[i].i)
            {
                ++max_left;
                ++max_right;
                ++wi;
            }

            cl = cr = co = 0;
            while (wi < m && walls[wi] < robs[i + 1].i)
            {
                bool ltr = walls[wi] <= robs[i].r,
                    rtl = walls[wi] >= robs[i + 1].l;
                if (ltr)
                {
                    cl++;
                    max_right++;
                }
                if (rtl) cr++;
                if (ltr && rtl) co++;
                wi++;
            }

            // WA原因：应该是cr而不是cl，所以其实上面也应该用cr(然后cl可以删了)
            (prev_max, max_left) = (Math.Max(max_left, max_right), Math.Max(max_left + cl, max_right + cl - co));
            max_right = prev_max;
        }

        // robs[n-1]
        if (wi < m && walls[wi] == robs[^1].i)
        {
            ++max_left;
            ++max_right;
            ++wi;
        }
        while (wi < m && walls[wi] <= robs[^1].r)
        {
            ++max_right;
            ++wi;
        }

        // done
        return Math.Max(max_left, max_right);
    }
}
