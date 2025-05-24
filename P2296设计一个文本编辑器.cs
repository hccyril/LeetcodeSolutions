using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;
// hard, 2025/2/27 Daily
// 为数不多的链表应用题
internal class P2296设计一个文本编辑器
{
    public class TextEditor
    {
        LinkedList<char> list;
        LinkedListNode<char> cur;

        public TextEditor()
        {
            list = new();
            cur = list.AddFirst('0');
        }

        public void AddText(string text)
        {
            foreach (var c in text)
                cur = list.AddAfter(cur, c);
        }

        public int DeleteText(int k)
        {
            int del = 0;
            while (k-- > 0 && cur.Value != '0')
            {
                cur = cur.Previous!;
                list.Remove(cur.Next!);
                ++del;
            }
            return del;
        }

        string GetText()
        {
            StringBuilder build = new();
            var p = cur;
            for (int k = 0; k < 10 && p.Value != '0'; ++k)
            {
                build.Insert(0, p.Value);
                p = p.Previous!;
            }
            return build.ToString();
        }

        public string CursorLeft(int k)
        {
            while (k-- > 0 && cur.Value != '0')
                cur = cur.Previous!;
            return GetText();
        }

        public string CursorRight(int k)
        {
            while (k-- > 0 && cur.Next != null)
                cur = cur.Next;
            return GetText();
        }
    }

    /**
     * Your TextEditor object will be instantiated and called as such:
     * TextEditor obj = new TextEditor();
     * obj.AddText(text);
     * int param_2 = obj.DeleteText(k);
     * string param_3 = obj.CursorLeft(k);
     * string param_4 = obj.CursorRight(k);
     */
}
