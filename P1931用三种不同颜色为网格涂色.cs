using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2025/5/18 Daily
// rating 2170
// 轮廓线DP
class P1931用三种不同颜色为网格涂色
{
    public int ColorTheGrid(int m, int n)
    {
        int size = 1;
        for (int i = 0; i < m; ++i) size *= 3;
        int[] dp = new int[size], dp1 = new int[size], a = new int[m], b = new int[m];

        void Decode(int x, int[] cs)
        {
            for (int i = 0; i < m; ++i)
            {
                cs[i] = x % 3;
                x /= 3;
            }
        }

        int Encode(int[] cs)
        {
            int x = 0;
            for (int i = m - 1; i >= 0; --i)
            {
                x *= 3;
                x += cs[i];
            }
            return x;
        }

        for (int i = 0; i < n; ++i)
            for (int j = 0; j < m; ++j)
            {
                if (i == 0 && j == 0)
                {
                    dp[0] = dp[1] = dp[2] = 1;
                    continue;
                }

                Array.Fill(dp1, 0);
                for (int s = 0; s < size; ++s)
                    if (dp[s] != 0)
                    {
                        Decode(s, a);
                        for (int k = m - 1; k > 0; --k)
                            b[k] = a[k - 1];
                        for (int c = 0; c < 3; ++c)
                            if ((j == 0 || c != a[0]) && (i == 0 || c != a[^1]))
                            {
                                b[0] = c;
                                int t = Encode(b);
                                dp1[t] = dp1[t].Add(dp[s]); 
                            }
                    }

                (dp, dp1) = (dp1, dp);
            }

        int ans = 0;
        foreach (int x in dp)
            ans = ans.Add(x);
        return ans;
    }
}
