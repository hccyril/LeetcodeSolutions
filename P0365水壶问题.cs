using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/6/29
    // 神坑，提交了四次都没过
    internal class P0365水壶问题
    {
        // ver2: GCD 参考评论区
        public bool CanMeasureWater_gcd(int jug1Capacity, int jug2Capacity, int targetCapacity)
        {
            if (jug1Capacity + jug2Capacity < targetCapacity) return false; // 要加上这句才对，否则WA
            if (jug1Capacity == jug2Capacity) return targetCapacity == jug1Capacity || jug1Capacity + jug1Capacity == targetCapacity;
            return targetCapacity % MathEX.Gcd(jug1Capacity, jug2Capacity) == 0;
        }

        // stack overflow (5000多次就爆了）
        bool Dfs(int i1, int i2, int t, int f1 = 0, int f2 = 0)
        {
            if (!hs.Add((f1, f2))) return false;
            Console.WriteLine("{0} {1} hs.cnt={2}", f1, f2, hs.Count); // DEBUG
            if (f1 == t || f2 == t || f1 + f2 == t) return true;
            if (f1 > 0 && Dfs(i1, i2, t, 0, f2)) return true;
            if (f2 > 0 && Dfs(i1, i2, t, f1, 0)) return true;
            if (f1 < i1 && Dfs(i1, i2, t, i1, f2)) return true;
            if (f2 < i2 && Dfs(i1, i2, t, f1, i2)) return true;
            if (f1 < i1 && f2 > 0)
            {
                int fill = Math.Min(i1 - f1, f2);
                if (Dfs(i1, i2, t, f1 + fill, f2 - fill)) return true;
            }
            if (f2 < i2 && f1 > 0)
            {
                int fill = Math.Min(i2 - f2, f1);
                if (Dfs(i1, i2, t, f1 - fill, f2 + fill)) return true;
            }
            return false;
        }

        // ver1: 把可以算出来的考虑到了，剩下的用BFS
        public bool CanMeasureWater(int jug1Capacity, int jug2Capacity, int targetCapacity)
        {
            int i1 = Math.Min(jug1Capacity, jug2Capacity), i2 = Math.Max(jug1Capacity, jug2Capacity), t = targetCapacity;
            if (t == i1 || t == i2 || t == i1 + i2 || t == i2 - i1) return true;
            if (i1 == i2) return false;
            if (t < i1 + i2 && t % i1 == 0) return true;
            if (t < i1 + i2 && t - i1 + i2 % i1 > 0 && (t - i1 + i2 % i1) % i1 == 0) return true;
            int diff = i2 - i1;
            if (t % diff == 0 && (t < i1 || t < i2 && t == (i1 / diff * diff + diff))) return true;
            if (t < i1 && (i1 - t) % diff == 0) return true;
            if (i1 / diff * diff + diff < i2 && i1 / diff * diff + diff - i1 == t) return true;
            return Bfs(i1, i2, t);
        }

        HashSet<(int, int)> hs = new();

        // BFS - 7万多次搜索
        bool AddQ(Queue<(int, int)> qu, int i1, int i2, int t, int f1, int f2)
        {
            if (f1 == t || f2 == t || f1 + f2 == t) return true;
            if (!hs.Add((f1, f2))) return false;
            qu.Enqueue((f1, f2));
            return false;
        }
        bool Bfs(int i1, int i2, int t)
        {
            Queue<(int, int)> qu = new();
            hs.Add((0, 0));
            qu.Enqueue((0, 0));
            while (qu.Any())
            {
                (int f1, int f2) = qu.Dequeue();
               // Console.WriteLine("{0} {1} hs.cnt={2}", f1, f2, hs.Count); // DEBUG
                if (f1 > 0 && AddQ(qu, i1, i2, t, 0, f2)) return true;
                if (f2 > 0 && AddQ(qu, i1, i2, t, f1, 0)) return true;
                if (f1 < i1 && AddQ(qu, i1, i2, t, i1, f2)) return true;
                if (f2 < i2 && AddQ(qu, i1, i2, t, f1, i2)) return true;
                if (f1 < i1 && f2 > 0)
                {
                    int fill = Math.Min(i1 - f1, f2);
                    if (AddQ(qu, i1, i2, t, f1 + fill, f2 - fill)) return true;
                }
                if (f2 < i2 && f1 > 0)
                {
                    int fill = Math.Min(i2 - f2, f1);
                    if (AddQ(qu, i1, i2, t, f1 - fill, f2 + fill)) return true;
                }
            }
            return false;
        }

        static bool test(int c1, int c2, int t)
        {
            var sln = new P0365水壶问题();
            Console.Write($"Run: {c1} {c2} {t} == ");
            var result = sln.CanMeasureWater(c1, c2, t);
            Console.WriteLine(result);
            return result;
        }

        internal static void Run()
        {
            System.Diagnostics.Debug.Assert(test(3, 5, 4) == true);
            System.Diagnostics.Debug.Assert(test(2, 6, 5) == false);
            System.Diagnostics.Debug.Assert(test(1, 2, 3) == true);
            System.Diagnostics.Debug.Assert(test(11, 3, 13) == true);
            System.Diagnostics.Debug.Assert(test(13, 11, 1) == true);
            System.Diagnostics.Debug.Assert(test(11, 13, 3) == true);
            System.Diagnostics.Debug.Assert(test(22003, 31237, 1) == true);
            System.Diagnostics.Debug.Assert(test(1, 1, 12) == false);
            System.Diagnostics.Debug.Assert(test(104693, 104701, 324244) == false);
            Console.WriteLine(nameof(P0365水壶问题) + ": 全部测试用例通过");
        }
    }
}
