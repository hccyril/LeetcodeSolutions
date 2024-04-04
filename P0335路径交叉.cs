using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleCore1;

// 几何题，困难 hard // 2021/10/29 daily
class P0335路径交叉
{
    // 2024/1/28 总结思路
    /**
     * 从第一次长度比前一条边长度短开始“内卷”，内卷时只要边长>=前一条边，就有交叉
     * 如果一开始就内卷，按前面的条件判断即可
     * 但如果一直是外卷（严格递增），就要判断“外转内”的情况: 这个情况只有一次，用signal来表示，signal之后的第一条边需要特殊处理（可以画图表示）
     * */
    public bool IsSelfCrossing(int[] a) // a: distances
    {
        int n = a.Length;
        bool isInroll = false, signal = false;
        for (int i = 2; i < n; ++i)
        {
            if (signal)
            {
                if (i >= 4 && a[i] >= a[i - 2] - a[i - 4])
                    return true;
                signal = false;
            }
            if (isInroll)
            {
                if (a[i] >= a[i - 2])
                {
                    return true;
                }
            }
            else
            {
                if (a[i] <= a[i - 2])
                {
                    isInroll = true;

                    // 考虑漏了一种情况>_<：[1,1,2,1,1]
                    //if (i >= 4 && a[i] >= a[i - 2] - a[i - 4])
                    //    signal = true;
                    if (a[i] == a[i - 2] || i >= 4 && a[i] >= a[i - 2] - a[i - 4])
                        signal = true;
                }
            }
        }
        return false;
    }

    // 2023/12/31
    // WA and/or TLE
    public bool IsSelfCrossing_ver1(int[] distance)
    {
        SortedSet<(int, int, int)> xs = new(), ys = new();
        int d = 0, x = 0, y = 0, lowest = 0, highest = 0, leftest = 0, rightest = 0, lastlen = 0;
        foreach (var len in distance)
        {
            d = d + 1 & 3;
            switch (d)
            {
                case 0:
                    if (y >= lowest)
                        foreach ((int x0, int low, int high) in xs.GetViewBetween((x - 1, 0, 0), (x + len + 1, 0, 0)))
                            if (x0 >= x && x0 <= x + len && y >= low && y <= high)
                                return true;
                    if (lastlen > 0)
                        xs.Add((x, y, y + lastlen));
                    x += len;
                    if (y < lowest) lowest = y;
                    break;
                case 1:
                    if (x <= rightest)
                        foreach ((int y0, int left, int right) in ys.GetViewBetween((y - 1, 0, 0), (y + len + 1, 0, 0)))
                            if (y0 >= y && y0 <= x + len && x >= left && x <= right)
                                return true;
                    if (lastlen > 0)
                        ys.Add((y, x - lastlen, x));
                    y += len;
                    if (x > rightest) rightest = x;
                    break;
                case 2:
                    if (y <= highest)
                        foreach ((int x0, int low, int high) in xs.GetViewBetween((x - len - 1, 0, 0), (x + 1, 0, 0)))
                            if (x0 >= x - len && x0 <= x && y >= low && y <= high)
                                return true;
                    if (lastlen > 0)
                        xs.Add((x, y - lastlen, y));
                    x -= len;
                    if (y > highest) highest = y;
                    break;
                case 3:
                    if (x >= leftest)
                        foreach ((int y0, int left, int right) in ys.GetViewBetween((y - len - 1, 0, 0), (y + 1, 0, 0)))
                            if (y0 >= y - len && y0 <= y && x >= left && x <= right)
                                return true;
                    if (lastlen > 0)
                        ys.Add((y, x, x + lastlen));
                    y -= len;
                    if (x < leftest) leftest = x;
                    break;
            }
            lastlen = len;
        }
        return false;
    }

    internal static void Run()
    {
        // 另外研究下从大到小的用例会不会超时
        var input = "[1,1,2,2,3,3,4,4,5,5,6,6,7,7,8,8,9,9,10,10,11,11,12,12,13,13,14,14,15,15,16,16,17,17,18,18,19,19,20,20,21,21,22,22,23,23,24,24,25,25,26,26,27,27,28,28,29,29,30,30,31,31,32,32,33,33,34,34,35,35,36,36,37,37,38,38,39,39,40,40,41,41,42,42,43,43,44,44,45,45,46,46,47,47,48,48,49,49,50,50,51,51,52,52,53,53,54,54,55,55,56,56,57,57,58,58,59,59,60,60,61,61,62,62,63,63,64,64,65,65,66,66,67,67,68,68,69,69,70,70,71,71,72,72,73,73,74,74,75,75,76,76,77,77,78,78,79,79,80,80,81,81,82,82,83,83,84,84,85,85,86,86,87,87,88,88,89,89,90,90,91,91,92,92,93,93,94,94,95,95,96,96,97,97,98,98,99,99,500,99,99,98,98,97,97,96,96,95,95,94,94,93,93,92,92,91,91,90,90,89,89,88,88,87,87,86,86,85,85,84,84,83,83,82,82,81,81,80,80,79,79,78,78,77,77,76,76,75,75,74,74,73,73,72,72,71,71,70,70,69,69,68,68,67,67,66,66,65,65,64,64,63,63,62,62,61,61,60,60,59,59,58,58,57,57,56,56,55,55,54,54,53,53,52,52,51,51,50,50,49,49,48,48,47,47,46,46,45,45,44,44,43,43,42,42,41,41,40,40,39,39,38,38,37,37,36,36,35,35,34,34,33,33,32,32,31,31,30,30,29,29,28,28,27,27,26,26,25,25,24,24,23,23,22,22,21,21,20,20,19,19,18,18,17,17,16,16,15,15,14,14,13,13,12,12,11,11,10,10,9,9,8,8,7,7,6,6,5,5,4,4,3,3,2,2,1,1]"
            .ToTestInput<int[]>();
        var sln = new P0335路径交叉();
        Console.WriteLine($"{sln.IsSelfCrossing(input)}");
    }
}
