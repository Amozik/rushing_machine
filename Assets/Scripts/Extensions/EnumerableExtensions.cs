using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace RushingMachine.Extensions
{
    public static class EnumerableExtensions
    {
        public static T GetRandomItem<T>(this IEnumerable<T> self)
        {
            var enumerable = self as T[] ?? self.ToArray();
            return !enumerable.Any() ? default : enumerable[Random.Range(0, enumerable.Length)];
        }
        
        public static T GetRandomItemExcept<T>(this IEnumerable<T> self, IEnumerable<T> exceptElements)
        {
            var clearCollection = from item in self
                where !exceptElements.Contains(item)
                select item;

            return clearCollection.GetRandomItem();
        }
        
        public static T GetRandomItemExcept<T>(this IEnumerable<T> self, T exceptElement)
        {
            return self.GetRandomItemExcept(new [] {exceptElement});
        }
    }
}