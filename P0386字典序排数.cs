using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/4/18 Daily
    internal class P0386字典序排数
    {
        public const string 相关题 = "440. 字典序的第K小数字";

        IEnumerable<int> GetNums(int start, int end)
        {
            if (start > 0) yield return start;
            var range = start == 0 ? Enumerable.Range(1, 9) : Enumerable.Range(start * 10, 10);
            range = range.Where(n => n <= end);
            foreach (int i in range)
                foreach (int c in GetNums(i, end))
                    yield return c;
        }
        public IList<int> LexicalOrder(int n)
            => GetNums(0, n).ToList();
    }
}
