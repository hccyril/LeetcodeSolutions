using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // 回溯 / DFS
    class P0698划分为k个相等的子集
    {
        int target;
        int[] nums;

        bool Dfs(int i, int k, int sum)
        {
#if DEBUG
            Console.WriteLine($"i={i} k={k} sum={sum}");
#endif
            if (k == 0) return true;
            sum -= nums[i];
            nums[i] = -nums[i];
            if (sum == 0)
            {
                if (k == 1) return true;
                for (int ii = 0; ii < nums.Length; ++ii)
                    if (nums[ii] > 0)
                    {
                        if (Dfs(ii, k - 1, target))
                            return true;
                        break;
                    }
            }
            else
            {
                int prev = -1;
                for (int j = i + 1; j < nums.Length; ++j)
                    if (nums[j] > 0 && nums[j] != prev && nums[j] <= sum)
                    {
                        prev = nums[j];
                        if (Dfs(j, k, sum))
                            return true;
                    }
            }
            // restore
            nums[i] = -nums[i];
            return false;
        }

        public bool CanPartitionKSubsets(int[] nums, int k)
        {
            this.nums = nums;
            int sum = nums.Sum();
            target = sum / k;
            if (target * k != sum || nums.Any(t => t > target)) return false;
            Array.Sort(nums);
            return Dfs(0, k, target);
        }

        internal static void Run()
        {
            int[] input = { 2, 2, 2, 2, 3, 4, 5 }; // should be false
            var output = new P0698划分为k个相等的子集().CanPartitionKSubsets(input, 4);
            Console.WriteLine("output=" + output);
        }
    }
}
