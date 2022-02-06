using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
     * 149. 直线上最多的点数
给你一个数组 points ，其中 points[i] = [xi, yi] 表示 X-Y 平面上的一个点。求最多有多少个点在同一条直线上。

 https://leetcode-cn.com/problems/max-points-on-a-line/

    #计算几何学：叉积与凸包的运用
     * */
    class P0149直线上最多的点数
    {
        private int CalcProduct(int[] p0, int[] p1, int[] p2)
        {
            int x1 = p1[0] - p0[0], y1 = p1[1] - p0[1];
            int x2 = p2[0] - p0[0], y2 = p2[1] - p0[1];
            return x1 * y2 - x2 * y1;
        }

        public int MaxPoints(int[][] points)
        {
            if (points.Length < 2) return points.Length;

            int max = 2;
            int[] temp = null;

            // 找一个最边上的点作为初始点
            int minp_i = 0;
            for (int i = 1; i < points.Length; ++i)
                if (points[i][0] < points[minp_i][0])
                    minp_i = i;
            temp = points[minp_i]; points[minp_i] = points[0]; points[0] = temp;

            for (int i = 0; i < points.Length - 1; ++i)
            {
                int[] p0 = points[i];
                // 求与pi相连的最大同直线点数，并返回最边上的点
                List<int[]> list = new List<int[]>();
                list.Add(new int[] { i + 1, 2 });
                for (int j = i + 2; j < points.Length; ++j)
                {
                    int k = list.Count - 1;
                    for (; k >= 0; --k)
                    {
                        int[] p1 = points[list[k][0]];
                        int[] p2 = points[j];
                        int prod = CalcProduct(p0, p1, p2);
                        if (prod == 0)
                        {
                            list[k][1]++;
                            if (list[k][1] > max) max = list[k][1];
                            break;
                        }
                        else if (prod > 0)
                        {
                            list.Insert(k + 1, new int[] { j, 2 });
                            break;
                        }
                    }
                    if (k < 0) list.Insert(0, new int[] { j, 2 });
                }
                minp_i = list.Last()[0];
                temp = points[minp_i]; points[minp_i] = points[i + 1]; points[i + 1] = temp;
            }

            return max;
        }

        
    }
}
