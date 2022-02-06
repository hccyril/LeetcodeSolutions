using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/2/5 双周赛71 D
    // 开始27:02，结束49:43 用时22:41
    internal class P2163删除元素后和的最小差值
    {
        public long MinimumDifference(int[] nums)
        {
            int n = nums.Length / 3;
            long sum = 0;
            long[] right = new long[n + 1];
            Heap<int> smallHp = new((a, b) => a < b);
            for (int i = 2 * n; i < nums.Length; ++i)
            {
                smallHp.Push(nums[i]);
                sum += nums[i];
            }
            right[n] = sum;
            for (int i = 2 * n - 1; i >= n; --i)
            {
                sum += nums[i];
                smallHp.Push(nums[i]);
                sum -= smallHp.Pop();
                right[i - n] = sum;
            }
            Heap<int> bigHp = new((a, b) => a > b);
            sum = 0;
            for (int i = 0; i < n; ++i)
            {
                sum += nums[i];
                bigHp.Push(nums[i]);
            }
            long minDiff = sum - right[0];
            for (int i = n; i < 2 * n; ++i)
            {
                sum += nums[i];
                bigHp.Push(nums[i]);
                sum -= bigHp.Pop();
                minDiff = Math.Min(minDiff, sum - right[i - n + 1]);
            }
            return minDiff;
        }
    }
}
