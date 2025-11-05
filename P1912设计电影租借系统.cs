using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2025/9/21 Daily
// rating 2182
// OOP, priority queue
internal class P1912设计电影租借系统
{
}

class MovieRentingSystem
{
    readonly int[][] allData;
    readonly Dictionary<int, PriorityQueue<int, (int, int)>> dic_movie = new(); // <movie, <index, (price, shop)>>
    readonly Dictionary<(int, int), int> dic_shopmovie = new(); // <(shop, movie), index>
    readonly HashSet<int> rentSet = new();
    readonly PriorityQueue<int, (int, int, int)> rentPQ = new();
    public MovieRentingSystem(int n, int[][] entries)
    {
        allData = entries;
        for (int i = 0; i < entries.Length; ++i)
        {
            int shop = allData[i][0], movie = allData[i][1], price = allData[i][2];
            dic_shopmovie[(shop, movie)] = i;
            if (!dic_movie.TryGetValue(movie, out var pq))
            {
                pq = new();
                dic_movie[movie] = pq;
            }
            pq.Enqueue(i, (price, shop));
        }
    }

    // 从PriorityQueue中抽出5个有效结果，得到结果之后再添加回PQ（后面Report的做法同理）
    public IList<int> Search(int movie)
    {
        if (!dic_movie.TryGetValue(movie, out var pq)) 
            return [];
        int last_i = -1;
        List<int> idList = new();
        while (pq.Count > 0 && idList.Count < 5)
        {
            int i = pq.Dequeue(); if (i == last_i) continue; last_i = i;
            if (!rentSet.Contains(i))
                idList.Add(i);
        }
        foreach (int i in idList)
        {
            int shop = allData[i][0], price = allData[i][2];
            pq.Enqueue(i, (price, shop));
        }
        return idList.Select(i => allData[i][0]).ToArray();
    }

    public void Rent(int shop, int movie)
    {
        int i = dic_shopmovie[(shop, movie)];
        int price = allData[i][2];
        rentSet.Add(i);
        rentPQ.Enqueue(i, (price, shop, movie));
    }

    public void Drop(int shop, int movie)
    {
        int i = dic_shopmovie[(shop, movie)];
        rentSet.Remove(i);
        int price = allData[i][2];
        dic_movie[movie].Enqueue(i, (price, shop));
    }

    public IList<IList<int>> Report()
    {
        List<int> idList = new();
        int last_i = -1;
        while (rentPQ.Count > 0 && idList.Count < 5)
        {
            int i = rentPQ.Dequeue(); if (i == last_i) continue; last_i = i;
            if (rentSet.Contains(i)) 
                idList.Add(i);
        }
        foreach (int i in idList)
        {
            int shop = allData[i][0], movie = allData[i][1], price = allData[i][2];
            rentPQ.Enqueue(i, (price, shop, movie));
        }
        return idList.Select(i => new int[] { allData[i][0], allData[i][1] }).ToArray();
    }
}
