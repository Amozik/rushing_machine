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
            return self.GetRandomItem(out _);
        }
        
        public static T GetRandomItem<T>(this IEnumerable<T> self, out int index)
        {
            var enumerable = self as T[] ?? self.ToArray();
            index = Random.Range(0, enumerable.Length);
            return !enumerable.Any() ? default : enumerable[index];
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

        public static T GetRandomItemExcept<T>(this IEnumerable<T> self, IEnumerable<int> exceptIndexes, out int index)
        {
            var clearCollection = self
                .Select((item, i) => (item, i))
                .Where(item => !exceptIndexes.Contains(item.i));
            
            var (rItem, rIndex) = clearCollection.GetRandomItem();
            index = rIndex;
            return rItem;
        }

        public static T GetRandomItemWithWeight<T>(this IEnumerable<T> self, Func<T, float> weight)
        {
            var itemsContainer = self
                .Select(item => (Item: item, Weight: weight.Invoke(item)))
                .OrderByDescending(item => item.Weight)
                .ToArray();

            var total = itemsContainer.Sum(item => item.Weight);
            var randomPoint = Random.Range(0, total);
            
            foreach (var element in itemsContainer)
            {
                if (randomPoint < element.Weight) {
                    return element.Item;
                }

                randomPoint -= element.Weight;
            }

            return itemsContainer[itemsContainer.Length - 1].Item;
        }
    }
}