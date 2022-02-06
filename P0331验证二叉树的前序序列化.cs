using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0331验证二叉树的前序序列化
    {
        string[] arr;
        int Deserial(int i)
        {
            if (i < 0 || i >= arr.Length) return -1;
            var node = arr[i];
            if (node == "#") return i + 1;
            else
            {
                int ret = Deserial(i + 1);
                if (ret == -1) return -1;
                return Deserial(ret);
            }
        }
        public bool IsValidSerialization(string preorder)
        {
            arr = preorder.Split(',');
            return Deserial(0) == arr.Length;
        }
    }
}
