using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2024/3/9 Daily
// rating 2647
internal class P2386找出数组的第K大和
{
    // 版本二：使用更多迭代操作
    public long KSum(int[] a, int k)
    {
        int n = a.Length;
        Array.Sort(a);
        int p = 0; while (p < n && a[p] <= 0) ++p;
        long max_sum = 0L;
        for (int i = p; i < n; ++i) max_sum += a[i];

        // 使用迭代从小到大枚举数组绝对值
        IEnumerable<int> SortedNums()
        {
            int i = p - 1, j = p;
            while (i >= 0 || j < n)
            {
                if (i >= 0 && (j >= n || -a[i] < a[j]))
                    yield return -a[i--];
                else // if (j < n) // correct but no need
                    yield return a[j++];
            }
        }

        //// DEBUG
        //Console.WriteLine(string.Join(" ", SortedNums()));
        //Console.WriteLine(string.Join('\n',
        //    SortedNums()
        //    .OrderedSum()
        //    .Select(s => max_sum - s)
        //));

        return SortedNums()
            .OrderedSum()
            .Skip(k - 1)
            .Select(s => max_sum - s)
            .First();
    }

    // 版本一：相对比较粗暴
    public long KSum_1(int[] nums, int k)
    {
        long max_sum = 0L;
        foreach (var x in nums.Where(x => x > 0)) max_sum += x;
        // long max_sum = nums.Where(x => x > 0).Sum(); // 前面迭代是int后面和是long会报错
        var sorted = nums.Select(t => Math.Abs(t)).OrderBy(t => t).ToArray();
        return max_sum - sorted.OrderedSumOfList().Skip(k - 1).First();
    }

    internal static void Run()
    {
        var sln = new P2386找出数组的第K大和();

        // simple case (ans=10)
        int[] nums = { 1, -2, 3, 4, -10, 12 };
        int k = 16;

        // WA case
        //string s = "[-347135403,-741775723,349271195,967839234,822470265,-545249891,293401682,908306445,296832265,9392523,-84929173,-784997375,699878100,291656873,-910458294,547370160,584504507,977373244,-963031162,819184328]";
        //int[] nums = s.ToTestInput<int[]>();
        //int k = 473;
        Console.WriteLine("ans=" + sln.KSum_1(nums, k));
    }
}
