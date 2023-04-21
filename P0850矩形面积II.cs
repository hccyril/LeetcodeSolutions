using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // Hard, 经典题
    // 2023/4/21 自己完成
    // 用了线段扫描，但没用线段树，用了自己写的IntervalSet

    class P0850矩形面积II
    {
        public int RectangleArea(int[][] rectangles)
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
            int[] r1 = { 471, 0, 947, 999 }, r2 = { 868, 0, 948, 538 }, r3 = { 929, 0, 952, 596 };

            // 6
            //int[] r1 = { 0, 0, 2, 2 }, r2 = { 1, 0, 2, 3 }, r3 = { 1, 0, 3, 1 };
            
            int[][] input1 = { r1, r2, r3 };
            Console.WriteLine(new P0850矩形面积II().RectangleArea(input1));

            // 957901
            var input = new int[][] { new int[] { 471, 0, 947, 999 }, new int[] { 780, 0, 823, 320 }, new int[] { 868, 0, 948, 538 }, new int[] { 907, 0, 911, 673 }, new int[] { 929, 0, 952, 596 }, new int[] { 458, 0, 889, 669 }, new int[] { 156, 0, 364, 754 }, new int[] { 900, 0, 973, 236 }, new int[] { 406, 0, 620, 454 }, new int[] { 773, 0, 946, 538 }, new int[] { 407, 0, 834, 23 }, new int[] { 759, 0, 858, 526 }, new int[] { 431, 0, 776, 599 }, new int[] { 969, 0, 979, 30 }, new int[] { 642, 0, 737, 339 }, new int[] { 239, 0, 448, 183 }, new int[] { 260, 0, 517, 903 }, new int[] { 14, 0, 674, 976 }, new int[] { 251, 0, 850, 112 }, new int[] { 57, 0, 794, 395 }, new int[] { 595, 0, 728, 149 }, new int[] { 970, 0, 989, 36 }, new int[] { 496, 0, 954, 791 }, new int[] { 447, 0, 832, 805 }, new int[] { 829, 0, 939, 100 }, new int[] { 169, 0, 568, 501 }, new int[] { 704, 0, 969, 411 }, new int[] { 607, 0, 609, 221 }, new int[] { 935, 0, 953, 437 }, new int[] { 47, 0, 670, 130 }, new int[] { 794, 0, 799, 230 }, new int[] { 943, 0, 959, 90 }, new int[] { 332, 0, 337, 732 }, new int[] { 123, 0, 228, 344 }, new int[] { 281, 0, 487, 598 }, new int[] { 381, 0, 732, 443 }, new int[] { 235, 0, 391, 548 }, new int[] { 646, 0, 930, 20 }, new int[] { 219, 0, 675, 95 }, new int[] { 8, 0, 212, 227 }, new int[] { 138, 0, 704, 658 }, new int[] { 368, 0, 782, 707 }, new int[] { 810, 0, 826, 957 }, new int[] { 543, 0, 697, 654 }, new int[] { 887, 0, 986, 180 }, new int[] { 837, 0, 900, 228 }, new int[] { 280, 0, 391, 331 }, new int[] { 180, 0, 229, 42 }, new int[] { 201, 0, 489, 687 }, new int[] { 648, 0, 680, 732 }, new int[] { 228, 0, 630, 922 }, new int[] { 886, 0, 960, 56 }, new int[] { 946, 0, 955, 522 }, new int[] { 903, 0, 992, 464 }, new int[] { 557, 0, 860, 38 }, new int[] { 89, 0, 268, 642 }, new int[] { 669, 0, 774, 185 }, new int[] { 1, 0, 724, 374 }, new int[] { 395, 0, 923, 782 }, new int[] { 82, 0, 230, 550 }, new int[] { 166, 0, 166, 808 }, new int[] { 441, 0, 644, 435 }, new int[] { 497, 0, 823, 224 }, new int[] { 372, 0, 973, 556 }, new int[] { 188, 0, 846, 127 }, new int[] { 226, 0, 396, 535 }, new int[] { 869, 0, 945, 575 }, new int[] { 406, 0, 526, 795 }, new int[] { 781, 0, 795, 569 }, new int[] { 563, 0, 831, 991 }, new int[] { 466, 0, 486, 641 }, new int[] { 274, 0, 855, 529 }, new int[] { 61, 0, 819, 364 }, new int[] { 285, 0, 421, 101 }, new int[] { 193, 0, 950, 748 }, new int[] { 320, 0, 655, 836 }, new int[] { 207, 0, 627, 945 }, new int[] { 782, 0, 899, 56 }, new int[] { 578, 0, 970, 913 }, new int[] { 499, 0, 684, 205 }, new int[] { 490, 0, 877, 16 }, new int[] { 483, 0, 668, 915 }, new int[] { 364, 0, 741, 16 } };
            Console.WriteLine(new P0850矩形面积II().RectangleArea(input));
        }
    }

