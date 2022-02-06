using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 超级堆 2022/2/5
    internal class P0347前K个高频元素
    {
        public int[] TopKFrequent(int[] nums, int k)
        {
            HashHeap hp = new(true);
            foreach (var n in nums)
                if (hp.ContainsKey(n)) hp.Update(n, 1);
                else hp.Push(n, 1);
            int[] ans = new int[k];
            for (k = 0; k < ans.Length; k++) ans[k] = hp.Pop().key;
            return ans;
        }
    }
}
