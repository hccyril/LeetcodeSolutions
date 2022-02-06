using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /**
     * 编写一个程序，通过填充空格来解决数独问题。

        数独的解法需 遵循如下规则：

        数字 1-9 在每一行只能出现一次。
        数字 1-9 在每一列只能出现一次。
        数字 1-9 在每一个以粗实线分隔的 3x3 宫内只能出现一次。（请参考示例图）
        数独部分空格内已填入了数字，空白格用 '.' 表示。

        来源：力扣（LeetCode）
        链接：https://leetcode-cn.com/problems/sudoku-solver
        著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。

     * */
    class P0037解数独
    {
        int[][] rows = InitStatList();
        int[][] cols = InitStatList();
        int[][] boxs = InitStatList();

        static int[][] InitStatList()
        {
            int[][] list = new int[9][];
            for (int i = 0; i < 9; ++i)
            {
                list[i] = new int[9];
                for (int j = 0; j < 9; ++j)
                    list[i][j] = 0;
            }
            return list;
        }

        private static int boxIndex(int i, int j)
        {
            return i / 3 * 3 + j / 3;
        }

        bool init(char[][] grid)
        {
            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    if (grid[i][j] != '.')
                    {
                        int val = grid[i][j] - '1';
                        rows[i][val] = 1;
                        cols[j][val] = 1;
                        boxs[boxIndex(i, j)][val] = 1;
                    }
                }
            }
            return true;
        }

        bool ok = false;

        public void Solve(char[][] grid, int i, int j)
        {
            if (i == 9) { ok = true; return; };
            int next_i = i, next_j = j + 1;
            if (next_j == 9)
            {
                next_i = i + 1;
                next_j = 0;
            }

            if (grid[i][j] == '.')
            {
                for (int val = 0; val < 9; ++val)
                {
                    if (CanFillValue(i, j, val))
                    {
                        grid[i][j] = (char)(val + '1');
                        rows[i][val] = 2;
                        cols[j][val] = 2;
                        boxs[boxIndex(i, j)][val] = 2;
                        if (Valid(grid, i, j))
                        {
                            Solve(grid, next_i, next_j);
                            if (ok) return;
                        }
                        grid[i][j] = '.';
                        rows[i][val] = 0;
                        cols[j][val] = 0;
                        boxs[boxIndex(i, j)][val] = 0;
                    }
                }
            }
            else
            {
                Solve(grid, next_i, next_j);
            }
        }

        bool CanFillValue(int i, int j, int val)
        {
            return rows[i][val] == 0 && cols[j][val] == 0 && boxs[boxIndex(i, j)][val] == 0;
        }

        bool HasFillValue(int i, int j)
        {
            for (int val = 0; val < 9; ++val)
            {
                if (CanFillValue(i, j, val)) return true;
            }
            return false;
        }

        bool Valid(char[][] grid, int i, int j)
        {
            for (int ii = 0; ii < 9; ++ii)
            {
                for (int jj = 0; jj < 9; ++jj)
                {
                    if ((ii > i || jj > j) && grid[ii][jj] == '.')
                        if (ii == i || jj == j || boxIndex(ii, jj) == boxIndex(i, j))
                            if (!HasFillValue(ii, jj))
                                return false;
                }
            }
            return true;
        }

        public void SolveSudoku(char[][] board)
        {
            init(board);
            Solve(board, 0, 0);
        }
    }
}

