using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 周赛280题D 2022/2/13
    // 记忆化回溯，当时没做出来
    internal class P2172数组的最大与和
    {
        /* test case:
         * [14,7,9,8,2,4,11,1,9] 8
         * ans=32 expected=40
         * 事实证明再次想太多，不需要针对高位进行剪枝，直接记忆化回溯就可解
         * */

        Dictionary<int, int> dic = new();

        public int MaximumANDSum(int[] nums, int numSlots)
        {
            int[] slots = new int[10];
            for (int i = 1; i <= numSlots; ++i) slots[i] = 2;
            Array.Sort(nums); Array.Reverse(nums);
            return Dfs(nums, 0, slots);
        }

        int Dfs(int[] nums, int i, int[] slots)
        {
            if (i >= nums.Length) return 0;
            int key = ZipState(i, slots);
            if (dic.ContainsKey(key)) return dic[key];
            int maxSum = 0;
            foreach (int s in Enumerable.Range(1, 9).Where(s => slots[s] > 0))
            {
                int sum = nums[i] & s;
                --slots[s];
                sum += Dfs(nums, i + 1, slots);
                ++slots[s];
                if (sum > maxSum) maxSum = sum;
            }
            return dic[key] = maxSum;
        }

        int ZipState(int n, int[] slots)
        {
            int key = 0;
            for (int i = 1; i <= 9; ++i)
                key |= slots[i] << (i * 2 + 5);
            key |= n;
            return key;
        }

#if VER1_想多了
        Dictionary<long, int> dic = new();

        public int MaximumANDSum(int[] nums, int numSlots)
        {
            int[] slots = new int[10];
            for (int i = 1; i <= numSlots; ++i) slots[i] = 2;
            Array.Sort(nums); Array.Reverse(nums);
            int map = (1 << nums.Length) - 1;
            return Dfs(nums, map, slots);
        }

        int Dfs(int[] nums, int map, int[] slots)
        {
            if (map == 0) return 0;
            long key = ZipState(map, slots);
            if (dic.ContainsKey(key)) return dic[key];
            int maxSum = 0;
            foreach ((int i, int s) in Pickup(nums, map, slots))
            {
                int sum = nums[i] & s;
                --slots[s];
                sum += Dfs(nums, map ^ 1 << i, slots);
                ++slots[s];
                if (sum > maxSum) maxSum = sum;
            }
            return dic[key] = maxSum;
        }

        IEnumerable<(int i, int s)> Pickup(int[] nums, int map, int[] slots)
        {
            // ...
        }

        long ZipState(int map, int[] slots)
        {
            long key = 0;
            for (int i = 1; i <= 9; ++i)
                key |= (long)slots[i] << (i * 2 + 20);
            key |= (long)map;
            return key;
        }
#endif
    }
}
