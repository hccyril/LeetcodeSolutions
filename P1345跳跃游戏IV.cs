using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/01/15 US daily
    // dijkstra，但路径长度都是1，所以也可以理解为bfs
    internal class P1345跳跃游戏IV
    {
        IList<int> EmptyList = new List<int>();
        IEnumerable<int> Next(int pos, IList<int> list, int[] vs)
        {
            if (pos > 0 && vs[pos - 1] < 0) yield return pos - 1;
            if (pos < vs.Length - 1 && vs[pos + 1] < 0) yield return pos + 1;
            foreach (var n in list)
                if (vs[n] < 0) yield return n;
        }
        public int MinJumps(int[] arr)
        {
            if (arr.Length == 1) return 0;
            if (arr.Length == 2) return 1;
            Dictionary<int, IList<int>> dic = new();
            for (int i = 0; i < arr.Length; ++i)
                (dic.ContainsKey(arr[i]) ? dic[arr[i]] : dic[arr[i]] = new List<int>()).Add(i);
            int[] vs = new int[arr.Length]; Array.Fill(vs, -1);
            Queue<int> qu = new();
            qu.Enqueue(0); vs[0] = 0;
            while (qu.Any())
            {
                int pos = qu.Dequeue(), moves = vs[pos] + 1;
                IList<int> list = EmptyList;
                if (dic.ContainsKey(arr[pos]))
                {
                    list = dic[arr[pos]];
                    dic.Remove(arr[pos]); // 第一次提交忘了这句，然后超时
                }
                foreach (var next in Next(pos, list, vs))
                {
                    vs[next] = moves;
                    if (next == arr.Length - 1) return moves;
                    qu.Enqueue(next);
                }
            }
            return -1;
        }
    }
}
