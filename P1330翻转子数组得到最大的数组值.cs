using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2023/5/12 Daily
    // rank: 2481 《--做了一整晚！！
    internal class P1330翻转子数组得到最大的数组值
    {
        public int MaxValueAfterReverse(int[] nums)
        {
            int n = nums.Length;
            int sm = Enumerable.Range(0, n - 1).Select(i => Math.Abs(nums[i] - nums[i + 1])).Sum();
            int x, ma = 0, 
                l = int.MaxValue, r = int.MinValue, 
                el = int.MaxValue, er = int.MinValue,
                fl = int.MaxValue, fr = int.MinValue;

            for (int i = 1; i < n - 1; ++i)
            {
                //if (i == 29998)
                //{
                //    Console.WriteLine("i=" + i);
                //    Console.WriteLine("l={0} r={1} sum={2} max={3} a[i]={4} a[i+1]={5}", l, r, sm, ma, nums[i], nums[i + 1]);
                //}
                x = Math.Min(nums[i], nums[i + 1]);
                if (x > l)
                    ma = Math.Max(ma, x - l << 1);
                x = Math.Max(nums[i], nums[i + 1]);
                if (x < r)
                    ma = Math.Max(ma, r - x << 1);
                l = Math.Min(l, Math.Max(nums[i], nums[i - 1]));
                r = Math.Max(r, Math.Min(nums[i], nums[i - 1]));

                el = Math.Min(el, nums[i] < nums[i - 1] ? nums[i - 1] * 2 - nums[i] : nums[i]);
                er = Math.Max(er, nums[i] > nums[i - 1] ? nums[i - 1] * 2 - nums[i] : nums[i]);

                fl = Math.Min(fl, nums[i] < nums[i + 1] ? nums[i + 1] * 2 - nums[i] : nums[i]);
                fr = Math.Max(fr, nums[i] > nums[i + 1] ? nums[i + 1] * 2 - nums[i] : nums[i]);
            }
            x = nums.Last();
            if (x > el) ma = Math.Max(ma, x - el);
            if (x < er) ma = Math.Max(ma, er - x);
            x = nums.First();
            if (x > fl) ma = Math.Max(ma, x - fl);
            if (x < fr) ma = Math.Max(ma, fr - x);

            return sm + ma;
        }

        // testcases:
        // [5,-7,9,-6,8] -> 57
        // [93997,2877,-93018,-76995,-70679] -> 369098
        // nums = [-93018,2877,93997,-76995,-70679] # ans=511435
        // nums=[0..29999] -> 89993 (就是交换第二个和倒数第二个）

        internal static void Run()
        {
            var sln = new P1330翻转子数组得到最大的数组值();
            int[] nums = Enumerable.Range(0, 30000).ToArray();
                //{ -93018, 2877, 93997, -76995, -70679 };
                //{ 93997, 2877, -93018, -76995, -70679 };
            int ans = sln.MaxValueAfterReverse(nums);
            Console.WriteLine("ans=" + ans);
        }
    }
}
