using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0380插入删除和获取随机元素
    {
        public class RandomizedSet
        {
            Random rand = new Random();
            Dictionary<int, int> dic;
            List<int> list;

            public RandomizedSet()
            {
                dic = new Dictionary<int, int>();
                list = new List<int>();
            }

            public bool Insert(int val)
            {
                if (dic.ContainsKey(val)) return false;
                dic[val] = list.Count;
                list.Add(val);
                return true;
            }

            public bool Remove(int val)
            {
                if (dic.ContainsKey(val))
                {
                    int i = dic[val];
                    list[i] = list[list.Count - 1];
                    dic[list[i]] = i;
                    dic.Remove(val);
                    list.RemoveAt(list.Count - 1);
                    return true;
                }
                return false;
            }

            public int GetRandom()
            {
                return list[rand.Next(list.Count)];
            }
        }

        /**
         * Your RandomizedSet object will be instantiated and called as such:
         * RandomizedSet obj = new RandomizedSet();
         * bool param_1 = obj.Insert(val);
         * bool param_2 = obj.Remove(val);
         * int param_3 = obj.GetRandom();
         */
    }
}
