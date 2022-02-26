using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/2/19 US Daily
    // 想了一整天，到晚上突然想通了，就一下做出来了
    internal class P1675数组的最小偏移量
    {
        public int MinimumDeviation(int[] nums)
        {
            Heap<int> hp = new((a, b) => a > b);
            int min = int.MaxValue;
            foreach (int n in nums)
            {
                int k = n;
                if ((k & 1) != 0) k <<= 1;
                if (k < min) min = k;
                hp.Push(k);
            }
            int diff = hp.Head - min;
            while (hp.Any() && (hp.Head & 1) == 0)
            {
                int k = hp.Pop() >> 1;
                hp.Push(k);
                if (k < min) min = k;
                diff = Math.Min(diff, hp.Head - min);
            }
            return diff;
        }
    }
}
