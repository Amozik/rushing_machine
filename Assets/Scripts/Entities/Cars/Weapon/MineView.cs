using System;
using RushingMachine.Entities.Views;
using UnityEngine;

namespace RushingMachine.Entities.Cars.Weapon
{
    public class MineView : BaseView
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        
        public event Action<MineView> BecameInvisibleEvent;

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