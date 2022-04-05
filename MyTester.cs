using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class MyTester
    {
        internal static void DicTest()
        {
            HashSet<int> ps = new() { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31 };
            Dictionary<string, int> dic = new();
            dic["hello"]++;
            foreach (var item in dic) Console.WriteLine(item.Key);
            SortedDictionary<int, string> sort = new();
            SortedSet<string> ss = new();
            //ss.GetViewBetween()
            
        }
        int MyCompare(int a, int b)
        {
            //var p = new ConsoleCore1.CompleteTreeNode
            return b - a;
        }
        internal void Test()
        {
            int[] arr1 = { 1, 2, 3 };
            int[][] arr = { arr1 }; //Array.Empty<int[]>();
            var it = arr.OrderBy(t => t.First()).GetEnumerator();
            Console.WriteLine(it.Current);
            it.MoveNext();
            Console.WriteLine(it.Current);
            //SortedDictionary<int, int> sd = new();
            
            //List<int> list = new();
            //list.BinarySearch()
            //Array.BinarySearch(sd.Keys, 80);
            //int[] arr = new int[100];
            //arr.GetLowerBound() // different from C++'s lower_bound
            //IComparer<int> ic;
            //Span<int> sp = stackalloc int[10000];
            //sp.BinarySearch(80, MyCompare);
            //TreeList<int> tl = new();

            //Stack<char> s = new Stack<char>();
            //s.Pop();
            //s.Count();

            //from i in System.Linq.Enumerable.Range(1,n)
            //// test
            //int[] arr = { };
            //arr.tolist
            //string str = "aaa";
            //SortedSet<int> ss = new SortedSet<int>();
            //int hr = 3, mi = 5;
            //Console.WriteLine($"{hr}:{mi:D2}");
            //List<char> list = new List<char>() { 'a', 'c', 'd' };
            //string s = new string(list.ToArray());
            //Console.WriteLine(s);
            //Array.BinarySearch() // 例如返回-6，表示应该插入5的位置
            //string s = "abc"; char[] ca = s.ToCharArray(); 
            //TestClass.Test1();
        }

        // https://ac.nowcoder.com/acm/problem/13134
        internal static void NC13134()
        {
            int n = 6;
            int[] nums = { 7, 2, 6, 4, 5, 6 };
            int len = 0, start = 0, a = -1, prev = -1, pv = -1, k = -1, kv = -1;
            for (int i = 0; i < n; ++i)
            {
                a = nums[i];
                while (a <= prev)
                {
                    if (k >= 0)
                    {
                        start = k;
                        if (k + 1 == i) prev = kv;
                        k = -1;
                    }
                    else
                    {
                        if (a > pv)
                        {
                            k = i;
                            kv = a;
                            prev = pv;
                        }
                        else
                        {
                            k = i;
                            kv = a;
                            a = prev + 1;
                        }
                    }
                }
                /*
                if (k == -1 && a > prev + 1) {
                    k = i;
                    kv = a;
                    a = prev + 1;
                }*/
                len = Math.Max(len, i - start + 1);
                pv = prev + 1;
                prev = a;
            }
            Console.WriteLine("result=" + len);
        }
    }
}
