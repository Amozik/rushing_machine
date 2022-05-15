using System;
using RushingMachine.Entities.Cars;
using UnityEngine;

namespace RushingMachine.Entities.Enemies
{
    public class TrafficCarView : CarView
    {
        public event Action<TrafficCarView> BecameInvisibleEvent;

        private bool _isSeen;

        private void Update()
        {
            if (!_isSeen && SpriteRenderer.isVisible)
            {
                _isSeen = true;
                //Debug.Log("became visible");
                return;
            }

            if (_isSeen && !SpriteRenderer.isVisible)
            {
                _isSeen = false;
                BecameInvisibleEvent?.Invoke(this);
                
                //Debug.Log("became invisible");
            }
        }
    }
}