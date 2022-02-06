using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // 双周赛题, hard
    // 2022/1/22 虚拟赛没时间看，1/23使用HashHeap
    internal class P2009使数组连续的最少操作数
    {
        // ver2: 滑动窗口 - AC
        public int MinOperations(int[] nums)
        {
            Array.Sort(nums);
            List<int> list = new();
            for (int i = 0; i < nums.Length; ++i)
            {
                if (i > 0 && nums[i] == nums[i - 1]) continue;
                list.Add(nums[i]);
            }
            int start = 0, max = 1;
            for (int i = 1; i < list.Count; ++i)
            {
                while (list[i] >= list[start] + nums.Length) ++start;
                max = Math.Max(max, i - start + 1);
            }
            return nums.Length - max;
        }

        // 考虑下面这个用例：
        // [1,3,4,5,6,8], 会输出2，但应该是1
        // 上面的修正了，但是input1的用例错了
        public int MinOperations_ver1(int[] nums)
        {
            Array.Sort(nums);
            HashHeap hp = new(true); // 超级大顶堆
            int start = int.MinValue;
            for (int i = 0; i < nums.Length; ++i)
            {
                if (i > 0 && nums[i] == nums[i - 1]) continue;
                if (nums[i] < start + nums.Length) hp.Update(start, 1);
                else hp.Push(start = nums[i], 1);
            }
            return nums.Length - hp.Head;
        }

        internal static void Run()
        {
            int[] input1 = { 41, 33, 29, 33, 35, 26, 47, 24, 18, 28 }; // exp: 5, actual: 6
        }
    }
}
