using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /** 431
     * 设计一个算法，可以将 N 叉树编码为二叉树，并能将该二叉树解码为原 N 叉树。
     * 一个 N 叉树是指每个节点都有不超过 N 个孩子节点的有根树。类似地，
     * 一个二叉树是指每个节点都有不超过 2 个孩子节点的有根树。
     * 你的编码 / 解码的算法的实现没有限制，你只需要保证一个 N 叉树可以编码为二叉树
     * 且该二叉树可以解码回原始 N 叉树即可。
     * */

    // hard(??), plus, 2022/3/1
    // N叉树转二叉树：左儿子，右兄弟
    internal class P0431将N叉树编码为二叉树
    {
    }
    
    // Definition for a Node.
    public class Node {
        public int val;
        public IList<Node> children;

        public Node() {}

        public Node(int _val) {
            val = _val;
        }

        public Node(int _val, IList<Node> _children) {
            val = _val;
            children = _children;
        }
    }

    /**
     * Definition for a binary tree node.
     * public class TreeNode {
     *     public int val;
     *     public TreeNode left;
     *     public TreeNode right;
     *     public TreeNode(int x) { val = x; }
     * }
     */

    public class Codec
    {
        // Encodes an n-ary tree to a binary tree.
        public TreeNode encode(Node root)
        {
            if (root == null) return null;
            TreeNode cur = null, tn = new(root.val);
            foreach (var cld in root.children ?? new List<Node>())
            {
                if (cur == null)
                    cur = tn.left = encode(cld);
                else cur = cur.right = encode(cld);
            }
            return tn;
        }

        // Decodes your binary tree to an n-ary tree.
        public Node decode(TreeNode root)
        {
            if (root == null) return null;
            Node nd = new(root.val, new List<Node>());
            for (var cld = root.left; cld != null; cld = cld.right)
                nd.children.Add(decode(cld));
            return nd;
        }
    }

    // Your Codec object will be instantiated and called as such:
    // Codec codec = new Codec();
    // codec.decode(codec.encode(root));
}
