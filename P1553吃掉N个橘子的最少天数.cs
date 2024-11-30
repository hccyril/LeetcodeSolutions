using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2024/5/12 Daily
// dijkstra
internal class P1553吃掉N个橘子的最少天数
{
    public int MinDays(int n)
    {
        SHeap<int, int> hp = new((a, b) => a < b, true);
        hp.Add(n, 0);
        while (hp.Any())
        {
            (int s, int p) = hp.Pop();
            if (s == 1) return p + 1;
            hp.Add(s / 3, p + 1 + s % 3);
            hp.Add(s / 2, p + 1 + s % 2);
        }
        return -1;
    }

    // 直接用BFS会超时
    public int MinDays_TLE(int n)
    {
        Queue<(int, int)> qu = new();
        qu.Enqueue((n, 0));
        while (qu.Any())
        {
            (int x, int c) = qu.Dequeue();
            if (x == 1) return c + 1;
            if (x % 3 == 0) qu.Enqueue((x / 3, c + 1));
            if ((x & 1) == 0) qu.Enqueue((x >> 1, c + 1));
            qu.Enqueue((x - 1, c + 1));
        }
        return -1;
    }
}
