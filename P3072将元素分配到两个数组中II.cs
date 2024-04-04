using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2024/3/16 -> WC387_20240303
// 树状数组（前置题315）+ 离散化 
internal class P3072将元素分配到两个数组中II
{
    public int[] ResultArray(int[] nums)
    {
        int n = nums.Length;
        DiscreteNums<int> da = new(nums);
        List<int> arr1 = new() { nums[0] }, arr2 = new() { nums[1] };
        Fenwick ft1 = new(100000), ft2 = new(100000);
        ft1.Update(da[0]); ft2.Update(da[1]);
        for (int i = 2; i < n; ++i)
        {
            int s1 = arr1.Count - ft1.Sum(da[i]),
                s2 = arr2.Count - ft2.Sum(da[i]);
            if (s1 > s2 || s1 == s2 && arr1.Count <= arr2.Count)
            {
                arr1.Add(nums[i]);
                ft1.Update(da[i]);
            } 
            else
            {
                arr2.Add(nums[i]);
                ft2.Update(da[i]);
            }
        }
        return arr1.Concat(arr2).ToArray();
    }
}
