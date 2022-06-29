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
        int mid = l + (r - l + 1 >> 1); // mid = l + r + 1 >> 1
        if (Find(root, mid) == null) r = mid - 1;
        else l = mid;
       #P0668 二分搜索定式2：
        mid = l + (r - l >> 1) // mid = l + r >> 1
        r = mid
        l = mid + 1
     * #P0269 火星词典(hard)
     *   使用sort和IComparer进行排序时，前提是比较函数是严格consistent的（也就是对于a>b,b>c一定有a>c）
     *   但实际情况未必如此，本题就是例子，使用sort排序出现WA，改成自己写的插入排序后就好了
     * #P0321 拼接最大数(hard)
     *   排序时有相同元素永远是最麻烦的，WA: [8,9] [3,9] 3, 如何先选数组2的9而不是数组1的9？
     * #P0432 要求所有的操作都在O(1)时间内
     *   问题1：数组，数组元素为直接计数，当dec(max)时，就找不到下一个max是谁了
     *   问题2：数组元素改成hashset，用例：添加一个a,5个b,10个c，然后这时dec(a)，min=a删除以后就不知道下一个min是谁了
     * #P1928 规定时间内到达终点的最小花费
     *   坑1：两点之间可以有多条边，权值不一样
     *   坑2：time应该作为key而不是value处理
     * #P2183 周赛题D 2022/2/20
     *   分解质因数时循环条件是p*p<=k，但漏了可能要特殊处理最后一个因子，例如6=2*3 //低级错误啊！导致迟了10分钟才提交通过，比赛时没做出来！
     * #P0459重复的子字符串, #P0471编码最短长度的字符串
     *   判断s是否子串的重复（例如“ababab”这种）：{s+s然后去掉头尾}是否包含s，而且重复的起始位置就在IndexOf(s)+1
     * #P0632 SortedSet的Comparer
     *   实现Comparer并避免返回0就可以解决不能重复添加元素的问题，但与此同时Remove方法会出问题，因为定位不到相同的元素
     * #P0871. Minimum Number of Refueling Stops
     *   ver1/ver2 会发生一个重复加油的错误
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

        // also 剑指 Offer 58 - I. 翻转单词顺序
        public string ReverseWords(string s)
            => string.Join(" ", s.Trim().Split(' ').Where(t => !string.IsNullOrWhiteSpace(t)).Reverse());

        public bool P0242_IsAnagram(string s, string t)
            => s.Length == t.Length &&
            !s.OrderBy(c => c)
            .Zip(t.OrderBy(c => c), (sc, tc) => (sc, tc))
            .Any(p => p.sc != p.tc);

        public int P0258_AddDigits(int num)
            => num < 10 ? num : P0258_AddDigits(num.ToString().Select(t => t - '0').Sum());
        // 347. Top K Frequent Elements (Medium)
        public int[] P0347_TopKFrequent(int[] nums, int k)
            => nums.GroupBy(n => n).OrderByDescending(g => g.Count()).Take(k).Select(g => g.Key).ToArray();
        public char P0389_FindTheDifference(string s, string t)
            => (s + "~").OrderBy(a => a).Zip(t.OrderBy(b => b), (sc, tc) => (sc, tc)).Where(c => c.sc != c.tc).Select(c => c.tc).First();

		public bool P0796_RotateString(string s, string goal) 
			=> s.Length == goal.Length && (s + s).Contains(goal);

        public string P0819_MostCommonWord(string paragraph, string[] banned)
            => paragraph.Split(' ', '!', '?', '\'', ',', ';', '.')
            .Select(t => t.ToLower())
            .Where(t => t != "" && !banned.Any(b => b == t))
            .GroupBy(t => t)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .First();

        public int[] P0905_SortArrayByParity(int[] nums)
            => nums.Where(n => (n & 1) == 0).Concat(nums.Where(n => (n & 1) == 1)).ToArray();

        public int[] P1337_KWeakestRows(int[][] mat, int k)
            => Enumerable.Range(0, mat.Length).OrderBy(r => mat[r].Sum()).Take(k).ToArray();
        //return (from i in Enumerable.Range(0, mat.Length)
        //        orderby mat[i].Sum()
        //        select i).Take(k).ToArray();

        public int P1665_MinimumEffort(int[][] tasks)
            => tasks.OrderBy(t => t[1] - t[0]).Aggregate(0, (e, t) => Math.Max(e + t[0], t[1]));

        public int P1672_MaximumWealth(int[][] accounts) 
            => accounts.Select(c => c.Sum()).Max();

        public int P1725_CountGoodRectangles(int[][] rectangles)
        {
            int maxLen = rectangles.Select(r => Math.Min(r[0], r[1])).Max();
            return rectangles.Count(r => r[0] >= maxLen && r[1] >= maxLen);
        }

        public int P1748_SumOfUnique(int[] nums)
            => nums.GroupBy(t => t).Select(gp => gp.Count() == 1 ? gp.Key : 0).Sum();

        public bool P1941_AreOccurrencesEqual(string s)
            => s.GroupBy(c => c).Select(g => g.Count()).GroupBy(n => n).Count() == 1;

        // 剑指 Offer 21. 调整数组顺序使奇数位于偶数前面
        public int[] Offer0021_Exchange(int[] nums)
            => nums.Where(n => (n & 1) == 1).Concat(nums.Where(n => (n & 1) == 0)).ToArray();

    }
}
