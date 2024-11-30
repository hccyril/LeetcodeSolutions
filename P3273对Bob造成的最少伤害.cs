using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// 2024/8/31 138双周赛-D
// 比赛时思路偏了，一直在想求max((T - t) * d)，沿着这个思路发现排序是动态的，做不出来
// 后来看了题解才发现这个思路是错的，只考虑到减少了的伤害值，但另一边damage总和也是不一样的
// 正解是直接排序就好了（虽然还是不太明白）
internal class P3273对Bob造成的最少伤害
{
    class DStruct : IComparable<DStruct>
    {
        public long t, d;

        public int CompareTo(DStruct other)
            => (t * other.d).CompareTo(other.t * d);
    }

    public long MinDamage(int power, int[] damage, int[] health)
    {
        int n = damage.Length;
        var a = Enumerable.Range(0, n)
            .Select(i => new DStruct() { d = damage[i], t = (health[i] + power - 1) / power })
            .ToArray();
        Array.Sort(a);
        long ans = 0, t = 0;
        foreach (var x in a)
        {
            t += x.t;
            ans += x.d * t;
        }
        return ans;
    }
}
