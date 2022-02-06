using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2021/6/3
    // 前缀和，如果pre[i]==pre[j]，说明sum[i..j] = pre[j] - pre[i] == 0
    class P0525连续数组
    {
        public int FindMaxLength(int[] nums)
        {
            int max = 0, sum = 0; ;
            Dictionary<int, int> dic = new Dictionary<int, int>();
            dic.Add(0, -1);
            for (int i = 0; i < nums.Length; ++i)
            {
                sum += nums[i] == 0 ? -1 : 1;
                if (dic.ContainsKey(sum))
                {
                    int count = i - dic[sum];
                    if (count > max) max = count;
                }
                else
                {
                    dic.Add(sum, i);
                }
            }
            return max;
        }
    }
}
