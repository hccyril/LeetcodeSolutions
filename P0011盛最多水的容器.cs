using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
     * 题目说明：
     * 给你 n 个非负整数 a1，a2，...，an，每个数代表坐标中的一个点 (i, ai) 。
     * 在坐标内画 n 条垂直线，垂直线 i 的两个端点分别为 (i, ai) 和 (i, 0) 。
     * 找出其中的两条线，使得它们与 x 轴共同构成的容器可以容纳最多的水。

       说明：你不能倾斜容器。
     * */
    class P0011盛最多水的容器
    {
        public int MaxArea(int[] height)
        {
            int max = 0;

            // ver3(kai) - 2021/6/3
            // before kai: timeout: for case2, d=50000000 Total Time: 2388641ms
            int max_h = 0, max_hi = 0, max_j = height.Length - 1;
            for (int i = 0; i < max_j; ++i)
            {
                if (height[i] <= max_h || height[i] <= max_hi) continue;
                //for (int j = i + 1; j < height.Length; ++j)
                for (int j = max_j; j > i; --j)
                {
                    if (height[j] <= max_h) continue;
                    //// if there exist better (longer and higher) afterwards, skip this
                    //bool isSkip = false;
                    //for (int k = j + 1; k < height.Length; ++k)
                    //    if (height[k] >= height[j])
                    //    {
                    //        isSkip = true;
                    //        break;
                    //    }
                    //if (isSkip) continue;

                    // calc current area
                    int h = Math.Min(height[i], height[j]);
                    int area = h * (j - i);
                    if (area > max)
                    {
                        max_h = h;
                        max_hi = height[i];
                        max_j = j;
                        max = area;
                    }
                }
            }

            //// ver2 - wrong answer
            //int max_i = 0, max_area_i = 0;
            //for (int i = height.Length - 2; i >= 0; --i)
            //{
            //    int area = height[i] * (height.Length - 1 - i);
            //    if (area > max_area_i)
            //    {
            //        max_area_i = area;
            //        max_i = i;
            //    }
            //}
            //for (int j = max_i + 1; j < height.Length; ++j)
            //{
            //    int area = Math.Min(height[max_i], height[j]) * (j - max_i);
            //    if (area > max)
            //    {
            //        max = area;
            //    }
            //}

            ////ver 1: O(n ^ 2) - timeout(6 sec for testcase)
            //    //int[] areas = new int[height.Length];
            //for (int j = 1; j < height.Length; ++j)
            //{
            //    for (int i = j - 1; i >= 0; --i)
            //    {
            //        int area = Math.Min(height[i], height[j]) * (j - i);
            //        if (area > max) max = area;
            //    }
            //}

            return max;
        }
    }
}
