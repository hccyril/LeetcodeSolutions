using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard // 2021/09/26 Daily
    // 不是BFS! 不是BFS! 这题是脑筋急转弯
    // 2022/7/16
    class P0782变为棋盘
    {
        // 行和列只能有两种，考虑一行一列就可以了
        public int MovesToChessboard(int[][] board)
        {
            bool Check(string s1, string s2)
            {
                int c0 = 0, c1 = 0;
                for (int i = 0; i < s1.Length; ++i)
                {
                    if (s1[i] == s2[i]) return false;
                    if (s1[i] == '0') ++c0; else ++c1;
                }
                return Math.Abs(c0 - c1) <= 1;
            }

            int MinCount(string s)
            {
                char a = '1', b = '0';
                int ca = 0, cb = 0;
                foreach (var c in s)
                {
                    a = a == '1' ? '0' : '1';
                    b = b == '1' ? '0' : '1';
                    if (c != a) ++ca;
                    if (c != b) ++cb;
                }
                return ca % 2 == 0 && cb % 2 == 0 ? Math.Min(ca, cb) / 2 : ca % 2 == 0 ? ca / 2 : cb / 2;
            }

            var gr = board.Select(x => string.Join("", x)).GroupBy(s => s);
            if (gr.Count() != 2) return -1;
            if (!Check(gr.First().Key, gr.Last().Key)) return -1;

            var gc = Enumerable.Range(0, board[0].Length)
                .Select(j => string.Join("", Enumerable.Range(0, board.Length).Select(i => board[i][j])))
                .GroupBy(s => s);
            if (gc.Count() != 2) return -1;
            if (!Check(gc.First().Key, gc.Last().Key)) return -1;

            return MinCount(gr.First().Key) + MinCount(gc.First().Key);
        }

    }
}
