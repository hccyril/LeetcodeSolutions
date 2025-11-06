using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// P3445奇偶频次间的最大差值II.cs
// hard, 2025/6/11 Daily, 没看题解自己想出来，但是实现用时较长
// rating 2693
// prefix sum
internal class P3445奇偶频次间的最大差值II
{
    /**
     * 经典最大子数组和问题，维护双指针，最大和=presum_j - min(presum_i)。
     * 根据本题的限制，需要维护presum_i在a和b分别为奇偶个数的最小值，也就是4种情况
     * 同时(j - i + 1)必须至少为k
     * */
    public int MaxDifference(string s, int k)
    {
        int n = s.Length, ans = ~n;
        for (char a = '0'; a <= '4'; ++a)
            for (char b = '0'; b <= '4'; ++b)
                if (a != b)
                {
                    int i = 0, a1b0 = n + 1, a0b1 = n + 1, a0b0 = 0, a1b1 = n + 1, cia = 0, cib = 0, cja = 0, cjb = 0, psi = 0, psj = 0;
                    for (int j = 0; j < n; ++j)
                    {
                        if (s[j] == a)
                        {
                            ++cja;
                            ++psj;
                        }
                        else if (s[j] == b)
                        {
                            ++cjb;
                            --psj;
                        }
                        if (j < k - 1 || cja == 0 || cjb == 0)
                            continue;
                        while (j - i + 1 > k && cja > cia && cjb > cib)
                        {
                            if (s[i] == a && cia + 1 == cja || s[i] == b && cib + 1 == cjb)
                                break;
                            if (s[i] == a)
                            {
                                ++cia;
                                ++psi;
                                ++i;
                            }
                            else if (s[i] == b)
                            {
                                ++cib;
                                --psi;
                                ++i;
                            }
                            else
                            {
                                ++i;
                                continue;
                            }
                            bool ia1 = (cia & 1) != 0, ia0 = !ia1, ib1 = (cib & 1) != 0, ib0 = !ib1;
                            if (ia0 && ib0)
                            {
                                a0b0 = Math.Min(a0b0, psi);
                            }
                            else if (ia1 && ib1)
                            {
                                a1b1 = Math.Min(a1b1, psi);
                            }
                            else if (ia0 && ib1)
                            {
                                a0b1 = Math.Min(a0b1, psi);
                            }
                            else if (ia1 && ib0)
                            {
                                a1b0 = Math.Min(a1b0, psi);
                            }
                        }
                        bool ja1 = (cja & 1) != 0, ja0 = !ja1, jb1 = (cjb & 1) != 0, jb0 = !jb1;
                        if (ja0 && jb0)
                        {
                            ans = Math.Max(ans, psj - a1b0);
                        }
                        else if (ja1 && jb1)
                        {
                            ans = Math.Max(ans, psj - a0b1);
                        }
                        else if (ja0 && jb1)
                        {
                            ans = Math.Max(ans, psj - a1b1);
                        }
                        else if (ja1 && jb0)
                        {
                            ans = Math.Max(ans, psj - a0b0);
                        }
                    }
                }
        return ans;
    }
}
