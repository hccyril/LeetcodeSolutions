using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // DP，想了很久 2022/1/13
    internal class P0452用最少数量的箭引爆气球
    {
        /*
         * 按endi排序后，考虑i和i-1，只有下面三种情况：
         * 
1.              ----                 （不相交，射完i再加上dp[i-1]
                            -----

2.                     ----           （完全重合，射中i-1时一定会带走i，所以就是等于dp[i-1]
                     ---------

3.             ------                 （部分重合，射在i的最左边，带走越多气球越好，然后求dp[i-k]
                   -------
         * */
        public int FindMinArrowShots(int[][] points)
        {
            //Array.Sort(points, (a, b) => a[1] == b[1] ? a[0] - b[0] : a[1] - b[1]); // MAXINT-MININT时会溢出
            points = points.OrderBy(p => p[1]).ThenBy(p => p[0]).ToArray();
            int[] dp = new int[points.Length];
            for (int i = 0; i < points.Length; ++i)
            {
                dp[i] = 1 + (i > 0 ? dp[i - 1] : 0);
                int j = i - 1, arrow = 1;
                for (; j >= 0 && points[j][1] >= points[i][0]; --j)
                {
                    if (points[j][0] >= points[i][0]) // case2: 完全重合
                    {
                        arrow = 0;
                        break;
                    } 
                }
                dp[i] = Math.Min(dp[i], j >= 0 ? dp[j] + arrow : 1);
            }
            return dp[dp.Length - 1];
        }
    }
}
