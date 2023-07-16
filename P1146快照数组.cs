using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2023/6/11 US Daily
    // 数据结构+二分查找
    internal class P1146快照数组
    {
    }

    public class SnapshotArray
    { 
        int ver = 0;
        readonly List<(int, int)>[] arr;

        public SnapshotArray(int length)
        {
            arr = Enumerable.Range(0, length).Select(i => new List<(int, int)>()).ToArray();
        }

        public void Set(int index, int val)
        {
            if (!arr[index].Any() || arr[index].Last().Item1 != ver)
                arr[index].Add((ver, val));
            else
                arr[index][^1] = (ver, val);
            // 还有类似这种range操作
            //foreach (var li in arr[3..^2])
            //{
            //}
        }

        public int Snap()
        {
            return ver++;
        }

        public int Get(int index, int snap_id)
        {
            if (!arr[index].Any()) return 0;
            int i = arr[index].BinarySearch((snap_id, int.MaxValue));
            i = i >= 0 ? i - 1 : ~i - 1;
            if (i < 0) return 0; // 注意默认值0可能不存
            return arr[index][i].Item2;
        }
    }

    /**
     * Your SnapshotArray object will be instantiated and called as such:
     * SnapshotArray obj = new SnapshotArray(length);
     * obj.Set(index,val);
     * int param_2 = obj.Snap();
     * int param_3 = obj.Get(index,snap_id);
     */

    /**
     * 快速求解（非正解）,主站那边过了，国内再提交却超时
     * 
    public class SnapshotArray
    {
        int ver = 0;
        Dictionary<(int, int), int> dic = new();

        public SnapshotArray(int length)
        {

        }

        public void Set(int index, int val)
        {
            dic[(index, ver)] = val;
        }

        public int Snap()
        {
            return ver++;
        }

        public int Get(int index, int snap_id)
        {
            int si = snap_id;
            while (si >= 0 && !dic.ContainsKey((index, si)))
                --si;
            return si >= 0 ? dic[(index, si)] : 0;
        }
    }
     */
}
