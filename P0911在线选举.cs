using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2021/12/11 daily
    // 堆、二分、哈希表综合应用（更新：哈希表不用也行）
    // 2022/1/23 更新Update方法
    internal class P0911在线选举_TopVotedCandidate
    {
        int[] times, winners;
        HashHeap hp = new((a, b) => a >= b);
        public P0911在线选举_TopVotedCandidate(int[] persons, int[] times)
        {
            this.times = times;
            winners = persons;
            foreach (var i in Enumerable.Range(0, persons.Length))
            {
                int p = persons[i], t = times[i];
                if (hp.ContainsKey(p))
                {
                    hp.Update(p, 1);
                    //(int index, int vote) = hp.Get(p);
                    //hp.UpdateAt(index, ++vote);
                }
                else
                    hp.Push(p, 1);
                winners[i] = hp.HeadKey;
            }
        }

        public int Q(int t)
        {
            int i = Array.BinarySearch(times, t);
            if (i >= 0) return winners[i];
            else
            {
                i = -i - 2;
                if (i >= 0 && i < times.Length) return winners[i];
                else throw new ArgumentException("invalid t!"); // test only
            }
        }
    }
}
