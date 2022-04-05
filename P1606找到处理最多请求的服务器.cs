using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/3/30 Daily
    // BiC36-D, rank: 2276
    // 使用两个SortedSet直接模拟
    internal class P1606找到处理最多请求的服务器
    {
        class Server
        {
            public int id;
            public int finish;
            public int cnt;
        }
        class CmpById : IComparer<Server>
        {
            public int Compare(Server x, Server y)
                => x.id.CompareTo(y.id);
        }
        class CmpByTime : IComparer<Server>
        {
            public int Compare(Server x, Server y)
                => x.finish == y.finish ? x.id.CompareTo(y.id) : x.finish.CompareTo(y.finish);
        }
        public IList<int> BusiestServers(int k, int[] arrival, int[] load)
        {
            Server[] arr = new Server[k];
            SortedSet<Server> ready = new(new CmpById()), working = new(new CmpByTime());
            for (int i = 0; i < k; ++i) ready.Add(arr[i] = new() { id = i });
            for (int i = 0; i < arrival.Length; ++i)
            {
                int arrive = arrival[i];
                while (working.Any() && working.Min.finish <= arrive)
                {
                    ready.Add(working.Min);
                    working.Remove(working.Min);
                }
                if (!ready.Any()) continue;
                Server selected = ready.GetViewBetween(new() { id = i % k }, null).FirstOrDefault() ?? ready.Min;
                ready.Remove(selected);
                selected.finish = arrive + load[i];
                selected.cnt++;
                working.Add(selected);
            }
            int max = arr.Max(s => s.cnt);
            return arr.Where(s => s.cnt == max).Select(s => s.id).ToArray();
        }
    }
}
