using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard
    // https://leetcode-cn.com/problems/stone-game-viii/
    internal class P1872石子游戏VIII
    {

        // ver3 - 想不到最后写出来如此简单。。。
        public int StoneGameVIII(int[] stones)
        {
            if (stones.Length <= 2) return stones.Sum();
            for (int i = 1; i < stones.Length; ++i) stones[i] += stones[i - 1];
            int dp = stones.Last();
            for (int i = stones.Length - 2; i >= 1; --i)
                dp = Math.Max(dp, stones[i] - dp);
            return dp;

            //if (stones.Length <= 2) return stones.Sum();
            //int min = stones.Last(), dp = Math.Max(stones.Last() + stones[stones.Length - 2], -min), dp2 = min;
            //for (int i = stones.Length - 2; i >= 0; --i)
            //    stones[i] += stones[i + 1];
            //for (int i = stones.Length - 3; i >= 0; --i)
            //{
            //    dp2 = dp;
            //    dp = Math.Max(stones[i], -min);
            //    if (dp2 < min) min = dp2;
            //}
            //return dp;
        }

        // ver2 - 回溯（答案正确，但数据量大时超时）
        int allSum = 0;
        static string[] choice = Array.Empty<string>();
        public int StoneGameVIII_ver2(int[] stones)
        {
            allSum = stones.Sum();
            if (stones.Length <= 2) return allSum;
            choice = new string[stones.Length]; // 调试用，记录分支节点
            int[] pres = new int[stones.Length], posts = new int[stones.Length], dp = new int[stones.Length];
            Array.Fill(dp, int.MinValue);
            // 前缀和
            pres[0] = stones[0];
            for (int i = 1; i < stones.Length; ++i) pres[i] = pres[i - 1] + stones[i];
            // 后缀和
            posts[stones.Length - 1] = stones[stones.Length - 1];
            for (int i = stones.Length - 2; i >= 0; --i) posts[i] = posts[i + 1] + stones[i];
            return DpDfs(0, stones, pres, posts, dp);
        }

        private int DpDfs(int i, int[] stones, int[] pres, int[] posts, int[] dp)
        {
            if (dp[i] > int.MinValue) return dp[i];
            int score = pres[i];
            dp[i] = allSum; choice[i] = "ALL";
            if (i == stones.Length - 2) return dp[i];
            for (int j = i + 1; j < stones.Length - 1; ++j) 
            {
                score += stones[j];
                int next = score - DpDfs(j, stones, pres, posts, dp);
                if (next > dp[i])
                {
                    dp[i] = next;
                    choice[i] = j.ToString();
                }
            }
            return dp[i];
        }

        // ver1 - wrong answer
        //public int StoneGameVIII(int[] stones)
        //{
        //    if (stones.Length <= 2) return stones.Sum();
        //    int min = int.MaxValue, dp = stones.Last();
        //    for (int i = stones.Length - 2; i >= 0; --i)
        //    {
        //        stones[i] += stones[i + 1];
        //        int dp2 = dp;
        //        dp = Math.Max(stones[i], -min);
        //        if (dp2 < min) min = dp2;
        //    }
        //    return dp;
        //}

        // ver0 - 一开始想当然只有两种情况，果然是WA
        //public int StoneGameVIII(int[] stones)
        //{
        //    if (stones.Length == 2) return stones.Sum();
        //    return Math.Max(stones.Sum(), -stones.Last());
        //    //int allSum = stones.Sum(), last = stones.Last(), preSum = allSum - last;
        //    //return Math.Max(-last, allSum);
        //}

        internal static void Run()
        {
            int[] input1 = { -1, 2, -3, 4, -5 };
            // expected: 38, ver1: 25
            int[] input2 = { 25, -35, -37, 4, 34, 43, 16, -33, 0, -17, -31, -42, -42, 38, 12, -5, -43, -10, -37, 12 };
            // 前缀和：25 -10 -47 -43 -9 34 50 17 17 0 -31 -73 -115 -77 -65 -70 -113 -123 -160 -148
            // 后缀和：-148 -173 -138 -101 -105 -139 -182 -198 -165 -165 -148 -117 -75 -33 -71 -83 -78 -35 -25 12
            int[] input3 = { 7, -6, 5, 10, 5, -2, -6 }; // expected: 13, ver2: 15 // 小问题，fixed

            int[] stones = input1;
            Console.WriteLine(new P1872石子游戏VIII().StoneGameVIII_ver2(stones));
            stones = input2;
            Console.WriteLine(new P1872石子游戏VIII().StoneGameVIII_ver2(stones));
            //stones = input3;
            //Console.WriteLine(new P1872石子游戏VIII().StoneGameVIII_ver2(stones));
            foreach (var c in choice)
                Console.Write(c + " ");
            Console.WriteLine();

            // input 4 - big data // ans: 368361
            stones = Common.ReadArray(1872); // size = 27838 // ver2 TLE
            Console.WriteLine("大数据输出：" + new P1872石子游戏VIII().StoneGameVIII(stones));
            //Console.WriteLine(new P1872石子游戏VIII().StoneGameVIII_ver2(stones));

            //for (int i = stones.Length - 2; i >= 0; --i)
            //    stones[i] += stones[i + 1];
            ////for (int i = 1; i < stones.Length; ++i)
            ////    stones[i] += stones[i - 1];
            //foreach (var stone in stones)
            //    Console.Write(stone + " ");
            //Console.WriteLine();
        }
    }

}
