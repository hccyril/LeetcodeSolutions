using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/11/14 US Daily
    // 并查集 UnionFind
    internal class P0947移除最多的同行或同列石头
    {
        public int RemoveStones(int[][] stones)
        {
            UnionFind uni = new(stones.Length);
            for (int i = 1; i < stones.Length; i++)
                for (int j = 0; j < i; j++)
                    if (stones[i][0] == stones[j][0] || stones[i][1] == stones[j][1])
                        uni.Union(j, i);
            return Enumerable.Range(0, stones.Length).GroupBy(i => uni.Find(i)).Select(g => g.Count() - 1).Sum();
        }
    }
}
