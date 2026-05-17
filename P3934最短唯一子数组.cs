namespace ConsoleCore1;

// 20260517 WC502-D
// rabin-karp 哈希算法
internal class P3934最短唯一子数组
{
    public int SmallestUniqueSubarray(int[] nums)
    {
        int n = nums.Length;
        bool Check(int k)
        {
            if (k == 0) return false;
            else if (k == n) return true;
            const int M1 = 1000000007, M2 = 998244353;
            Counter<long> cn1 = new(), cn2 = new();
            long d1 = 0, d2 = 0, h1 = 0, h2 = 0;
            long pk1 = 2.PowerMod(k << 3, M1), pk2 = 2.PowerMod(k << 3, M2);
            for (int i = 0; i < n; ++i)
            {
                long x = nums[i];
                h1 = ((h1 << 8) + x) % M1;
                h2 = ((h2 << 8) + x) % M2;
                if (i >= k)
                {
                    long y = nums[i - k];
                    h1 = (h1 - y * pk1) % M1; if (h1 < 0) h1 += M1;
                    h2 = (h2 - y * pk2) % M2; if (h2 < 0) h2 += M2;
                }
                if (i >= k - 1)
                {
                    if (++cn1[h1] == 1)
                        ++d1;
                    else if (cn1[h1] == 2)
                        --d1;
                    if (++cn2[h2] == 1)
                        ++d2;
                    else if (cn2[h2] == 2)
                        --d2;
                }
            }
            return d1 > 0 || d2 > 0;
        }
        int l = 0, r = n;
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
}
