using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
     * 236. 二叉树的最近公共祖先
给定一个二叉树, 找到该树中两个指定节点的最近公共祖先。

百度百科中最近公共祖先的定义为：“对于有根树 T 的两个节点 p、q，最近公共祖先表示为一个节点 x，满足 x 是 p、q 的祖先且 x 的深度尽可能大（一个节点也可以是它自己的祖先）。”
     * */
    class P0236二叉树的最近公共祖先
    {
        List<TreeNode> _list;
        int _lca = 0;
        TreeNode _nodeLca = null;
        List<TreeNode> _pq;

        void Dfs(TreeNode tn)
        {
            _list.Add(tn);
            foreach (var p in _pq)
            {
                if (tn.val == p.val)
                {
                    _pq.Remove(p);
                    if (_pq.Any())
                    {
                        _lca = _list.Count - 1;
                    }
                    else
                    {
                        _nodeLca = _list[_lca];
                        throw new Exception(); // 找到答案，通过try-catch跳出整个递归调用
                    }
                    break;
                }
            }
            if (tn.left != null)
                Dfs(tn.left);
            if (tn.right != null)
                Dfs(tn.right);
            _list.Remove(tn);
            if (_list.Count <= _lca) _lca = _list.Count - 1; // 第一次提交写成了大于等于，这都能写错！
        }

        public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            _list = new List<TreeNode>();
            _pq = new List<TreeNode>() { p, q };
            try
            {
                Dfs(root);
            }
            catch
            {
                return _nodeLca;
            }
            return null;
        }

        // 别人的代码，更简洁一些
        //public TreeNode lowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        //{
        //    if (root == null || root == p || root == q) return root;
        //    TreeNode left = lowestCommonAncestor(root.left, p, q);
        //    TreeNode right = lowestCommonAncestor(root.right, p, q);
        //    return left == null ? right : right == null ? left : root;
        //}
    }
}
