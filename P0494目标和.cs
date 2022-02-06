using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
     * 给你一个整数数组 nums 和一个整数 target 。

向数组中的每个整数前添加 '+' 或 '-' ，然后串联起所有整数，可以构造一个 表达式 ：

例如，nums = [2, 1] ，可以在 2 之前添加 '+' ，在 1 之前添加 '-' ，然后串联起来得到表达式 "+2-1" 。
返回可以通过上述方法构造的、运算结果等于 target 的不同 表达式 的数目。

 

示例 1：

输入：nums = [1,1,1,1,1], target = 3
输出：5
解释：一共有 5 种方法让最终目标和为 3 。
-1 + 1 + 1 + 1 + 1 = 3
+1 - 1 + 1 + 1 + 1 = 3
+1 + 1 - 1 + 1 + 1 = 3
+1 + 1 + 1 - 1 + 1 = 3
+1 + 1 + 1 + 1 - 1 = 3
示例 2：

输入：nums = [1], target = 1
输出：1
 

提示：

1 <= nums.length <= 20
0 <= nums[i] <= 1000
0 <= sum(nums[i]) <= 1000
-1000 <= target <= 100

来源：力扣（LeetCode）
链接：https://leetcode-cn.com/problems/target-sum
著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。
     * */
    class P0494目标和
    {
        public class CalcStat
        {
            int[] arr = new int[2001];

            public CalcStat Init()
            {
                Set(0, 1);
                return this;
            }
            public CalcStat Clear()
            {
                for (int i = 0; i < 2001; ++i)
                    arr[i] = 0;
                return this;
            }
            public int Index(int sum)
            {
                return sum + 1000;
            }
            public void Set(int sum, int count)
            {
                arr[Index(sum)] += count;
            }
            public int Get(int sum)
            {
                return arr[Index(sum)];
            }

            public void Update(CalcStat c2, int num)
            {
                for (int i = -1000; i <= 1000; ++i)
                {
                    if (arr[Index(i)] > 0)
                    {
                        int count = arr[Index(i)];
                        int sum = i + num;
                        c2.Set(sum, count);
                        sum = i - num;
                        c2.Set(sum, count);
                    }
                }
            }
        }
        public int FindTargetSumWays(int[] nums, int target)
        {
            CalcStat c1 = new CalcStat().Init();
            CalcStat cTemp = new CalcStat();
            foreach (int num in nums)
            {
                c1.Update(cTemp, num);
                CalcStat cNew = c1.Clear();
                c1 = cTemp;
                cTemp = cNew;
            }
            return c1.Get(target);
        }
    }
}
