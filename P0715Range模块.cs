using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/6/20 Daily
    // sortedset
    internal class P0715Range模块
    {
        // 2022/7/12 用 Interval类重写，快了不少 1600ms -> 320ms
        public class RangeModule
        {
            SortedSet<Interval> sort = new();

            public void AddRange(int left, int right)
            {
                sort.AddRange(left, right - 1);
            }

            public bool QueryRange(int left, int right)
            {
                --right;
                if (sort.TryGetValue(new(left), out var rs))
                    return rs.start <= left && rs.end >= right;
                return false;
            }

            public void RemoveRange(int left, int right)
            {
                sort.RemoveRange(left, right - 1);
            }
        }

        // ver1
        class RangeStruct : IComparable<RangeStruct>
        {
            public int key, start, end;
            public int CompareTo(RangeStruct other) => key.CompareTo(other.key);
        }
        SortedSet<RangeStruct> sort = new();
        IEnumerable<(int, int)> GetBetween(int left, int right)
        {
            var range = sort.GetViewBetween(new() { key = 0 }, new() { key = left - 1 });
            var start = range.Any() ? range.Max : null;
            if (start == null || start.key != start.start) start = new() { key = left };

            range = sort.GetViewBetween(new() { key = right + 1 }, new() { key = int.MaxValue });
            var end = range.Any() ? range.Min : null;
            if (end == null || end.key != end.end) end = new() { key = right };

            foreach (var p in sort.GetViewBetween(start, end))
                if (p.key == p.end) yield return (p.start, p.end);
        }
        public void AddRange(int left, int right)
        {
            (int addLeft, int addRight) = (left, right);
            List<int> removeList = new();
            foreach ((int l, int r) in GetBetween(left, right))
            {
                if (l <= right && r >= left)
                {
                    removeList.Add(l);
                    removeList.Add(r);
                    if (l < addLeft) addLeft = l;
                    if (r > addRight) addRight = r;
                }
            }
            foreach (int key in removeList)
                sort.Remove(new() { key = key });
            sort.Add(new() { key = addLeft, start = addLeft, end = addRight });
            sort.Add(new() { key = addRight, start = addLeft, end = addRight });
        }

        public bool QueryRange(int left, int right)
        {
            foreach ((int l, int r) in GetBetween(left, right))
            {
                return (l <= left && r >= right);
            }
            return false;
        }

        public void RemoveRange(int left, int right)
        {
            List<int> removeList = new();
            int addLeft = -1, addRight = -1;
            foreach ((int l, int r) in GetBetween(left, right))
            {
                removeList.Add(l);
                removeList.Add(r);
                if (l < left) addLeft = l;
                if (r > right) addRight = r;
            }
            foreach (var key in removeList)
                sort.Remove(new() { key = key });
            if (addLeft > 0)
            {
                sort.Add(new() { key = addLeft, start = addLeft, end = left });
                sort.Add(new() { key = left, start = addLeft, end = left });
            }
            if (addRight > 0)
            {
                sort.Add(new() { key = right, start = right, end = addRight });
                sort.Add(new() { key = addRight, start = right, end = addRight });
            }
        }
    }
}

// archive
//public void AddRange(int left, int right)
//{
//    (int addLeft, int addRight) = (left, right);
//    List<(int, int)> removeList = new();
//    foreach ((int l, int r) in sort.GetViewBetween((left, left), (right + 1, right + 1)))
//    {
//        if (l <= right && r >= left) {
//            removeList.Add((l, r));
//            if (l < addLeft) addLeft = l;
//            if (r > addRight) addRight = r;
//        }
//    }
//    foreach ((int l, int r) in removeList)
//        sort.Remove((l, r));
//    sort.Add((addLeft, addRight));
//}
