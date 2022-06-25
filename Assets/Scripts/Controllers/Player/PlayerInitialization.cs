using RushingMachine.Controllers.Interfaces;
using RushingMachine.Data;
using RushingMachine.Entities.Cars.Move;
using RushingMachine.Entities.Cars.Weapon;
using RushingMachine.Entities.Interfaces;
using RushingMachine.Entities.Player;
using UnityEngine;

namespace RushingMachine.Controllers.Player
{
    public class PlayerInitialization : IInitialization
    {
        private readonly PlayerView _player;
        private IMove _move;
        private IWeapon _weapon;
        
        public IMove Move => _move;
        public IWeapon Weapon => _weapon;
        
        public PlayerInitialization(PlayerConfig playerConfig)
        {
            _player = Object.Instantiate(playerConfig.view);
            
            _move = new MoveTransform(_player.Transform, 10f);
            _weapon = new PlayerWeapon(_player.MineThrower, playerConfig.mineView);
        }

        public void Initialization()
        {
            
        }
    }
}