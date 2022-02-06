using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2021/12/12, 记忆化回溯=DP
    internal class P0416分割等和子集
    {
        // BFS - seems troublesome...
        public bool CanPartition(int[] nums)
        {
            // pre-check
            int sum = nums.Sum(), i = 0;
            if ((sum & 1) != 0) return false;
            int half = sum >> 1;
            for (i = 0; i < nums.Length; ++i)
                if (nums[i] > half) return false;
                else if (nums[i] == half) return true;
            if (nums.Any(t => t > half)) return false;

            // init
            int[] iarr = new int[nums.Length], sums = new int[nums.Length];
            int count = nums.Length;
            for (i = 0; i < nums.Length; ++i) iarr[i] = i;
            HashHeap hp = new(false);
            for (i = 0; i < nums.Length; ++i)
                hp.Push(i, nums[i]);

            // BFS
            while (hp.Any())
            {
                (i, sum) = hp.Pop();
                sums[i] = sum;
                for (int j = count - 1; j >= 0; --j)
                {
                    if (iarr[j] == i)
                    {
                        iarr[j] = iarr[count - 1];
                        count--;
                    }
                    else
                    {
                        int next = iarr[j];
                        //TODO
                    }
                }
            }

            throw new NotImplementedException();
        }

        int[] nums;
        Dictionary<int, bool> dic;
        // 背包问题：前i个子数组能否组出sum的和
        bool Search(int i, int sum)
        {
            int key = (i << 15) | sum;
            if (dic.ContainsKey(key)) return dic[key];

            // 0
            if (i > 0)
            {
                var d0 = Search(i - 1, sum);
                if (d0) return dic[key] = true;
            }

            // 1
            if (nums[i] == sum) return dic[key] = true;
            else if (i > 0 && nums[i] < sum) return dic[key] = Search(i - 1, sum - nums[i]);

            // end
            return dic[key] = false;
        }
        // ver1: 记忆化回溯（DP）
        public bool CanPartition_DP(int[] nums)
        {
            this.nums = nums;
            dic = new();
            int sum = nums.Sum();
            if ((sum & 1) != 0) return false;
            return Search(nums.Length - 1, sum >> 1);
        }
    }
}
