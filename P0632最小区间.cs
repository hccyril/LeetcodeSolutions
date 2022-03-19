using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/3/16
    // 排序（SortedSet）
    internal class P0632最小区间
    {
        class MyComp : IComparer<(int, int)>
        {
            readonly IList<IList<int>> nums;
            public MyComp(IList<IList<int>> nums) => this.nums = nums;
            public int Compare((int, int) a, (int, int) b)
            {
                // 一个compare方法写了三个版本！主要是没考虑到值的影响会带来Remove删除失败
                int cmp = nums[a.Item1][a.Item2].CompareTo(nums[b.Item1][b.Item2]);
                if (cmp == 0) cmp = a.Item1.CompareTo(b.Item1);
                if (cmp == 0) cmp = a.Item2.CompareTo(b.Item2);
                return cmp;

                // ver2 
                //if (a.Item1 == b.Item1 && a.Item2 == b.Item2) return 0;
                //return nums[a.Item1][a.Item2] < nums[b.Item1][b.Item2] ? -1 : 1;

                // ver1
                //=> nums[a.Item1][a.Item2] < nums[b.Item1][b.Item2] ? -1 : 1;
            }
        }
        public int[] SmallestRange(IList<IList<int>> nums)
        {
            SortedSet<(int, int)> sort = new(new MyComp(nums));
            for (int i = 0; i < nums.Count; ++i)
                sort.Add((i, 0));
            int start = nums[sort.Min.Item1][sort.Min.Item2], end = nums[sort.Max.Item1][sort.Max.Item2], range = end - start;
            while (sort.Any())
            {
                (int i, int j) = sort.Min;
                if (j == nums[i].Count - 1) break;
                sort.Remove(sort.Min);
                sort.Add((i, j + 1));
                int s = nums[sort.Min.Item1][sort.Min.Item2], t = nums[sort.Max.Item1][sort.Max.Item2];
                if (t - s < range)
                {
                    range = t - s;
                    start = s;
                    end = t;
                }
            }
            return new int[] { start, end };
        }

        internal static void Run()
        {
            var sln = new P0632最小区间();
            var input = new int[][] { new int[] { 1,2,3 }, new int[] { 1,2,3 }, new int[] { 1,2,3 } };
            //var input = new int[][] { new int[] { 4, 10, 15, 24, 26 }, new int[] { 0, 9, 12, 20 }, new int[] { 5, 18, 22, 30 } };
            var ans = sln.SmallestRange(input);
            Console.WriteLine(ans[0] + " " + ans[1]);
        }
    }
}
