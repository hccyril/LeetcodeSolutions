using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0909蛇梯棋
    {
        public int SnakesAndLadders(int[][] board)
        {
            // 2d array -> 1d array
            int N = board.Length;
            int[] ba = new int[N * N];
            int[] arr = new int[N * N];
            int i = 0;
            bool reverse = true;
            for (int row = N - 1; row >= 0; --row)
            {
                reverse = !reverse;
                int start = reverse ? N - 1 : 0;
                int end = reverse ? -1 : N;
                int inc = reverse ? -1 : 1;
                for (int col = start; col != end; col += inc)
                {
                    int val = board[row][col];
                    arr[i] = -1;
                    ba[i++] = val < 0 ? val : val - 1;
                }
            }
            arr[0] = 0;

            // BFS
            Queue<int> qu = new Queue<int>();
            qu.Enqueue(0);
            while (qu.Any())
            {
                i = qu.Dequeue();
                int move = arr[i] + 1;

                for (int step = 1; step <= 6 && i + step < ba.Length; ++step)
                {
                    int de = i + step;
                    if (ba[de] >= 0) de = ba[de];
                    if (arr[de] < 0)
                    {
                        arr[de] = move;
                        qu.Enqueue(de);
                    }
                }
            }

            return arr[arr.Length - 1];
        }
    }
}
