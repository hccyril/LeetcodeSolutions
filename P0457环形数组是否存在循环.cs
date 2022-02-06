using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0457环形数组是否存在循环
    {
        enum State
        {
            无效回路 = -1,
            当前回路 = -2,
            正向回路 = -3,
            负向回路 = -4,
            有效回路 = -5
        }
        int N;
        int[] arr;
        int[] nums;
        int Move(int i)
        {
            while (i >= N) i -= N;
            while (i < 0) i += N;
            return i;
        }
        int Dfs(int i)
        {
            if (arr[i] < 0)
            {
                if (arr[i] == (int)State.当前回路)
                {
                    // 当前i为循环的起点，做初始标记
                    arr[i] = nums[i] > 0 ? (int)State.正向回路 : (int)State.负向回路;
                }
                else arr[i] = (int)State.无效回路;
                return arr[i];
            }
            int nextI = arr[i];
            arr[i] = (int)State.当前回路;
            int next = Dfs(nextI);
            switch (next)
            {
                case (int)State.正向回路:
                case (int)State.负向回路:
                    if (arr[i] == next)
                        return arr[i] = (int)State.有效回路;
                    else
                        return arr[i] = (nums[i] > 0) == (next == (int)State.正向回路) ? next : (int)State.无效回路;
                default:
                    return next;
            }
        }
        public bool CircularArrayLoop(int[] nums)
        {
            this.nums = nums;
            N = nums.Length;
            arr = new int[N];
            for (int i = 0; i < N; ++i)
            {
                arr[i] = Move(i + nums[i]);
                if (arr[i] == i) arr[i] = (int)State.无效回路;
            }
            for (int i = 0; i < N; ++i)
                if (Dfs(i) == (int)State.有效回路)
                    return true;
            return false;
        }
    }
}