// 以下为官方题解
#if OFFICIAL_3
        // 方法三：线性扫描
        /*
         * 思想

将每个矩形都看作是一条从底部传递到顶部的水平线段，把从底部到顶部中间的区域称为活动区域，底部边和顶部边称为水平间隔。每个矩形都会更新两次，即在底部添加水平间隔和顶部删除水平间隔。那么 N 个矩形共有 2 * N 次更新，且每次最多更新 N 个水平间隔。

算法

例如矩形 rec = [1,0,3,1]，第一次更新是在 y = 0 时添加水平间隔 [1, 3]，第二次更新是在 y = 1 时删除水平间隔。这里需要注意添加和删除的多重性。如果在 y = 0 时，添加了两条水平间隔 [1, 3] 和 [0, 2]，那么在 y = 1 时只会删除 [1, 3]，不影响 [0, 2]。

为每个矩形创建添加和删除事件，然后以 y 从小到大的顺序处理所有事件。存在一个问题，在处理 add(x1, x2) 和 remove(x1, x2) 事件时如何查询到位于同一 y 坐标的其他水平间隔。

因为 remove(...) 操作总是在 add(...) 之后，因此可以把所有的水平间隔以 y 坐标由小到大的顺序排列。然后使用类似于 LeetCode 合并区间问题实现查询操作 query()。

作者：LeetCode
链接：https://leetcode-cn.com/problems/rectangle-area-ii/solution/ju-xing-mian-ji-ii-by-leetcode/
来源：力扣（LeetCode）
著作权归作者所有。商业转载请联系作者获得授权，非商业转载请注明出处。
         * */
        public int RectangleArea(int[][] rectangles)
        {
            int OPEN = 0, CLOSE = 1;
            int[][] events = new int[rectangles.Length * 2][];
            int t = 0;
            foreach (int[] rec in rectangles)
            {
                events[t++] = new int[] { rec[1], OPEN, rec[0], rec[2] };
                events[t++] = new int[] { rec[3], CLOSE, rec[0], rec[2] };
            }

            Array.Sort(events, (a, b) => a[0].CompareTo(b[0]));

            List<int[]> active = new List<int[]>();
            int cur_y = events[0][0];
            long ans = 0;
            foreach (int[] evt in events)
            {
                int y = evt[0], typ = evt[1], x1 = evt[2], x2 = evt[3];

                // Calculate query
                long query = 0;
                int cur = -1;
                foreach (int[] xs in active)
                {
                    cur = Math.Max(cur, xs[0]);
                    query += Math.Max(xs[1] - cur, 0);
                    cur = Math.Max(cur, xs[1]);
                }

                ans += query * (y - cur_y);

                if (typ == OPEN)
                {
                    active.Add(new int[]{x1, x2});
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

            //return ans;
            ans %= 1_000_000_007;
            return (int)ans;
        }
#endif
}
