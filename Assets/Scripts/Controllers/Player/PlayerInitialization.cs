using RushingMachine.Controllers.Interfaces;
using RushingMachine.Data;
using RushingMachine.Entities.Cars.Move;
using RushingMachine.Entities.Interfaces;
using RushingMachine.Entities.Player;
using UnityEngine;

namespace RushingMachine.Controllers.Player
{
    public class PlayerInitialization : IInitialization
    {
        private readonly PlayerView _player;
        private IMove _move;
        
        public IMove Move => _move;
        
        public PlayerInitialization(PlayerConfig playerConfig)
        {
            _player = Object.Instantiate(playerConfig.view);
            
            _move = new MoveTransform(_player.transform, 10f);
        }

        public void Initialization()
        {
            
        }
    }
}