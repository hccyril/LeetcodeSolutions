using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// Hard, 经典题
    class P0850矩形面积II
    {
    // 2025/2/16 扫描线线段树
        public int RectangleArea(int[][] rectangles)
        {
        ScanSegmentTree st = new(rectangles.SelectMany(r => new long[] { r[0], r[2] }));
        const int mod = 1000000007;
        long area = 0;
        int y0 = 0;
        foreach ((int y, int x1, int x2, int val) in rectangles
                .SelectMany(rec => new[] { (rec[1], rec[0], rec[2], 1), (rec[3], rec[0], rec[2], -1) })
                .OrderBy(t => t.Item1))
        {
            if (y > y0)
            {
                area += st.Length * (y - y0);
                area %= mod;
                y0 = y;
            }
            st.Add(x1, x2, val);
        }
        return (int)area;
    }
    
    // 2023/4/21 自己完成
    // 用了线段扫描，但没用线段树，用了自己写的IntervalSet
    public int RectangleArea_1(int[][] rectangles)
    {
            List<(int, int, int, int)> li = new();
            foreach (var rec in rectangles)
            {
                int x1 = rec[0], y1 = rec[1], x2 = rec[2], y2 = rec[3];
                li.Add((x1, 1, y1, y2 - 1));
                li.Add((x2, -1, y1, y2 - 1));
            }
            li.Sort();

            InSet ins = new();
            int x0 = li.First().Item1;
            long area = 0L;
            foreach ((int x, int val, int s, int e) in li)
            {
                if (x > x0)
                {
                    area += (long)ins.Length * (x - x0);
                    x0 = x;
                }
                ins.Update(new() { start = s, end = e, val = val });
            }
            return (int)(area % 1000000007);
        }

        internal static void Run()
        {
            // 478504
        //int[] r1 = { 471, 0, 947, 999 }, r2 = { 868, 0, 948, 538 }, r3 = { 929, 0, 952, 596 };

            // 6
        int[] r1 = { 0, 0, 2, 2 }, r2 = { 1, 0, 2, 3 }, r3 = { 1, 0, 3, 1 };
            
            int[][] input1 = { r1, r2, r3 };
            Console.WriteLine(new P0850矩形面积II().RectangleArea(input1));

        //// 957901
        //var input = new int[][] { new int[] { 471, 0, 947, 999 }, new int[] { 780, 0, 823, 320 }, new int[] { 868, 0, 948, 538 }, new int[] { 907, 0, 911, 673 }, new int[] { 929, 0, 952, 596 }, new int[] { 458, 0, 889, 669 }, new int[] { 156, 0, 364, 754 }, new int[] { 900, 0, 973, 236 }, new int[] { 406, 0, 620, 454 }, new int[] { 773, 0, 946, 538 }, new int[] { 407, 0, 834, 23 }, new int[] { 759, 0, 858, 526 }, new int[] { 431, 0, 776, 599 }, new int[] { 969, 0, 979, 30 }, new int[] { 642, 0, 737, 339 }, new int[] { 239, 0, 448, 183 }, new int[] { 260, 0, 517, 903 }, new int[] { 14, 0, 674, 976 }, new int[] { 251, 0, 850, 112 }, new int[] { 57, 0, 794, 395 }, new int[] { 595, 0, 728, 149 }, new int[] { 970, 0, 989, 36 }, new int[] { 496, 0, 954, 791 }, new int[] { 447, 0, 832, 805 }, new int[] { 829, 0, 939, 100 }, new int[] { 169, 0, 568, 501 }, new int[] { 704, 0, 969, 411 }, new int[] { 607, 0, 609, 221 }, new int[] { 935, 0, 953, 437 }, new int[] { 47, 0, 670, 130 }, new int[] { 794, 0, 799, 230 }, new int[] { 943, 0, 959, 90 }, new int[] { 332, 0, 337, 732 }, new int[] { 123, 0, 228, 344 }, new int[] { 281, 0, 487, 598 }, new int[] { 381, 0, 732, 443 }, new int[] { 235, 0, 391, 548 }, new int[] { 646, 0, 930, 20 }, new int[] { 219, 0, 675, 95 }, new int[] { 8, 0, 212, 227 }, new int[] { 138, 0, 704, 658 }, new int[] { 368, 0, 782, 707 }, new int[] { 810, 0, 826, 957 }, new int[] { 543, 0, 697, 654 }, new int[] { 887, 0, 986, 180 }, new int[] { 837, 0, 900, 228 }, new int[] { 280, 0, 391, 331 }, new int[] { 180, 0, 229, 42 }, new int[] { 201, 0, 489, 687 }, new int[] { 648, 0, 680, 732 }, new int[] { 228, 0, 630, 922 }, new int[] { 886, 0, 960, 56 }, new int[] { 946, 0, 955, 522 }, new int[] { 903, 0, 992, 464 }, new int[] { 557, 0, 860, 38 }, new int[] { 89, 0, 268, 642 }, new int[] { 669, 0, 774, 185 }, new int[] { 1, 0, 724, 374 }, new int[] { 395, 0, 923, 782 }, new int[] { 82, 0, 230, 550 }, new int[] { 166, 0, 166, 808 }, new int[] { 441, 0, 644, 435 }, new int[] { 497, 0, 823, 224 }, new int[] { 372, 0, 973, 556 }, new int[] { 188, 0, 846, 127 }, new int[] { 226, 0, 396, 535 }, new int[] { 869, 0, 945, 575 }, new int[] { 406, 0, 526, 795 }, new int[] { 781, 0, 795, 569 }, new int[] { 563, 0, 831, 991 }, new int[] { 466, 0, 486, 641 }, new int[] { 274, 0, 855, 529 }, new int[] { 61, 0, 819, 364 }, new int[] { 285, 0, 421, 101 }, new int[] { 193, 0, 950, 748 }, new int[] { 320, 0, 655, 836 }, new int[] { 207, 0, 627, 945 }, new int[] { 782, 0, 899, 56 }, new int[] { 578, 0, 970, 913 }, new int[] { 499, 0, 684, 205 }, new int[] { 490, 0, 877, 16 }, new int[] { 483, 0, 668, 915 }, new int[] { 364, 0, 741, 16 } };
        //Console.WriteLine(new P0850矩形面积II().RectangleArea(input));
        }
}
