using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 数学题
    internal class P0902最大为N的数字组合
    {
        // 官方题解转C#
        public int AtMostNGivenDigitSet(string[] digits, int n)
        {
            int len = digits.Length; // bijective-base B
            char[] ca = n.ToString().ToCharArray();
            int K = ca.Length;
            int[] A = new int[K];
            int t = 0;

            foreach (char c in ca)
            {
                int c_index = 0;  // Largest such that c >= D[c_index - 1]
                bool match = false;
                for (int i = 0; i < len; ++i)
                {
                    if (c >= digits[i][0])
                        c_index = i + 1;
                    if (c == digits[i][0])
                        match = true;
                }

                A[t++] = c_index;
                if (match) continue;

                if (c_index == 0)
                { // subtract 1
                    for (int j = t - 1; j > 0; --j)
                    {
                        if (A[j] > 0) break;
                        A[j] += len;
                        A[j - 1]--;
                    }
                }

                while (t < K)
                    A[t++] = len;
                break;
            }

            int ans = 0;
            foreach (int x in A)
                ans = ans * len + x;
            return ans;
        }
    }
}
