using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/4/18
    // BFS
    internal class P0403青蛙过河
    {
        public bool CanCross(int[] stones)
        {
            if (stones.Length < 2 || stones[1] != 1) return false;
            if (stones.Length == 2 && stones[1] == 1) return true; // 漏了这个边界条件！！
            HashSet<int> stoneHash = new(stones);
            Queue<(int, int)> qu = new();
            HashSet<(int, int)> hs = new();
            qu.Enqueue((1, 1));
            hs.Add((1, 1));
            while (qu.Any())
            {
                (int step, int start) = qu.Dequeue();
                foreach (int t in Enumerable.Range(step - 1, 3).Where(t => t > 0))
                {
                    int next = start + t;
                    if (stoneHash.Contains(next))
                    {
                        if (next == stones.Last()) return true;
                        if (next < stones.Last() && hs.Add((t, next)))
                            qu.Enqueue((t, next));
                    }
                }
            }
            return false;
        }
    }
}
