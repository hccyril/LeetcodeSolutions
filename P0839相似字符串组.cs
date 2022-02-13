using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/2/10
    // also 《剑指 Offer II 117. 相似的字符串》
    internal class P0839相似字符串组
    {
        // ver1: 并查集（比较暴力）
        bool IsSimilar(string s1, string s2)
        {
            int count = 0;
            for (int i = 0; i < s1.Length; ++i)
                if (s1[i] != s2[i])
                    if (++count > 2) return false;
            return true;
        }
        public int NumSimilarGroups(string[] strs)
        {
            UnionFind uni = new(strs.Length);
            for (int i = 0; i < strs.Length - 1; ++i) 
                for (int j = 0; j < strs.Length; ++j)
                    if (uni.Find(i) != uni.Find(j) && IsSimilar(strs[i], strs[j]))
                        uni.Union(i, j);
            return uni.GroupCount();
        }


    }
}
