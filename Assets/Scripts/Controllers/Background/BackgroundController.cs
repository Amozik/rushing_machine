using RushingMachine.Controllers.Interfaces;
using RushingMachine.Extensions;
using UnityEngine;

namespace RushingMachine.Controllers.Background
{
    public class BackgroundController : IUpdate
    {
        private Transform _back;
        private float _speed;
        private float _offset = 87.88f;
        
        public BackgroundController(GameObject back, float speed)
        {
            _speed = speed;
            _back = back.transform;
        }

        public void Update(float deltaTime)
        {
            var backPosition = _back.position;
            var offsetPositionY = Mathf.Repeat(backPosition.y + _speed * deltaTime, _offset);

            _back.position = backPosition.Change(y: offsetPositionY);
        }
    }
}