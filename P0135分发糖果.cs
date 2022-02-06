using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
     * 135. Candy
     * https://leetcode.com/problems/candy/
     * There are n children standing in a line. Each child is assigned a rating value given in the integer array ratings.

You are giving candies to these children subjected to the following requirements:

Each child must have at least one candy.
Children with a higher rating get more candies than their neighbors.
Return the minimum number of candies you need to have to distribute the candies to the children.
     * */
    class P0135分发糖果
    {
        public int Candy(int[] ratings)
        {
            int sum = 0; // 总糖果数
            int ra = -1, rb = -1; // 当前小孩a与b的rating分数
            int val = -1; // 当前小孩得到的糖果数
            int up = 1; // 上行累计
            int down = 1, downVal = 1; // 下行累计
            foreach (var r in ratings)
            {
                // 初始化
                if (ra == -1)
                {
                    ra = r;
                    val = 1;
                    continue;
                }
                rb = r;

                // 情形1：上行累计：... < a < b 
                if (ra < rb)
                {
                    sum += val; // 之前糖果数确定，可以结算
                    val = ++up; // b至少比a多拿一颗糖果
                }
                else
                {
                    up = 1; // 上行累计中断，重置up参数
                }

                // 情形 2: 出现分数相等
                if (ra == rb)
                {
                    sum += val; // a的糖果数确定，结算
                    val = 1; // b至少拿一颗糖果
                }

                // 情形 3: 下行累计： ... > a > b
                if (ra > rb)
                {
                    /* 
                     * 假定下行累计有n个小孩，那最小糖果数应该为n + (n-1) + ... + 3 + 2 + 1
                     *     那所以有a,b,c三个小孩那应该是3,2,1总共6颗糖果
                     * 但有一种特殊情况：如果a前面有10个小孩分数升序排列，那a最少有10颗糖果
                     *     b和c仍然是2,1,总共是10,2,1
                     * 因此这两种情况都要计算，取其最大值
                     * */
                    val += down++; // val为除了第一个，后面的依次多一颗糖果
                    downVal += down; // downVal为所有下行累计的依次拿1+2+3+...+n颗糖果
                    if (val < downVal) val = downVal; // 取以上两者最大值
                }
                else
                {
                    down = downVal = 1; // 下行累计中断，重置down参数
                }

                // 计算完毕，准备下一个循环
                ra = r;
            }
            return sum + val; // 留意可能有val未结算，两者相加才是最终结果
        }


        public static void Run()
        {
            Console.WriteLine(new P0135分发糖果().Candy(new int[] { 1, 3, 2, 2, 1 }));
        }
    }
}
