using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// Medium, 2023/12/17 US Daily
internal class P2353设计食物评分系统
{
}

public class FoodRatings
{
    readonly Dictionary<string, SortedSet<(int, string)>> sd = new();
    readonly Dictionary<string, (int, string)> di = new();

    public FoodRatings(string[] foods, string[] cuisines, int[] ratings)
    {
        for (int i = 0; i < foods.Length; ++i)
        {
            di[foods[i]] = (ratings[i], cuisines[i]);
            sd.TryAdd(cuisines[i], new());
            sd[cuisines[i]].Add((-ratings[i], foods[i]));
        }
    }

    public void ChangeRating(string food, int newRating)
    {
        (int ra, string cs) = di[food];
        sd[cs].Remove((-ra, food));
        sd[cs].Add((-newRating, food));
        di[food] = (newRating, cs);
    }

    public string HighestRated(string cuisine)
        => sd[cuisine].Min.Item2;
}
