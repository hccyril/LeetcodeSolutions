using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, BiC74-D // 2022.3.20
    // DP
    internal class P2209用地毯覆盖后的最少白色砖块
    {
        public int MinimumWhiteTiles(string floor, int numCarpets, int carpetLen)
        {
            int[,] dp = new int[numCarpets + 1, floor.Length];
            for (int i = 0; i < floor.Length; ++i)
            {
                dp[0, i] = floor[i] == '1' ? 1 : 0;
                if (i > 0) dp[0, i] += dp[0, i - 1];
            }
            for (int k = 1; k <= numCarpets; ++k)
            {
                for (int i = 0; i < floor.Length; ++i)
                {
                    dp[k, i] = 0;
                    if (i >= carpetLen)
                    {
                        dp[k, i] += dp[k - 1, i - carpetLen];
                        int dp2 = dp[k, i - 1] + (floor[i] == '1' ? 1 : 0);
                        if (dp2 < dp[k, i]) dp[k, i] = dp2;
                    }
                }
            }
            // debug
            //for (int i = 0; i <= numCarpets; ++i)
            //{
            //    for (int j = 0; j < floor.Length; ++j)
            //        Console.Write(dp[i, j] + " ");
            //    Console.WriteLine();
            //}
            return dp[numCarpets, floor.Length - 1];
        }

        internal static void Run()
        {
            var sln = new P2209用地毯覆盖后的最少白色砖块();
            int ans = sln.MinimumWhiteTiles("10110101", 2, 2);
            Console.WriteLine(nameof(P2209用地毯覆盖后的最少白色砖块) + ": " + ans);

        }
    }
}
