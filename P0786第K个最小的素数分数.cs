using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard
    // 最原始『第k个』系列堆算法：面试题17.14 https://leetcode-cn.com/problems/smallest-k-lcci/
    // 堆排序居然过了，直接给排序好的素数，感觉应该是有更好的解法
    internal class P0786第K个最小的素数分数
    {
        class Fraction : IComparable<Fraction>
        {
            int n, m; // value = n / m
            public Fraction(int n, int m)
            {
                this.n = n; this.m = m;
            }
            public int[] ToArray() => new int[] { n, m };
            public int CompareTo(Fraction other) => n * other.m - m * other.n;
        }
        public int[] KthSmallestPrimeFraction(int[] arr, int k)
        {
            Heap<Fraction> hp = new((a, b) => a.CompareTo(b) > 0);
            for (int i = 0; i < arr.Length; ++i)
                for (int j = i + 1; j < arr.Length; ++j)
                {
                    var f = new Fraction(arr[i], arr[j]);
                    if (hp.Count < k || hp.Head.CompareTo(f) > 0)
                    {
                        if (hp.Count == k) hp.Pop();
                        hp.Push(f);
                    }
                }
            return hp.Head.ToArray();
        }
    }
}
