using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 131双周赛-D
// 线段树，比赛时没做出来
internal class P3161物块放置查询
{
    // ref: https://leetcode.cn/problems/block-placement-queries/solutions/2790221/xian-duan-shu-by-mipha-2022-k6db/
    // 线段树应该维护左边界，右边界，以及最大区间，避免反复运算

    (int lb, int rb, int ma)[] a;

    void Update(int x, int i, int l, int r)
    {
        if (l == r)
        {
            a[i].rb = a[i].lb = x;
            a[i].ma = 0;
            return;
        }
        else if (l > r) return;

        int m = l + r >> 1, il = i << 1, ir = i << 1 | 1;
        if (x <= m) Update(x, il, l, m);
        else Update(x, ir | 1, m + 1, r);

        a[i].lb = a[il].lb > 0 ? a[il].lb : a[ir].lb;
        a[i].rb = a[ir].rb > 0 ? a[ir].rb : a[il].rb;
        // #坑01：好无语啊，查了这么久，最后发现是把>=写成了>，怎么就会没考虑到ma==0的情况的呢。。
        int lmax = a[il].ma >= 0 ? a[il].ma : m - l + 1, 
            rmax = a[ir].ma >= 0 ? a[ir].ma : r - m,
            lb = a[il].rb > 0 ? a[il].rb : l - 1,
            rb = a[ir].lb > 0 ? a[ir].lb : r + 1;
        a[i].ma = Math.Max(Math.Max(lmax, rmax), rb - lb - 1);
        //if (i == 2 && a[i].ma == 1)
        //    Debug.Fail("");
    }

    // 查询 [sl, sr] 当中是否有至少长度为sz的连续非障碍物
    bool Query(int sl, int sr, int sz, int i, int l, int r)
    {
        if (r - l + 1 < sz || sr - sl + 1 < sz) return false;
        else if (a[i].lb < 0) return true;
        else if (sl <= l && sr >= r)
            return a[i].ma < 0 ? (r - l + 1 >= sz) : a[i].ma >= sz;
            //if (a[i].ma < 0 ? (r - l + 1 >= sz) : a[i].ma >= sz)
            //    return true; // 断点用
            //else return false;

        int m = l + r >> 1;
        bool b = false;
        if (sl <= m) b = Query(sl, Math.Min(sr, m), sz, i << 1, l, m);
        if (sr > m) b = b || Query(Math.Max(m + 1, sl), sr, sz, i << 1 | 1, m + 1, r);
        if (b) return b;
        if (sl <= m && sr > m)
        {
            // 坑02：忘了考虑lo或hi为-1的情况
            int lo = a[i << 1].rb; if (lo < 0) lo = l - 1;
            int hi = a[i << 1 | 1].lb; if (hi < 0) hi = r + 1;
            return Math.Min(hi, sr + 1) - Math.Max(lo, sl - 1) - 1 >= sz;
            //if (Math.Min(hi, sr + 1) - Math.Max(lo, sl - 1) - 1 >= sz)
            //    return true; // 断点用
            //else
            //    return false;
        }
        return false;
    }
    public IList<bool> GetResults(int[][] queries)
    {
        int n = Math.Min(50000, 3 * queries.Length);
        int size = 1;
        while (size < n) size <<= 1;
        a = new (int lb, int rb, int ma)[size << 1];
        Array.Fill(a, (-1, -1, -1));
        List<bool> ans = new();

        foreach (var v in queries)
        {
            if (v[0] == 1)
            {
                Update(v[1], 1, 1, n);
            }
            else
            {
                if (v[2] == 1)
                    ans.Add(true);
                else
                    ans.Add(Query(1, v[1] - 1, v[2] - 1, 1, 1, n));
            }
        }
        return ans;
    }


    void RunTest1()
    {
        int n = 5, size = 16;
        a = new (int lb, int rb, int ma)[size];
        Array.Fill(a, (-1, -1, -1));
        for (int i = 1; i <= n; ++i)
            Update(i, 1, 1, n);
        Console.WriteLine(string.Join(" ", a.Select(t => t.ma)));
    }

