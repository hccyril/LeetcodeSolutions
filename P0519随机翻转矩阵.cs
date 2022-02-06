using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    internal class P0519随机翻转矩阵
    {
        // 这题的麻烦之处在于：
        // 1. 不能创建数组，会超出空间限制
        // 2. 每次只能调用一次random函数

        public class Solution
        {
            readonly int N, n;
            int count;
            readonly Random rand = new();
            readonly int[] farr;

            public Solution(int m, int n)
            {
                this.n = n;
                N = m * n;
                count = 0;
                farr = new int[N];
            }

            public int[] Flip()
            {
                int next = rand.Next(N - count);
                int i;
                for (i = 0; i < count; i++)
                {
                    if (next < farr[i]) break;
                    else next++;
                }
                for (int j = count - 1; j >= i; --j)
                {
                    farr[j + 1] = farr[j];
                }
                count++;
                farr[i] = next;
                return new int[] { next / n, next % n };
            }

            public void Reset()
            {
                count = 0;
            }
        }

        /**
         * Your Solution object will be instantiated and called as such:
         * Solution obj = new Solution(m, n);
         * int[] param_1 = obj.flip();
         * obj.reset();
         */

        internal static void Run()
        {
            string[] actions = { "Solution", "flip", "flip", "flip", "reset", "flip" };
            // "[[3,1],[],[],[],[],[]]";
            Solution so = new(3, 1);
            so.Flip();
            so.Flip();
            so.Flip();
            so.Reset();
            so.Flip();
        }
    }
}
