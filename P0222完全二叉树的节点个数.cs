using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // 这个研究点比较多，或者可以考虑发一篇论文
    // 题目本身可以用O(n)来解，但如果数据量大的话还是有做的价值的
    class P0222完全二叉树的节点个数
    {
        //// 官方题解：O(logN*logN)
        //public virtual int CountNodes(TreeNode root)
        //{
        //    if (root == null)
        //    {
        //        return 0;
        //    }
        //    int level = 0;
        //    TreeNode node = root;
        //    while (node.left != null)
        //    {
        //        level++;
        //        node = node.left;
        //    }
        //    int low = 1 << level, high = (1 << (level + 1)) - 1;
        //    while (low < high)
        //    {
        //        int mid = (high - low + 1) / 2 + low;
        //        if (Exists(root, level, mid))
        //        {
        //            low = mid;
        //        }
        //        else
        //        {
        //            high = mid - 1;
        //        }
        //    }
        //    return low;
        //}

        //public bool Exists(TreeNode root, int level, int k)
        //{
        //    int bits = 1 << (level - 1);
        //    TreeNode node = root;
        //    while (node != null && bits > 0)
        //    {
        //        if ((bits & k) == 0)
        //        {
        //            node = node.left;
        //        }
        //        else
        //        {
        //            node = node.right;
        //        }
        //        bits >>= 1;
        //    }
        //    return node != null;
        //}

        #region ver2 - O(logn)二分搜索法 - 2022/2/4
        // 15ms for 2147483647
        public int CountNodes_ver2(TreeNode root)
        {
            if (root == null) return 0;
            int l = 1, r = int.MaxValue;
            while (l < r)
            {
                int mid = l + (r - l + 1 >> 1);
                if (Find(root, mid) == null) r = mid - 1;
                else l = mid;
            }
            return l;
        }

        TreeNode Find(TreeNode root, int n)
        {
            if (n == 1) return root;
            var parent = Find(root, n >> 1); if (parent == null) return null;
            return (n & 1) == 0 ? parent.left : parent.right;
        }
        #endregion


        // O(n)递归算法
        public virtual int CountNodes(TreeNode root) => root == null ? 0 :
            CountNodes(root.left) + CountNodes(root.right) + 1;

        internal static void Run()
        {
            var obj = new P0222Runner();
            // O(n):65s, O(logN*logN): 0ms
            // 另外同样是O(n)，优化了存储空间后时间变成7s
            int n = 1000000000; // 145109ms
                                // 65536000; // 7s

            //Console.WriteLine("count=" + obj.CountNodes(new CompleteTreeNode(n))); 
            Console.WriteLine("count=" + obj.CountNodes_ver2(new CompleteTreeNode(n)));
            Console.WriteLine("count=" + obj.CountNodes_ver2(new CompleteTreeNode(2147483647)));
        }
    }
    
    class P0222Runner : P0222完全二叉树的节点个数
    {
        public override int CountNodes(TreeNode root)
        {
            if (root is CompleteTreeNode ctn)
            {
                using (ctn)
                {
                    return base.CountNodes(ctn);
                }
            }
            return base.CountNodes(root);
        }
    }

    class CompleteTreeNode : TreeNode, IDisposable
    {
        static Stack<CompleteTreeNode> recycle = new Stack<CompleteTreeNode>();

        int _count = 0;
        public CompleteTreeNode(int count)
        {
            _count = count;
            val = 1;
        }

        private static CompleteTreeNode CreateInstance(int count, int val)
        {
            CompleteTreeNode t = recycle.Any() ? recycle.Pop() : new CompleteTreeNode();
            t._count = count;
            t.val = val;
            return t;
        }
        private CompleteTreeNode() { }

        CompleteTreeNode _left, _right;
        public override TreeNode left
        {
            get
            {
                if (_left != null) return _left;
                if (val >= 1073741824 || (val << 1) > _count) return null;
                return _left = CreateInstance(_count, val << 1);
            }
            set { }
        }

        public override TreeNode right
        {
            get
            {
                if (_right != null) return _right;
                if (val >= 1073741824 || (val << 1) >= _count) return null;
                return _right = CreateInstance(_count, (val << 1) + 1);
            }
            set { }
        }

        public void Dispose()
        {
            _left = _right = null;
            recycle.Push(this);
        }
    }
}
