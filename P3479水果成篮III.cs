using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleCore1;

// medium, 2025/8/6 Daily
// rating 2193
// 树状数组
internal class P3479水果成篮III
{
    // 树状数组（修改后，添加了队列维护值相同的篮子）
    // AC: 2025/8/6 by hccyril
    public int NumOfUnplacedFruits(int[] fruits, int[] baskets)
    {
        int n = fruits.Length, mask = (1 << 30) - 1;
        for (int i = 0; i < n; ++i)
        {
            baskets[i] = baskets[i] ^ mask;
            fruits[i] = fruits[i] ^ mask;
        }

        // 离散化预处理
        SortedSet<int> ss = [.. fruits.Concat(baskets)];
        Dictionary<int, int> dic = [];
        int idx = 1;
        foreach (var x in ss)
        {
            dic[x] = idx++;
        }
        for (int i = 0; i < n; ++i)
        {
            baskets[i] = dic[baskets[i]];
            fruits[i] = dic[fruits[i]];
        }

        // 树状数组求解
        Queue<int>[] qs = [.. Enumerable.Range(0, idx).Select(_ => new Queue<int>())];
        MaxFenwick<int> ft = new(idx, 0);
        for (int i = 0; i < n; ++i)
        {
            qs[baskets[i]].Enqueue(n - i);
            if (qs[baskets[i]].Count == 1)
                ft.Up(baskets[i], n - i);
        }

        bool Query(int x)
        {
            if (ft.Max(x) == 0)
                return false;
            int v = ft.Max(x), b = baskets[n - v];
            qs[b].Dequeue();
            if (qs[b].Count > 0)
                ft.Down(b, qs[b].Peek());
            else
                ft.Down(b, 0);
            return true;
        }
        return fruits.Count(x => !Query(x));
    }

    // 树状数组
    // WA: [23,62,47,18], [39,94,49,39]
    // 39有0和3两个下标，这个没有考虑到
    public int NumOfUnplacedFruits_1(int[] fruits, int[] baskets)
    {
        int n = fruits.Length, mask = (1 << 20) - 1; // WA: 应该是30不是20，低级错误
        for (int i = 0; i < n; ++i)
        {
            baskets[i] = baskets[i] ^ mask;
            fruits[i] = fruits[i] ^ mask;
        }

        // 离散化预处理
        SortedSet<int> ss = [.. fruits.Concat(baskets)];
        Dictionary<int, int> dic = [];
        int idx = 1;
        foreach (var x in ss)
        {
            dic[x] = idx++;
        }
        for (int i = 0; i < n; ++i)
        {
            baskets[i] = dic[baskets[i]];
            fruits[i] = dic[fruits[i]];
        }

        // 树状数组
        MaxFenwick<int> ft = new(idx, 0);
        for (int i = 0; i < n; ++i)
            ft.Up(baskets[i], n - i);

        bool Query(int x)
        {
            if (ft.Max(x) == 0)
                return false;
            int v = ft.Max(x), b = baskets[n - v];
            ft.Down(b, 0);
            return true;
        }
        return fruits.Count(x => !Query(x));
    }

    // 使用有序集合的版本，写到一半发现还是要枚举添加
    public int NumOfUnplacedFruits_SortedSet(int[] fruits, int[] baskets)
    {
        const long MASK = (1L << 20) - 1, // 20 bits for basket index
            MAX = 1000000001L << 20 | MASK;

        // build tree
        int n = fruits.Length;
        SortedSet<long>[] ss = [.. Enumerable.Range(0, n).Select(_ => new SortedSet<long>())];
        var rs = new SortedSet<long>();
        for (int i = 0; i < n; ++i)
        {
            long x = baskets[i], k = x | (long)i;
            var a = rs;
            while (a.Count > 0 && a.Max >> 20 >= x)
                a = ss[a.Max & MASK];
            a.Add(k);
        }

        bool Query(int x)
        {
            long k = (long)x << 20;
            if (rs.Count == 0 || rs.Max < k)
            {
                return false;
            }
            else if (rs.Min >= k)
            {
                int i = (int)(rs.Min & MASK);
                rs.Remove(rs.Min);
                foreach (var p in ss[i])
                    rs.Add(p);
                //rs.Concat(ss[i]);
            }
            else
            {
                long c = rs.GetViewBetween(k, MAX).Min;
                rs.Remove(c);
                int i = (int)(c & MASK);
                // TODO
            }
            return true;
        }
        return fruits.Count(x => !Query(x));
    }
}
