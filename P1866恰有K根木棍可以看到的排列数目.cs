using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2023/7/14 practice
    // 练习失败，截至2023/7/14 15:54，用时1:48:33.55，目前超时而且答案错误
    // 2023/7/21 通过，先写出超时的方法，然后把所有答案按杨辉三角来输出，通过找规律再写出来
    // 提交通过后再看题解恍然大悟，原来自己dp状态转移时想的是最长的棍子要怎么插，但反过来想长度为1的棍子要怎么插就很容易了（因为长度1的棍子除了放最前面，放其他位置都是不可见的）
    /** 以下推导Wrong，只是留个纪念。。
     * 公式：F(n): 全部的组合 = { fk1, fk2, fk3, ... fk[n] }
     * D(n): n个数，保证最大的数放在最后面
     * 有D(n)=F(n-1) + 1 (例如 D[6][k=4] == F[5][k=3])
     * 
     * 则：F(n) = fd1 + fd2 + ... + fd[n]
     *     fd[m] = C(n-1, m-1) * D[m] * (n-m)!
     * 
     * */
    internal class P1866恰有K根木棍可以看到的排列数目
    {
        // ver3 根据找到的规律构建（但是没想明白为什么）
        static int[][] GetAllResult3()
        {
            int[][] a = new int[1001][];
            a[0] = new int[] { 1 };
            for (int n = 1; n <= 1000; ++n)
            {
                a[n] = new int[n + 1];
                a[n][n] = 1;
                int sum = 1;
                for (int k = n - 1; k > 1; --k)
                {
                    a[n][k] = a[n - 1][k].Multi(n - 1).Add(a[n - 1][k - 1]);
                    sum = sum.Add(a[n - 1][k - 1]);
                }
                a[n][1] = sum;
            }
            return a;
        }
        public int RearrangeSticks(int n, int k)
        {
            AllResult ??= GetAllResult3();
            return AllResult[n][k];
        }

        // 2023/7/21 再尝试
        // 记忆化回溯，但怎么都算不清时间复杂度，感觉应该超时
        // 但暂时也想不到更好的办法，只好试一试了
        // 写出来答案貌似是对的，但是超时

        Dictionary<(int, int), int> dc = new();

        public int RearrangeSticks_TLE(int n, int k)
        {
            if (n == k) return 1;
            else if (k == 1) return AF[n - 1];
            else if (dc.ContainsKey((n, k)))
                return dc[(n, k)];
            int ans = 0;
            for (int i = n - 1; i >= k - 1; --i)
                ans = RearrangeSticks(i, k - 1).Multi(CD[n - 1][i]).Multi(AF[n - i - 1]).Add(ans);
            return dc[(n, k)] = ans;
        }

        static readonly int[][] CD = 1000.AllCombinations();

        static readonly int[] AF = 1000.AllFactorials();

        static int[][] AllResult = null;
        static int[][] GetAllResult()
        {
            int[][] a = new int[1001][];

            int[] F(int n)
            {
                if (a[n] != null) return a[n];
                if (n == 0) return a[0] = new int[] { 0 };
                else if (n == 1) return a[1] = new int[] { 0, 1 };
                else a[n] = new int[n + 1];
                for (int m = 1; m <= n; ++m)
                {
                    var dm = F(m - 1);
                    for (int k = 0; k < dm.Length; ++k)
                        a[n][k + 1] = a[n][k + 1].Add(dm[k].Multi(CD[n - 1][m - 1]).Multi(AF[n - m]));
                }
                return a[n];
            }
            for (int i = 0; i <= 1000; ++i) F(i);
            return a;
        }

        // ver1 - 超时而且答案错
        public int RearrangeSticks_ver1(int n, int k)
        {
            if (AllResult == null)
                AllResult = GetAllResult();
            return AllResult[n][k];
        }

        internal static void Run()
        {
            var sln = new P1866恰有K根木棍可以看到的排列数目();
            // WA, expect=647427950
            int ans = sln.RearrangeSticks(20, 11);

            // time: 9688ms
            //int ans = sln.RearrangeSticks(1000, 500);
            Console.WriteLine("ans=" + ans);

            //for (int n = 1; n <= 10; ++n)
            //{
            //    for (int k = 1; k <= n; ++k)
            //        Console.Write("   " + sln.RearrangeSticks(n, k));
            //    Console.WriteLine();
            //}

        }
    }
}
