﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
     * 给你一个整数数组 cost 和一个整数 target 。请你返回满足如下规则可以得到的 最大 整数：

给当前结果添加一个数位（i + 1）的成本为 cost[i] （cost 数组下标从 0 开始）。
总成本必须恰好等于 target 。
添加的数位中没有数字 0 。
由于答案可能会很大，请你以字符串形式返回。

如果按照上述要求无法得到任何整数，请你返回 "0" 。

 

示例 1：

输入：cost = [4,3,2,5,6,7,2,5,5], target = 9
输出："7772"
解释：添加数位 '7' 的成本为 2 ，添加数位 '2' 的成本为 3 。所以 "7772" 的代价为 2*3+ 3*1 = 9 。 "977" 也是满足要求的数字，但 "7772" 是较大的数字。
 数字     成本
  1  ->   4
  2  ->   3
  3  ->   2
  4  ->   5
  5  ->   6
  6  ->   7
  7  ->   2
  8  ->   5
  9  ->   5
示例 2：

输入：cost = [7,6,5,5,5,6,8,7,8], target = 12
输出："85"
解释：添加数位 '8' 的成本是 7 ，添加数位 '5' 的成本是 5 。"85" 的成本为 7 + 5 = 12 。
示例 3：

输入：cost = [2,4,6,2,4,6,4,4,4], target = 5
输出："0"
解释：总成本是 target 的条件下，无法生成任何整数。
示例 4：

输入：cost = [6,10,15,40,40,40,40,40,40], target = 47
输出："32211"
 

提示：

cost.length == 9
1 <= cost[i] <= 5000
1 <= target <= 5000

来源：力扣（LeetCode）
链接：https://leetcode-cn.com/problems/form-largest-integer-with-digits-that-add-up-to-target
著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。
     * */
    class P1449数位成本和为目标值的最大数字
    {
        // 这个是网上的示例，自己没时间写
        public string LargestNumber(int[] cost, int target)
        {
            int[] dp = new int[target + 1];
            for (int ii = 0; ii < dp.Length; ++ii)
                dp[ii] = -2147483648;
            dp[0] = 0;
            foreach (int c in cost)
            {
                for (int j = c; j <= target; ++j)
                {
                    dp[j] = Math.Max(dp[j], dp[j - c] + 1);
                }
            }
            if (dp[target] < 0)
            {
                return "0";
            }
            StringBuilder sb = new StringBuilder();
            int n = cost.Length;
            for (int i = n - 1; i >= 0; i--)
            {
                while (target - cost[i] >= 0 && dp[target] - dp[target - cost[i]] == 1)
                {
                    target -= cost[i];
                    sb.Append(i + 1);
                }
            }
            return sb.ToString();
        }
    }
}
