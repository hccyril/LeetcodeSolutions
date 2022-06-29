using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/6/5
    // Dictionary+HashSet+List
    public class RandomizedCollection
    {
        Dictionary<int, HashSet<int>> dic = new();
        List<int> list = new();
        Random rand = new();

        public bool Insert(int val)
        {
            if (!dic.ContainsKey(val)) dic[val] = new();
            dic[val].Add(list.Count);
            list.Add(val);
            return dic[val].Count == 1;
        }

        public bool Remove(int val)
        {
            if (!dic.ContainsKey(val) || !dic[val].Any()) return false;
            var ds = dic[val];
            int ind = ds.First(); ds.Remove(ind);
            if (ind < list.Count - 1)
            {
                list[ind] = list.Last();
                dic[list[ind]].Remove(list.Count - 1);
                dic[list[ind]].Add(ind);
            }
            list.RemoveAt(list.Count - 1);
            return true;
        }

        public int GetRandom() => list[rand.Next(list.Count)];
    }
    internal class P0381O1时间插入删除和获取随机元素_允许重复
    {
    }
}
