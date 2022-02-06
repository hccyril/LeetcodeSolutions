using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
     * 1004. Max Consecutive Ones III
     * Given a binary array nums and an integer k, return the maximum number of consecutive 1's in the array if you can flip at most k 0's
     * */
    class P1004最大连续1的个数III
    {
        public int LongestOnes(int[] nums, int k)
        {
            int[] dp = new int[k + 1]; // dp[i]表示当前位置翻转i个0得到的最大连续1长度（不一定是全局最优）
            int[] dpNext = new int[k + 1];
            for (int j = 0; j <= k; ++j) dp[j] = dpNext[j] = -1;

            int[] counts = new int[nums.Length]; // 表示当前位置有多少个连续的0或者1，因此count[i]>0
            int i = 0, maxlen = 0;
            int one = nums[0]; // 0或者1,表示当前数组位置存的是0的数量还是1的数量
            foreach (var num in nums)
            {
                if (num == one)
                {
                    counts[i]++;
                }
                else
                {
                    one = num;
                    counts[++i] = 1;
                }
            }

            // dp
            one = nums[0]; dp[0] = 0;
            foreach (var count in counts)
            {
                if (count == 0) break;

                if (one == 1)
                {
                    for (int j = 0; j <= k; ++j)
                    {
                        if (dp[j] == -1) break;
                        dp[j] += count;
                        if (dp[j] > maxlen) maxlen = dp[j];
                    }
                }
                else // one == 0
                {
                    for (int j = 0; j <= k; ++j)
                    {
                        if (j < count)
                        {
                            dpNext[j] = j;
                            if (j > maxlen) maxlen = j;
                        }
                        else
                        {
                            int flip = j - count;
                            if (dp[flip] == -1) break;
                            dpNext[j] = count + dp[flip];
                            if (dpNext[j] > maxlen) maxlen = dpNext[j];
                        }
                    }

                    var temp = dp; dp = dpNext; dpNext = temp;
                }

                one = 1 - one;
            }

            return maxlen;
        }
    }

    /* backup
        public int LongestOnes(int[] nums, int k)
        {
            int[] dp = new int[k + 1];
            int[] dpNext = new int[k + 1];
            int[] counts = new int[nums.Length];
            int i = 0, len = 1, maxlen = 0;
            int one = nums[0];
            foreach (var num in nums)
            {
                if (num == one)
                {
                    counts[i]++;
                }
                else
                {
                    one = num;
                    counts[++i] = 1;
                }
            }

            // dp
            one = nums[0];
            foreach (var count in counts)
            {
                if (count == 0) break;

                if (one == 1)
                {
                    for (int j = 0; j < len; ++j)
                    {
                        dp[j] += count;
                        if (dp[j] > maxlen) maxlen = dp[j];
                    }
                }
                else // one == 0
                {
                    int lenNext = 0;
                    for (int j = 0; j <= count && j <= k; ++j)
                    {
                        //if (done > 0) { dpNext[j] = done; continue; }
                        if (j < count)
                        {
                            dpNext[lenNext++] = j;
                            if (j > maxlen) maxlen = j;
                        }
                        else
                        {
                            int flip = k - count;
                            int c = (flip >= len ? dp[len - 1] : dp[flip]) + count;
                            dpNext[lenNext++] = count + c;
                            dpNext[j] = done = count + dp[k - count];
                        }
                    }
                }

                one = 1 - one; // 1 < -- > 0
            }
     * */
}
