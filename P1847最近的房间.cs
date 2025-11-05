using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2024/12/16 Daily
// rating 2081
// 有序集合
// also 测试 SortedSet的GetViewBetween效率如何
internal class P1847最近的房间
{
    public int[] ClosestRoom(int[][] rooms, int[][] queries)
    {
        const int MAX_ID = 99999999, MIN_ID = 0;
        SortedSet<int> st = new()
        {
            MIN_ID, MAX_ID
        };
        var sort_rooms = rooms.Select(p => ((int size, int id))(p[1], p[0]))
            .OrderByDescending(t => t.size)
            .ToArray();
        int i = 0, qn = queries.Length;
        int[] ans = new int[qn];
        foreach ((int index, int preferred, int minSize) in Enumerable.Range(0, qn)
            .Select(i => ((int index, int preferred, int minSize))(i, queries[i][0], queries[i][1]))
            .OrderByDescending(t => t.minSize))
        {
            while (i < sort_rooms.Length && sort_rooms[i].size >= minSize)
                st.Add(sort_rooms[i++].id);
            if (st.Count <= 2) 
                ans[index] = -1;
            else
            {
                int l = st.GetViewBetween(0, preferred).Max,
                    r = st.GetViewBetween(preferred + 1, MAX_ID).Min;
                ans[index] = r < MAX_ID && (l == 0 || r - preferred < preferred - l) ? r : l;
            }
        }
        return ans;
    }
}
