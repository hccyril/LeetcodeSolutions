using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace ConsoleCore1
{
    // 2023/7/9 - 45分钟挑战失败
    // 2023/7/9 11:57 - 实际做完大概用了75分钟左右
    class NodeStruct
    {
        internal int id;
        internal (int, int)[] sa = new (int, int)[3];
        public void Update(int k, int v)
        {
            if (sa[0].Item2 == 0 || v > sa[0].Item2)
            {
                sa[2] = sa[1];
                sa[1] = sa[0];
                sa[0] = (k, v);
            }
            else if (sa[1].Item2 == 0 || v > sa[1].Item2)
            {
                sa[2] = sa[1];
                sa[1] = (k, v);
            }
            else if (sa[2].Item2 == 0 || v > sa[2].Item2)
            {
                sa[2] = (k, v);
            }
        }

        internal static int Merge(NodeStruct e, NodeStruct f)
        {
            int maxs = -1;
            foreach ((int k1, int v1) in e.sa)
            {
                if (v1 == 0 || k1 == f.id) continue;
                foreach ((int k2, int v2) in f.sa)
                {
                    if (v2 == 0 || k2 == e.id || k2 == k1) continue;
                    maxs = Math.Max(maxs, v1 + v2);
                }
            }
            return maxs;
        }
    }
    internal class P2242节点序列的最大得分
    {
        
        public int MaximumScore(int[] scores, int[][] edges)
        {
            int n = scores.Length;
            NodeStruct[] a = new NodeStruct[n];
            for (int i = 0; i < n; ++i)
                a[i] = new NodeStruct() { id = i };
            foreach (var ed in edges)
            {
                int x = ed[0], y = ed[1];
                a[x].Update(y, scores[y]);
                a[y].Update(x, scores[x]);
            }
            int maxs = -1;
            foreach (var ed in edges)
            {
                int x = ed[0], y = ed[1], s = -1;
                if ((s = NodeStruct.Merge(a[x], a[y])) > 0)
                    maxs = Math.Max(maxs, s + scores[x] + scores[y]);

            }
            return maxs;
        }

        internal static void Run()
        {
            int[] scores = { 9, 20, 6, 4, 11, 12 };
            int[][] edges = "[[0,3],[5,3],[2,4],[1,3]]".ToTestInput<int[][]>();
            var sln = new P2242节点序列的最大得分();
            int ans= sln.MaximumScore(scores, edges);
            Console.WriteLine("ans=" + ans);
        }
    }
}
