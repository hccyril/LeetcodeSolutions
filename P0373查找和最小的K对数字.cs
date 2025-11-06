using System;
using System.Collections.Generic;

// medium, 经典题，2023/7/10重做
// 优先队列（堆）
internal class P0373查找和最小的K对数字
{
    // 规则：
    // (i, j) 出堆时，入堆 (i, j+1)
    // (i, 0) 出堆时，入堆 (i + 1, 0)
    public IList<IList<int>> KSmallestPairs(int[] nums1, int[] nums2, int k)
    {
        PriorityQueue<(int, int), int> pq = new();
        pq.Enqueue((0, 0), nums1[0] + nums2[0]);
        List<IList<int>> ansLi = new();
        while (pq.Count > 0 && k-- > 0)
        {
            (int i, int j) = pq.Dequeue();
            ansLi.Add(new int[] { nums1[i], nums2[j] });
            if (j < nums2.Length - 1)
                pq.Enqueue((i, j + 1), nums1[i] + nums2[j + 1]);
            if (j == 0 && i < nums1.Length - 1)
                pq.Enqueue((i + 1, 0), nums1[i + 1] + nums2[0]);
        }
        return ansLi;
    }

    internal static void Run()
    {
        int[] a1 = { 1, 7, 11 }, a2 = { 2, 4, 6 };
        int k = 3;
        var sln = new P0373查找和最小的K对数字();
        var ans = sln.KSmallestPairs(a1, a2, k);
        foreach (var li in ans)
            Console.WriteLine(string.Join(" ", li));
    }
}

