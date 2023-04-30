using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 周赛 WC284-D // 2022.3.14
    // dijkstra 扩展
    internal class P2203得到要求路径的最小带权子图
    {
        // ver3 三次dijkstra - AC
        // 做完之后才经高人提点发现这题不需要用优先队列
        public long MinimumWeight(int n, int[][] edges, int src1, int src2, int dest)
        {
            long[] a1 = edges.DijkstraAll(n, src1),
                   a2 = edges.DijkstraAll(n, src2),
                   da = edges.DijkstraAll(n, dest, true);
            var qry = Enumerable.Range(0, n).Where(i => a1[i] >= 0 && a2[i] >= 0 && da[i] >= 0).Select(i => a1[i] + a2[i] + da[i]);
            if (qry.Any()) return qry.Min();
            else return -1;
        }

        // ver2: TLE
        // 超时原因：搜索一条边是n，两边同时进行就变成n*n了
        //MinimumWeight=6015
        //Total Time: 164719ms
        public long MinimumWeight_TLE(int n, int[][] edges, int src1, int src2, int dest)
        {
            var dg = edges.DirectedGraphWithLength(); 
            for (int k = -1; k < n; ++k)
                if (!dg.ContainsKey(k))
                    dg[k] = new();
            SHeap<(int, int), long> hp = new((a, b) => a < b, true);
            hp.Add((src1, src2), 0L);
            while (hp.Any())
            {
                ((int s1, int s2), long path) = hp.Pop();
                //Console.WriteLine("{0} {1} {2}", s1, s2, path); // debug
                if (s1 == dest && s2 == dest) return path;
                if (s1 == s2)
                {
                    foreach ((int nt, int len) in dg[s1])
                    {
                        hp.Add((nt, nt), path + len);
                    }
                }
                else
                {
                    if (s1 != dest)
                    foreach ((int n1, int len) in dg[s1])
                    {
                        hp.Add((n1, s2), path + len);
                    }
                    if (s2 != dest)
                    foreach ((int n2, int len) in dg[s2])
                    {
                        hp.Add((s1, n2), path + len);
                    }
                }
            }
            return -1;
        }
        /* ver1 WA
        public long MinimumWeight(int n, int[][] edges, int src1, int src2, int dest) {
            var dg = edges.BuildGraph(); 
            for (int k = -1; k < n; ++k)
                if (!dg.ContainsKey(k))
                    dg[k] = new();
            BitArray visit1 = new(n), visit2 = new(n);
            SHeap<(int, int), long> hp = new((a, b) => a < b);
            hp.Add((src1, src2), 0L);
            while (hp.Any())
            {
                ((int s1, int s2), long path) = hp.Pop();
                //Console.WriteLine("{0} {1} {2}", s1, s2, path);
                if (s1 == -1)
                {
                    visit1[s2] = visit2[s2] = true;
                    if (s2 == dest) return path;
                }
                else if (s2 == -1)
                {
                    visit1[s1] = visit2[s1] = true;
                    if (s1 == dest) return path;
                }
                else
                {
                    visit1[s1] = visit2[s2] = true;
                }
                foreach ((int n1, int len) in dg[s1])
                {
                    if (visit1[n1]) continue;
                    if (visit2[n1])
                        hp.Add((-1, s2), path + len);
                    else
                        hp.Add((n1, s2), path + len);
                }
                foreach ((int n2, int len) in dg[s2])
                {
                    if (visit2[n2]) continue;
                    if (visit1[n2])
                        hp.Add((s1, -1), path + len);
                    else
                        hp.Add((s1, n2), path + len);
                }
            }
            return -1;        
        }
         * */

        internal static void Run()
        {
            var sln = new P2203得到要求路径的最小带权子图();
            long ans = 0L;
            // TLE
            //MinimumWeight=6015
            //Total Time: 164719ms // update: 188ms
            int[][] edges = Common.ReadInput<int[][]>(2203);
            //var l1 = edges.Dijkstra(2762, 9567);
            //var l2 = edges.Dijkstra(45, 9567);
            //Console.WriteLine("L1={0} L2={1}", l1, l2);
            ans = sln.MinimumWeight(11790, edges, 2762, 45, 9567); // 6015

            //// case 48/78 - WA (ans:-1, expect: 91) // update: AC
            //int[][] edges = new int[][] { new int[] { 5, 8, 28 }, new int[] { 5, 4, 25 }, new int[] { 5, 1, 42 }, new int[] { 0, 6, 22 }, new int[] { 5, 9, 26 }, new int[] { 0, 2, 35 }, new int[] { 5, 3, 10 }, new int[] { 0, 7, 41 }, new int[] { 9, 7, 24 }, new int[] { 6, 7, 19 }, new int[] { 2, 7, 23 } };
            //var ans = sln.MinimumWeight(10, edges, 0, 5, 7); // 91

            //// 示例-通过
            //int[][] edges = new int[][] { new int[] { 0, 2, 2 }, new int[] { 0, 5, 6 }, new int[] { 1, 0, 3 }, new int[] { 1, 4, 5 }, new int[] { 2, 1, 1 }, new int[] { 2, 3, 3 }, new int[] { 2, 3, 4 }, new int[] { 3, 4, 2 }, new int[] { 4, 5, 1 } };
            //var ans = sln.MinimumWeight(6, edges, 0, 1, 5); // 9

            Console.WriteLine("MinimumWeight=" + ans); 
        }
    }
}
