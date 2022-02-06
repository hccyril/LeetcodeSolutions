using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0863二叉树中所有距离为_K_的结点
    {
        int _target, _k;
        IList<int> _ans;
        int[] _dic;
        void Scan(TreeNode root)
        {
            if (root == null) return;

            if (root.val == _target)
            {
                _dic[root.val] = 1;
            }
            else if (_dic[root.val] == 0)
            {
                _dic[root.val] = -1;

                Scan(root.left);
                if (root.left != null && _dic[root.left.val] > 0)
                {
                    _dic[root.val] = _dic[root.left.val] + 1;
                }
                else
                {
                    Scan(root.right);
                    if (root.right != null && _dic[root.right.val] > 0)
                    {
                        _dic[root.val] = _dic[root.right.val] + 1;
                    }
                }
            }

            if (_dic[root.val] >= _k)
            {
                if (_dic[root.val] == _k)
                    _ans.Add(root.val);
            }
            else if (_dic[root.val] > 0)
            {
                if (root.left != null && _dic[root.left.val] <= 0)
                {
                    _dic[root.left.val] = _dic[root.val] + 1;
                    Scan(root.left);
                }
                if (root.right != null && _dic[root.right.val] <= 0)
                {
                    _dic[root.right.val] = _dic[root.val] + 1;
                    Scan(root.right);
                }
            }
        }
        public IList<int> DistanceK(TreeNode root, TreeNode target, int k)
        {
            _target = target.val; _k = k + 1;
            _dic = new int[501];
            _ans = new List<int>();
            Scan(root);
            return _ans;
        }
    }
}
