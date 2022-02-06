using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // 目前遇到的最难的DP
    class P0546移除盒子
    {
#if OFFICIAL
        // 以下是官方题解

        int[,,] dp;

        public int RemoveBoxes(int[] boxes)
        {
            int length = boxes.Length;
            dp = new int[length, length, length];
            return CalculatePoints(boxes, 0, length - 1, 0);
        }

        public int CalculatePoints(int[] boxes, int l, int r, int k)
        {
            if (l > r)
            {
                return 0;
            }
            if (dp[l, r, k] == 0)
            {
                int r1 = r, k1 = k;
                while (r1 > l && boxes[r1] == boxes[r1 - 1])
                {
                    r1--;
                    k1++;
                }
                dp[l, r, k] = CalculatePoints(boxes, l, r1 - 1, 0) + (k1 + 1) * (k1 + 1);
                for (int i = l; i < r1; i++)
                {
                    if (boxes[i] == boxes[r1])
                    {
                        dp[l, r, k] = Math.Max(dp[l, r, k], CalculatePoints(boxes, l, i, k1 + 1) + CalculatePoints(boxes, i + 1, r1 - 1, 0));
                    }
                }
            }
            return dp[l, r, k];
        }
#else
        int[,] dp;
        int Dfs(int[] box, int start, int end)
        {
            if (start > end) return 0;
            if (dp[start, end] > 0) return dp[start, end];
            if (start == end) return dp[start, end] = 1;

            int same = 0, prevScore = 0, di = -1;
            for (int i = start; i <= end; ++i)
            {
                if (box[i] == box[start])
                {
                    same++;
                    if (di >= 0)
                    {
                        prevScore += Dfs(box, di, i - 1);
                        di = -1;
                    }
                    int score = prevScore + same * same;
                    if (score > dp[start, end]) dp[start, end] = score;
                }
                else if (di < 0)
                {
                    di = i;
                    int score = prevScore + same * same + Dfs(box, di, end);
                    if (score > dp[start, end]) dp[start, end] = score;
                }
            }
            return dp[start, end];
        }
        // ver1: WA
        public int RemoveBoxes(int[] boxes)
        {
            dp = new int[boxes.Length, boxes.Length];
            return Dfs(boxes, 0, boxes.Length - 1);
        }
#endif
        internal static void Run()
        {
            int[] input = { 1, 2, 2, 1, 1, 1, 2, 1, 1, 2, 1, 2, 1, 1, 2, 2, 1, 1, 2, 2, 1, 1, 1, 2, 2, 2, 2, 1, 2, 1, 1, 2, 2, 1, 2, 1, 2, 2, 2, 2, 2, 1, 2, 1, 2, 2, 1, 1, 1, 2, 2, 1, 2, 1, 2, 2, 1, 2, 1, 1, 1, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 1, 1, 1, 1, 2, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1, 2, 2, 1 };
            int output = new P0546移除盒子().RemoveBoxes(input);
            Console.WriteLine(output);
        }
    }
}
