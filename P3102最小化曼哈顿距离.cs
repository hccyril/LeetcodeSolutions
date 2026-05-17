using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// 20260515
// 看到了有关曼哈顿距离的新思路，在本题上验证
internal class P3102最小化曼哈顿距离
{
    public int MinimumDistance(int[][] points)
    {
        (_, int x, int y) = points.MaxDist();
        (int d1, _, _) = points.MaxDist(x);
        (int d2, _, _) = points.MaxDist(y);
        return Math.Min(d1, d2);
    }
}

internal static partial class EX
{
    public static (int, int, int) MaxDist(this int[][] points, int excludeIndex = -1)
    {
        int d1 = int.MinValue, d2 = int.MinValue, d3 = int.MinValue, d4 = int.MinValue,
                i1 = 0, i2 = 0, i3 = 0, i4 = 0, d = 0;
        for (int i = 0; i < points.Length; ++i) 
            if (i != excludeIndex)
            {
                int x = points[i][0], y = points[i][1];
                d = x + y;
                if (d > d1) (d1, i1) = (d, i);
                d = x - y;
                if (d > d2) (d2, i2) = (d, i);
                d = -x + y;
                if (d > d3) (d3, i3) = (d, i);
                d = -x - y;
                if (d > d4) (d4, i4) = (d, i);
            }
        if (d1 + d4 > d2 + d3)
            return (d1 + d4, i1, i4);
        else
            return (d2 + d3, i2, i3);
    }
}

/*  之前python版本的实现
class Solution:
    def minimumDistance(self, points: List[List[int]]) -> int:
        n = len(points)
        def maxDist(ir):
            nonlocal n, points
            a = list(sorted((i for i in range(len(points)) if i != ir), key=lambda i: points[i][0]))
            n = len(a)
            b, c = [0] * n, [0] * n
            for i in range(n - 1, -1, -1):
                b[i], c[i] = a[i], a[i]
                if i < n - 1:
                    if points[b[i]][0] + points[b[i]][1] < points[b[i + 1]][0] + points[b[i + 1]][1]:
                        b[i] = b[i + 1]
                    if points[c[i]][0] - points[c[i]][1] < points[c[i + 1]][0] - points[c[i + 1]][1]:
                        c[i] = c[i + 1]
            maxd, x, y = 0, 0, 0
            for i in range(n - 1):
                d = abs(points[a[i]][0] - points[b[i + 1]][0]) + abs(points[a[i]][1] - points[b[i + 1]][1])
                if d > maxd:
                    maxd = d
                    x, y = a[i], b[i + 1]
                d = abs(points[a[i]][0] - points[c[i + 1]][0]) + abs(points[a[i]][1] - points[c[i + 1]][1])
                if d > maxd:
                    maxd = d
                    x, y = a[i], c[i + 1]
            return maxd, x, y
        _, x, y = maxDist(-1)
        d1, _, _ = maxDist(x)
        d2, _, _ = maxDist(y)
        return min(d1, d2) 
*  */