using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest
{
    // hard, 2023/5/4
    // BFS, 两次WA之后通过
    internal class LCP009
    {
        public int MinJump(int[] jump)
        {
            int n = jump.Length;
            if (jump[0] >= n) return 1;
            Queue<(int, int)> qu = new(); // forward: qu(p, s): jump to p need s steps
            List<(int, int)> stk = new(); // backward: stk(p, s): min jump back steps for pos[:p] = s
            int[] a = new int[n]; // a[i]: min steps to jump to pos i
            Array.Fill(a, -1);
            a[0] = 0;
            qu.Enqueue((jump[0], 1));
            stk.Add((jump[0], 2));
            int i = 1, si = 0, bs = 2;
            while (i < n)
            {
                // adjust i
                while (a[i] >= 0) ++i;

                // adjust stk
                while (si < stk.Count && stk[si].Item1 <= i)
                    ++si;
                
                if (si < stk.Count) bs = stk[si].Item2;
                else bs = n + 1; // set bs = INF
                

                // adjust queue
                while (qu.Any() && qu.Peek().Item1 < i)
                    qu.Dequeue();

                // start - check if process from i or qu
                (int p, int s) = qu.Any() && qu.Peek().Item2 < bs ? qu.Dequeue() : (i, bs);
                // process
                a[p] = s;
                (p, s) = (p + jump[p], s + 1);
                if (p >= n) return s;
                qu.Enqueue((p, s));
                ++s; //(p, s) = (jump[p], s + 2);
                if (p > stk.Last().Item1)
                {
                    if (s <= stk.Last().Item2)
                        stk.RemoveAt(stk.Count - 1);
                    stk.Add((p, s));
                }
            }

            return -1;
        }

        internal static void Run()
        {
            var s = "[5,3,2,1,4,4,2,2,3,1,1,3,1,2,5,3,5,2,5,2,2,5,5,2,5,5,1,1,3,1,2,5,1,5,2,4,1,4,2,4,4,1,3,4,5,3,1,4,4,2]";
            //var s = "[1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1]";
            //string s = "[10,8,24,38,36,28,3,1,2,1,2,24,2,4,15,6,5,4,1,7,2,5,6,1,6,8,4,8,6,2,2,6,3,1,8,3,5,1,6,3,6,9,15,5,8,4,8,11,1,657]";
            int[] input = s.ToTestInput<int[]>();
            var sln = new LCP009();
            var ans = sln.MinJump(input);
            Console.WriteLine("ans=" + ans);
        }
    }
}
