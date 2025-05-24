using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// 收集可以用LINQ一行代码实现的题目
internal class LinqDemo
{
    // ------------------- AI (待验证）-------------------

    // 1. 统计数组中等于某个值的元素个数（LeetCode 1512. 好数对的数目）
    public int P1512_NumIdenticalPairs(int[] nums)
        => nums.GroupBy(x => x).Sum(g => g.Count() * (g.Count() - 1) / 2);

    // 2. 统计字符串中某个字符出现的次数（LeetCode 771. 宝石与石头）
    public int P771_NumJewelsInStones(string jewels, string stones)
        => stones.Count(jewels.Contains);

    // 3. 判断数组是否存在重复元素（LeetCode 217. 存在重复元素）
    public bool P217_ContainsDuplicate(int[] nums)
        => nums.Distinct().Count() != nums.Length;

    // 4. 统计数组中所有偶数的和（LeetCode 1588. 所有奇数长度子数组的和，变体）
    public int SumOfEvenNumbers(int[] nums)
        => nums.Where(x => x % 2 == 0).Sum();

    // 5. 反转字符串数组（LeetCode 344. 反转字符串，变体）
    public string[] ReverseStringArray(string[] arr)
        => arr.Reverse().ToArray();

    // LeetCode 1365. 有多少小于当前数字的数字
    public int[] P1365_SmallerNumbersThanCurrent(int[] nums)
        => nums.Select(x => nums.Count(y => y < x)).ToArray();

    // LeetCode 1431. 拥有最多糖果的孩子
    public IList<bool> P1431_KidsWithCandies(int[] candies, int extraCandies)
        => candies.Select(x => x + extraCandies >= candies.Max()).ToList();

    // LeetCode 1480. 一维数组的动态和
    public int[] P1480_RunningSum(int[] nums)
        => nums.Select((x, i) => nums.Take(i + 1).Sum()).ToArray();

    // LeetCode 1672. 最富有客户的资产总量
    public int P1672_MaximumWealth(int[][] accounts)
        => accounts.Max(a => a.Sum());

    // LeetCode 1929. 数组串联
    public int[] P1929_GetConcatenation(int[] nums)
        => nums.Concat(nums).ToArray();

    // LeetCode 1108. IP 地址无效化
    public string P1108_DefangIPaddr(string address)
        => string.Join("[.]", address.Split('.'));

    // LeetCode 804. 唯一摩尔斯密码词
    public int P804_UniqueMorseRepresentations(string[] words)
    {
        string[] morse = [".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--.."];
        return words.Select(w => string.Concat(w.Select(c => morse[c - 'a']))).Distinct().Count();
    }

    // ------------- Mine --------------------

    public IList<int> P2942_FindWordsContaining(string[] words, char x)
    => Enumerable.Range(0, words.Length)
        .Where(i => words[i].Contains(x))
        .ToArray();
}

