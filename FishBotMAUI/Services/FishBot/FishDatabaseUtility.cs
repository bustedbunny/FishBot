using FishBot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishBotMAUI.Services.FishBot
{

    public static class FishDatabaseUtility
    {
        private static readonly Random _rng = new();

        private static List<FishRecord> _cachedList;
        private static int _cacheVersion = -1;
        public static string GetRandom(this FishingDatabase database)
        {
            if (_cacheVersion != database.Version)
            {
                _cachedList = database.Content.ToList();
                _cacheVersion = database.Version;
            }

            _cachedList.Shuffle();

            var rng = _rng.NextDouble();

            double cumulativeProbability = 0f;
            foreach (var val in _cachedList)
            {
                cumulativeProbability += val.Probability;
                if (rng <= cumulativeProbability)
                {
                    return val.Name;
                }
            }

            return default;
        }
        public static void Shuffle<T>(this List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = _rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}
