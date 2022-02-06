using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0600不含连续1的非负整数
    {
        public int FindIntegers(int n)
        {
            int[] dp = new int[32]; // dp[i]表示i位二进制数里有多少个满足条件的整数
            dp[0] = 1; dp[1] = 2;
            for (int i = 2; i < 32; ++i) dp[i] = dp[i - 1] + dp[i - 2];

            int t = 0, temp = 0, count = 0;
            bool prev1 = false; // 是否当前位的下一位(i+1)为1
            for (int i = 30; i >= 0; --i)
            {
                if (prev1) prev1 = false; // 上一位试了1，这一位无法试1，只能跳过
                else
                {   // 否则尝试在当前位放1
                    temp = t + (1 << i);
                    if (temp <= n)
                    {
                        // 可以放1，也就是放0时后面随便放，因此直接加上不用数了
                        count += dp[i]; 
                        t = temp;
                        prev1 = true;
                    }
                }
            }
            return count + 1;
        }

        public static void Run()
        {
            Console.WriteLine(new P0600不含连续1的非负整数().FindIntegers(5));
        }
    }
}
