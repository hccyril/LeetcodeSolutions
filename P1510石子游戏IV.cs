using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P1510石子游戏IV // 动态规划（记忆化回溯）
    {
        int[] dp = new int[100001];
        public bool WinnerSquareGame(int n)
        {
            if (n == 0) return false;
            if (dp[n] != 0) return dp[n] > 0;
            for (int i = 1, sq; (sq = i * i) <= n; ++i)
                if (!WinnerSquareGame(n - sq)) 
                    return (dp[n] = 1) > 0;
            return (dp[n] = -1) > 0;
        }
    }
}
