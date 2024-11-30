using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2024/10/2
// rating 2470
// max-fenwick tree
internal class P2286以组为单位订音乐会的门票
{
    internal static void Run()
    {
        // ["BookMyShow","scatter","gather","gather","gather"]
        // [[3,999999999],[1000000000,2],[999999999,2],[999999999,2],[999999999,2]]
        var sln = new BookMyShow(3, 999999999);
        bool b = sln.Scatter(1000000000, 2);
        Console.WriteLine("b=" + b);

        int[] ans = sln.Gather(999999999, 2);
        Console.WriteLine("{0} {1}", ans[0], ans[1]);
        ans = sln.Gather(999999999, 2);
        Console.WriteLine("{0} {1}", ans[0], ans[1]);
        ans = sln.Gather(999999999, 2);
        Console.WriteLine("{0} {1}", ans[0], ans[1]);
    }
}

public class BookMyShow
{
    readonly int n, m;
    readonly MaxFenwick<int> mft;
    readonly FenwickLong ft;
    int low = 0; // the lowest row that has a seat

    public BookMyShow(int n, int m)
    {
        this.n = n; this.m = m;
        mft = new(n, m);
        ft = new(n);
    }

    public int[] Gather(int k, int maxRow)
    {
        if (mft.Max(maxRow) < k) 
            return Array.Empty<int>();
        int l = 0, r = maxRow;
        while (l < r)
        {
            int mid = l + r >> 1;
            if (mft.Max(mid) < k)
                l = mid + 1;
            else
                r = mid;
        }
        int seats = mft.Get(l);
        mft.Down(l, seats - k);
        ft.Update(l, k);
        while (low < n && mft.Get(low) == 0) ++low;
        return new int[] { l, m - seats };
    }

    public bool Scatter(int k, int maxRow)
    {
        long used = ft.Sum(maxRow), total = (long)m * (maxRow + 1);
        if (total - used < k) return false;

        while (k > 0)
        {
            int seats = mft.Get(low);
            if (seats >= k)
            {
                seats -= k;
                mft.Down(low, seats);
                ft.Update(low, k);
                while (low < n && mft.Get(low) == 0)
                    ++low;
                break;
            }
            else
            {
                k -= seats;
                mft.Down(low, 0);
                ft.Update(low, seats);
                ++low;
            }
        }

        return true;
    }
}
