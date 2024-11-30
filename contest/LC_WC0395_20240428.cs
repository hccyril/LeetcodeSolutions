using System;
using System.Linq;

namespace ConsoleCore1.contest;

// 第395场周赛 （在leetcode.com进行）
// 比赛时全用python完成，考虑赛后用c#重写
internal class LC_WC0395_20240428
{
	#region Problem A
	public int AddedInteger(int[] nums1, int[] nums2)
		=> nums2.Min() - nums1.Min();

	#endregion

	#region Problem B
	public int MinimumAddedInteger(int[] nums1, int[] nums2)
	{
        int n1 = nums1.Length, n2 = nums2.Length, ans = 20000;
        Array.Sort(nums2);
        for (int i = 0; i < n1 - 1; ++i)
            for (int j = i + 1; j < n1; ++j)
            {
                int d = -20000, l = 0;
                bool ok = true;
                foreach (var x in Enumerable.Range(0, n1).Where(k => k != i && k != j).Select(k => nums1[k]).OrderBy(x => x))
                {
                    if (d < -1001)
                        d = nums2[l] - x;
                    else if (nums2[l] - x != d)
                    {
                        ok = false;
                        break;
                    }
                    ++l;
                }
                if (ok)
                    ans = Math.Min(ans, d);
            }
        return ans;
	}

	#endregion

	#region Problem C
    // self+ref: 使用lowbit优化
	public long MinEnd(int n, int x)
	{
        long ans = x, t = ~x;
        --n;
        for (int j = 0; (n >> j) != 0; ++j)
        {
            long lb = t & -t;
            ans |= lb * (n >> j & 1);
            t &= t - 1;
        }
        return ans;
	}
	#endregion

	#region Problem D
    /**
     * 考虑例子[4,3,5,4]，枚举任意子数组的SetCount，会发现可以组成一个单调矩阵
     *  i/j |   0   1   2   3
     *  ------------------------
     *   0  |   1   2   3   3
     *   1  |       1   2   3
     *   2  |           1   2
     *   3  |               1
     *   因此，可以参考力扣240以及二分猜答案的策略，将问题转化为找单调矩阵中第K大的数
     * */
	public int MedianOfUniquenessArray(int[] nums)
	{
		int n = nums.Length;
		long k = ((long)n * (n + 1) / 2) + 1 >> 1;
		//Console.WriteLine("k=" + k);
		bool Check(int x)
		{
			int i = 0, j = -1;
			long t = 0;
			var cn = new Counter<int>();
			while (i < n)
			{
				while (j + 1 < n && cn.Count <= x)
					++cn[nums[++j]];
				if (j == n - 1 && cn.Count <= x)
					break;
				while (i < n && cn.Count > x)
				{
					t += n - j;
					--cn[nums[i++]];
				}
			}
			t = (long)n * (n + 1) / 2 - t;
			return t >= k;
		}

		int l = 1, r = n;
		while (l < r)
		{
			int m = l + r >> 1;
			if (Check(m))
				r = m;
			else
				l = m + 1;
		}
		return l;
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
        LC_WC0395_20240428 sln = new();

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
        return 0;
    }

    int RunTestE()
    {
        return 0;
    }
    #endregion
}
