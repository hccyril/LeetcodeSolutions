using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleCore1;

// hard, 20260123 Daily
// rating 2608
// sortedset + priority queue
// 用到了并查集的思想（需同时维护前面和后面的边界）
internal class P3510移除最小数对使数组有序II
{
    public int MinimumPairRemoval(int[] nums)
    {
        int n = nums.Length;
        int[] pa = [.. Enumerable.Range(0, n)], // prev-arr-index
              pr = [.. Enumerable.Range(0, n)]; // right-index 
        long[] ps = [.. nums.Select(t => (long)t)]; // pair-sum of (a[i] + a[i - 1])
        PriorityQueue<(int, int), (long, int)> pq = new(); // <(index, prev-index), (sum, prev-index)>
        int rpc = 0; // reverse-pair-count, when rpc == 0 the array is sorted
        for (int i = 1; i < n; ++i)
        {
            long s = (long)nums[i] + nums[i - 1];
            if (nums[i - 1] > nums[i]) ++rpc;
            pq.Enqueue((i, i - 1), (s, i));
        }
        int Check(int i)
        {
            if (i < 0) return -1;
            Stack<int> stk = new();
            int j = -1;
            while (pa[i] != i)
            {
                if (j >= 0) stk.Push(j);
                j = i;
                i = pa[i];
            }
            while (stk.Count > 0)
                pa[stk.Pop()] = i;
            return i;
        }
        void Merge(int i, int j) // (j, i) -> (i)
            => (pa[Check(i)], pr[Check(j)]) = (Check(j), i);
        int ans = 0;
        while (rpc > 0)
        {
            (int i, int j0) = pq.Dequeue();
            if (i < n - 1 && pa[i + 1] != i + 1) continue; // FixWA: 必须是合并区间的右边界才是有效的
            int j = Check(Check(i) - 1);
            if (j != j0) continue;
            ++ans;
            if (i < n - 1 && ps[i] > ps[i + 1]) --rpc;
            if (ps[j] > ps[i]) --rpc;
            int k = -1;
            if (j > 0)
            {
                k = Check(j - 1);
                if (ps[k] > ps[j]) --rpc;
            }
            ps[i] = ps[j] = ps[i] + ps[j];
            Merge(i, j);
            // re-enqueue for the right pair
            if (i < n - 1)
            {
                if (ps[i] > ps[i + 1]) ++rpc;
                pq.Enqueue((pr[i + 1], j), (ps[i + 1] + ps[i], pr[i + 1]));
            }
            // re-enqueue for the left pair
            if (k >= 0)
            {
                if (ps[k] > ps[j]) ++rpc;
                pq.Enqueue((i, k), (ps[k] + ps[i], i));
            }
        }
        return ans;
    }

    internal static void Run()
    {
        var sln = new P3510移除最小数对使数组有序II();
        int[] a = [5, 2, 3, 1];
        Console.WriteLine("ans=" + sln.MinimumPairRemoval(a));
    }
}
