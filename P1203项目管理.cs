using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/4/30
    // rank: 2419
    // 拓扑排序
    internal class P1203项目管理
    {
        // ver2: 提交完了才想到，前置筛选那里只关心前置为0的组/项目，因此用哈希表就可以了，没必要用堆
        public int[] SortItems(int n, int m, int[] group, IList<IList<int>> beforeItems)
        {
            Dictionary<int, int> itemCD = new(), gpCD = new();
            Dictionary<int, List<int>> bgDic = new();
            Dictionary<int, List<int>> afterItems = new(), afterGroups = new();
            HashSet<int> groupList = new();
            Dictionary<int, HashSet<int>> itemList = new();

            for (int i = 0; i < n; ++i)
            {
                if (group[i] < 0) group[i] = ~i;
                if (beforeItems[i].Count == 0)
                {
                    if (!itemList.ContainsKey(group[i])) itemList[group[i]] = new();
                    itemList[group[i]].Add(i);
                }
                else itemCD[i] = beforeItems[i].Count;
                    
                foreach (var bi in beforeItems[i])
                {
                    if (!afterItems.ContainsKey(bi)) afterItems[bi] = new();
                    afterItems[bi].Add(i);

                    // 组间优先（否则就是组内优先）
                    if (group[bi] < 0 || group[bi] != group[i])
                    {
                        int bgi = group[bi] < 0 ? ~bi : group[bi];
                        if (!bgDic.ContainsKey(group[i])) bgDic[group[i]] = new();
                        bgDic[group[i]].Add(bgi);

                        if (!afterGroups.ContainsKey(bgi)) afterGroups[bgi] = new();
                        afterGroups[bgi].Add(group[i]);
                    }
                }
            }
            var gps = Enumerable.Range(0, n).GroupBy(i => group[i]);
            var gcDic = gps.ToDictionary(g => g.Key, g => g.Count());
            foreach (var g in gps)
                if (bgDic.ContainsKey(g.Key)) gpCD[g.Key] = bgDic[g.Key].Count;
                else groupList.Add(g.Key);

            // start
            int[] ans = new int[n];
            int idx = 0, gp = 0, gc = -1;
            while (idx < n)
            {
                if (gc <= 0)
                {
                    if (gc == 0 && afterGroups.ContainsKey(gp))
                        foreach (var ag in afterGroups[gp])
                            if (--gpCD[ag] == 0)
                                groupList.Add(ag);

                    if (!groupList.Any()) return Array.Empty<int>();
                    gp = groupList.First(); groupList.Remove(gp);
                    gc = gcDic[gp];
                }

                if (!itemList.ContainsKey(gp) || !itemList[gp].Any()) return Array.Empty<int>();
                int t = itemList[gp].First(); itemList[gp].Remove(t);
                ans[idx++] = t;
                --gc;
                if (afterItems.ContainsKey(t))
                    foreach (var ai in afterItems[t])
                        if (--itemCD[ai] == 0)
                        {
                            if (!itemList.ContainsKey(group[ai])) itemList[group[ai]] = new();
                            itemList[group[ai]].Add(ai);
                        }
            }

            // finish
            return ans;
        }

        // ver1: AC
        public int[] SortItems_HeapSolution(int n, int m, int[] group, IList<IList<int>> beforeItems)
        {
            SHeap<int, int> hp = new((a, b) => a < b);
            Dictionary<int, List<int>> bgDic = new();
            SHeap<int, int> gh = new((a, b) => a < b);
            Dictionary<int, List<int>> afterItems = new(), afterGroups = new();

            for (int i = 0; i < n; ++i)
            {
                if (group[i] < 0) group[i] = ~i;
                hp.Add(i, beforeItems[i].Count);
                foreach (var bi in beforeItems[i])
                {
                    if (!afterItems.ContainsKey(bi)) afterItems[bi] = new();
                    afterItems[bi].Add(i);

                    // 组间优先（否则就是组内优先）
                    if (group[bi] < 0 || group[bi] != group[i])
                    {
                        int bgi = group[bi] < 0 ? ~bi : group[bi];
                        if (!bgDic.ContainsKey(group[i])) bgDic[group[i]] = new();
                        bgDic[group[i]].Add(bgi);

                        if (!afterGroups.ContainsKey(bgi)) afterGroups[bgi] = new();
                        afterGroups[bgi].Add(group[i]);
                    }
                }
            }
            var gps = Enumerable.Range(0, n).GroupBy(i => group[i]);
            var gcDic = gps.ToDictionary(g => g.Key, g => g.Count());
            HashSet<int> groupList = new();
            foreach (var g in gps)
                if (bgDic.ContainsKey(g.Key)) gh.Add(g.Key, bgDic[g.Key].Count);
                else groupList.Add(g.Key);
            Dictionary<int, HashSet<int>> itemList = new();

            // start
            int[] ans = new int[n];
            int idx = 0, gp = 0, gc = -1;
            while (idx < n)
            {
                while (hp.Any() && hp.HeadValue == 0)
                {
                    (int item, _) = hp.Pop();
                    if (!itemList.ContainsKey(group[item])) itemList[group[item]] = new();
                    itemList[group[item]].Add(item);
                }
                if (gc <= 0)
                {
                    if (gc == 0 && afterGroups.ContainsKey(gp))
                    {
                        foreach (var ag in afterGroups[gp])
                            gh[ag]--;
                        while (gh.Any() && gh.HeadValue == 0)
                            groupList.Add(gh.Pop().key);
                    }

                    if (!groupList.Any()) return Array.Empty<int>();
                    gp = groupList.First(); groupList.Remove(gp);
                    gc = gcDic[gp];
                }

                if (!itemList.ContainsKey(gp) || !itemList[gp].Any()) return Array.Empty<int>();
                int t = itemList[gp].First(); itemList[gp].Remove(t);
                ans[idx++] = t; 
                --gc;
                if (afterItems.ContainsKey(t))
                    foreach (var ai in afterItems[t])
                        hp[ai]--;
            }

            // finish
            return ans;
        }

        internal static void Run()
        {
            int n = 8, m = 2;
            int[] gps = { -1, -1, 1, 0, 0, 1, 0, -1 };
            var bis = new int[][] { new int[] { }, new int[] { 6 }, new int[] { 5 }, new int[] { 6 }, new int[] { 3 }, new int[] { }, new int[] { 4 }, new int[] { } };
            var sln = new P1203项目管理();
            var ans = sln.SortItems(n, m, gps, bis);
            Console.WriteLine(nameof(P1203项目管理) + ": " + string.Join("", ans));
        }
    }
}
