using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/3/19 US Daily
    // 我刷的第1000题
    internal class P0895最大频率栈
    {
        class FStruct : IComparable<FStruct>
        {
            static int _id = 1;
            public int val;
            public readonly Stack<int> stk = new();
            public FStruct(int v) => val = v;
            public void AddFreq() => stk.Push(_id++);
            public int CompareTo(FStruct fs)
                => val == fs.val ? 0 : stk.Count == fs.stk.Count ? stk.Peek().CompareTo(fs.stk.Peek()) : stk.Count.CompareTo(fs.stk.Count); 
        }

        public class FreqStack
        {
            SortedSet<FStruct> sort = new();
            Dictionary<int, FStruct> dic = new();

            public void Push(int val)
            {
                if (dic.TryGetValue(val, out FStruct fs))
                    sort.Remove(fs);
                else 
                    dic[val] = fs = new(val);

                fs.AddFreq();
                sort.Add(fs);
            }

            public int Pop()
            {
                var fs = sort.Max;
                sort.Remove(fs);
                if (fs.stk.Count > 1)
                {
                    fs.stk.Pop();
                    sort.Add(fs);
                }
                else
                {
                    dic.Remove(fs.val);
                }
                return fs.val;
            }
        }
    }
}
