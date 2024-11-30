using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// Hard, 2024/7/21 US Daily
// 拓扑排序
internal class P2392给定条件下构造矩阵
{
    public int[][] BuildMatrix(int k, int[][] rowConditions, int[][] colConditions)
    {
        var ra = rowConditions.TopoSort(1, k);
        if (!ra.Any()) return Array.Empty<int[]>();
        var ca = colConditions.TopoSort(1, k);
        if (!ca.Any()) return Array.Empty<int[]>();
        int[,] px = new int[k + 1, 2];
        for (int i = 0; i  < k; ++i)
        {
            int x = ra[i], y = ca[i];
            px[x, 0] = i;
            px[y, 1] = i;
        }
        var a = Enumerable.Range(0, k).Select(_ => new int[k]).ToArray();
        for (int i = 1; i <= k; ++i)
            a[px[i, 0]][px[i, 1]] = i;
        return a;
    }

    internal static void Run()
    {
        var sln = new P2392给定条件下构造矩阵();

        // sample
        //int k = 3;
        //var rc = "[[1,2],[3,2]]".ToTestInput<int[][]>();
        //var cc = "[[2,1],[3,2]]".ToTestInput<int[][]>();

        // test2
        int k = 3;
        var rc = "[[1,2],[2,3],[3,1]]".ToTestInput<int[][]>();
        var cc = "[[2,1]]".ToTestInput<int[][]>();

        Console.WriteLine("P2392给定条件下构造矩阵 Ans={0}", sln.BuildMatrix(k, rc, cc));
    }
}
