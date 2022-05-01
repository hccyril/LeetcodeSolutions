using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleCore1
{
    /**
     * Index:
     * 4: hard, 繁琐
     * 5: medium, 最长回文子串(quite easy)
     * 6: medium, 复杂计算
     * 7: easy
     * 8: medium, atoi, 复杂计算，坑多，提交通过率极低！
     * 9: easy
     * 10: hard, 有限自动机+DFS matching
     * 11: medium, 盛水容器，时间复杂度要求高，提交好几次才过
     * 15: medium, 三数之和，看似简单但是要求高，最后优化到用Binary Search才过
     * 32最长有效括号,hard
     * 42: 接雨水,hard 比较有意思的题目
     * 44: hard, 通配符（*？）匹配
     * P0076最小覆盖子串：hard, 比较复杂的滑动窗口
     * 65: hard, 复杂计算
     [not yet]* 218天际线: hard, 计算多个矩形的投影
     * 236: medium, Tree+DFS (最小公共祖先, LCA)
     * 279: medium, DP (除了DP也可以用数学的四平方和定理）
     * 315: hard, !!!真的难，做了一整晚。。
     * [notyet] 363: hard, 矩阵问题
     * [notyet] 378: medium, 有序矩阵中第K小的元素: 矩阵中的二分查找，有点hard
     * [notyet] 451: medium, 根据字符出现频率排序 hash + sort
     * 474: medium, DP
     * 483: hard, Math,《最小好进制》使用等比数列求和
     * 494: medium, DP
     * 518: medium, DP, 跟普通的找零钱问题不太一样
     * 639: hard, DP
     * P0658找到_K_个最接近的元素, medium, 更加典型的滑动窗口
     * 677：前缀树 
     * 752: medium, BFS, 可以拿来讲广度优先搜索
     * 773: hard, BFS, 相对难和复杂一点的广度优先搜索
     * 791. medium, 自定义字符串排序（自定义排序）
     * 778: hard, Modified Dijkstra, 比较经典可以用来讲课 ###
     * 838: medium, 多米诺骨牌模拟
     * 877: 石子游戏，dp, 但看出是先手必胜则一行搞定 ###
     * 879: hard, DP (在存储上也要做优化，否则时间差很远)
     * 909, medium, BFS 棋盘游戏
     * [notyet] 930, medium, 和相同的二元子数组（想做但没时间，英文版还未提交）
     * 1004, 原以为是DP，也提交通过了，看评论才知道是滑动窗口-_-||
     * 1049: medium, DP (脑子转不过弯，没反应过来是背包问题）,可以先做1046（贪心法）进行比较
     * P1220统计元音字母序列的数目: hard, DP
     * P1833雪糕的最大数量 medium 典型的贪心算法
     * P1838滑动窗口
   [not yet]* 1449: hard, DP
     * */

    // 主题周：
    // DP（背包）： 474, 494, 1049, 879, 518, 279, 1449
    // 经典动态规划：718,300
    // BFS: 752, 773, 909

    // 特殊算法：
    // 摩尔投票法（Boyer-Moore 算法）：找数组中的众数
    // https://leetcode-cn.com/problems/find-majority-element-lcci/


    ///<summary>
    /// Main
    ///</summary>
    class Program
    {
        static void Main(string[] args)
        {
            // all - count time
            var time = System.Environment.TickCount;

            // general run
            //new MyTester().Test();
            P1203项目管理.Run();

            // all - count time result
            time = System.Environment.TickCount - time;
            Console.WriteLine("\r\nTotal Time: " + time + "ms");
        }
    }

    class TestClass
    {
        public static void Test1()
        {
            DateTime dt = new DateTime(2022, 1, 3);
            Console.WriteLine(dt.DayOfWeek);
        }
        //public string DestCity(IList<IList<string>> paths)
        //{
        //    //paths.First(t => !paths.Any)
        //}
    }
}
