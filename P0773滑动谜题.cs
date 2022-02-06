using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
     * 773. 滑动谜题
     * https://leetcode-cn.com/problems/sliding-puzzle/
在一个 2 x 3 的板上（board）有 5 块砖瓦，用数字 1~5 来表示, 以及一块空缺用 0 来表示.

一次移动定义为选择 0 与一个相邻的数字（上下左右）进行交换.

最终当板 board 的结果是 [[1,2,3],[4,5,0]] 谜板被解开。

给出一个谜板的初始状态，返回最少可以通过多少次移动解开谜板，如果不能解开谜板，则返回 -1 。

示例：

输入：board = [[1,2,3],[4,0,5]]
输出：1
解释：交换 0 和 5 ，1 步完成
输入：board = [[1,2,3],[5,4,0]]
输出：-1
解释：没有办法完成谜板
输入：board = [[4,1,2],[5,0,3]]
输出：5
解释：
最少完成谜板的最少移动次数是 5 ，
一种移动路径:
尚未移动: [[4,1,2],[5,0,3]]
移动 1 次: [[4,1,2],[0,5,3]]
移动 2 次: [[0,1,2],[4,5,3]]
移动 3 次: [[1,0,2],[4,5,3]]
移动 4 次: [[1,2,0],[4,5,3]]
移动 5 次: [[1,2,3],[4,5,0]]
输入：board = [[3,2,4],[1,5,0]]
输出：14
提示：

board 是一个如上所述的 2 x 3 的数组.
board[i][j] 是一个 [0, 1, 2, 3, 4, 5] 的排列.
     * */
    class P0773滑动谜题
    {
        private int Calc(int n, int multi, ref int map)
        {
            int index = 0;
            for (int i = 0; i < 6; ++i)
            {
                int bit = 1 << i;
                if ((bit & map) == 0)
                {
                    if (i == n)
                    {
                        map |= bit;
                        return index * multi;
                    }
                    ++index;
                }
            }
            return 0;
        }
        
        // 只有6个数，排列组合总共720种情况，但是具体排列对应的索引是多少要算出来
        private int GetIndex(int[][] board)
        {
            int map = 0, index = 0;
            index += Calc(board[0][0], 120, ref map);
            index += Calc(board[0][1], 24, ref map);
            index += Calc(board[0][2], 6, ref map);
            index += Calc(board[1][0], 2, ref map);
            index += Calc(board[1][1], 1, ref map);
            return index;
        }

        private int UnCalc(int multi, ref int index, ref int map)
        {
            int ind = 0, n = index / multi; index %= multi;
            for (int i = 0; i < 6; ++i)
            {
                int bit = 1 << i;
                if ((bit & map) == 0)
                {
                    if (ind == n)
                    {
                        map |= bit;
                        return i;
                    }
                    ++ind;
                }
            }
            return 0;
        }

        private void FillBoard(int[][] board, int index)
        {
            int map = 0;
            board[0][0] = UnCalc(120, ref index, ref map);
            board[0][1] = UnCalc(24, ref index, ref map);
            board[0][2] = UnCalc(6, ref index, ref map);
            board[1][0] = UnCalc(2, ref index, ref map);
            board[1][1] = UnCalc(1, ref index, ref map);
            board[1][2] = UnCalc(720, ref index, ref map);
        }

        private bool MoveBoard(int[][] board, int[] di, int[][] nextBoard)
        {
            int i0 = 0, j0 = 0;
            for (int i = 0; i < 2; ++i)
                for (int j = 0; j < 3; ++j)
                {
                    nextBoard[i][j] = board[i][j];
                    if (board[i][j] == 0)
                    {
                        i0 = i; j0 = j;
                    }
                }

            int i1 = i0 + di[0], j1 = j0 + di[1];
            if (i1 >= 0 && i1 < 2 && j1 >= 0 && j1 < 3)
            {
                nextBoard[i0][j0] = board[i1][j1];
                nextBoard[i1][j1] = 0;
                return true;
            }

            return false;
        }

        public int SlidingPuzzle(int[][] board)
        {
            const int END_INDEX = 153; // [123][450]对应的索引
            int[] moves = new int[720];
            int[][] nextBoard = new int[][] { new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 } };
            int[][] directions = new int[][] { new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 } };

            int index = GetIndex(board); if (index == END_INDEX) return 0;

            Queue<int> qu = new Queue<int>();
            qu.Enqueue(index);

            while (qu.Any())
            {
                index = qu.Dequeue();
                int nextMove = moves[index] + 1;
                FillBoard(board, index);

                foreach (var di in directions)
                {
                    if (MoveBoard(board, di, nextBoard))
                    {
                        index = GetIndex(nextBoard);
                        if (index == END_INDEX) return nextMove;
                        if (moves[index] == 0)
                        {
                            moves[index] = nextMove;
                            qu.Enqueue(index);
                        }
                    }
                }
            }

            return -1;
        }

        

        public static void Run()
        {
            var o = new P0773滑动谜题();
            Console.WriteLine(o.SlidingPuzzle(
                new int[2][] { new int[] { 1, 2, 3 }, new int[] { 4, 5, 0 } }));
            Console.WriteLine(o.SlidingPuzzle(
                new int[2][] { new int[] { 1, 2, 3 }, new int[] { 4, 0, 5 } }));
            Console.WriteLine(o.SlidingPuzzle(
                new int[2][] { new int[] { 1, 2, 3 }, new int[] { 5, 4, 0 } }));
            Console.WriteLine(o.SlidingPuzzle(
                new int[2][] { new int[] { 4, 1, 2 }, new int[] { 5, 0, 3 } }));
            Console.WriteLine(o.SlidingPuzzle(
                new int[2][] { new int[] { 3, 2, 4 }, new int[] { 1, 5, 0 } }));
        }
    }
}
