using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0542_01矩阵
    {
        int M, N;
        bool CanMove(int i, int j, int d, out int i1, out int j1)
        {
            i1 = i; j1 = j;
            switch (d)
            {
                case 0:
                    i1--;
                    return i1 >= 0;
                case 1:
                    i1++;
                    return i1 < M;
                case 2:
                    j1--;
                    return j1 >= 0;
                case 3:
                    j1++;
                    return j1 < N;
            }
            return false;
        }

        public int[][] UpdateMatrix(int[][] mat)
        {
            M = mat.Length; if (M > 0) N = mat[0].Length;
            int count = 0;
            Queue<int> qu = new Queue<int>();

            for (int i = 0; i < mat.Length; ++i)
                for (int j = 0; j < mat[i].Length; ++j)
                    if (mat[i][j] == 0)
                    {
                        qu.Enqueue(i);
                        qu.Enqueue(j);
                    }
                    else count++;
            // BFS
            while (qu.Any() && count > 0)
            {
                int i = qu.Dequeue();
                int j = qu.Dequeue();
                int i1, j1;
                for (int d = 0; count > 0 && d < 4; ++d)
                    if (CanMove(i, j, d, out i1, out j1) && mat[i1][j1] == 1)
                    {
                        mat[i1][j1] = mat[i][j] - 1;
                        qu.Enqueue(i1);
                        qu.Enqueue(j1);
                        count--;
                    }
            }

            for (int i = 0; i < mat.Length; ++i)
                for (int j = 0; j < mat[i].Length; ++j)
                    if (mat[i][j] < 0)
                        mat[i][j] = -mat[i][j];
            return mat;
        }
    }
}
