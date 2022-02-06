using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
     * 752. 打开转盘锁
     * https://leetcode-cn.com/problems/open-the-lock/
你有一个带有四个圆形拨轮的转盘锁。每个拨轮都有10个数字： '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' 。每个拨轮可以自由旋转：例如把 '9' 变为 '0'，'0' 变为 '9' 。每次旋转都只能旋转一个拨轮的一位数字。

锁的初始数字为 '0000' ，一个代表四个拨轮的数字的字符串。

列表 deadends 包含了一组死亡数字，一旦拨轮的数字和列表里的任何一个元素相同，这个锁将会被永久锁定，无法再被旋转。

字符串 target 代表可以解锁的数字，你需要给出最小的旋转次数，如果无论如何不能解锁，返回 -1。

 

示例 1:

输入：deadends = ["0201","0101","0102","1212","2002"], target = "0202"
输出：6
解释：
可能的移动序列为 "0000" -> "1000" -> "1100" -> "1200" -> "1201" -> "1202" -> "0202"。
注意 "0000" -> "0001" -> "0002" -> "0102" -> "0202" 这样的序列是不能解锁的，
因为当拨动到 "0102" 时这个锁就会被锁定。
示例 2:

输入: deadends = ["8888"], target = "0009"
输出：1
解释：
把最后一位反向旋转一次即可 "0000" -> "0009"。
示例 3:

输入: deadends = ["8887","8889","8878","8898","8788","8988","7888","9888"], target = "8888"
输出：-1
解释：
无法旋转到目标数字且不被锁定。
示例 4:

输入: deadends = ["0000"], target = "8888"
输出：-1
 

提示：

死亡列表 deadends 的长度范围为 [1, 500]。
目标数字 target 不会在 deadends 之中。
每个 deadends 和 target 中的字符串的数字会在 10,000 个可能的情况 '0000' 到 '9999' 中产生。
     * */
    class P0752打开转盘锁
    {
        public int OpenLock(string[] deadends, string target)
        {
            if (target == "0000") return 0;
            int[] arr = new int[10000];
            int[] nums = new int[8];
            Queue<int> qu = new Queue<int>();
            foreach (var ds in deadends)
            {
                string s = ds.TrimStart('0'); if (s == "") s = "0";
                arr[int.Parse(s)] = -1;
            }
            target = target.TrimStart('0');
            int ntarget = target == "" ? 0 : int.Parse(target);
            if (arr[0] == -1) return -1;
            qu.Enqueue(0);
            while (qu.Any())
            {
                var m = qu.Dequeue();
                var move = arr[m] + 1;

                // 1-2
                int n = m + 1000; if (n >= 10000) n -= 10000; nums[0] = n;
                n = m >= 1000 ? m - 1000 : m + 9000; nums[1] = n;

                // 3-4
                int dn = m % 1000;
                int up = m - dn;
                n = dn + 100; if (n >= 1000) n -= 1000; nums[2] = n + up;
                n = dn >= 100 ? m - 100 : m + 900; nums[3] = n;

                // 5-6
                dn = m % 100;
                up = m - dn;
                n = dn + 10; if (n >= 100) n -= 100; nums[4] = n + up;
                n = dn >= 10 ? dn - 10 : dn + 90; nums[5] = n + up;

                // 7-8
                dn = m % 10;
                up = m - dn;
                n = dn + 1; if (n >= 10) n -= 10; nums[6] = n + up;
                n = dn >= 1 ? dn - 1 : dn + 9; nums[7] = n + up;

                foreach (var num in nums)
                {
                    if (num == ntarget) return move;
                    else if (arr[num] == 0)
                    {
                        arr[num] = move;
                        qu.Enqueue(num);
                    }
                }
            }
            return -1;
        }
    }
}
