using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0036有效的数独
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

        //private static List<char[]> InitStatList()
        //{
        //    List<char[]> list = new List<char[]>();
        //    for (int i = 0; i < 9; ++i)
        //    {
        //        char[] carr = new char[9];
        //        for (int j = 0; j < 9; ++j)
        //            carr[j] = '.';
        //        list.Add(carr);
        //    }
        //    return list;
        //}

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
                        if (rows[i][val] > 0) return false;
                        rows[i][val] = 1;
                        if (cols[j][val] > 0) return false;
                        cols[j][val] = 1;
                        if (boxs[boxIndex(i, j)][val] > 0) return false;
                        boxs[boxIndex(i, j)][val] = 1;
                    }
                }
            }
            return true;
        }

        public bool IsValidSudoku(char[][] board)
        {
            return init(board);
        }
    }
}

