using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /**
     * 给定不同面额的硬币和一个总金额。写出函数来计算可以凑成总金额的硬币组合数。假设每一种面额的硬币有无限个。 

 

示例 1:

输入: amount = 5, coins = [1, 2, 5]
输出: 4
解释: 有四种方式可以凑成总金额:
5=5
5=2+2+1
5=2+1+1+1
5=1+1+1+1+1

来源：力扣（LeetCode）
链接：https://leetcode-cn.com/problems/coin-change-2
著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。
     * */

    class P0518零钱兑换II
    {
        public int Change(int amount, int[] coins)
        {
            int[] arr = new int[amount + 1];
            arr[0] = 1;
            foreach (var coin in coins)
            {
                for (int a = amount - 1; a >= 0; --a)
                {
                    if (arr[a] > 0)
                        for (int c = a + coin; c <= amount; c += coin)
                            arr[c] += arr[a];
                }
            }
            //for (int money = 0; money <= amount; ++money)
            //{
            //    foreach (var coin in coins)
            //    {
            //        var c = money + coin;
            //        if (c <= amount)
            //            arr[c]++;
            //    }
            //}
            return arr[amount];
        }
    }
}
