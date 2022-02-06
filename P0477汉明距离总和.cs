using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 位运算 （前置问题：461汉明距离）
    class P0477汉明距离总和
    {
        /* 题解（贴在评论区）
         * 只看一位就好理解了：
         * 一开始房间里一个数都没有，依次进来 0 和 1 ：
         * 进来一个 0 跟房间里的所有 1 打招呼（距离+1），进来一个 1 跟房间里所有的 0 打招呼，
         * 最后统计打招呼的总数

            然后只有30个房间（10^9最多30位），用一个数组应用以上统计算法就行了
        */
        public int TotalHammingDistance(int[] nums)
        {
            int sum = 0;
            int[] bits = new int[30];
            for (int i = 0; i < 30; ++i) bits[i] = 1 << i;
            int[] a0 = new int[30], a1 = new int[30];
            foreach (var n in nums)
                for (int i = 0; i < 30; ++i)
                    if ((n & bits[i]) != 0)
                    {
                        sum += a0[i];
                        a1[i]++;
                    }
                    else
                    {
                        sum += a1[i];
                        a0[i]++;
                    }
            return sum;
        }
    }
}
