using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/7/2
    // rank: 2391
    // BFS (如何构建状态是关键）
    // key: target, value: numSteps, speed
    internal class P0818赛车
    {
        public int Racecar(int target)
        {
            if (target == 1) return 1; // 又贡献了一个错误提交= =|||
            Dictionary<int, int> map = new();
            HashSet<(int, int)> hs = new();
            Queue<(int, int, int)> qu = new();
            map[0] = 0; map[1] = 1;
            hs.Add((0, 1));
            qu.Enqueue((0, 1, 0));
            while (qu.Any())
            {
                (int t, int sp, int mv) = qu.Dequeue();

                // A
                if (Math.Abs(sp) <= target) {
                    int next_t = t + sp, next_sp = sp * 2, next_mv = mv + 1;
                    if (hs.Add((next_t, next_sp)))
                    {
                        qu.Enqueue((next_t, next_sp, next_mv));
                        next_t = next_t + next_sp;
                        if (!map.ContainsKey(next_t))
                        {
                            if (next_t == target) return next_mv + 1;
                            map[next_t] = next_mv + 1;
                        }
                    }
                }

                // R
                int r = sp > 0 ? -1 : 1;
                if (hs.Add((t, r)))
                {
                    qu.Enqueue((t, r, mv + 1));
                    int next_t = t + r;
                    if (!map.ContainsKey(next_t))
                    {
                        if (next_t == target) return mv + 2;
                        map[next_t] = mv + 2;
                    }
                }
            }
            return map[target];
        }
    }
}
