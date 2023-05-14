using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest
{
    // hard, 2022/9/10
    // DP + 栈模拟回溯
    internal class LCP053
    {
        public int DefendSpaceCity(int[] time, int[] position)
        {
            int mp = position.Max();
            HashSet<(int, int)> pts = new();
            for (int i = 0; i < time.Length; ++i)
                pts.Add((position[i], time[i]));
            Dictionary<int, int> dp = new();

            IEnumerable<(int, int)> PermuteNext(int p, int k)
            {
                if (p == 0)
                {
                    int i = 0, mp = 0;
                    Stack<int> stk = new();
                    while (i >= 0)
                    {
                        if (i == 5)
                        {   // output
                            int c = 0;
                            bool b = false;
                            for (int j = 1; j <= 5; ++j)
                                if ((mp & (1 << j)) > 0)
                                {
                                    c += b ? 1 : 2;
                                    b = true;
                                }
                                else b = false;
                            yield return (mp, c);
                            mp &= 31;
                            --i;
                            continue;
                        }
                        int tp = 0;
                        if (i < stk.Count)
                            tp = stk.Pop() + 1;
                        if (tp == 2)
                        {
                            mp &= (1 << i) - 1;
                            --i;
                            continue;
                        }
                        if (tp == 0 || pts.Contains((p, i + 1)))
                        {
                            stk.Push(tp);
                            if (tp == 1)
                                mp |= 1 << i + 1;
                            ++i;
                            continue;
                        }
                        else
                        {
                            stk.Push(tp);
                            continue;
                        }
                    }
                }
                else
                {
                    int i = 0, mp = 0;
                    Stack<int> stk = new();
                    while (i >= 0)
                    {
                        if (i == 5)
                        {   // output
                            int c = 0, last = -1;
                            foreach (int s in stk.Reverse())
                            {
                                c += s > 0 && s == last ? 1 : s == 1 ? 2 : s == 2 ? 3 : 0;
                                last = s;
                            }
                            yield return (mp, c);
                            mp &= 31;
                            --i;
                            continue;
                        }
                        int t = i + 1, tp = 0;
                        if (i < stk.Count)
                            tp = stk.Pop() + 1;
                        if (tp == 3)
                        {
                            mp &= (1 << i) - 1;
                            --i;
                            continue;
                        }
                        bool ok = true;
                        if ((tp == 0 || tp == 1) && (k & (1 << t)) == 0 && pts.Contains((p - 1, t)))
                            ok = false;
                        else if (tp == 1 && !pts.Contains((p, t)))
                            ok = false;
                        else if (tp == 2 && (k & (1 << t)) > 0)
                            ok = false;

                        if (ok)
                        {
                            stk.Push(tp);
                            if (tp > 0)
                                mp |= 1 << t;
                            ++i;
                            continue;
                        }
                        else
                        {
                            stk.Push(tp);
                            continue;
                        }
                    }
                }
            }

            foreach ((int k, int c) in PermuteNext(0, 0))
                dp[k] = c;
            for (int p = 1; p <= mp; ++p)
            {
                Dictionary<int, int> d = new();
                foreach (int k in dp.Keys)
                    foreach ((int nk, int c) in PermuteNext(p, k))
                        d[nk] = d.ContainsKey(nk) ? Math.Min(d[nk], dp[k] + c) : dp[k] + c;
                dp = d;
            }

            if (dp == null || !dp.Any()) return 0;
            return dp.Keys
                .Where(k => !Enumerable.Range(1, 5).Any(t => (k & (1 << t)) == 0 && pts.Contains((mp, t))))
                .Select(k => dp[k])
                .Min();
        }

        internal static int Run()
        {
            var sln = new LCP053();
            //int[] time = { 1, 2, 1 }, position = { 6, 3, 3 };
            int[] time = { 1, 1, 1, 2, 2, 3, 5 }, position = { 1, 2, 3, 1, 2, 1, 3 };
            Console.WriteLine(sln.DefendSpaceCity(time, position));
            return 0;
        }
        /** ARCHIVE
         * 中间过程允许漏掉，但最后结果不允许
            bool DefendLast(int m)
            {
                for (int i = 1; i <= 5; ++i)
                    if ((m & (1 << i)) == 0 && pts.Contains((mp, i)))
                        return false;
                return true;
            }
            // DefendLast(k)
         * */
    }
}
