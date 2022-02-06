using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0084柱状图中最大的矩形
    {
        public int LargestRectangleArea(int[] heights)
        {
            int maxArea = 0;
            List<int[]> list = new List<int[]>();
            for (int hi = 0; hi < heights.Length; ++hi)
            {
                int h = heights[hi];
                int i, area;
                for (i = 0; i < list.Count; ++i)
                {
                    if (list[i][0] > h) break;
                    area = list[i][1] += list[i][0];
                    if (area > maxArea) maxArea = area;
                }
                if (i < list.Count) list.RemoveRange(i, list.Count - i);
                if (h > 0 && (i == 0 || h > list[i - 1][0]))
                {
                    area = h;
                    for (i = hi - 1; i >= 0 && heights[i] >= h; --i) area += h;
                    list.Add(new int[] { h, area });
                    if (area > maxArea) maxArea = area;
                }
            }
            return maxArea;
        }

        public static void Run()
        {
            int[] input = { 0, 1, 0, 1 };
            int output = new P0084柱状图中最大的矩形().LargestRectangleArea(input);
            Console.WriteLine(output);
        }
    }
}
