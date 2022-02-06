using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, Heap
    // 与1296题相同
    internal class P0846一手顺子
    {
        public bool IsNStraightHand(int[] hand, int groupSize)
        {
            if (hand.Length % groupSize != 0) return false;
            Dictionary<int, int> dic = new();
            HashHeap hp = new(false);
            foreach (var h in hand)
            {
                if (!dic.ContainsKey(h))
                {
                    dic[h] = 1;
                    hp.Push(h, h);
                }
                else
                {
                    ++dic[h];
                }
            }
            while (hp.Any())
            {
                int first = hp.Head;
                for (int i = 0; i < groupSize; ++i)
                {
                    int key = first + i;
                    if (!dic.ContainsKey(key)) return false;
                    --dic[key];
                    if (dic[key] == 0)
                    {
                        hp.Remove(key);
                        dic.Remove(key);
                    }
                }
            }
            return true;
        }
    }
}
