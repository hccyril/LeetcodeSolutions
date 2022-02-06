using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
     * 401. 二进制手表
二进制手表顶部有 4 个 LED 代表 小时（0-11），底部的 6 个 LED 代表 分钟（0-59）。每个 LED 代表一个 0 或 1，最低位在右侧。

例如，下面的二进制手表读取 "3:25" 。
    给你一个整数 turnedOn ，表示当前亮着的 LED 的数量，返回二进制手表可以表示的所有可能时间。你可以 按任意顺序 返回答案。

小时不会以零开头：

例如，"01:00" 是无效的时间，正确的写法应该是 "1:00" 。
分钟必须由两位数组成，可能会以零开头：

例如，"10:2" 是无效的时间，正确的写法应该是 "10:02" 。
 

示例 1：

输入：turnedOn = 1
输出：["0:01","0:02","0:04","0:08","0:16","0:32","1:00","2:00","4:00","8:00"]
示例 2：

输入：turnedOn = 9
输出：[]
 

解释：

0 <= turnedOn <= 10

来源：力扣（LeetCode）
链接：https://leetcode-cn.com/problems/binary-watch
著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。
     * */

    class P0401二进制手表
    {
        int[] nums = new int[] { 8, 4, 2, 1, 32, 16, 8, 4, 2, 1 };

        void Rec(IList<string> list, int i, int on, int hour, int minute)
        {
            if (i >= 10)
            {
                if (on == 0 && hour < 12 && minute < 60)
                {
                    list.Add($"{hour}:{minute:D2}");
                }
                return;
            }

            // i is OFF
            if (i < 10 - on)
                Rec(list, i + 1, on, hour, minute);
            // i is ON
            if (on > 0)
            {
                if (i < 4)
                {
                    if (hour + nums[i] < 12)
                        Rec(list, i + 1, on - 1, hour + nums[i], minute);
                }
                else 
                {
                    if (minute + nums[i] < 60)
                        Rec(list, i + 1, on - 1, hour, minute + nums[i]);
                }
            }
        }

        public IList<string> ReadBinaryWatch(int turnedOn)
        {
            IList<string> list = new List<string>();
            Rec(list, 0, turnedOn, 0, 0);
            return list;
        }
    }
}
