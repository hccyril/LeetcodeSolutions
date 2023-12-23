using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // Medium, 2023/9/23 Daily
    // 没有用线段树，只是模拟
    public class LockingTree
    {
        readonly List<int>[] t;
        readonly int[] a, pa;

        public LockingTree(int[] parent)
        {
            pa = parent;
            int n = parent.Length;
            a = new int[n];
            t = Enumerable.Range(0, n).Select(_ => new List<int>()).ToArray();
            //t = new List<int>[n];
            //for (int i = 0; i < n; ++i)
            //    t[i] = new List<int>();
            for (int i = 1; i < n; ++i)
                t[parent[i]].Add(i);
        }

        public bool Lock(int i, int u)
            => a[i] == 0 && (a[i] = u) != 0;

        public bool Unlock(int i, int u = -1)
            => (u < 0 && a[i] != 0 || a[i] == u) && (a[i] = 0) == 0;

        bool Dfs(int j)
        {
            bool b = Unlock(j);
            foreach (int k in t[j])
                b = Dfs(k) || b;
                //b = b || Dfs(k); // 巨坑！当前面b已经true时后面Dfs就不执行了，造成WA
            return b;
        }

        public bool Upgrade(int i, int u)
        {
            if (a[i] != 0) return false;
            for (int p = pa[i]; p >= 0; p = pa[p])
                if (a[p] != 0) return false;

            if (Dfs(i))
            {
                Lock(i, u);
                return true;
            }
            return false;
        }
    }

    /**
     * Your LockingTree object will be instantiated and called as such:
     * LockingTree obj = new LockingTree(parent);
     * bool param_1 = obj.Lock(num,user);
     * bool param_2 = obj.Unlock(num,user);
     * bool param_3 = obj.Upgrade(num,user);
     */

    internal class P1993树上的操作
    {
    }
}
