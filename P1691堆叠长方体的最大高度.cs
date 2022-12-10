using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/12/10 Daily
    // rank只有21xx, 但未能在一个半小时内做出来
    // 5:05-7:20 (2:15)
    // 类似最长递增子序列的DP解法
    // 总结思考：审错题没有看到高度也要满足，造成重大失误，而且高度也限制的话就可以用贪心，每个箱子必定是最长的边作为高
    internal class P1691堆叠长方体的最大高度
    {
        // 审错题了，高度也要严格大于
        public int MaxHeight(int[][] cuboids)
        {
            CubeStruct.MAX_N = cuboids.Length;
            List<CubeStruct> li = new(cuboids.Length * 3), stk = new(cuboids.Length);
            for (int i = 0; i < cuboids.Length; ++i)
            {
                li.AddRange(EnumCubes(i, cuboids[i][0], cuboids[i][1], cuboids[i][2]));
            }
            li.Sort();

            int maxh = 0;

            for (int i = 0; i < li.Count; ++i)
            {
                var c = li[i];
                c.sumH = c.H;
                c.ba[c.i] = c.H;

                for (int j = 0; j < i; ++j)
                {
                    if (li[j].i != c.i && li[j].W >= c.W && li[j].H >= c.H)
                    {
                        int sum = li[j].sumH - li[j].ba[c.i] + c.H;
                        if (sum > c.sumH)
                        {
                            c.sumH = sum;
                            Array.Copy(li[j].ba, c.ba, cuboids.Length);
                            c.ba[c.i] = c.H;
                        }
                    }
                }

                maxh = Math.Max(maxh, c.sumH);
            }

            return maxh;
        }

        class CubeStruct : IComparable<CubeStruct>
        {
            public static int MAX_N;

            public int i, L, W, H, sumH;
            public int[] ba;

            public CubeStruct(int ind, int a, int b, int c)
            {
                i = ind; H = c;
                L = Math.Max(a, b);
                W = Math.Min(a, b);
                ba = new int[MAX_N];
            }

            public int CompareTo(CubeStruct co)
            {
                if (L == co.L)
                {
                    if (W == co.W)
                        return co.H.CompareTo(H);
                    return co.W.CompareTo(W);
                }
                else return co.L.CompareTo(L);
            }
        }

        private IEnumerable<CubeStruct> EnumCubes(int i, int a, int b, int c)
        {
            yield return new(i, b, c, a);
            if (b != a)
                yield return new(i, a, c, b);
            if (c != a && c != b)
                yield return new(i, a, b, c);
        }

        public int MaxHeight_ver1_WA(int[][] cuboids)
        {
            List<CubeStruct> li = new(cuboids.Length * 3), stk = new(cuboids.Length);
            for (int i = 0; i < cuboids.Length; ++i)
            {
                li.AddRange(EnumCubes(i, cuboids[i][0], cuboids[i][1], cuboids[i][2]));
            }
            li.Sort();

            /*/ DEBUG
            foreach (CubeStruct c in li)
            {
                Console.WriteLine("{0} {1} {2} {3}", c.i, c.L, c.W, c.H);
            } //*/

            int[] ba = new int[cuboids.Length];
            int h = 0, maxh = 0;
            foreach (var cub in li)
            {
                if (ba[cub.i] > 0)
                {
                    h -= ba[cub.i];
                    ba[cub.i] = 0;
                }
                for (int j = stk.Count - 1; j >= 0; --j)
                {
                    var c0 = stk[j];
                    if (ba[c0.i] == 0 || cub.W > c0.W)
                    {
                        h -= ba[c0.i];
                        ba[c0.i] = 0;
                        stk.RemoveAt(j);
                    }
                    else break;
                }
                stk.Add(cub);
                maxh = Math.Max(maxh, h += ba[cub.i] = cub.H);
            }
            return maxh;
        }

        internal static void Run()
        {
            var input = JsonConvert.DeserializeObject<int[][]>("[[50,45,20],[95,37,53],[45,23,12]]");
            Console.WriteLine(new P1691堆叠长方体的最大高度().MaxHeight(input));
        }
    }
}
