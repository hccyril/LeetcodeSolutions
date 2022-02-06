using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // TODO 2021/12/09 做到跳跃游戏（jump game）系列顺带做
    // BFS超时，要考虑用动态规划
    internal class P1871跳跃游戏VII
    {
        public bool CanReach(string s, int minJump, int maxJump)
        {
            throw new NotImplementedException();
        }
        /* 官方题解（C++）
        class Solution {
        public:
            bool canReach(string s, int minJump, int maxJump) {
                 int n = s.size();
                vector<int> f(n), pre(n);
                f[0] = 1;
                // 由于我们从 i=minJump 开始动态规划，因此需要将 [0,minJump) 这部分的前缀和预处理出来
                for (int i = 0; i < minJump; ++i) {
                    pre[i] = 1;
                }
                for (int i = minJump; i < n; ++i) {
                    int left = i - maxJump, right = i - minJump;
                    if (s[i] == '0') {
                        int total = pre[right] - (left <= 0 ? 0 : pre[left - 1]);
                        f[i] = (total != 0);
                    }
                    pre[i] = pre[i - 1] + f[i];
                }
                return f[n - 1];       
            }
        };         
         * */
    }
}
