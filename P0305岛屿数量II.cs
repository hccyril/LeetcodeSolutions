﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /* 305. 岛屿数量 II
     * 给你一个大小为 m x n 的二进制网格 grid 。网格表示一个地图，其中，0 表示水，1 表示陆地。最初，grid 中的所有单元格都是水单元格（即，所有单元格都是 0）。

    可以通过执行 addLand 操作，将某个位置的水转换成陆地。给你一个数组 positions ，其中 positions[i] = [ri, ci] 是要执行第 i 次操作的位置 (ri, ci) 。

    返回一个整数数组 answer ，其中 answer[i] 是将单元格 (ri, ci) 转换为陆地后，地图中岛屿的数量。

    岛屿 的定义是被「水」包围的「陆地」，通过水平方向或者垂直方向上相邻的陆地连接而成。你可以假设地图网格的四边均被无边无际的「水」所包围。

    示例 1：
    输入：m = 3, n = 3, positions = [[0,0],[0,1],[1,2],[2,1]]
    输出：[1,1,2,3]
    解释：
    起初，二维网格 grid 被全部注入「水」。（0 代表「水」，1 代表「陆地」）
    - 操作 #1：addLand(0, 0) 将 grid[0][0] 的水变为陆地。此时存在 1 个岛屿。
    - 操作 #2：addLand(0, 1) 将 grid[0][1] 的水变为陆地。此时存在 1 个岛屿。
    - 操作 #3：addLand(1, 2) 将 grid[1][2] 的水变为陆地。此时存在 2 个岛屿。
    - 操作 #4：addLand(2, 1) 将 grid[2][1] 的水变为陆地。此时存在 3 个岛屿。

    示例 2：
    输入：m = 1, n = 1, positions = [[0,0]]
    输出：[1]

    提示：
    1 <= m, n, positions.length <= 10^4
    1 <= m * n <= 10^4
    positions[i].length == 2
    0 <= ri < m
    0 <= ci < n

    进阶：你可以设计一个时间复杂度 O(k log(mn)) 的算法解决此问题吗？（其中 k == positions.length）
     * */

    // hard, plus, 2022/2/24 限时免费VIP
    internal class P0305岛屿数量II
    {
        public IList<int> NumIslands2(int m, int n, int[][] positions)
        {
            IList<int> ansList = new List<int>();
            UnionFind uni = new(positions.Length);
            Dictionary<(int, int), int> dic = new();
            HashSet<int> hs = new();
            for (int i = 0; i < positions.Length; ++i)
            {
                int x = positions[i][0], y = positions[i][1];

                // 搞笑了测试用例有一个添加了重复的点
                if (dic.ContainsKey((x, y)))
                {
                    ansList.Add(hs.Count);
                    continue;
                }

                dic[(x, y)] = i;
                hs.Add(i);
                foreach ((int nx, int ny) in GraphEX.FourDir(m, n, x, y))
                {
                    if (dic.ContainsKey((nx, ny)))
                    {
                        int j = uni.Find(dic[(nx, ny)]);
                        if (j != i)
                        {
                            uni.Union(i, j);
                            hs.Remove(j);
                        }
                    }
                }
                ansList.Add(hs.Count);
            }
            return ansList;
        }
    }
}
