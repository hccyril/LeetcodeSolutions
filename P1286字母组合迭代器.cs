using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium
    // OOP / Data struct
    class P1286字母组合迭代器
    {
        public class CombinationIterator
        {
            string chs;
            int[] arr;
            int map = 0;

            public CombinationIterator(string characters, int combinationLength)
            {
                chs = new string(characters.ToCharArray().OrderBy(t => t).ToArray());
                arr = Enumerable.Range(0, combinationLength).Select(t => -1).ToArray();
                Next();
            }

            public string Next()
            {
                string ans = HasNext() ? string.Join("", from t in arr select chs[t]) : null;
                int i = arr[0] < 0 ? 0 : arr.Length - 1;
                while (i >= 0 && i < arr.Length)
                {
                    if (arr[i] >= 0) map ^= 1 << arr[i];
                    // 如果是combination就要指定比前一个字母+1，否则是permutation就不用
                    int k = arr[i] < 0 && i > 0 ? arr[i - 1] + 1 : arr[i] + 1;
                    for (arr[i] = -1; k < chs.Length; ++k)
                    {
                        if ((map & (1 << k)) == 0)
                        {
                            map ^= 1 << k;
                            arr[i] = k;
                            break;
                        }
                    }
                    if (arr[i] >= 0) ++i;
                    else --i;
                }
                return ans;
            }

            public bool HasNext() => arr[0] >= 0;
        }

        /**
         * Your CombinationIterator object will be instantiated and called as such:
         * CombinationIterator obj = new CombinationIterator(characters, combinationLength);
         * string param_1 = obj.Next();
         * bool param_2 = obj.HasNext();
         */
    }
}
