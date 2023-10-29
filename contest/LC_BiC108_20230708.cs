using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest
{
    // 排名 用户名 得分 完成时间    题目1(3) 题目2(4) 题目3(4) 题目4(5)
    // 176 / 2334	呱呱编程实验室 	16	0:53:29	0:05:08	0:16:49*1	0:32:22	0:48:29
    internal class LC_BiC108_20230708
    {
        #region Problem A
        int MaxN(int[] a, int i)
        {
            int inc = 1;
            for (int j = i + 1; j < a.Length; ++j)
            {
                if (a[j] != a[j - 1] + inc)
                    return j - i;
                inc = -inc;
            }
            return a.Length - i;
        }
        public int AlternatingSubarray(int[] nums)
        {
            int n = nums.Length, maxn = 0;
            for (int i = 0; i < n; ++i)
                maxn = Math.Max(maxn, MaxN(nums, i));
            return maxn >= 2 ? maxn : -1;
        }
        #endregion

        #region Problem B
        public int SolveB(int x)
        {
            return x;
        }
        #endregion

        #region Problem C
        string[] sa = { "1", "101", "11001", "1111101", "1001110001", "110000110101", "11110100001001" };


        public int MinimumBeautifulSubstrings(string s)
        {
            return -1; // finished by Python3
        }
        #endregion

        #region Problem D
        void Update(Dictionary<(int, int), int> di, int x, int y)
        {
            if (!di.ContainsKey((x, y))) di[(x, y)] = 0;
            di[(x, y)]++;
        }
        int M, N;
        IEnumerable<(int, int)> Cells(int x, int y)
        {
            if (x > 0 && y > 0) yield return (x - 1, y - 1);
            if (x > 0 && y < N - 1) yield return (x - 1, y);
            if (x < M - 1 && y > 0) yield return (x, y - 1);
            if (x < M - 1 && y < N - 1) yield return (x, y);
        }

        public long[] CountBlackBlocks(int m, int n, int[][] coordinates)
        {
            M = m; N = n;
            Dictionary<(int, int), int> di = new();
            foreach (var co in coordinates)
            {
                int i = co[0], j = co[1];
                foreach ((int x, int y) in Cells(i, j))
                {
                    Update(di, x, y);
                }
            }
            long[] a = new long[5];
            foreach ((int x, int y) in di.Keys)
            {
                a[di[(x, y)]]++;
            }
            a[0] = (long)(m - 1) * (n - 1) - a[1] - a[2] - a[3] - a[4];
            return a;
        }
        #endregion

        #region Problem E
        public int SolveE(int x)
        {
            return x;
        }
        #endregion

        #region Run Test
        internal static int Run()
        {
            char p = 'D';
            LC_BiC108_20230708 sln = new();

            return p switch
            {
                'A' => sln.RunTestA(),
                'B' => sln.RunTestB(),
                'C' => sln.RunTestC(),
                'D' => sln.RunTestD(),
                'E' => sln.RunTestE(),
                _ => -1
            };
        }

        int RunTestA()
        {
            return 0;
        }

        int RunTestB()
        {
            return 0;
        }

        int RunTestC()
        {
            return 0;
        }

        int RunTestD()
        {
            return 0;
        }

        int RunTestE()
        {
            return 0;
        }
        #endregion
    }
}
