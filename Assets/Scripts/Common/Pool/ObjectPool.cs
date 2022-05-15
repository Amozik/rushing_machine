using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RushingMachine.Common.Pool
{
    internal sealed class ObjectPool<T> where T : Component
    {
        private readonly Stack<T> _stack = new Stack<T>();
        private readonly T _prefab;

        public ObjectPool(T prefab)
        {
            _prefab = prefab;
        }

        public void Push(T go)
        {
            _stack.Push(go);
            go.gameObject.SetActive(false);
        }

        public T Pop()
        {
            T go;
            if (_stack.Count == 0)
            {
                go = Object.Instantiate(_prefab);
            }
            else
            {
                go = _stack.Pop();
            }
            go.gameObject.SetActive(true);

            return go;
        }
    }
}