using RushingMachine.Entities.Interfaces;
using UnityEngine;

namespace RushingMachine.Entities.Cars.Move
{
    internal class MoveTransform : IMove
    {
        private readonly Transform _transform;
        private Vector3 _move;

        public float Speed { get; protected set; }
        
        public MoveTransform(Transform transform, float speed)
        {
            _transform = transform;
            Speed = speed;
        }

        public Vector2 Move(float horizontal, float vertical,  float deltaTime)
        {
            var speed = deltaTime * Speed;
            _move.Set(horizontal * speed, vertical * speed, 0.0f);
            _transform.localPosition += _move;

            return _transform.localPosition;
        }
    }
}