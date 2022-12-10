using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // 2021/12/09 做到跳跃游戏（jump game）系列顺带做
    // BFS超时，要考虑用动态规划
    // 2022/12/10 滑动窗口
    internal class P1871跳跃游戏VII
    {
        public bool CanReach(string s, int minJump, int maxJump)
        {
            BitArray ba = new(s.Length);
            ba[0] = true;
            int cnt = 0;
            for (int i = 1; i < s.Length; ++i)
            {
                int l = i - maxJump, r = i - minJump;
                if (r >= 0 && ba[r]) ++cnt;
                if (l > 0 && ba[l - 1]) --cnt;
                ba[i] = s[i] == '0' && cnt > 0;
            }
            return ba[s.Length - 1];
        }

        // ver1: 超时
        //string s;
        //bool[] reached;
        //Queue<int> qu;
        //bool Step(int i)
        //{
        //    if (!reached[i] && s[i] == '0')
        //    {
        //        reached[i] = true;
        //        if (i == s.Length - 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            qu.Enqueue(i);
        //        }
        //    }
        //    return false;
        //}
        //public bool CanReach(string s, int minJump, int maxJump)
        //{
        //    this.s = s;
        //    if (s.Length == 1) return true;
        //    if (s[s.Length - 1] != '0') return false;
        //    reached = new bool[s.Length];
        //    reached[0] = true;
        //    qu = new();
        //    qu.Enqueue(0);
        //    while (qu.Any())
        //    {
        //        int start = qu.Dequeue();
        //        for (int end = start + minJump; end < s.Length && end <= start + maxJump; ++end)
        //            if (Step(end)) return true;
        //    }
        //    return false;
        //}
    }
}
