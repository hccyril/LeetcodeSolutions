using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // 2022/1/16 周赛最后一题
    // 比赛的时候没想到，比完赛到晚上才想到原来这么简单
    internal class P2141同时运行N台电脑的最长时间
    {
        public long MaxRunTime(int n, int[] batteries)
        {
            long rest = 0;
            Heap<long> hp = new Heap<long>((a, b) => a < b);
            foreach (var b in batteries)
            {
                if (hp.Count < n) { hp.Push(b); continue; }
                if (b > hp.Head)
                {
                    hp.Push(b);
                    rest += hp.Pop();
                }
                else
                    rest += b;
            }
            int col = 0;
            long min = 0, target = 0;
            while (rest >= target)
            {
                rest -= target;
                min = hp.Pop();
                ++col;
                target = hp.Any() ? col * (hp.Head - min) : rest + 1;
            }
            min += rest / col;

            return min;
        }
    }
}
