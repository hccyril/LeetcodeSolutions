using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/2/18
    // 欧拉回路 （回溯）
    // 使用回溯AC通过
    // 待优化：每个节点最多只有一个单边（有去无回），这个单边一定是最后走
    internal class P0332重新安排行程
    {
        class MyCmp : IComparer<int>
        {
            IList<IList<string>> tickets;
            public MyCmp(IList<IList<string>> tickets) => this.tickets = tickets;
            public int Compare(int x, int y)
                => tickets[x][1].CompareTo(tickets[y][1]) < 0 ? -1 : 1;
        }
        IList<IList<string>> tickets;
        Dictionary<string, SortedSet<int>> dic = new();
        BitArray barr;
        int rest;
        bool Dfs(string st, IList<string> list)
        {
            if (!dic.ContainsKey(st)) return false;
            foreach (int i in dic[st])
                if (!barr[i])
                {
                    list.Add(tickets[i][1]);
                    if (--rest == 0) return true;
                    barr[i] = true;
                    if (Dfs(tickets[i][1], list)) return true;
                    barr[i] = false;
                    ++rest;
                    list.RemoveAt(list.Count - 1);
                }
            return false;
        }
        public IList<string> FindItinerary(IList<IList<string>> tickets)
        {
            this.tickets = tickets;
            barr = new(rest = tickets.Count);
            MyCmp cmp = new(tickets);
            foreach (int i in Enumerable.Range(0, tickets.Count))
            {
                if (!dic.ContainsKey(tickets[i][0]))
                    dic[tickets[i][0]] = new(cmp);
                dic[tickets[i][0]].Add(i);
            }
            IList<string> ansList = new List<string>(); ansList.Add("JFK");
            Dfs("JFK", ansList);
            return ansList;
        }
    }
}
