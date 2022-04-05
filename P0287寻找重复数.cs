using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/2/14
    // 「Floyd 判圈算法」（又称龟兔赛跑算法）// 2022/3/29 REDO
    internal class P0287寻找重复数
    {
        public int FindDuplicate(int[] nums)
        {
            int meet = -1;
            // 兔子每次跑两步，乌龟跑一步，最终它们会在环中某处相遇
            for (int hare = 0, tortoise = 0; true;)
            {
                hare = nums[nums[hare]];
                tortoise = nums[tortoise];
                if (hare == tortoise)
                {
                    meet = hare;
                    break;
                }
            }
            // 兔子从相遇点出发每次一步，乌龟从起点出发每次一步，最终会在环入口相遇
            for (int hare = meet, tortoise = 0; true;)
            {
                hare = nums[hare];
                tortoise = nums[tortoise];
                if (hare == tortoise) return hare;
            }
        }
    }
}
