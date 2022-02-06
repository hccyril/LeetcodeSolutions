using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0297二叉树的序列化与反序列化
    {
        

        // Encodes a tree to a single string.
        public string serialize(TreeNode root)
        {
            if (root == null) return "null";
            List<TreeNode> tl = new List<TreeNode>();
            List<int> list = new List<int>();
            tl.Add(root);
            list.Add(0);
            list.Add(root.val);

            for (int i = 0; i < tl.Count; ++i)
            {
                var tn = tl[i];
                int key = i + 1;
                if (tn.left != null)
                {
                    tl.Add(tn.left);
                    list.Add(-key);
                    list.Add(tn.left.val);
                }
                if (tn.right != null)
                {
                    tl.Add(tn.right);
                    list.Add(key);
                    list.Add(tn.right.val);
                }
            }
            return string.Join(",", list);
        }

        // Decodes your encoded data to tree.
        public TreeNode deserialize(string data)
        {
            if (data.StartsWith("null")) return null;
            TreeNode root = null;
            string[] split = data.Split(',');
            int i = 0;
            List<TreeNode> tl = new List<TreeNode>();

            while (i < split.Length)
            {
                int key = int.Parse(split[i++]);
                int val = int.Parse(split[i++]);
                TreeNode tn = new TreeNode(val);
                tl.Add(tn);
                if (key == 0) root = tn;
                else
                {
                    bool isLeft;
                    TreeNode par;
                    if (key < 0)
                    {
                        isLeft = true;
                        par = tl[-key - 1];
                    }
                    else
                    {
                        isLeft = false;
                        par = tl[key - 1];
                    }
                    if (isLeft) par.left = tn;
                    else par.right = tn;
                }
            }
            return root;
        }

        // ver1: key has exceed 2^32
        //// Encodes a tree to a single string.
        //public string serialize(TreeNode root)
        //{
        //    if (root == null) return "null";
        //    List<int> list = new List<int>();
        //    SortedList<int, TreeNode> sl = new SortedList<int, TreeNode>();
        //    sl.Add(1, root);

        //    while (sl.Any())
        //    {
        //        var kv = sl.First();
        //        sl.RemoveAt(0);
        //        list.Add(kv.Key);
        //        list.Add(kv.Value.val);

        //        if (kv.Value.left != null) sl.Add(kv.Key << 1, kv.Value.left);
        //        if (kv.Value.right != null) sl.Add((kv.Key << 1) + 1, kv.Value.right);
        //    }
        //    return string.Join(",", list);
        //}

        //// Decodes your encoded data to tree.
        //public TreeNode deserialize(string data)
        //{
        //    if (data.StartsWith("null")) return null;
        //    TreeNode root = null;
        //    string[] split = data.Split(',');
        //    int i = 0;
        //    SortedList<int, TreeNode> sl = new SortedList<int, TreeNode>();
        //    while (i < split.Length)
        //    {
        //        int key = int.Parse(split[i++]);
        //        int val = int.Parse(split[i++]);
        //        TreeNode tn = new TreeNode(val);
        //        sl.Add(key, tn);
        //        if (key == 1) root = tn;
        //        else
        //        {
        //            var par = sl[key >> 1];
        //            if ((key & 1) == 0) par.left = tn;
        //            else par.right = tn;
        //        }
        //    }
        //    return root;
        //}
    }
}
