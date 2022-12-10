using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/9/16
    // 最小生成树MST
    // 如果有一个点有多于一个入度，则必定根节点入度为0，以那个点为起点做Prim算法
    //  - 补充：如果两条边都可以删必须删后面那条，所以需要一些复杂点的处理
    // 否则必定有环，以第一条边开始做Kruskal
    internal class P0685冗余连接II
    {
        public int[] FindRedundantDirectedConnection(int[][] edges)
        {
            int n = edges.Length;
            UnionFind uni = new(n + 1);
            BitArray ba = new BitArray(n, true);
            int i0 = n, i2 = -1;
            for (int i = 0; i < n; ++i)
            {
                var ed = edges[i];
                int v = ed[1] - 1;
                if (ba[v])
                {
                    ba[v] = false;
                    --i0;
                }
                else i2 = i;
            }

            // Prim
            if (i0 > 0)
            {
                int st = Enumerable.Range(0, n).First(i => ba[i]) + 1;
                ba.SetAll(false);
                ba[i2] = true; // 先假设入度为2的第二条边是多余的
                uni.Union(0, st);
                for (int re = 1; re < n; ++re)
                {
                    bool found = false;
                    for (int i = 0; i < n; ++i)
                        if (!ba[i] && uni.Find(edges[i][0]) == 0 && uni.Find(edges[i][1]) != 0)
                        {
                            found = true;
                            ba[i] = true;
                            uni.Union(0, edges[i][1]);
                            break;
                        }
                    if (!found)
                    {
                        // 构建最小生成树失败，说明i2不是多余的，第一条边才是
                        return edges.First(ed => ed[1] == edges[i2][1]);
                    }

                    //int i = Enumerable.Range(0, n).First(i => !ba[i] && uni.Find(edges[i][0]) == 0 && uni.Find(edges[i][1]) != 0);
                    //ba[i] = true;
                    //uni.Union(0, edges[i][1]);
                }
                return edges[i2]; // 构建MST成功，说明i2是冗余边的假设是对的
            }

            // Kruskal
            else
            {
                ba.SetAll(false);
                for (int re = 1; re < n; ++re)
                {
                    int i = Enumerable.Range(0, n).First(i => !ba[i] && !uni.Check(edges[i][0], edges[i][1]));
                    ba[i] = true;
                    uni.Union(edges[i][0], edges[i][1]);
                }
                return edges[Enumerable.Range(0, n).First(i => !ba[i])];
            }
        }

        // WA cases:
        // [[2,1],[3,1],[4,2],[1,4]] // my: [3,1] ex: [2,1]
        // [[5,2],[5,1],[3,1],[3,4],[3,5]] // my [5,1] ex: [3,1]
    }
}
