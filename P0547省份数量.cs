using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/2/10
    // 并查集
    // also 《剑指 Offer II 116. 省份数量》
    internal class P0547省份数量
    {
        public int FindCircleNum(int[][] isConnected)
        {
            UnionFind uni = new(isConnected.Length);
            for (int i = 0; i < isConnected.Length - 1; ++i)
                for (int j = i + 1; j < isConnected.Length; ++j)
                    if (isConnected[i][j] == 1)
                        uni.Union(i, j);
            return uni.GroupCount();
            //return Enumerable.Range(0, isConnected.Length).Select(i => uni.Find(i)).Distinct().Count();
        }
    }
}
