using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/2/3
    // 想多了，原来是贪心，不过贪心的证明很难，参考官方题解
    internal class P1414和为K的最少斐波那契数字数目
    {
        // 最后发现用贪心法可解。。。
        public int FindMinFibonacciNumbers_Greedy(int k)
        {
            List<int> fib = new();
            fib.Add(1); fib.Add(2);
            for (int i = 2, n; (n = fib[i - 1] + fib[i - 2]) <= k; fib.Add(n), ++i) ;
            int cnt = 0;
            for (; k > 0; ++cnt)
                k -= fib.Where(t => t <= k).Max();
            return cnt;
        }

        // ver1: out of memory for k = 513314
        // ver2: added hashset, then TLE for k = 8852252
        public int FindMinFibonacciNumbers(int k)
        {
            List<int> fib = new();
            fib.Add(1); fib.Add(2);
            for (int i = 2, n; (n = fib[i - 1] + fib[i - 2]) <= k; fib.Add(n), ++i) ;
            fib.Reverse();
            Queue<(int n, int cnt)> qu = new();
            qu.Enqueue((k, 0));
            HashSet<int> hs = new();
            hs.Add(k);
            while (qu.Any())
            {
                (int n, int cnt) = qu.Dequeue();
                ++cnt;
                foreach (var fn in fib)
                    if (fn == n) return cnt;
                    else if (fn < n && hs.Add(n - fn)) qu.Enqueue((n - fn, cnt));
            }
            return -1;
        }
    }
}
