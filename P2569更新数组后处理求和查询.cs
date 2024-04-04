using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// 2024/1/20 难题挑战
// rating 2397
// 想了30分钟还是思路不清晰，只感觉应该使用线段树，放弃
// 2024/1/25 想了好几天之后补做
internal class P2569更新数组后处理求和查询
{
    public long[] HandleQuery(int[] nums1, int[] nums2, int[][] queries)
    {
        int n = nums1.Length;
        var st = new BitSegmentTree(n);
        for (int i = 0; i < n; ++i)
            if (nums1[i] == 1)
                st.Update(i);
        long sm = 0L;
        foreach (int x in nums2) sm += x;
        List<long> ans = new();
        foreach (var qa in queries)
            switch (qa[0])
            {
                case 1:
                    int l = qa[1], r = qa[2];
                    st.Flip(l, r);
                    break;
                case 2:
                    long q = qa[1];
                    sm += q * st.Count;
                    break;
                case 3:
                    ans.Add(sm);
                    break;
            }
        return ans.ToArray();
    }
    internal static void Run()
    {
        var sln = new P2569更新数组后处理求和查询();

        //int[] n1 = { 1, 0, 1 }, n2 = { 0, 0, 0 };
        //var qa = "[[1,1,1],[2,1,0],[3,0,0]]".ToTestInput<int[][]>();

        // WA
        int[] n1 = { 1, 0, 1 }, n2 = { 44, 28, 35 };
        var qa = "[[1,0,1],[2,10,0],[2,2,0],[2,7,0],[3,0,0],[3,0,0],[1,2,2],[1,1,2],[2,1,0],[1,0,2],[1,2,2],[1,0,2],[3,0,0],[1,1,2],[3,0,0],[1,0,1],[2,21,0],[1,0,1],[2,26,0],[1,1,1]]"
            .ToTestInput<int[][]>();

        Console.WriteLine(string.Join(" ", sln.HandleQuery(n1, n2, qa)));
    }
}
