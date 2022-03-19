using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/3/9 Daily, rank: 2130
    // 转化为前缀和
    internal class P0798得分最高的最小轮调
    {
        public int BestRotation(int[] nums)
        {
            Span<int> rts = stackalloc int[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                int n = nums[i];
                if (n <= i)
                {
                    rts[0]++;
                    if (i - n + 1 < nums.Length)
                    {
                        rts[i - n + 1]--;
                        if (i + 1 < nums.Length && nums.Length - 1 >= n)
                            rts[i + 1]++;
                    }
                }
                else // n > i (imply: n != 0)
                {
                    if (i + 1 < nums.Length && nums.Length - 1 >= n)
                    {
                        rts[i + 1]++;
                        if (n > i + 1)
                            rts[i + nums.Length - n + 1]--;
                    }
                    //int r = i + 1, l = nums.Length - 1;
                    //if (r < nums.Length && l >= n)
                    //{
                    //    rts[r]++;
                    //    rts[r + l - n + 1]--;
                    //}
                }
            }
            int max_rotate = 0, sum = rts[0], max_sum = rts[0];
            for (int i = 1; i < nums.Length; i++)
            {
                sum += rts[i];
                if (sum > max_sum)
                {
                    max_rotate = i;
                    max_sum = sum;
                }
            }
            return max_rotate;
        }
    }
}
