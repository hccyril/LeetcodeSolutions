using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/2/5
    /* SortedDictionary的效率不算特别高
     * 执行用时：1196 ms, 在所有 C# 提交中击败了11.11%的用户
     * 内存消耗：64.3 MB, 在所有 C# 提交中击败了11.11%的用户
     * */
    internal class P0729我的日程安排表I
    {
        SortedDictionary<int, int> sd = new();
        public bool Book(int start, int end)
        {
            foreach (int st in sd.Keys.Where(t => t < end))
                if (sd[st] > start) return false;
            sd[start] = end;
            return true;
        }
    }
}
