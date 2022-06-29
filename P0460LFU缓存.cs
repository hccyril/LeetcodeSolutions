using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/5/31
    // 链表+Dictionary
    internal class P0460LFU缓存
    {
        public class LFUCache
        {
            // ver3
            Dictionary<int, LinkedListNode<(int key, int val, int cnt)>> dic = new();
            int count, minCnt;
            List<LinkedList<(int key, int val, int cnt)>> list = new() { new(), new() };

            public LFUCache(int capacity)
            {
                count = capacity;
            }

            int UpdateCount(LinkedListNode<(int key, int val, int cnt)> node, int? value = null)
            {
                (int key, int val, int cnt) = node.Value;
                if (value != null) val = value.Value;
                list[cnt].Remove(node);
                if (!list[cnt].Any() && cnt == minCnt) ++minCnt;
                if (list.Count == ++cnt)
                    list.Add(new());
                list[cnt].AddLast((key, val, cnt));
                dic[key] = list[cnt].Last;
                return val;
            }

            public int Get(int key)
            {
                if (count == 0) return -1;
                //Console.WriteLine("get: " + key);
                if (dic.TryGetValue(key, out var node))
                    return UpdateCount(node);
                else
                    return -1;
            }

            public void Put(int key, int value)
            {
                if (count == 0) return;
                //Console.WriteLine("put: " + key);

                if (dic.ContainsKey(key))
                {
                    UpdateCount(dic[key], value);
                }
                else
                {
                    if (dic.Count == count)
                    {
                        dic.Remove(list[minCnt].First.Value.key);
                        list[minCnt].RemoveFirst();
                    }
                    list[1].AddLast((key, value, 1));
                    dic[key] = list[1].Last;
                    minCnt = 1;
                }

                //foreach (var v in dic.Values)
                //Console.Write(" ({0})={1}", v.Value.key, v.Value.cnt);
                //Console.WriteLine();
            }

            // ver2 各种问题
            //Dictionary<int, LinkedListNode<(int key, int val, int cnt)>> dic = new();
            //int count, minCnt;
            //List<LinkedList<(int key, int val, int cnt)>> list = new() { new(), new() };

            //public LFUCache(int capacity)
            //{
            //    count = capacity;
            //}

            //int UpdateCount(LinkedListNode<(int key, int val, int cnt)> node, int? value = null)
            //{
            //    (int key, int val, int cnt) = node.Value;
            //    if (value != null) val = value.Value;
            //    list[cnt].Remove(node);
            //    if (!list[cnt].Any() && cnt == minCnt) ++minCnt;
            //    if (list.Count == ++cnt)
            //        list.Add(new());
            //    list[cnt].AddLast((key, val, cnt));
            //    return val;
            //}

            //public int Get(int key)
            //{
            //    if (dic.TryGetValue(key, out var node))
            //        return UpdateCount(node);
            //    else
            //        return -1;
            //}

            //public void Put(int key, int value)
            //{
            //    if (dic.ContainsKey(key))
            //    {
            //        UpdateCount(dic[key], value);
            //    }
            //    else
            //    {
            //        if (dic.Count == count)
            //            list[minCnt].RemoveFirst();
            //        list[1].AddLast((key, value, 1));
            //        dic[key] = list[1].Last;
            //        minCnt = 1;
            //    }
            //}

            // ver1 写到中间才发现单链表写不下去，要双链表才行 

            //class NodeStruct
            //{
            //    public int key, val, cnt;
            //    public NodeStruct next;
            //}

            //class ListStruct
            //{
            //    public NodeStruct head, tail;
            //    public void AddTail(NodeStruct node)
            //    {
            //        if (head == null)
            //        {
            //            head = tail = node;
            //        }
            //        else
            //        {
            //            tail.next = node;
            //            tail = node;
            //        }
            //    }
            //}

            //public class LFUCache
            //{
            //    Dictionary<int, NodeStruct> dic = new();
            //    int count, minCnt;
            //    List<ListStruct> list = new() { new(), new() };

            //    public LFUCache(int capacity)
            //    {
            //        count = capacity;
            //    }

            //    int UpdateCount(NodeStruct node)
            //    {
            //        if (list.Count == ++node.cnt)
            //            list.Add(new());

            //        return node.val;
            //    }

            //    void RemoveOne()
            //    {

            //    }

            //    public int Get(int key)
            //    {
            //        if (dic.TryGetValue(key, out var node))
            //            return UpdateCount(node);
            //        else
            //            return -1;
            //    }

            //    public void Put(int key, int value)
            //    {
            //        if (dic.ContainsKey(key))
            //        {
            //            dic[key].val = value;
            //            UpdateCount(dic[key]);
            //        }
            //        else
            //        {
            //            if (dic.Count == count)
            //                RemoveOne();
            //            list[1].AddTail(dic[key] = new() { key = key, val = value, cnt = 1 });
            //            minCnt = 1;
            //        }
            //    }
        }
    }
}
