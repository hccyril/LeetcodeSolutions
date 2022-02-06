using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 前缀树
    // 但是看官方题解有另一个解法：AC自动机
    internal class P1032字符流
    {
        public class StreamChecker
        {
            readonly WordTree tree = new();
            readonly List<WordTree> list = new();

            public StreamChecker(string[] words)
            {
                foreach (var word in words)
                    tree.AddWord(word);
            }

            public bool Query(char letter)
            {
                bool ans = false;
                for (int i = list.Count - 1; i >= 0; --i)
                {
                    if (list[i].ContainsChar(letter))
                    {
                        list[i] = list[i][letter];
                        if (list[i].Word != null) ans = true;
                    }
                    else
                    {
                        list[i] = list[list.Count - 1];
                        list.RemoveAt(list.Count - 1);
                    }
                }
                if (tree.ContainsChar(letter))
                {
                    list.Add(tree[letter]);
                    if (list[list.Count - 1].Word != null) ans = true;
                }
                return ans;
            }
        }
    }
}
