using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/5/26 Daily
    // 相当于暴力解决了
    internal class P0699掉落的方块
    {
        class SquareStruct : IComparable<SquareStruct>
        {
            public int left, height, width;
            public int CompareTo(SquareStruct other)
                => left != other.left ? left.CompareTo(other.left) : height.CompareTo(other.height);
        }
        public IList<int> FallingSquares(int[][] positions)
        {
            SortedSet<SquareStruct> sort = new();
            SquareStruct sl = new(), sr = new();
            IList<int> ans = new List<int>();
            int max = 0;
            foreach (var p in positions)
            {
                SquareStruct s = new() { left = p[0], width = p[1] };
                //sl.left = s.left; 
                sr.left = s.left + s.width;
                foreach (var s0 in sort.GetViewBetween(sl, sr))
                {
                    //Console.WriteLine("{0} {1} {2}", s0.left, s0.height, s0.width);
                    if (s0.left + s0.width > s.left && s0.height > s.height) s.height = s0.height;
                }
                s.height += s.width;
                sort.Add(s);
                if (s.height > max) max = s.height;
                ans.Add(max);
            }
            return ans;
        }
    }
}
