using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // 回溯，用栈而不是递归
    internal class P0216组合总和III
    {
        public IList<IList<int>> CombinationSum3(int k, int n)
        {
            IList<IList<int>> ans = new List<IList<int>>();
            int[] arr = new int[k];
            int i = 1; arr[0] = 1;
            while (i >= 0)
            {
                if (i == k)
                {
                    if (arr.Sum() == n) ans.Add(new List<int>(arr));
                    --i;
                    continue;
                }
                else if (arr[i] == 0)
                    arr[i] = arr[i - 1] + 1;
                else
                    ++arr[i];
                if (arr[i] > 9) arr[i--] = 0;
                else ++i;

                // debug
                //Console.WriteLine($"{arr[0]} {arr[1]} {arr[2]}");
            }
            return ans;
        }

        internal static void Run()
        {
            new P0216组合总和III().CombinationSum3(3, 7);
        }
    }
}
