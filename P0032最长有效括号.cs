using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0032最长有效括号
    {
        public int LongestValidParentheses(string s)
        {
            int sum, max = 0;
            Stack<int> st = new Stack<int>();
            foreach (var ch in s)
            {
                if (ch == '(')
                {
                    st.Push(1);
                }
                else // ')'
                {   // 右括号如果合法一定在stack里有对应的左括号，只管进行结算
                    sum = 1;
                    bool pair = false;
                    while (st.Any())
                    {
                        var p = st.Pop();
                        if (p == 1)
                        {
                            sum += p;
                            pair = true;
                            break;
                        }
                        else // p > 1
                        {
                            sum += p;
                        }
                    }
                    if (pair)
                    {
                        while (st.Any() && st.Peek() > 1)
                            sum += st.Pop();
                        if (sum > max) max = sum;
                        st.Push(sum);
                    }
                }
            }
            return max;
        }
    }
}
