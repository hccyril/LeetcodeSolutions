using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
     * 1744. 你能在你最喜欢的那天吃到你最喜欢的糖果吗？
     * https://leetcode-cn.com/problems/can-you-eat-your-favorite-candy-on-your-favorite-day/
给你一个下标从 0 开始的正整数数组 candiesCount ，其中 candiesCount[i] 表示你拥有的第 i 类糖果的数目。同时给你一个二维数组 queries ，其中 queries[i] = [favoriteTypei, favoriteDayi, dailyCapi] 。

你按照如下规则进行一场游戏：

你从第 0 天开始吃糖果。
你在吃完 所有 第 i - 1 类糖果之前，不能 吃任何一颗第 i 类糖果。
在吃完所有糖果之前，你必须每天 至少 吃 一颗 糖果。
请你构建一个布尔型数组 answer ，用以给出 queries 中每一项的对应答案。此数组满足：

answer.length == queries.length 。answer[i] 是 queries[i] 的答案。
answer[i] 为 true 的条件是：在每天吃 不超过 dailyCapi 颗糖果的前提下，你可以在第 favoriteDayi 天吃到第 favoriteTypei 类糖果；否则 answer[i] 为 false 。
注意，只要满足上面 3 条规则中的第二条规则，你就可以在同一天吃不同类型的糖果。

