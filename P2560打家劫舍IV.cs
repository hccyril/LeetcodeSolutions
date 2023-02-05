using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // 2023/2/5 WC331-C，比赛时最后一题没做出来连带这题也没做出来
    // 中等题竟然想了一天没做出来，最后要看题解才开窍，这题要深刻反省
    internal class P2560打家劫舍IV
    {
        // 看了题解才恍然大悟，还是用二分，但check过程用DP
        public int MinCapability(int[] nums, int k)
        {
            int n = nums.Length;
            int[] dp = new int[n];
            int left = nums.Min(), right = nums.Max();
            while (left < right)
            {
                int m = left + right >> 1;

                // DP: dp[i]=前i个房屋偷取不超过m的最大个数
                for (int i = 0; i < n; ++i)
                    dp[i] = Math.Max(
                        i > 0 ? dp[i - 1] : 0, // 不偷i
                        nums[i] > m ? 0 : 1 + (i > 1 ? dp[i - 2] : 0)); // 偷i

                if (dp.Last() >= k)
                    right = m;
                else
                    left = m + 1;
            }
            return left;
        }

        // ver1: WA
        public int MinCapability_ver1_WA(int[] nums, int k)
        {
            if (nums.Length == 1) return nums[0];
            SortedSet<(int, int)> dp = new();
            dp.Add((nums[0], 0));
            int last = 0;
            for (int i = 1; i < nums.Length; ++i)
            {
                if (last < 0)
                {
                    if (dp.Count < k || nums[i] < dp.Max.Item1)
                    {
                        dp.Add((nums[i], i));
                        last = i;

                        while (dp.Count > k) dp.Remove(dp.Max);
                    }
                }
                else if (nums[i] < nums[last])
                {
                    dp.Add((nums[i], i));
                    dp.Remove((nums[last], last));
                    last = i;
                }
                else
                {
                    last = -1;
                }
            }
            return dp.Max.Item1;
        }

        internal static void Run()
        {
            var sln = new P2560打家劫舍IV();
            int[] input1 = { 9, 6, 20, 21, 8 }; int k1 = 3; // my : 8 ans: 20
            int[] input2 = { 6, 8, 2, 5, 9, 7 }; int k2 = 3; // ans: 7
            int ans = sln.MinCapability(input1, k1);
            Console.WriteLine("ans1=" + ans);
            ans = sln.MinCapability(input2, k2);
            Console.WriteLine("ans2=" + ans);
        }

    }
}
