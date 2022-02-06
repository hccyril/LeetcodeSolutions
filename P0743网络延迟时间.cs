using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, dijkstra // 2021/08/02 daily
    class P0743网络延迟时间
    {
        // 使用SuperHeap再做一遍 2021/11/20 // 在US网站上提交比第一次快了一点点(227ms vs 232ms)
        // 2022/1/23 更新Update方法
        public int NetworkDelayTime_SuperHeap(int[][] times, int n, int k)
        {
            // init
            List<int>[] dests = new List<int>[n + 1];
            int[,] path = new int[n + 1, n + 1];
            foreach (var t in times)
            {
                if (dests[t[0]] == null) dests[t[0]] = new List<int>();
                dests[t[0]].Add(t[1]);
                path[t[0], t[1]] = t[2];
            }

            // init 2
            HashHeap hh = new HashHeap(false);
            hh.Push(k, 0);
            HashSet<int> hs = new HashSet<int>();

            // dijkstra
            int w = 0;
            while (hh.Any())
            {
                (k, w) = hh.Pop();
                hs.Add(k); if (dests[k] == null) continue;
                foreach (var v in dests[k]) if (!hs.Contains(v))
                    {
                        int vw = w + path[k, v];
                        if (hh.ContainsKey(v))
                        {
                            int w0 = hh[v];
                            if (vw < w0) hh.Update(v, vw - w0);
                            //(int i, int w0) = hh.Get(v);
                            //if (vw < w0) hh.UpdateAt(i, vw);
                        }
                        else hh.Push(v, vw);
                    }
            }

            // done
            return w;
        }
        public int NetworkDelayTime(int[][] times, int n, int k)
        {
            int count = n;
            int[] delays = new int[n + 1]; // delays[0]存最大延迟
            Array.Fill(delays, -1); // 居然一直都不知道这句！！
            //for (int i = 1; i <= n; ++i) delays[i] = -1;
            Heap<int[]> heap = new Heap<int[]>((a, b) => a[1] < b[1]);
            heap.Push(new int[] { k, 0 });
            while (heap.Any() && count > 0)
            {
                var p = heap.Pop();
                if (delays[p[0]] < 0)
                {
                    delays[p[0]] = p[1];
                    if (p[1] > delays[0]) delays[0] = p[1];
                    count--;
                    foreach (var t in times)
                        if (t[0] == p[0] && delays[t[1]] < 0)
                            heap.Push(new int[] { t[1], p[1] + t[2] });
                }
            }
            return count == 0 ? delays[0] : -1;
        }
    }
}
