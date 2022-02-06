using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
     * 给你一个二进制字符串数组 strs 和两个整数 m 和 n 。

请你找出并返回 strs 的最大子集的大小，该子集中 最多 有 m 个 0 和 n 个 1 。

如果 x 的所有元素也是 y 的元素，集合 x 是集合 y 的 子集 。

 

示例 1：

输入：strs = ["10", "0001", "111001", "1", "0"], m = 5, n = 3
输出：4
解释：最多有 5 个 0 和 3 个 1 的最大子集是 {"10","0001","1","0"} ，因此答案是 4 。
其他满足题意但较小的子集包括 {"0001","1"} 和 {"10","1","0"} 。{"111001"} 不满足题意，因为它含 4 个 1 ，大于 n 的值 3 。

来源：力扣（LeetCode）
链接：https://leetcode-cn.com/problems/ones-and-zeroes
著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。
     * */
    class P0474一和零
    {
        class Count
        {
            public int m;
            public int n;

            public Count(int m, int n)
            {
                this.m = m;
                this.n = n;
            }

            int[] arr = new int[1 << 15];
            int[] arr2 = new int[1 << 15];

            static int Index(int i, int j)
            {
                return (i << 7) | j;
            }

            public void Update(string s)
            {
                int i = 0; 
                foreach (char ch in s)
                {
                    if (ch == '0') i++;
                }
                Update(i, s.Length - i);
            }

            public void Update(int i, int j)
            {
                for (int ii = 0; ii + i <= m; ++ii)
                {
                    for (int jj = 0; jj + j <= n; ++jj)
                    {
                        int c = Get(ii, jj) + 1;
                        Set(ii + i, jj + j, c);
                    }
                }

                // finish update
                for (i = 0; i <= m; ++i)
                    for (j = 0; j <= n; ++j)
                        arr[Index(i, j)] = arr2[Index(i, j)];
                //int[] arrTemp = arr;
                //arr = arr2;
                //arr2 = arrTemp;
            }

            public void Set(int i, int j, int c)
            {
                int index = Index(i, j);
                if (arr2[index] < c)
                    arr2[index] = c;
            }

            public int Get(int i, int j)
            {
                return arr[Index(i, j)];
            }
        }
        public int FindMaxForm(string[] strs, int m, int n)
        {
            Count c = new Count(m, n);
            foreach (string s in strs)
            {
                c.Update(s);
            }
            return c.Get(m, n);
        }

        public int FindMaxFormTest(string[] strs, int m, int n)
        {
            Count c = new Count(m, n);
            foreach (string s in strs)
            {
                c.Update(s);
                Console.WriteLine("\ns=" + s);
                for (int i = 1; i <= m; ++i)
                {
                    for (int j = 1; j <= n; ++j)
                        Console.Write(c.Get(i, j) + " ");
                    Console.WriteLine();
                }
            }
            return c.Get(m, n);
        }
    }
}
