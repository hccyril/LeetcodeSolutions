using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/4/28 US Daily
    // NOT DP!
    internal class P1631最小体力消耗路径
    {
        // 这题比当年778还更难一些，但778是hard，这题是medium
        public const string 相关题目 = "778. 水位上升的泳池中游泳";

        // 还有一种解法：并查集（待完成）：从小到大依次把边加入图中，直到起点到终点连通

        // ver2: Dijkstra
        public int MinimumEffortPath(int[][] heights)
        {
            SHeap<(int, int), int> hp = new((a, b) => a < b, true);
            hp.Add((0, 0), 0);
            while (hp.Any())
            {
                ((int i, int j), int cost) = hp.Pop();
                if (i == heights.Length - 1 && j == heights[i].Length - 1)
                    return cost;
                foreach ((int ni, int nj) in heights.FourDir(i, j))
                    hp.Add((ni, nj), Math.Max(cost, Math.Abs(heights[i][j] - heights[ni][nj])));
            }
            return -1;
        }

        // ver1: 递推, WA
        //public int MinimumEffortPath(int[][] heights)
        //{
        //    int[] dp = new int[heights[0].Length];
        //    for (int i = 0; i < heights.Length; ++i)
        //        for (int j = 0; j < heights[i].Length; ++j)
        //        {
        //            if (i == 0 && j == 0) continue;
        //            int up = int.MaxValue, left = int.MaxValue;
        //            if (i > 0)
        //                up = Math.Max(dp[j], Math.Abs(heights[i - 1][j] - heights[i][j]));
        //            if (j > 0)
        //                left = Math.Max(dp[j - 1], Math.Abs(heights[i][j - 1] - heights[i][j]));
        //            dp[j] = Math.Min(up, left);
        //        }
        //    return dp.Last();
        //}
    }
}
