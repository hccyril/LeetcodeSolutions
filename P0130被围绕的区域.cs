using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // 这题写的心累，其实后面看评论（而且自己也想到了），发现先回溯边界的O，然后再把剩下的O全部变成X就可以了
    class P0130被围绕的区域
    {
        char[][] board;
        int m, n;
        char Dfs(int i, int j, char c)
        {
            if (board[i][j] == 'X')
            {
                return c = 'X';
            }
            if (board[i][j] == 'O')
            {
                if (i == 0 || i == m - 1 || j == 0 || j == n - 1 || c == 'L')
                    board[i][j] = c = 'L';
                else
                    board[i][j] = 'N';
            }
            else if (board[i][j] == 'N' && c == 'L')
            {
                board[i][j] = 'L';
            }
            else return c;

            char c1 = 'L'; if (i > 0) c1 = Dfs(i - 1, j, c);
            char c2 = 'L'; if (i < m - 1) c2 = Dfs(i + 1, j, c);
            char c3 = 'L'; if (j > 0) c3 = Dfs(i, j - 1, c);
            char c4 = 'L'; if (j < n - 1) c4 = Dfs(i, j + 1, c);

            if (c1 == 'X' && c2 == 'X' && c3 == 'X' && c4 == 'X' && board[i][j] == 'N') 
                board[i][j] = 'X';
            return c;
        }
        public void Solve(char[][] board)
        {
            this.board = board;
            m = board.Length; n = board[0].Length;
            for (int i = 0; i < board.Length; ++i)
                for (int j = 0; j < board[i].Length; ++j)
                    Dfs(i, j, '0');
            for (int i = 0; i < board.Length; ++i)
                for (int j = 0; j < board[i].Length; ++j)
                    if (board[i][j] == 'L')
                        board[i][j] = 'O';
                    else if (board[i][j] == 'N')
                        board[i][j] = 'X';
        }

        internal static void Run()
        {
            var input = new char[][] { new char[] { 'X', 'X', 'X', 'X', 'O', 'O', 'X', 'X', 'O' }, new char[] { 'O', 'O', 'O', 'O', 'X', 'X', 'O', 'O', 'X' }, new char[] { 'X', 'O', 'X', 'O', 'O', 'X', 'X', 'O', 'X' }, new char[] { 'O', 'O', 'X', 'X', 'X', 'O', 'O', 'O', 'O' }, new char[] { 'X', 'O', 'O', 'X', 'X', 'X', 'X', 'X', 'O' }, new char[] { 'O', 'O', 'X', 'O', 'X', 'O', 'X', 'O', 'X' }, new char[] { 'O', 'O', 'O', 'X', 'X', 'O', 'X', 'O', 'X' }, new char[] { 'O', 'O', 'O', 'X', 'O', 'O', 'O', 'X', 'O' }, new char[] { 'O', 'X', 'O', 'O', 'O', 'X', 'O', 'X', 'O' } };
            new P0130被围绕的区域().Solve(input);
            foreach (var arr in input)
            {
                Console.WriteLine(string.Join(' ', arr));
            }
        }
    }
}
