using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2023/5/20
    // 学习并实现了链式前向星（《算法竞赛》10.1.3 -> Common.cs - StarGraph class），通过这一题来练手
    internal class P1334阈值距离内邻居最少的城市
    {
        public int FindTheCity(int n, int[][] edges, int distanceThreshold)
        {
            var sg = edges.BuildGraph(true);
            int minN = n, ans = -1;

            for (int s = 0; s < n; ++s)
            {
                bool ok = true;

                // dijkstra
                SHeap<int, int> hp = new((a, b) => a < b, true);
                hp.Add(s, 0);
                int nc = 0;
                while (hp.Any())
                {
                    if (nc + hp.Count > minN)
                    {
                        ok = false;
                        break;
                    }

                    (int u, int p) = hp.Pop(); ++nc;
                    foreach ((int v, int w) in sg.Edges(u))
                        if (p + w <= distanceThreshold)
                            hp.Add(v, p + w);
                }

                if (ok)
                {
                    minN = nc;
                    ans = s;
                }
            }

            return ans;
        }
    }
}