    void RunTest2()
    {
        var queries = Common.ReadInput<int[][]>(3161);
        HashSet<int> expectAns = new(new int[] { 0, 1, 8, 9, 17, 20, 21, 26, 28, 29, 38, 51, 53, 54, 79, 91, 97, 164, 224, 268, 272, 274, 437, 450, 588, 660, 722, 781, 998, 1200, 1255, 1259, 1284, 2750, 3042, 3770, 4091, 4426, 4600, 4938, 5561, 6079, 6575, 7042, 7467, 8112, 8182, 8918, 10784, 10917, 12935, 16560, 18970, 24791, 28349, 31752, 33803, 33947, 38279, 44876, 48645, 55912, 56562 });
        int n = Math.Min(50000, 3 * queries.Length);
        int size = 1;
        while (size < n) size <<= 1;
        a = new (int lb, int rb, int ma)[size << 1];
        Array.Fill(a, (-1, -1, -1));
        List<bool> ans = new();

        List<int> ax = new();
        foreach (var v in queries)
        {
            if (v[0] == 1)
            {
                Update(v[1], 1, 1, n);
                ax.Add(v[1]);
            }
            else
            {
                if (v[2] == 1)
                    ans.Add(true);
                else
                    ans.Add(Query(1, v[1] - 1, v[2] - 1, 1, 1, n));

                if (ans[^1] != expectAns.Contains(ans.Count - 1))
                {
                    ax.Sort();
                    Console.WriteLine(string.Join(" ", ax));
                    Debug.Fail("Wrong");
                }
            }
        }
    }

    internal static void Run()
    {
        var sln = new P3161物块放置查询();
        //sln.RunTest();
        //int[] a = { 1, 7 }, b = { 2, 7, 6 };
        //int[][] c = { a, b };
        //int[][] c = "[[1,7],[2,7,6],[1,2],[2,7,5],[2,7,6]]".ToTestInput<int[][]>();
        //int[][] c = "[[1,3],[2,6,4]]".ToTestInput<int[][]>();
        int[][] c = "[[1,1],[2,7,6],[2,1,6]]".ToTestInput<int[][]>();

        var ans = sln.GetResults(c);
        for (int i = 0; i < ans.Count; ++i)
            Console.WriteLine(ans[i]);
    }
}

// version1 - TLE / WA
//internal class BiC131D
//{
//    int[] a = new int[200005];

//    void Update(int x, int i = 1, int l = 0, int r = 50000)
//    {
//        ++a[i];
//        if (l == r) return;
//        int m = l + r >> 1;
//        if (x <= m) Update(x, i << 1, l, m);
//        else Update(x, i << 1 | 1, m + 1, r);
//    }

//    // 区间最左边的障碍物
//    int Left(int i, int l, int r)
//    {
//        if (a[i] == 0) return r + 1;
//        else if (a[i] == r - l + 1) return l;
//        int m = l + r >> 1;
//        if (a[i << 1] > 0) return Left(i << 1, l, m);
//        else return Left(i << 1 | 1, m + 1, r);
//    }

//    // 区间最右边的障碍物
//    int Right(int i, int l, int r)
//    {
//        if (a[i] == 0) return l - 1;
//        else if (a[i] == r - l + 1) return r;
//        int m = l + r >> 1;
//        if (a[i << 1 | 1] > 0) return Right(i << 1 | 1, m + 1, r);
//        else return Right(i << 1, l, m);
//    }

//    bool Query(int sl, int sr, int sz, int i = 1, int l = 0, int r = 50000)
//    {
//        if (r - l + 1 < sz || sr - sl + 1 < sz) return false;
//        else if (a[i] == 0) return true;
//        int m = l + r >> 1;
//        bool b = false;
//        if (sl <= m) b = Query(sl, Math.Min(sr, m), sz, i << 1, l, m);
//        if (sr > m) b = b || Query(Math.Max(m + 1, sl), sr, sz, i << 1 | 1, m + 1, r);
//        if (b) return b;
//        if (sl <= m && sr > m)
//        {
//            int lo = Right(i << 1, l, m);
//            int hi = Left(i << 1 | 1, m + 1, r);
//            return Math.Min(hi, sr + 1) - Math.Max(lo, sl - 1) - 1 >= sz;
//        }
//        return false;
//    }
//    public IList<bool> GetResults(int[][] queries)
//    {
//        List<bool> ans = new();
//        foreach (var v in queries)
//        {
//            if (v[0] == 1)
//            {
//                Update(v[1]);
//            }
//            else
//            {
//                if (v[2] == 1)
//                    ans.Add(true);
//                else
//                    ans.Add(Query(1, v[1] - 1, v[2] - 1));
//            }
//        }
//        return ans;
//    }

//    internal static int Run()
//    {
//        var sln = new BiC131D();
//        //int[] a = { 1, 7 }, b = { 2, 7, 6 };
//        //int[][] c = { a, b };
//        //int[][] c = "[[1,7],[2,7,6],[1,2],[2,7,5],[2,7,6]]".ToTestInput<int[][]>();
//        int[][] c = "[[1,3],[2,6,4]]".ToTestInput<int[][]>();
//        var ans = sln.GetResults(c);
//        for (int i = 0; i < ans.Count; ++i)
//            Console.WriteLine(ans[i]);
//        return 0;
//    }
//}