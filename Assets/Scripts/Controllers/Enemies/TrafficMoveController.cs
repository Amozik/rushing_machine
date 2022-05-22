using RushingMachine.Controllers.Interfaces;
using RushingMachine.Entities.Interfaces;
using UnityEngine;

namespace RushingMachine.Controllers.Enemies
{
    public class TrafficMoveController : IUpdate
    {
        private readonly IMove _move;
        private Vector2 _direction = new Vector2(0, 1);
        
        public TrafficMoveController(IMove move)
        {
            _move = move;
        }
        public void Update(float deltaTime)
        {
            _move.Move(_direction.x, _direction.y, deltaTime);
        }
    }
}