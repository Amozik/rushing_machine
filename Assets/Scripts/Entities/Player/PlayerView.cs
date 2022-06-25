using RushingMachine.Entities.Cars;
using UnityEngine;

namespace RushingMachine.Entities.Player
{
    public class PlayerView : CarView
    {
        [SerializeField]
        private Transform _mineThrower;
        public Transform MineThrower => _mineThrower;
        
    }
}