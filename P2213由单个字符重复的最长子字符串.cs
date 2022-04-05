using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, WC285-D // 2022/3/20
    // SortedSet（官方提示用线段树）
    internal class P2213由单个字符重复的最长子字符串
    {
        class CharStruct
        {
            int _st;
            public int start { get => _st; set { int e = end; _st = value; end = e; } }
            public int len;
            public int end { get => _st + len - 1; set => len = value + 1 - _st; }
        }
        class CompByLen : IComparer<CharStruct>
        {
            public int Compare(CharStruct x, CharStruct y) 
                => x.len == y.len ? x.start.CompareTo(y.start) : x.len.CompareTo(y.len);
        }
        class CompByIndex : IComparer<CharStruct>
        {
            public int Compare(CharStruct x, CharStruct y)
                => x.end.CompareTo(y.end);
        }
        public int[] LongestRepeating(string s, string queryCharacters, int[] queryIndices)
        {
            var ca = s.ToCharArray();
            CharStruct cs = null;
            SortedSet<CharStruct> sortLen = new(new CompByLen()), // 根据长度排序
                sortIndex = new(new CompByIndex()); // 根据位置排序
            for (int i = 0; i <= ca.Length; ++i)
            {
                if (i == ca.Length || i > 0 && ca[i] != ca[i - 1])
                {
                    cs.end = i - 1;
                    sortLen.Add(cs);
                    sortIndex.Add(cs);
                    cs = null;
                }
                if (cs == null && i < ca.Length) cs = new() { start = i };
            }
            int[] ans = new int[queryIndices.Length];
            cs = new() { end = ca.Length }; // 存query的upperbound
            for (int i = 0; i < queryIndices.Length; ++i)
            {
                char c = queryCharacters[i];
                int idx = queryIndices[i];
                CharStruct left = null, mid = null, right = null;
                foreach (var t in sortIndex.GetViewBetween(new() { end = idx - 1 }, cs))
                    if (t.end == idx - 1) left = t;
                    else if (t.start == idx + 1) right = t;
                    else if (t.start <= idx && t.end >= idx) mid = t;
                    else if (t.start > idx) break;
                sortLen.Remove(mid);
                sortIndex.Remove(mid);
                if (mid.start < idx && c != ca[mid.start])
                {   // 拆左边
                    left = new() { start = mid.start, end = idx - 1 };
                    sortLen.Add(left);
                    sortIndex.Add(left);
                    mid.start = idx;
                }
                else if (left != null && c == ca[left.start])
                {   // 拼接左边
                    sortLen.Remove(left);
                    sortIndex.Remove(left);
                    mid.start = left.start;
                }
                if (mid.end > idx && c != ca[mid.end])
                {   // 拆右边
                    right = new() { start = idx + 1, end = mid.end };
                    sortLen.Add(right);
                    sortIndex.Add(right);
                    mid.end = idx;
                }
                else if (right != null && c == ca[right.start])
                {   // 拼接右边
                    sortLen.Remove(right);
                    sortIndex.Remove(right);
                    mid.end = right.end;
                }
                sortLen.Add(mid);
                sortIndex.Add(mid);
                ca[idx] = c;
                ans[i] = sortLen.Max.len;
            }
            return ans;
        }
    }
}
