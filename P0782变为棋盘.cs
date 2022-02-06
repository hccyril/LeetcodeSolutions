using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard // TODO 2021/09/26 Daily
    // 不是BFS! 不是BFS! 这题是脑筋急转弯
    class P0782变为棋盘
    {
        // 行和列只能有两种，考虑一行一列就可以了

        // 还未完成
        public int MovesToChessboard(int[][] board)
        {
            throw new NotImplementedException();
        }

        /* 以下是官方题解：
         class Solution {
    public int movesToChessboard(int[][] board) {
        int N = board.length;

        // count[code] = v, where code is an integer
        // that represents the row in binary, and v
        // is the number of occurrences of that row
        Map<Integer, Integer> count = new HashMap();
        for (int[] row: board) {
            int code = 0;
            for (int x: row)
                code = 2 * code + x;
            count.put(code, count.getOrDefault(code, 0) + 1);
        }

        int k1 = analyzeCount(count, N);
        if (k1 == -1) return -1;

        // count[code], as before except with columns
        count = new HashMap();
        for (int c = 0; c < N; ++c) {
            int code = 0;
            for (int r = 0; r < N; ++r)
                code = 2 * code + board[r][c];
            count.put(code, count.getOrDefault(code, 0) + 1);
        }

        int k2 = analyzeCount(count, N);
        return k2 >= 0 ? k1 + k2 : -1;
    }

    public int analyzeCount(Map<Integer, Integer> count, int N) {
        // Return -1 if count is invalid
        // Otherwise, return number of swaps required
        if (count.size() != 2) return -1;

        List<Integer> keys = new ArrayList(count.keySet());
        int k1 = keys.get(0), k2 = keys.get(1);

        // If lines aren't in the right quantity
        if (!(count.get(k1) == N/2 && count.get(k2) == (N+1)/2) &&
                !(count.get(k2) == N/2 && count.get(k1) == (N+1)/2))
            return -1;
        // If lines aren't opposite
        if ((k1 ^ k2) != (1<<N) - 1)
            return -1;

        int Nones = (1 << N) - 1;
        int ones = Integer.bitCount(k1 & Nones); // bitCount统计二进制中1的个数
        int cand = Integer.MAX_VALUE;
        if (N%2 == 0 || ones * 2 < N) // zero start
            cand = Math.min(cand, Integer.bitCount(k1 ^ 0xAAAAAAAA & Nones) / 2);

        if (N%2 == 0 || ones * 2 > N) // ones start
            cand = Math.min(cand, Integer.bitCount(k1 ^ 0x55555555 & Nones) / 2);

        return cand;
    }
}
         * */
    }
}
