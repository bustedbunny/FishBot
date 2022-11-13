using System.Diagnostics.CodeAnalysis;

namespace FishBot;

public static class RandomUtility
{
    private static readonly Random _rng = new Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = _rng.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }

    public static T? GetRandom<T>(this Dictionary<T, float> dict)
    {
        var shuffledList = dict.ToList();
        shuffledList.Shuffle();

        var rng = _rng.NextDouble();

        double cumulativeProbability = 0f;
        foreach (var val in shuffledList)
        {
            cumulativeProbability += val.Value;
            if (rng <= cumulativeProbability)
            {
                return val.Key;
            }
        }

        return default;
    }
}