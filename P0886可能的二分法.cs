using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/10/16 daily
    // BFS
    internal class P0886可能的二分法
    {
        // 最后写得巨复杂，过了。。
        public bool PossibleBipartition(int n, int[][] dislikes)
        {
            List<int>[] di = new List<int>[n + 1];
            for (int i = 1; i <= n; i++) di[i] = new();
            foreach (var d in dislikes)
                di[d[0]].Add(d[1]);

            int[] a = new int[n + 1];
            Queue<int> qu = new();
            qu.Enqueue(1);
            while (qu.Any())
            {
                int i = qu.Dequeue();
                if (a[i] == 0) 
                {
                    int b = di[i].FirstOrDefault(t => a[t] != 0);
                    a[i] = a[b] == 1 ? 2 : 1;
                }
                foreach (int j in di[i])
                    if (a[j] == a[i]) return false;
                    else if (a[j] == 0)
                    {
                        a[j] = 3 - a[i];
                        qu.Enqueue(j);
                    }
                if (!qu.Any())
                {
                    i = Enumerable.Range(1, n).FirstOrDefault(t => a[t] == 0);
                    if (i != 0) qu.Enqueue(i);
                }
            }

            return true;
        }

        // ver2 - 还是WA
        public bool PossibleBipartition_ver2(int n, int[][] dislikes)
        {
            List<int>[] di = new List<int>[n + 1];
            for (int i = 1; i <= n; i++) di[i] = new();
            foreach (var d in dislikes)
                di[d[0]].Add(d[1]);

            int[] a = new int[n + 1];
            for (int i = 1; i <= n; i++)
            {
                if (a[i] == 0) // 漏了这个判断导致case2失败！
                {
                    int b = di[i].FirstOrDefault(t => a[t] != 0);
                    a[i] = a[b] == 1 ? 2 : 1;
                }
                foreach (int j in di[i])
                    if (a[j] == a[i]) return false;
                    else if (a[j] == 0) a[j] = 3 - a[i];
            }

            return true;
        }

        // ver1 - WA
        public bool PossibleBipartition_ver1(int n, int[][] dislikes)
        {
            HashSet<(int, int)> hs = new();
            foreach (var d in dislikes)
                hs.Add((Math.Min(d[0], d[1]), Math.Max(d[0], d[1])));
            int[] a = new int[n + 1];
            for (int i = 1; i <= n; ++i)
            {
                if (a[i] == 0) a[i] = 1;
                for (int j = i + 1; j <= n; ++j)
                    if (hs.Contains((i, j)))
                        if (a[j] == a[i]) return false;
                        else if (a[j] == 0) a[j] = 3 - a[i];
            }
            return true;
        }

        internal static void Run()
        {
            // case1: 答案：true
            int n = 50;
            var da = "[[21,47],[4,41],[2,41],[36,42],[32,45],[26,28],[32,44],[5,41],[29,44],[10,46],[1,6],[7,42],[46,49],[17,46],[32,35],[11,48],[37,48],[37,43],[8,41],[16,22],[41,43],[11,27],[22,44],[22,28],[18,37],[5,11],[18,46],[22,48],[1,17],[2,32],[21,37],[7,22],[23,41],[30,39],[6,41],[10,22],[36,41],[22,25],[1,12],[2,11],[45,46],[2,22],[1,38],[47,50],[11,15],[2,37],[1,43],[30,45],[4,32],[28,37],[1,21],[23,37],[5,37],[29,40],[6,42],[3,11],[40,42],[26,49],[41,50],[13,41],[20,47],[15,26],[47,49],[5,30],[4,42],[10,30],[6,29],[20,42],[4,37],[28,42],[1,16],[8,32],[16,29],[31,47],[15,47],[1,5],[7,37],[14,47],[30,48],[1,10],[26,43],[15,46],[42,45],[18,42],[25,42],[38,41],[32,39],[6,30],[29,33],[34,37],[26,38],[3,22],[18,47],[42,48],[22,49],[26,34],[22,36],[29,36],[11,25],[41,44],[6,46],[13,22],[11,16],[10,37],[42,43],[12,32],[1,48],[26,40],[22,50],[17,26],[4,22],[11,14],[26,39],[7,11],[23,26],[1,20],[32,33],[30,33],[1,25],[2,30],[2,46],[26,45],[47,48],[5,29],[3,37],[22,34],[20,22],[9,47],[1,4],[36,46],[30,49],[1,9],[3,26],[25,41],[14,29],[1,35],[23,42],[21,32],[24,46],[3,32],[9,42],[33,37],[7,30],[29,45],[27,30],[1,7],[33,42],[17,47],[12,47],[19,41],[3,42],[24,26],[20,29],[11,23],[22,40],[9,37],[31,32],[23,46],[11,38],[27,29],[17,37],[23,30],[14,42],[28,30],[29,31],[1,8],[1,36],[42,50],[21,41],[11,18],[39,41],[32,34],[6,37],[30,38],[21,46],[16,37],[22,24],[17,32],[23,29],[3,30],[8,30],[41,48],[1,39],[8,47],[30,44],[9,46],[22,45],[7,26],[35,42],[1,27],[17,30],[20,46],[18,29],[3,29],[4,30],[3,46]]";

            // case 2: false
            //int n = 3;
            //var da = "[[1,2],[1,3],[2,3]]";

            int[][] dislikes = JsonConvert.DeserializeObject<int[][]>(da);
            Console.WriteLine("n={0} result={1}", n, new P0886可能的二分法().PossibleBipartition(n, dislikes));
        }
    }
}
