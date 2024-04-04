using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2024/1/28 WC 382-D
// 比赛分8分，做出人数很少，估计rating2500以上
// 比赛时想到一点思路，但没做出来，后面看题解发现一个关键点没想通
// # 关键问题转换：对于bit，操作k次之后or和等于0 <==> 将数组分成 >= (n-k) 个子数组，每个子数组的and和为0
internal class P3022给定操作次数内使剩余元素的或值最小
{
    public int MinOrAfterOperations(int[] nums, int k)
    {
        int n = nums.Length, mask = 0, ans = 0;
        for (int i = 29; i >= 0; --i)
        {
            mask |= 1 << i;
            int nSub = 0, res = mask;
            foreach (int x in nums)
            {
                res = res == 0 ? x & mask : res & x;
                if (res == 0)
                    ++nSub;
            }
            if (nSub < n - k)
            {
                ans |= 1 << i;
                mask ^= 1 << i;
            }
        }
        return ans;
    }
}
