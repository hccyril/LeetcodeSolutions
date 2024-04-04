using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest;

// 106 / 2547	呱呱编程实验室 	19	1:29:28	 0:02:22	 0:19:53  1	 1:19:28  1	 1:02:54
// 状态不是太好，第二题便花了很多时间而且错了一次，幸好第四题很快想到思路，第三题又是脑筋急转弯（而不是花时间的题），所幸还是AK了
internal class LC_BiC122_20240120
{
    #region Problem A
    // 在nums[1:]中找两个最小的数
    public int MinimumCost(int[] nums)
    {
        int ans = nums[0] + nums[1] + nums[2];
        for (int i = 1; i < nums.Length - 1; ++i)
            for (int j = i + 1; j < nums.Length; ++j)
                ans = Math.Min(ans, nums[0] + nums[i] + nums[j]);
        return ans;
    }
    #endregion

    #region Problem B
    int BiC(int x) => x == 0 ? 0 : 1 + BiC(x & x - 1);

    bool IsSorted(int[] nums)
    {
        for (int i = 1; i < nums.Length; ++i)
            if (nums[i] < nums[i - 1]) return false;
        return true;
    }
    // 直接套冒泡排序，当需要交换而无法交换时返回false
    public bool CanSortArray(int[] a)
    {
        int bound, exchange = a.Length - 1;
        while (exchange != 0)
        {
            bound = exchange;
            exchange = 0;
            for (int i = 0; i < bound; i++)
                if (a[i] > a[i + 1])
                {
                    if (BiC(a[i]) == BiC(a[i + 1]))
                    {
                        (a[i], a[i + 1]) = (a[i + 1], a[i]);
                        exchange = i;
                    }
                    else return false;
                }
        }
        return true;
    }
    /* 第一遍做的时候审错题了，以为任意两个BiC一样的都可以交换，后来才发现相邻的才可以
    void Version1() {
        List<int>[] di = new List<int>[10];
        for (int i = 0; i < 10; ++i) di[i] = new List<int>();
        foreach (int x in nums) di[BiC(x)].Add(x);
        foreach (var li in di) li.Sort();
        int[] ia = new int[10];
        for (int i = 0; i < nums.Length; ++i) {
            int b = BiC(nums[i]);
            nums[i] = di[b][ia[b]++];
        }
        return IsSorted(nums);
    }*/
    #endregion

    #region Problem C
    // 脑筋急转弯，最后就剩两种情况需要考虑
    /*
    def minimumArrayLength(self, nums: List[int]) -> int:
        nums.sort()
        mi = nums[0]
        for x in nums:
            if x % mi != 0:
                return 1
        c = nums.count(nums[0])
        return (c + 1) >> 1
     * */
    #endregion

    #region Problem D
    // 一开始想到的是两个有序集合是重叠的，一个小集合(k-1)一个大集合(dist+1)，后来中途想到会出问题
    // 所幸很快想到要改成两个不重叠的集合并维护数量
    // 标注!!的地方在一开始写漏了，所幸都在三个测试用例中被发现了，没有WA
    public long MinimumCost(int[] nums, int k, int dist)
    {
        long x0 = nums[0], ans = 1000000000L * 100001L, sm = 0L;
        SortedSet<(int, int)> sk = new(), sd = new();
        int j = 0;
        for (int i = 1; i + dist < nums.Length; ++i)
        {
            while (j < i + dist)
            {
                ++j;
                if (sk.Count < k - 1 || sk.Max.Item1 > nums[j])
                {
                    sk.Add((nums[j], j));
                    sm += nums[j];
                    if (sk.Count > k - 1)
                    {
                        sm -= sk.Max.Item1;
                        sd.Add(sk.Max);
                        sk.Remove(sk.Max);
                    }
                }
                else
                    sd.Add((nums[j], j));
            }

            /*Console.WriteLine("i={0} sm={1}", i, sm);
            foreach ((int x, int xi) in sk)
                Console.Write("{0} ", x);
            Console.WriteLine();*/

            ans = Math.Min(ans, x0 + sm);

            // remove i
            if (sk.Remove((nums[i], i)))
            {
                sm -= nums[i]; // !!
                if (sd.Any())
                {
                    sk.Add(sd.Min);
                    sm += sd.Min.Item1; // !!
                    sd.Remove(sd.Min);
                }
            }
            else
            {
                sd.Remove((nums[i], i));
            }
        }
        return ans;
    }
    #endregion

    #region Problem E
    public int SolveE(int x)
    {
        return x;
    }
    #endregion

    #region Run Test
    internal static int Run()
    {
        char p = 'D';
        LC_BiC122_20240120 sln = new();

        return p switch
        {
            'A' => sln.RunTestA(),
            'B' => sln.RunTestB(),
            'C' => sln.RunTestC(),
            'D' => sln.RunTestD(),
            'E' => sln.RunTestE(),
            _ => -1
        };
    }

    int RunTestA()
    {
        return 0;
    }

    int RunTestB()
    {
        return 0;
    }

    int RunTestC()
    {
        return 0;
    }

    int RunTestD()
    {
        return 122;
    }

    int RunTestE()
    {
        return 0;
    }
    #endregion
}
