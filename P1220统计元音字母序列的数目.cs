using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /* 2021/8/19 daily
     * 1220. 统计元音字母序列的数目 
     * https://leetcode-cn.com/problems/count-vowels-permutation/
给你一个整数 n，请你帮忙统计一下我们可以按下述规则形成多少个长度为 n 的字符串：

字符串中的每个字符都应当是小写元音字母（'a', 'e', 'i', 'o', 'u'）
每个元音 'a' 后面都只能跟着 'e'
每个元音 'e' 后面只能跟着 'a' 或者是 'i'
每个元音 'i' 后面 不能 再跟着另一个 'i'
每个元音 'o' 后面只能跟着 'i' 或者是 'u'
每个元音 'u' 后面只能跟着 'a'
由于答案可能会很大，所以请你返回 模 10^9 + 7 之后的结果。
     * */
    class P1220统计元音字母序列的数目
    {
        // 单元测试：n=5, 预期结果：68
        public static void Run()
        {
            Console.WriteLine(new P1220统计元音字母序列的数目().CountVowelPermutation(5));
        }

        public int CountVowelPermutation(int n)
        {
            if (n == 1) return 5;
            int[] dp = new int[5], dp1 = new int[5], temp;
            const int a = 0, e = 1, i = 2, o = 3, u = 4;
            dp1[a] = dp1[e] = dp1[i] = dp1[o] = dp1[u] = 1;
            for (--n; n > 0; --n)
            {
                // a: 每个元音 'a' 后面都只能跟着 'e'
                dp[a] = dp1[e];

                // e: 每个元音 'e' 后面只能跟着 'a' 或者是 'i'
                dp[e] = dp1[a].Add(dp1[i]);

                // i: 每个元音 'i' 后面 不能 再跟着另一个 'i'
                dp[i] = dp1[a].Add(dp1[e]).Add(dp1[o]).Add(dp1[u]);

                // o: 每个元音 'o' 后面只能跟着 'i' 或者是 'u'
                dp[o] = dp1[i].Add(dp1[u]);

                // u: 每个元音 'u' 后面只能跟着 'a'
                dp[u] = dp1[a];

                if (n == 1)
                {
                    return dp[a].Add(dp[e]).Add(dp[i]).Add(dp[o]).Add(dp[u]);
                }
                temp = dp1; dp1 = dp; dp = temp;
            }
            return -1;
        }
    }
}
