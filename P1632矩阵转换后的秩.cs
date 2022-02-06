using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P1632矩阵转换后的秩
    {
        int m, n;
        int[][] ans;
        int[] maxRowVals, maxRowRanks,maxColVals, maxColRanks;

        private void UpdateCell(int cv, int cellRank, int i, int j, HashSet<int> hs)
        {
            if (hs == null) hs = new HashSet<int>();
            int key = (i << 10) | j;
            if (hs.Contains(key)) return;
            hs.Add(key);

            if (maxRowVals[i] == cv && maxRowRanks[i] < cellRank)
            {
                int orgRank = maxRowRanks[i];
                for (int col = 0; col < n; ++col)
                    if (ans[i][col] == orgRank)
                    {
                        UpdateCell(cv, cellRank, i, col, hs);
                    }
            }
            if (maxColVals[j] == cv && maxColRanks[j] < cellRank)
            {
                int orgRank = maxColRanks[j];
                for (int row = 0; row < m; ++row)
                    if (ans[row][j] == orgRank)
                    {
                        UpdateCell(cv, cellRank, row, j, hs);
                    }
            }

            ans[i][j] = maxRowRanks[i] = maxColRanks[j] = cellRank;
            maxRowVals[i] = maxColVals[j] = cv;
        }
        public int[][] MatrixRankTransform(int[][] matrix)
        {
            m = matrix.Length; n = matrix[0].Length;
            ans = new int[m][];
            for (int i = 0; i < m; ++i) ans[i] = new int[n];

            SortedList<int, List<int[]>> sortedCells = new SortedList<int, List<int[]>>();
            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                {
                    int c = matrix[i][j];
                    if (!sortedCells.ContainsKey(c)) sortedCells.Add(c, new List<int[]>());
                    sortedCells[c].Add(new int[] { i, j });
                }

            maxRowVals = new int[m]; maxRowRanks = new int[m];
            maxColVals = new int[n]; maxColRanks = new int[n];

            foreach (var kv in sortedCells)
                foreach (var p in kv.Value)
                {
                    int i = p[0], j = p[1], cv = kv.Key;
                    if (maxRowRanks[i] == 0) maxRowVals[i] = -1999999999;
                    if (maxColRanks[j] == 0) maxColVals[j] = -1999999999;
                    int maxVal = Math.Max(maxRowVals[i], maxColVals[j]);
                    int cellRank;
                    if (cv > maxVal)
                    {
                        cellRank = Math.Max(maxRowRanks[i] + 1, maxColRanks[j] + 1);
                    }
                    else
                    {
                        if (maxRowVals[i] == cv && maxColVals[j] == cv)
                            cellRank = Math.Max(maxRowRanks[i], maxColRanks[j]);
                        else if (maxRowVals[i] == cv)
                            cellRank = Math.Max(maxRowRanks[i], maxColRanks[j] + 1);
                        else // rowVal < cv, celVal == cv
                            cellRank = Math.Max(maxRowRanks[i] + 1, maxColRanks[j]);
                    }

                    UpdateCell(cv, cellRank, i, j, null);
                }
            return ans;
        }

        internal static void Run()
        {
            // test case 1
            //int[][] input = new int[][] { new int[] { 7, 7 }, new int[] { 7, 7 } };
            // test case 2
            //var input = new int[][] { 
            //    new int[] { -23, 20, -49, -30, -39, -28,  -5, -14 }, 
            //    new int[] { -19,  4, -33,   2, -47,  28,  43,  -6 }, 
            //    new int[] { -47, 36, -49,   6,  17,  -8, -21, -30 }, 
            //    new int[] { -27, 44,  27,  10,  21,  -8,   3,  14 }, 
            //    new int[] { -19, 12, -25,  34, -27, -48, -37,  14 }, 
            //    new int[] { -47, 40,  23,  46, -39,  48, -41,  18 }, 
            //    new int[] { -27, -4,   7, -10,   9,  36,  43,   2 }, 
            //    new int[] {  37, 44,  43, -38,  29, -44,  19,  38 } };
            // test case 3: WA again!
            var input = new int[][] { new int[] { 25, 8, 31, 42, -39, 8, 31, -10, 33, -44, 7, -30, 9, 44, 15, 26 }, new int[] { -3, -48, -17, -18, 9, -12, -21, 10, 1, 44, -17, 14, -27, 48, -21, -6 }, new int[] { 49, 28, 27, -18, -31, 4, -13, 34, 49, 48, 47, -18, 33, 40, 15, 38 }, new int[] { 5, -28, -49, -38, 1, 32, -25, -50, 29, -32, 35, -46, -43, 48, -49, -6 }, new int[] { -27, -24, 23, -14, -47, -12, 7, 6, 25, -16, 47, -26, 13, -12, -33, -18 }, new int[] { 45, -48, 3, -26, -23, -36, -17, 38, 17, 12, 15, 46, 37, 40, 47, 26 }, new int[] { -19, -24, -21, -2, -7, -48, 47, 30, 5, -8, 23, -46, 21, -32, -33, -26 }, new int[] { -27, 32, 27, -26, 21, -32, -49, -10, 5, 20, -29, 46, -43, -44, 39, 22 }, new int[] { -43, 48, 27, 26, -27, 12, -1, -10, -27, 12, -29, -34, 41, -28, -25, -30 }, new int[] { 25, -36, 35, -26, 37, -20, 31, 14, -19, -40, -29, -2, -39, -28, 11, 46 }, new int[] { 49, -32, -29, -6, -47, 32, -17, -18, -23, 24, 23, 22, -47, -44, 27, 14 }, new int[] { 37, -44, -33, -18, -47, 24, -17, -46, -43, -32, 15, -46, -27, -8, -25, 46 }, new int[] { 41, -40, 31, -30, 13, -24, -29, 22, -15, -16, 47, 2, -39, 4, -25, -42 }, new int[] { -3, 12, 7, 14, -7, 8, -37, -34, -7, -12, 39, -38, 1, 44, 27, -34 }, new int[] { -47, 4, 7, -2, -43, -32, 27, 2, -43, -8, -33, 14, 49, -48, -5, 30 }, new int[] { -15, 8, -33, -26, -23, -32, -25, 22, 13, -20, -9, 26, 29, 4, -1, 2 } };
            var output = new P1632矩阵转换后的秩().MatrixRankTransform(input);
            foreach (var row in output)
                Console.WriteLine(string.Join(' ', row));
        }
    }
}
