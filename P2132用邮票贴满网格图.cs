using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 双周赛题
    // 历时一整天，真正比赛的话肯定挂了= =
    /*
        01/18/2022 22:53	Accepted	744 ms	64.8 MB	csharp
        01/18/2022 22:49	Wrong Answer	N/A	N/A	csharp
        01/18/2022 22:45	Wrong Answer	N/A	N/A	csharp
        01/18/2022 13:59	Wrong Answer	N/A	N/A	csharp
        01/18/2022 13:26	Wrong Answer	N/A	N/A	csharp
    */
    internal class P2132用邮票贴满网格图
    {
        /* 官方提示是这个，但是他自带反例。。。
         * Tips1:
         * We can check if every empty cell is a part of a consecutive row of 
         * empty cells that has a width of at least stampWidth as well as a 
         * consecutive column of empty cells that has a height of at least stampHeight.
         * Tips2:
         * We can prove that this condition is sufficient and necessary to fit the stamps 
         * while following the given restrictions and requirements.
         * Tips3:
         * For each row, find every consecutive row of empty cells, and mark all the cells 
         * where the consecutive row is at least stampWidth wide. Do the same for the 
         * columns with stampHeight. Then, you can check if every cell is marked twice.
         * */
        /* 以上说法是错的，反例如下：
       [[0,0,0,0,0],
        [0,0,0,0,0],
        [0,0,1,0,0],
        [0,0,0,0,1],
        [0,0,0,1,1]] 2 2
        */

        // 重新写（ver3）
        int[][] grid;
        (int h, int w)[,] arr;
        bool Check(int i, int j, int height, int width)
        {
            if (i < 0 || j < 0 || i >= grid.Length || j >= grid[i].Length || grid[i][j] == 1) return false;
            (int h, int w) = arr[i, j];
            if (h >= height && w >= width) return true;
            if (height > 1 && !Check(i + 1, j, height - 1, width) ||
                width > 1 && !Check(i, j + 1, height, width - 1))
            {
                arr[i, j] = (-1, -1);
                return false;
            }
            arr[i, j] = (height, width); 
            return true;
        }
        public bool PossibleToStamp(int[][] grid, int stampHeight, int stampWidth)
        {
            if (stampHeight == 1 && stampWidth == 1) return true; // 没加这句导致WA: [[0],[0],[0],[0],[0],[0],[0],[0],[0]] 1 1
            this.grid = grid;
            arr = new (int h, int w)[grid.Length, grid[0].Length];
            for (int i = grid.Length - 1; i >= 0; --i)
            {
                for (int j = grid[i].Length - 1; j >= 0; --j)
                {
                    if (grid[i][j] == 0)
                    {
                        if (arr[i, j].h > 0)
                        {   // case1: pass, pre-check left-top corner
                            Check(i - stampHeight + 1, j - stampWidth + 1, stampHeight, stampWidth);
                        }
                        else if ((stampHeight == 1 || i == grid.Length - 1 || grid[i + 1][j] == 1) && (stampWidth == 1 || j == grid[i].Length - 1 || grid[i][j + 1] == 1))
                        // 原先是下面这行，少了h或者w=1的判断，导致WA：[[0,0,1,1],[0,0,1,1]] 2 1
                        // else if ((i == grid.Length - 1 || grid[i + 1][j] == 1) && (j == grid[i].Length - 1 || grid[i][j + 1] == 1))
                        {   // case2: fail & right-bottom corner, check left-top
                            if (!Check(i - stampHeight + 1, j - stampWidth + 1, stampHeight, stampWidth))
                                return false;
                        }
                        else return false; // case 3: just fail
                    }
                }
            }
            return true;
        }
        /*/
    // ver1 & ver2 之后，还是WA，反例：[[0,0],[0,0],[0,1],[0,1],[0,1],[0,0],[1,1]], 1, 2

    // ver1: WA, 完全没想到[[0,0,0,0],[0,0,0,0],[0,0,0,1]], 3, 3 这个反例
    int[][] grid;
        bool Check(int i, int j, int height, int width)
        {
            if (i >= grid.Length || j >= grid[i].Length || grid[i][j] == 1) return false;
            if (grid[i][j] == 2) return true;
            if (height > 1 && !Check(i + 1, j, height - 1, width)) return false;
            if (width > 1 && !Check(i, j + 1, height, width - 1)) return false;
            if (height > 1 && width > 1 && !Check(i + height - 1, j + width - 1, 1, 1)) return false;
            grid[i][j] = 2; return true;
        }
        bool CheckR(int i, int j, int height, int width)
        {
            if (i < 0 || j < 0 || grid[i][j] == 1) return false;
            if (grid[i][j] == 3) return true;
            if (height > 1 && !CheckR(i - 1, j, height - 1, width)) return false;
            if (width > 1 && !CheckR(i, j - 1, height, width - 1)) return false;
            if (height > 1 && width > 1 && !CheckR(i - height + 1, j - width + 1, 1, 1)) return false;
            grid[i][j] = 3; return true;
        }
        // ver2: 加上下面方法，除了检测左上角还有右下角，然后。。。第一个用例又WA了= =
        // 自己看看自己写的代码！！！
        //bool CheckR(int i, int j, int height, int width)
        //{
        //    if (i < 0 || j < 0 || grid[i][j] == 1) return false;
        //    if (grid[i][j] == 3) return true;
        //    if (height > 1 && !Check(i - 1, j, height - 1, width)) return false;
        //    if (width > 1 && !Check(i, j - 1, height, width - 1)) return false;
        //    if (height > 1 && width > 1 && !Check(i - height + 1, j - width + 1, 1, 1)) return false;
        //    grid[i][j] = 3; return true;
        //}
        public bool PossibleToStamp(int[][] grid, int stampHeight, int stampWidth)
        {
            this.grid = grid;
            for (int i = 0; i < grid.Length; i++)
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] != 1 && (i == 0 || grid[i - 1][j] == 1) && (j == 0 || grid[i][j - 1] == 1))
                        if (!Check(i, j, stampHeight, stampWidth))
                            return false;
                    if (grid[i][j] != 1 && (i == grid.Length - 1 || grid[i + 1][j] == 1) && (j == grid[i].Length - 1 || grid[i][j + 1] == 1))
                        if (!CheckR(i, j, stampHeight, stampWidth))
                            return false;
                }
            return true;
        }
        //*/

        internal static void Run()
        {
            int[] a1 = { 1, 0, 0, 0 }, a2 = { 1, 0, 0, 0 }, a3 = { 1, 0, 0, 0 }, a4 = { 1, 0, 0, 0 }, a5 = { 1, 0, 0, 0 };
            int[][] grid = { a1, a2, a3, a4, a5 };
            Console.WriteLine(new P2132用邮票贴满网格图().PossibleToStamp(grid, 4, 3));
        }
        /*WA过的几个特殊用例：
[[1,0,0,0],[1,0,0,0],[1,0,0,0],[1,0,0,0],[1,0,0,0]]
4
3
[[0,0,0,0],[0,0,0,0],[0,0,0,1]]
3
3
[[0,0],[0,0],[0,1],[0,1],[0,1],[0,0],[1,1]]
1
2
[[0],[0],[0],[0],[0],[0],[0],[0],[0]]
1
1
[[0,0,1,1],[0,0,1,1]]
2
1
        */
    }
}
