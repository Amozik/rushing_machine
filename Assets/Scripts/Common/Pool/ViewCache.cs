using System.Collections.Generic;
using UnityEngine;

namespace RushingMachine.Common.Pool
{
    public class ViewCache<TKey, TValue> where TValue : Component
    {
        private readonly Dictionary<TKey, ObjectPool<TValue>> _viewCache;

        public ViewCache(int capacity)
        {
            _viewCache = new Dictionary<TKey, ObjectPool<TValue>>(capacity);
        }
        
        public TValue Create(TKey id, TValue prefab)
        {
            if (!_viewCache.TryGetValue(id, out var viewPool))
            {
                viewPool = new ObjectPool<TValue>(prefab);
                _viewCache[id] = viewPool;
            }

            return viewPool.Pop();
        }

        public void Destroy(TKey id, TValue gameObject)
        {
            _viewCache[id].Push(gameObject); 
        }
    }
}