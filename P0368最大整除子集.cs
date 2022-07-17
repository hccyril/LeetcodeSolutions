using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, DP // 2021/11/15 Daily
	// 2022/7/16 完成
    class P0368最大整除子集
    {
		public IList<int> LargestDivisibleSubset(int[] nums) {
			Array.Sort(nums);
			int[] dp = new int[nums.Length];
			int[] next = new int[nums.Length];
			for (int i = 0; i < nums.Length; ++i) {
				dp[i] = 1; next[i] = -1;
				for (int j = 0; j < i; ++j) 
					if (nums[i] % nums[j] == 0 && dp[j] >= dp[i]) {
						dp[i] = dp[j] + 1;
						next[i] = j;
					}
			}
			List<int> ans = new();
			int maxLen = dp.Max();
			for (int i = Enumerable.Range(0, nums.Length).First(i => dp[i] == maxLen); 
				 i >= 0; ans.Add(nums[i]), i = next[i]);
			return ans;
		}
    }
}
