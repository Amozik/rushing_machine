using RushingMachine.Entities.Interfaces;
using UnityEngine;

namespace RushingMachine.Entities.Cars.Move
{
    internal class MoveRigidbody : IMove
    {
        private Rigidbody2D _rigidbody;
        private Vector3 _force;
        public float Speed { get; }

        public MoveRigidbody(Rigidbody2D rigidbody, float speed)
        {
            _rigidbody = rigidbody;
            Speed = speed;
        }

        public Vector2 Move(float horizontal, float vertical, float deltaTime)
        {
            var speed = deltaTime * Speed;
            _force.Set(horizontal, vertical, 0.0f);
            _rigidbody.AddForce(_force * speed);

            return _rigidbody.position;
        }
    }
}