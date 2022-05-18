using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/5/8 US Daily

    public interface NestedInteger
    {

        // @return true if this NestedInteger holds a single integer, rather than a nested list.
        bool IsInteger();

        // @return the single integer that this NestedInteger holds, if it holds a single integer
        // Return null if this NestedInteger holds a nested list
        int GetInteger();

        // @return the nested list that this NestedInteger holds, if it holds a nested list
        // Return null if this NestedInteger holds a single integer
        IList<NestedInteger> GetList();
    }

    public class NestedIterator
    {
        Stack<(IList<NestedInteger>, int)> stk = new();
        int nextValue;

        public NestedIterator(IList<NestedInteger> nestedList)
        {
            stk.Push((nestedList, -1));
            GoNext();
        }

        public bool HasNext()
        {
            return stk.Any();
        }

        public int Next()
        {
            int ans = nextValue;
            GoNext();
            return ans;
        }

        void GoNext()
        {
            while (stk.Any())
            {
                (IList<NestedInteger> list, int i) = stk.Pop();

                if (++i < list.Count)
                {
                    stk.Push((list, i));

                    var n = list[i];
                    while (!n.IsInteger() && n.GetList().Any())
                    {
                        stk.Push((n.GetList(), 0));
                        n = n.GetList()[0];
                    }
                    if (n.IsInteger())
                    {
                        nextValue = n.GetInteger();
                        break;
                    }
                }
            }
        }
    }

    internal class P0341扁平化嵌套列表迭代器
    {
    }

    // 初步原因查明：给的NestedInteger应该不是普通的interface，题目也明确说了不允许implement这个接口
    // ver1: 怎么试都有问题
    // Console 输出如下：
    /*
    init count=3
    NestStruct count=3
    n.Count=3
    Init Push stk=1, count=0
    GoNext stk=1, count=0
    pop: -1, count=0
     * */
    /**
     * // This is the interface that allows for creating nested lists.
     * // You should not implement it, or speculate about its implementation
     * interface NestedInteger {
     *
     *     // @return true if this NestedInteger holds a single integer, rather than a nested list.
     *     bool IsInteger();
     *
     *     // @return the single integer that this NestedInteger holds, if it holds a single integer
     *     // Return null if this NestedInteger holds a nested list
     *     int GetInteger();
     *
     *     // @return the nested list that this NestedInteger holds, if it holds a nested list
     *     // Return null if this NestedInteger holds a single integer
     *     IList<NestedInteger> GetList();
     * }
     */
    //public class NestedIterator
    //{
    //    Stack<(NestedInteger, int)> stk = new();

    //    class NestStruct : NestedInteger
    //    {
    //        public List<NestedInteger> list = new();
    //        public bool IsInteger() => false;
    //        public int GetInteger() => 0;
    //        public IList<NestedInteger> GetList() => list;
    //    }

    //    public NestedIterator(IList<NestedInteger> nestedList)
    //    {
    //        Console.WriteLine("init count=" + nestedList.Count);
    //        NestStruct n = new();
    //        n.list.AddRange(nestedList);
    //        Console.WriteLine("NestStruct count=" + n.GetList().Count);

    //        stk.Push((n, -1));
    //        Console.WriteLine("n.Count=" + n.GetList().Count);
    //        Console.WriteLine("Init Push stk=" + stk.Count + ", count=" + stk.Peek().Item1.GetList().Count);
    //        GoNext();
    //    }

    //    public bool HasNext()
    //    {
    //        return stk.Any();
    //    }

    //    public int Next()
    //    {
    //        (_, int ans) = stk.Pop();
    //        GoNext();
    //        return ans;
    //    }

    //    void GoNext()
    //    {
    //        Console.WriteLine("GoNext stk=" + stk.Count + ", count=" + stk.Peek().Item1.GetList().Count);
    //        while (stk.Any())
    //        {
    //            (NestedInteger n, int i) = stk.Pop();

    //            Console.WriteLine("pop: " + i + ", count=" + n.GetList().Count);

    //            if (++i < n.GetList().Count)
    //            {
    //                stk.Push((n, i));

    //                Console.WriteLine("push: " + i);

    //                n = n.GetList()[i];
    //                while (!n.IsInteger())
    //                {
    //                    stk.Push((n, 0));

    //                    Console.WriteLine("push: " + 0);

    //                    n = n.GetList()[0];
    //                }
    //                stk.Push((n, n.GetInteger()));

    //                Console.WriteLine("push: " + stk.Peek().Item2);

    //                break;
    //            }
    //        }
    //    }
    //}

    /**
     * Your NestedIterator will be called like this:
     * NestedIterator i = new NestedIterator(nestedList);
     * while (i.HasNext()) v[f()] = i.Next();
     */
}
