using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 周赛题
    // 回溯
    internal class P2122还原原数组
    {
        class CountStruct
        {
            public int num;
            public int count;
            public int orgval;
        }
        Dictionary<int, CountStruct> dic = new();
        public int[] RecoverArray(int[] nums)
        {
            Array.Sort(nums);
            List<CountStruct> list = new();
            foreach (var n in nums)
            {
                if (dic.ContainsKey(n))
                {
                    ++dic[n].count;
                }
                else
                {
                    var cs = new CountStruct() { num = n, count = 1 };
                    list.Add(cs);
                    dic[n] = cs;
                }
            }
            foreach (var cs in list) cs.orgval = cs.count;
            int half = nums.Length >> 1;
            for (int i = 1; i <= half; i++)
                if (nums[i] > nums[0] && ((nums[i] - nums[0]) & 1) == 0)
                {
                    int dk = nums[i] - nums[0]; //, k = dk >> 1;
                    if (CheckValid(list, dic, dk, half))
                        return BuildArray(list, dic, dk, half);
                    if (i < half)
                        foreach (var cs in list) cs.count = cs.orgval;
                }
            return Array.Empty<int>();
        }

        private bool CheckValid(List<CountStruct> list, Dictionary<int, CountStruct> dic, int dk, int half)
        {
            int i = 0;
            CountStruct low = list[i];
            for (int len = 0; len < half; ++len)
            {
                while (low.count == 0) low = list[++i];
                --low.count;
                bool ok = false;
                int n = low.num + dk;
                if (dic.ContainsKey(n))
                {
                    var high = dic[n];
                    if (high.count > 0)
                    {
                        --high.count;
                        ok = true;
                    }
                }
                if (!ok) return false;
            }
            return true;
        }

        private int[] BuildArray(List<CountStruct> list, Dictionary<int, CountStruct> dic, int dk, int half)
        {
            int k = dk >> 1;
            foreach (var cs in list) cs.count = cs.orgval;
            int[] ans = new int[half];
            int i = 0;
            CountStruct low = list[i];

            for (int len = 0; len < half; ++len)
            {
                while (low.count == 0) low = list[++i];
                --low.count;
                int n = low.num + dk, a = low.num + k;
                --dic[n].count;
                ans[len] = a;
            }
            return ans;
        }
    }
}
