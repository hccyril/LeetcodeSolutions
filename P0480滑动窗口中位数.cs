using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, // 利用加强版head和295的题解可以很容易做出来
    class P0480滑动窗口中位数
    {
        public double[] MedianSlidingWindow(int[] nums, int k)
        {
            var finder = new P0295数据流的中位数.MedianFinder();
            for (int i = 0; i < k; ++i)
                finder.AddNum(i, nums[i]);
            List<double> ansList = new List<double>();
            ansList.Add(finder.FindMedian());
            for (int start = k, end = 0; start < nums.Length; ++start, ++end)
            {
                finder.Remove(end);
                finder.AddNum(start, nums[start]);
                ansList.Add(finder.FindMedian());
            }
            return ansList.ToArray();
        }
    }
}
