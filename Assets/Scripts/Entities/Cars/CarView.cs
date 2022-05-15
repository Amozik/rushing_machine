using RushingMachine.Entities.Interfaces;
using RushingMachine.Entities.Views;
using UnityEngine;

namespace RushingMachine.Entities.Cars
{
    public class CarView : BaseView
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
    }
}