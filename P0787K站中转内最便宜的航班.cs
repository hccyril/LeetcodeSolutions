using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // dijkstra with k 
    // 一开始超时，后来加上path数组后得解
    class P0787K站中转内最便宜的航班
    {
        public int FindCheapestPrice(int n, int[][] flights, int src, int dst, int k)
        {
            List<int[]>[] dic = new List<int[]>[n];
            for (int i = 0; i < n; ++i) dic[i] = new List<int[]>();
            foreach (var fl in flights)
                dic[fl[0]].Add(fl);

            int[] path = new int[n];
            for (int i = 0; i < n; ++i) path[i] = 0xFFFFFF;
            Heap<int[]> hp = new Heap<int[]>((a, b) => a[2] < b[2]);
            hp.Push(new int[] { src, -1, 0 });
            while (hp.Any())
            {
                var arr = hp.Pop();
                int node = arr[0], trans = arr[1], cost = arr[2];
                if (node == dst) return cost;
                path[node] = trans;
                if (trans < k)
                    foreach (var fl in dic[node])
                        if (trans + 1 < path[fl[1]])
                            hp.Push(new int[] { fl[1], trans + 1, cost + fl[2] });
            }
            return -1;
        }

        internal static void Run()
        {
            //answer: -1
            //Total Time: 53969ms -> 50235ms -> 16ms
            var flights = new int[][] { new int[] { 11, 12, 74 }, new int[] { 1, 8, 91 }, new int[] { 4, 6, 13 }, new int[] { 7, 6, 39 }, new int[] { 5, 12, 8 }, new int[] { 0, 12, 54 }, new int[] { 8, 4, 32 }, new int[] { 0, 11, 4 }, new int[] { 4, 0, 91 }, new int[] { 11, 7, 64 }, new int[] { 6, 3, 88 }, new int[] { 8, 5, 80 }, new int[] { 11, 10, 91 }, new int[] { 10, 0, 60 }, new int[] { 8, 7, 92 }, new int[] { 12, 6, 78 }, new int[] { 6, 2, 8 }, new int[] { 4, 3, 54 }, new int[] { 3, 11, 76 }, new int[] { 3, 12, 23 }, new int[] { 11, 6, 79 }, new int[] { 6, 12, 36 }, new int[] { 2, 11, 100 }, new int[] { 2, 5, 49 }, new int[] { 7, 0, 17 }, new int[] { 5, 8, 95 }, new int[] { 3, 9, 98 }, new int[] { 8, 10, 61 }, new int[] { 2, 12, 38 }, new int[] { 5, 7, 58 }, new int[] { 9, 4, 37 }, new int[] { 8, 6, 79 }, new int[] { 9, 0, 1 }, new int[] { 2, 3, 12 }, new int[] { 7, 10, 7 }, new int[] { 12, 10, 52 }, new int[] { 7, 2, 68 }, new int[] { 12, 2, 100 }, new int[] { 6, 9, 53 }, new int[] { 7, 4, 90 }, new int[] { 0, 5, 43 }, new int[] { 11, 2, 52 }, new int[] { 11, 8, 50 }, new int[] { 12, 4, 38 }, new int[] { 7, 9, 94 }, new int[] { 2, 7, 38 }, new int[] { 3, 7, 88 }, new int[] { 9, 12, 20 }, new int[] { 12, 0, 26 }, new int[] { 10, 5, 38 }, new int[] { 12, 8, 50 }, new int[] { 0, 2, 77 }, new int[] { 11, 0, 13 }, new int[] { 9, 10, 76 }, new int[] { 2, 6, 67 }, new int[] { 5, 6, 34 }, new int[] { 9, 7, 62 }, new int[] { 5, 3, 67 } };
            Console.WriteLine(new P0787K站中转内最便宜的航班().FindCheapestPrice(13, flights, 10, 1, 10));
        }
    }
}
