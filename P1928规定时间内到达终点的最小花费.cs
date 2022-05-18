using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/5/16
    // rank: 2413
    // advanced dijkstra
    internal class P1928规定时间内到达终点的最小花费
    {
        public int MinCost(int maxTime, int[][] edges, int[] passingFees)
        {
            var dg = edges.UndirectedGraphWithLength();
            SHeap<(int, int), int> hp = new((a, b) => a < b);
            int[] minTime = new int[passingFees.Length];
            minTime[0] = -1;
            hp.Add((0, 0), passingFees[0]);
            while (hp.Any())
            {
                ((int node, int time), int cost) = hp.Pop();
                //Console.WriteLine("node={0} cost={1} time={2}", node, cost, time); // DEBUG
                if (node == passingFees.Length - 1) return cost;
                if (minTime[node] > 0 && time >= minTime[node]) continue;
                if (time > 0 && minTime[node] == 0 || time < minTime[node]) minTime[node] = time;
                foreach ((int next, int travelTime) in dg[node])
                {
                    int t = time + travelTime;
                    if (t <= maxTime && (minTime[next] == 0 || t < minTime[next]))
                    {
                        hp.Add((next, t), cost + passingFees[next]);
                        //Console.WriteLine("\t{0} -> {1} (cost={2} time={3})", node, next, cost + passingFees[next], t);
                    }
                }
            }
            return -1;
        }

        class InputStruct
        {
            public int maxTime { get; set; }
            public int[][] edges { get; set; }
            public int[] passingFees { get; set; }
        }
        internal static void Run()
        {
            // test case 85 / 92 - WA
            // my: 1455, exp: 1158
            var input = Common.ReadInput<InputStruct>(1928);
            var sln = new P1928规定时间内到达终点的最小花费();
            int ans = sln.MinCost(input.maxTime, input.edges, input.passingFees);
            Console.WriteLine(ans);
        }
    }
}
