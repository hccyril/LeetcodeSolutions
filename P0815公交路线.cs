using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
     * 815. 公交路线
     * 给你一个数组 routes ，表示一系列公交线路，其中每个 routes[i] 表示一条公交线路，第 i 辆公交车将会在上面循环行驶。

例如，路线 routes[0] = [1, 5, 7] 表示第 0 辆公交车会一直按序列 1 -> 5 -> 7 -> 1 -> 5 -> 7 -> 1 -> ... 这样的车站路线行驶。
现在从 source 车站出发（初始时不在公交车上），要前往 target 车站。 期间仅可乘坐公交车。

求出 最少乘坐的公交车数量 。如果不可能到达终点车站，返回 -1 。

 

来源：力扣（LeetCode）
链接：https://leetcode-cn.com/problems/bus-routes
著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。
     * */
    class P0815公交路线
    {
        public int NumBusesToDestination(int[][] routes, int source, int target)
        {
            if (source == target) return 0; // 这个坑。。。

            // site hash
            Dictionary<int, List<int>> siteHash = new Dictionary<int, List<int>>();
            for (int i = 0; i < routes.Length; ++i)
            {
                var route = routes[i];
                foreach (var site in route)
                {
                    List<int> routeList;
                    if (!siteHash.ContainsKey(site))
                    {
                        routeList = new List<int>();
                        siteHash.Add(site, routeList);
                    }
                    else
                        routeList = siteHash[site];

                    routeList.Add(i);
                }
            }
            if (!siteHash.ContainsKey(source)) return -1;

            int[] routeCheck = new int[routes.Length];
            Queue<int> qu = new Queue<int>();
            foreach (var ri in siteHash[source])
            {
                routeCheck[ri] = 1;
                qu.Enqueue(ri);
            }

            // BFS
            while (qu.Any())
            {
                var ri = qu.Dequeue();
                var rc = routeCheck[ri];

                foreach (var site in routes[ri])
                {
                    if (site == target) return rc;

                    foreach (var nextRoute in siteHash[site])
                    {
                        if (routeCheck[nextRoute] == 0)
                        {
                            routeCheck[nextRoute] = rc + 1;
                            qu.Enqueue(nextRoute);
                        }
                    }
                }
            }

            return -1;
        }
    }
}
