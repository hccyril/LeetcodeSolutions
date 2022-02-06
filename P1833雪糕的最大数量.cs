using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
     * 1833. 雪糕的最大数量
     * https://leetcode-cn.com/problems/maximum-ice-cream-bars/
夏日炎炎，小男孩 Tony 想买一些雪糕消消暑。

商店中新到 n 支雪糕，用长度为 n 的数组 costs 表示雪糕的定价，其中 costs[i] 表示第 i 支雪糕的现金价格。Tony 一共有 coins 现金可以用于消费，他想要买尽可能多的雪糕。

给你价格数组 costs 和现金量 coins ，请你计算并返回 Tony 用 coins 现金能够买到的雪糕的 最大数量 。

注意：Tony 可以按任意顺序购买雪糕。
     * */
    class P1833雪糕的最大数量
    {
        // 最后发现是贪心。。。
        public int MaxIceCream(int[] costs, int coins)
        {
            List<int> sortedCosts = new List<int>(costs);
            sortedCosts.Sort();
            int count = 0;
            foreach (var cost in sortedCosts)
            {
                coins -= cost;
                if (coins >= 0) count++;
                else break;
            }
            return count;
        }

        // version 2: still TLE
        //public int MaxIceCream(int[] costs, int coins)
        //{
        //    int max = 0;
        //    SortedSet<int> keys = new SortedSet<int>();
        //    keys.Add(0);
        //    int[] dp = new int[coins + 1];
        //    foreach (var cost in costs)
        //    {
        //        var list = new List<int>(keys); list.Reverse();
        //        foreach (var key in list)
        //        {
        //            int newCost = key + cost;
        //            if (newCost <= coins)
        //            {
        //                int count = dp[key] + 1;
        //                if (count > max) max = count;
        //                keys.Add(newCost);
        //                if (dp[newCost] < count)
        //                    dp[newCost] = count;
        //            }
        //        }
        //    }
        //    return max;
        //}

        // version 1: Time Limit Exceed
        //public int MaxIceCream(int[] costs, int coins)
        //{
        //    List<int> sortedCosts = new List<int>(costs);
        //    sortedCosts.Sort();
        //    SortedList<int, int> dp = new SortedList<int, int>();
        //    dp.Add(0, 0);
        //    foreach (var cost in sortedCosts)
        //    {
        //        foreach (var key in dp.Keys.Reverse())
        //        {
        //            int newCost = key + cost;
        //            if (newCost <= coins)
        //            {
        //                int count = dp[key] + 1;
        //                if (dp.ContainsKey(newCost))
        //                {
        //                    if (count > dp[newCost])
        //                        dp[newCost] = count;
        //                }
        //                else
        //                {
        //                    dp[newCost] = count;
        //                }
        //            }
        //        }
        //    }
        //    return dp.Values.Max();
        //}
    }
}
