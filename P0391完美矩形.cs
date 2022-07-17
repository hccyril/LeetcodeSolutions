using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard // 2021/11/16 daily
    // 2022/7/15 完成
    // 按x2排序，然后用二分搜索可以快速定位到x重合的矩形，进一步判断是否y重合
    // 官方题解（未实现）：使用哈希表记录每个顶点，重合的次数必须是（1，2，4）
    class P0391完美矩形
    {
        // ver2 尝试做一些优化 1703ms -> 594ms
        long Area(long x1, long y1, long x2, long y2) => (x2 - x1) * (y2 - y1);
        long Area(int[] r) => Area(r[0], r[1], r[2], r[3]);
        public bool IsRectangleCover(int[][] rectangles)
        {
            var a = Enumerable.Range(0, rectangles.Length)
                .Select(i => (rectangles[i][2], i))
                .OrderBy(t => t)
                .ToArray();

            int min_x = rectangles[a[0].i][0], max_x = rectangles[a[0].i][2], min_y = rectangles[a[0].i][1], max_y = rectangles[a[0].i][3];
            long sumArea = Area(rectangles[a[0].i]);

            for (int i = 1; i < a.Length; ++i)
            {
                var rec = rectangles[a[i].i];
                sumArea += Area(rec);
                min_x = Math.Min(min_x, rec[0]);
                min_y = Math.Min(min_y, rec[1]);
                max_x = Math.Max(max_x, rec[2]);
                max_y = Math.Max(max_y, rec[3]);

                // check overlap
                int f = ~Array.BinarySearch(a, 0, i, (rec[0], int.MaxValue));
                for (; f < i; ++f)
                {
                    (int r, int j) = a[f];
                    var re1 = rectangles[j];
                    if (Math.Max(rec[1], re1[1]) < Math.Min(rec[3], re1[3]))
                        return false;
                }
            }
            return Area(min_x, min_y, max_x, max_y) == sumArea;
        }

        // ver1: TLE, 1703ms
        //long Area(long x1, long y1, long x2, long y2) => (x2 - x1) * (y2 - y1);
        //long Area(int[] r) => Area(r[0], r[1], r[2], r[3]); 
        //public bool IsRectangleCover(int[][] rectangles)
        //{
        //    var a = Enumerable.Range(0, rectangles.Length)
        //        .Select(i => (rectangles[i][2], i))
        //        .OrderBy(t => t)
        //        .ToArray();

        //    int min_x = int.MaxValue, max_x = int.MinValue, min_y = int.MaxValue, max_y = int.MinValue;
        //    long sumArea = 0L;

        //    for (int i = 0; i < rectangles.Length; ++i)
        //    {
        //        var rec = rectangles[i];
        //        sumArea += Area(rec);
        //        min_x = Math.Min(min_x, rec[0]);
        //        min_y = Math.Min(min_y, rec[1]);
        //        max_x = Math.Max(max_x, rec[2]);
        //        max_y = Math.Max(max_y, rec[3]);

        //        // check overlap
        //        int f = ~Array.BinarySearch(a, (rec[0], int.MaxValue));
        //        for (; f < a.Length; ++f)
        //        {
        //            (int r, int j) = a[f];
        //            if (j == i) continue;
        //            var re1 = rectangles[j];
        //            if (re1[0] >= rec[2]) break;
        //            if (Math.Max(rec[1], re1[1]) < Math.Min(rec[3], re1[3]))
        //                return false;
        //        }
        //    }
        //    return Area(min_x, min_y, max_x, max_y) == sumArea;
        //}

        internal static void Run()
        {
            var input = Common.ReadInput<int[][]>(391);
            //Console.WriteLine("input size=" + input.Length);
            var sln = new P0391完美矩形();
            Console.WriteLine(sln.IsRectangleCover(input));
        }
    }
}
