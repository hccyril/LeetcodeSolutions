using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 周赛题C, 2022/3/1
    // 记忆化回溯
    internal class P1986完成任务的最少工作时间段
    {
        public int MinSessions(int[] tasks, int sessionTime)
        {
            Dictionary<int, int> dic = new();
            HashSet<int> hs = new();
            int map = (1 << tasks.Length) - 1;
            for (int bit = 1; bit <= map; ++bit)
            {
                int time = 0;
                for (int i = 0; i < tasks.Length; ++i)
                    if ((1 << i & bit) != 0)
                        time += tasks[i];
                if (time <= sessionTime)
                    hs.Add(bit);
            }

            int Dfs(int map)
            {
                if (map == 0) return 0;
                else if (dic.ContainsKey(map)) return dic[map];
                int count = int.MaxValue;
                for (int bit = 1; bit <= map; ++bit)
                    if ((bit & map) == bit && hs.Contains(bit))
                        count = Math.Min(count, 1 + Dfs(map ^ bit));
                return dic[map] = count;
            }

            return Dfs(map);
        }
    }
}
