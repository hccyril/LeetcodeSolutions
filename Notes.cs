using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    internal class Notes
    {
    }

    /**
     * 《一些踩过的坑》
     * 比较函数不要使用 return a - b，当a == int.MinValue时会溢出，同理取中间值不要用mid = (a + b) / 2 // P493
     * 负数右移并不等于除以2，例如-3 >> 1 == -2，但是-3 / 2 还是对的（-1）// P493
     * BinarySearch的返回值为>=0时不一定是最左边或者最右边的值，当数组存在相同值而且要取upper_bound或者lower_bound时要特别注意！// P493
     * #P0222 二分搜索的标准写法：
        int mid = l + (r - l + 1 >> 1);
        if (Find(root, mid) == null) r = mid - 1;
        else l = mid;
     * #P0269 火星词典(hard)
     *   使用sort和IComparer进行排序时，前提是比较函数是严格consistent的（也就是对于a>b,b>c一定有a>c）
     *   但实际情况未必如此，本题就是例子，使用sort排序出现WA，改成自己写的插入排序后就好了
     * #P0321 拼接最大数(hard)
     *   排序时有相同元素永远是最麻烦的，WA: [8,9] [3,9] 3, 如何先选数组2的9而不是数组1的9？
     * to be continued....
     *
     * */

    /// <summary>
    /// 可以用LINQ快速解的题目示例
    /// </summary>
    internal class Notes_Linq_Examples
    {
        public IList<IList<string>> P0049_GroupAnagrams(string[] strs)
         => strs
            .Select(s => new { str = s, key = string.Join("", s.ToCharArray().OrderBy(c => c)) })
            .GroupBy(t => t.key)
            .Select(gp => gp.Select(t => t.str).ToList() as IList<string>)
            .ToList();

        public bool P0242_IsAnagram(string s, string t)
            => s.Length == t.Length &&
            !s.OrderBy(c => c)
            .Zip(t.OrderBy(c => c), (sc, tc) => (sc, tc))
            .Any(p => p.sc != p.tc);

        public int P258_AddDigits(int num)
            => num < 10 ? num : P258_AddDigits(num.ToString().Select(t => t - '0').Sum());

        public char P389_FindTheDifference(string s, string t)
            => (s + "~").OrderBy(a => a).Zip(t.OrderBy(b => b), (sc, tc) => (sc, tc)).Where(c => c.sc != c.tc).Select(c => c.tc).First();

        public int P1672_MaximumWealth(int[][] accounts) 
            => accounts.Select(c => c.Sum()).Max();

        public int P1725_CountGoodRectangles(int[][] rectangles)
        {
            int maxLen = rectangles.Select(r => Math.Min(r[0], r[1])).Max();
            return rectangles.Count(r => r[0] >= maxLen && r[1] >= maxLen);
        }

        public int P1748_SumOfUnique(int[] nums)
            => nums.GroupBy(t => t).Select(gp => gp.Count() == 1 ? gp.Key : 0).Sum();
    }
}
