using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0882细分图中的可到达结点
    {
        public int ReachableNodes(int[][] edges, int maxMoves, int n)
        {
            List<int[]>[] graph = new List<int[]>[n];
            for (int i = 0; i < n; ++i) graph[i] = new List<int[]>();
            foreach (var ed in edges)
            {
                graph[ed[0]].Add(new int[] { ed[1], ed[2] });
                graph[ed[1]].Add(new int[] { ed[0], ed[2] });
            }

            HashSet<int> visited = new HashSet<int>();
            Dictionary<int, int> halfWays = new Dictionary<int, int>();
            Heap<int[]> hp = new Heap<int[]>((a, b) => a[1] < b[1]);
            hp.Push(new int[] { 0, 0 });
            int reachable = 0;
            while (hp.Any())
            {
                var arr = hp.Pop();
                int node = arr[0], moves = arr[1];

                // visited.Add(node); // 坑啊！！！要考虑到这个node可能会进来两次，第二次就不要加reachable!!!
                if (!visited.Add(node)) continue;

                reachable++;
                foreach (var ed in graph[node])
                {
                    int next = ed[0], cnt = ed[1];
                    if (visited.Contains(next))
                    {
                        // although next has been visited, but could be halfway
                        int key = (next << 12) | node;
                        if (halfWays.ContainsKey(key))
                        {
                            int left = halfWays[key];
                            halfWays.Remove(key);
                            reachable += Math.Min(left, maxMoves - moves);
                        }
                    }
                    else
                    {
                        int[] nextStep = { next, moves + cnt + 1 };
                        if (nextStep[1] <= maxMoves)
                        {
//#if DEBUG
//                            Console.WriteLine($"{node} {next} {nextStep[1]}");
//#endif
                            hp.Push(nextStep);
                            reachable += cnt;
                        }
                        else if (moves < maxMoves)
                        {   // edge is too long to reach
                            int take = maxMoves - moves;
                            int left = cnt - take;
                            reachable += take;
                            halfWays[(node << 12) | next] = left;
                        }
                    }
                }
            }
            return reachable;
        }

        internal static void Run()
        {
            int ans = new P0882细分图中的可到达结点().ReachableNodes(
                new int[][] { new int[] { 2, 4, 2 }, new int[] { 3, 4, 5 }, new int[] { 2, 3, 1 }, new int[] { 0, 2, 1 }, new int[] { 0, 3, 5 } },
             14, 5);
            Console.WriteLine(ans);
        }
    }
}
