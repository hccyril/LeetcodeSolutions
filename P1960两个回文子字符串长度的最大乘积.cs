using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2023/7/11
    // rank: 2690
    // 特殊算法：回文串Manacher
    internal class P1960两个回文子字符串长度的最大乘积
    {
        public long MaxProduct(string s)
        {
            var arms = s.Manacher();
            // 后缀最大值
            int[] px = new int[arms.Length];
            int ma = 0;
            long maxp = 0L;
            for (int i = arms.Length - 1, j = arms.Length - 1; i > 0; --i)
            {
                int arm = Math.Min(arms[j], j - i);
                ma = Math.Max(ma, arm * 2 + 1);
                while (j - arm > i)
                {
                    --j;
                    arm = Math.Min(arms[j], j - i);
                    ma = Math.Max(ma, arm * 2 + 1);
                }
                px[i] = ma;
            }
            // WRONG
            //for (int i = arms.Length - 1, j = arms.Length - 1, k = 1; j >= 0; --j, i -= k ^= 1)
            //{
            //    ma = Math.Max(ma, arms[i] * 2 + 1);
            //    px[j] = ma;
            //}
            // 前缀，同时计算最大值
            ma = 0;
            for (int j = 0, i = 0; j < arms.Length - 1; ++j)
            {
                int arm = Math.Min(arms[i], j - i);
                ma = Math.Max(ma, arm * 2 + 1);
                while (i + arm < j)
                {
                    ++i;
                    arm = Math.Min(arms[i], j - i);
                    ma = Math.Max(ma, arm * 2 + 1);
                }
                maxp = Math.Max(maxp, (long)ma * px[j + 1]);
            }
            // WRONG
            //for (int i = 0, j = 0, k = 1; j < arms.Length - 1; ++j, i += k ^= 1)
            //{
            //    ma = Math.Max(ma, arms[i] * 2 + 1);
            //    maxp = Math.Max(maxp, (long)ma * px[j + 1]);
            //}
            return maxp;
        }

        internal static void Run()
        {
            string s = "ababbb";
            var sln = new P1960两个回文子字符串长度的最大乘积();
            var ans = sln.MaxProduct(s);
            Console.WriteLine("ans=" + ans);
        }
    }
}