请你返回得到的数组 answer 。
     * */
    class P1744你能在你最喜欢的那天吃到你最喜欢的糖果吗
    {
        /**
         * 总共提交了6-7次才过，自己水平很有问题，需要总结下：
         * 开头很快就写完了，但是
         * 1. 没考虑到整数溢出的情况，int修改为long
         * 2. 发现判断条件写错了，要重新复习下“两个区间相交”的写法（具体看代码）
         * 3. 虽然maxEat < minCandies但仍然答案应该是true, 经查maxEat应该算到当天（即day+1）
         * 4. 以上3修改完以后条件情况变了, maxEat应该是 大于 而不是 大于等于 minCandies
         * */
        static bool Query(long[] sums, int[] candiesCount, int[] qry)
        {
            int fav = qry[0];
            int day = qry[1];
            int cap = qry[2];

            long maxCandies = sums[fav];
            long minCandies = maxCandies - candiesCount[fav];

            long minEat = day;
            long maxEat = (long)(day + 1) * (long)cap;

            // true: 在day之前，fav前面的糖可以吃光而fav的糖没吃光，也就是 min <= eat < max
            // 判断条件怎么老写不对。。。不就是两个区间必须相交
            return minEat < maxCandies && maxEat > minCandies;
            // 以下写法是错的，纪念下。。（2021/6/29）
            //return minEat >= minCandies && minEat < maxCandies || 
            //       maxEat >= minCandies && maxEat < maxCandies;
        }

        public bool[] CanEat(int[] candiesCount, int[][] queries)
        {
            long[] sums = new long[candiesCount.Length];
            for (int i = 0; i < sums.Length; ++i)
                sums[i] = (i == 0 ? 0 : sums[i - 1]) + candiesCount[i];

            List<bool> ans = new List<bool>();
            foreach (var qry in queries)
            {
                ans.Add(Query(sums, candiesCount, qry));
            }
            return ans.ToArray();
        }


        public static bool[] CanEatTest(int[] candiesCount, int[][] queries, bool[] rightans)
        {
            long[] sums = new long[candiesCount.Length];
            for (int i = 0; i < sums.Length; ++i)
                sums[i] = (i == 0 ? 0 : sums[i - 1]) + candiesCount[i];

            List<bool> ans = new List<bool>();
            int index = 0;
            foreach (var qry in queries)
            {
                bool b = Query(sums, candiesCount, qry);
                if (b != rightans[index])
                {
                    b = Query(sums, candiesCount, qry);
                }
                ans.Add(b);
                index++;
            }
            return ans.ToArray();
        }

        public static void Run()
        {
            // test case 1
            int[] ar1 = { 7, 11, 5, 3, 8 };
            int[][] ar2 = new int[][] { new int[] { 2, 2,6 }, new int[] { 4, 2, 4 }, new int[] { 2, 13, 1000000000 } };
            bool[] ar3;
            ar3 = new P1744你能在你最喜欢的那天吃到你最喜欢的糖果吗().CanEat(ar1, ar2);
            Console.WriteLine(string.Join(" ", ar3));

            // test case 2
            ar1 = new int[] { 5,2,6,4,1 };
            ar2 = //new int[][] { new int[] { 0, 2, 2 }, new int[] { 4, 2, 4 }, new int[] { 2, 13, 1000000000 } };
                new int[][] { new int[] { 3, 1, 2 }, new int[] { 4, 10, 3 }, new int[] { 3, 10, 100 }, new int[] { 4, 100, 30 }, new int[] { 1, 3, 1 } };
            //ar3 = new Solution1744().CanEat(ar1, ar2);
            //Console.WriteLine(string.Join(" ", ar3));

            // test case 3
            //ar1 = new int[] { 46, 5, 47, 48, 43, 34, 15, 26, 11, 25, 41, 47, 15, 25, 16, 50, 32, 42, 32, 21, 36, 34, 50, 45, 46, 15, 46, 38, 50, 12, 3, 26, 26, 16, 23, 1, 4, 48, 47, 32, 47, 16, 33, 23, 38, 2, 19, 50, 6, 19, 29, 3, 27, 12, 6, 22, 33, 28, 7, 10, 12, 8, 13, 24, 21, 38, 43, 26, 35, 18, 34, 3, 14, 48, 50, 34, 38, 4, 50, 26, 5, 35, 11, 2, 35, 9, 11, 31, 36, 20, 21, 37, 18, 34, 34, 10, 21, 8, 5 };
            //ar2 = new int[][] { new int[] { 80, 2329, 69 }, new int[] { 14, 1485, 76 }, new int[] { 33, 2057, 83 }, new int[] { 13, 1972, 27 }, new int[] { 11, 387, 25 }, new int[] { 24, 1460, 47 }, new int[] { 22, 1783, 35 }, new int[] { 1, 513, 33 }, new int[] { 66, 2124, 85 }, new int[] { 19, 642, 26 }, new int[] { 15, 1963, 79 }, new int[] { 93, 722, 96 }, new int[] { 15, 376, 88 }, new int[] { 60, 1864, 89 }, new int[] { 86, 608, 4 }, new int[] { 98, 257, 35 }, new int[] { 35, 651, 47 }, new int[] { 96, 795, 73 }, new int[] { 62, 2077, 18 }, new int[] { 27, 1724, 57 }, new int[] { 34, 1984, 75 }, new int[] { 49, 2413, 95 }, new int[] { 76, 1664, 5 }, new int[] { 28, 38, 13 }, new int[] { 85, 54, 42 }, new int[] { 12, 301, 3 }, new int[] { 62, 2016, 29 }, new int[] { 45, 2316, 37 }, new int[] { 43, 2360, 28 }, new int[] { 87, 192, 98 }, new int[] { 27, 2082, 21 }, new int[] { 74, 762, 37 }, new int[] { 51, 35, 17 }, new int[] { 73, 2193, 4 }, new int[] { 60, 425, 65 }, new int[] { 11, 1522, 58 }, new int[] { 21, 1699, 66 }, new int[] { 42, 1473, 5 }, new int[] { 30, 2010, 48 }, new int[] { 91, 796, 74 }, new int[] { 82, 2162, 31 }, new int[] { 23, 2569, 65 }, new int[] { 24, 684, 23 }, new int[] { 70, 1219, 51 }, new int[] { 5, 1817, 15 }, new int[] { 81, 2446, 34 }, new int[] { 96, 771, 60 }, new int[] { 49, 1171, 60 }, new int[] { 41, 567, 67 }, new int[] { 39, 799, 59 }, new int[] { 90, 957, 81 }, new int[] { 84, 2122, 27 }, new int[] { 82, 1707, 44 }, new int[] { 11, 1889, 20 }, new int[] { 80, 1697, 83 }, new int[] { 24, 1786, 60 }, new int[] { 90, 1847, 99 }, new int[] { 51, 114, 21 }, new int[] { 44, 466, 85 }, new int[] { 56, 469, 20 }, new int[] { 44, 350, 96 }, new int[] { 66, 1946, 10 }, new int[] { 14, 2470, 12 }, new int[] { 69, 1175, 18 }, new int[] { 98, 1804, 25 }, new int[] { 77, 2187, 40 }, new int[] { 89, 2265, 45 }, new int[] { 19, 2246, 45 }, new int[] { 40, 2373, 79 }, new int[] { 60, 2222, 17 }, new int[] { 37, 385, 5 }, new int[] { 97, 1759, 97 }, new int[] { 10, 903, 5 }, new int[] { 87, 842, 45 }, new int[] { 74, 2398, 66 }, new int[] { 62, 49, 94 }, new int[] { 48, 156, 77 }, new int[] { 76, 2310, 80 }, new int[] { 64, 2360, 95 }, new int[] { 70, 1699, 83 }, new int[] { 39, 1241, 66 }, new int[] { 92, 2312, 21 }, new int[] { 63, 2148, 29 }, new int[] { 95, 594, 74 }, new int[] { 89, 90, 51 }, new int[] { 82, 137, 70 }, new int[] { 54, 301, 97 }, new int[] { 15, 819, 43 }, new int[] { 47, 1402, 60 }, new int[] { 17, 2377, 43 }, new int[] { 50, 1937, 95 }, new int[] { 62, 1174, 74 }, new int[] { 67, 1411, 87 }, new int[] { 39, 1151, 48 } };
            //bool[] rightans = { false, false, false, false, true, false, false, false, false, false, false, true, true, false, true, true, true, true, false, false, false, false, true, false, true, true, false, false, false, true, false, true, false, false, true, false, false, false, false, true, true, false, true, true, false, false, true, true, true, true, true, true, true, false, true, false, true, true, true, true, true, false, false, true, true, false, true, false, false, false, true, true, false, true, false, true, true, false, false, true, false, true, false, true, true, true, true, false, true, false, false, true, true, true };
            //CanEatTest(ar1, ar2, rightans);
        }
    }
}
