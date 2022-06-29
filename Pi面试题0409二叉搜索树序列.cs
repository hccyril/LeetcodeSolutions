using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/5/28
    // 回溯 DFS
    internal class Pi面试题0409二叉搜索树序列
    {
        int count = 0;
        int Count(TreeNode rt) => rt == null ? 0 : 1 + Count(rt.left) + Count(rt.right);
        IList<IList<int>> ansList;

        IEnumerable<TreeNode> FrontNodes(List<TreeNode> list, HashSet<int> hs)
        {
            int n = list.Count;
            for (int i = 0; i < n; ++i)
            {
                var node = list[i];
                if (node.left != null && !hs.Contains(node.left.val))
                    yield return node.left;
                if (node.right != null && !hs.Contains(node.right.val))
                    yield return node.right;
            }
        }
        void Dfs(List<TreeNode> list, HashSet<int> hs)
        {
            if (list.Count == count)
            {
                ansList.Add(list.Select(t => t.val).ToArray());
                return;
            }
            foreach (var node in FrontNodes(list, hs))
            {
                list.Add(node); hs.Add(node.val);
                Dfs(list, hs);
                list.RemoveAt(list.Count - 1); hs.Remove(node.val);
            }
        }

        public IList<IList<int>> BSTSequences(TreeNode root)
        {
            count = Count(root);
            ansList = new List<IList<int>>();
            if (root == null)
            {   // ！！！！
                ansList.Add(Array.Empty<int>());
                return ansList;
            }
            List<TreeNode> list = new() { root };
            HashSet<int> hs = new() { root.val };
            Dfs(list, hs);
            return ansList;
        }
    }
}
