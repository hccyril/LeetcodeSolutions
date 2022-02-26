using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /**
     * 265. 粉刷房子 II
    假如有一排房子共有 n 幢，每个房子可以被粉刷成 k 种颜色中的一种。房子粉刷成不同颜色的花费成本也是不同的。你需要粉刷所有的房子并且使其相邻的两个房子颜色不能相同。

    每个房子粉刷成不同颜色的花费以一个 n x k 的矩阵表示。

    例如，costs[0][0] 表示第 0 幢房子粉刷成 0 号颜色的成本；costs[1][2] 表示第 1 幢房子粉刷成 2 号颜色的成本，以此类推。
    返回 粉刷完所有房子的最低成本 。

    示例 1：
    输入: costs = [[1,5,3],[2,9,4]]
    输出: 5
    解释: 
    将房子 0 刷成 0 号颜色，房子 1 刷成 2 号颜色。花费: 1 + 4 = 5; 
    或者将 房子 0 刷成 2 号颜色，房子 1 刷成 0 号颜色。花费: 3 + 2 = 5. 
    
    示例 2:
    输入: costs = [[1,3],[2,4]]
    输出: 5

    提示：
    costs.length == n
    costs[i].length == k
    1 <= n <= 100
    2 <= k <= 20
    1 <= costs[i][j] <= 20

    进阶：您能否在 O(nk) 的时间复杂度下解决此问题？
     * */

    // hard, plus, 2022/2/25
    // DP: O(nk)
    internal class P0265粉刷房子II
    {
        public int MinCostII(int[][] costs)
        {
            int[] dp = costs[0];
            int i1 = -1, v1 = int.MaxValue, v2 = int.MaxValue;
            for (int i = 0; i < costs.Length; ++i)
            {
                if (i > 0)
                    for (int j = 0; j < costs[i].Length; ++j)
                        dp[j] = (j == i1 ? v2 : v1) + costs[i][j];
                i1 = -1; v1 = int.MaxValue; v2 = int.MaxValue; // 记得什么时候该初始化
                for (int j = 0; j < dp.Length; ++j)
                    if (dp[j] < v1)
                    {
                        v2 = v1;
                        v1 = dp[j];
                        i1 = j;
                    }
                    else if (dp[j] < v2) v2 = dp[j];
            }
            return v1;
        }
    }
}
