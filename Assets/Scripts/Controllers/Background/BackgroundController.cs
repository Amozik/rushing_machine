using RushingMachine.Controllers.Interfaces;
using RushingMachine.Extensions;
using UnityEngine;

namespace RushingMachine.Controllers.Background
{
    public class BackgroundController : IUpdate
    {
        private Transform _back;
        private float _speed = -10f;
        private float _offset = 87.9f;
        
        public BackgroundController(GameObject back)
        {
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