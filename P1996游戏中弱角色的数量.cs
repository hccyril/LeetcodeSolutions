using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium 排序/单调栈 // 2021/1/28 Daily
    internal class P1996游戏中弱角色的数量
    {
        public int NumberOfWeakCharacters(int[][] properties)
        {
            int count = 0;
            // 注意这里防御值要按降序排列，否则WA
            var sorted = properties.Select(t => (t[0], t[1])).OrderBy(p => p.Item1).ThenByDescending(p => p.Item2);
            Stack<(int attack, int defence)> stk = new();
            foreach ((int attack, int defence) in sorted)
            {
                while (stk.Any() && stk.Peek().attack < attack && stk.Peek().defence < defence)
                {
                    ++count;
                    stk.Pop();
                }
                stk.Push((attack, defence));
            }
            return count;
        }
        /** official (c++)
            sort(begin(properties), end(properties), [](const vector<int> & a, const vector<int> & b) {
                return a[0] == b[0] ? (a[1] > b[1]) : (a[0] < b[0]);
            });
            stack<int> st;
            int ans = 0;
            for (auto & p: properties) {
                while (!st.empty() && st.top() < p[1]) {
                    ++ans;
                    st.pop();
                }
                st.push(p[1]);
            }
            return ans;
        */
    }
}
