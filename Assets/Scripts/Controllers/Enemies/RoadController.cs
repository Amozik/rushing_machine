using System.Collections.Generic;
using RushingMachine.Controllers.Interfaces;
using RushingMachine.Entities.Enemies;
using RushingMachine.Extensions;
using UnityEngine;

namespace RushingMachine.Controllers.Enemies
{
    public class RoadController : IFixedUpdate
    {
        private readonly List<TrafficCarView> _enemies;
        private readonly List<Vector3> _tempPositions = new List<Vector3>();
        private Transform _transform;
        private float _speed;
        private float _offset = 87.9f;

        public RoadController(Transform transform, float speed, List<TrafficCarView> enemies)
        {
            _transform = transform;
            _speed = speed;
            _enemies = enemies;
        }

        public void FixedUpdate(float deltaTime)
        {
            var backPosition = _transform.position;
            var offsetPositionY = Mathf.Repeat(backPosition.y + _speed * deltaTime, _offset);

            if ((offsetPositionY - backPosition.y) * Mathf.Sign(_speed) < 0)
            {
                //Debug.Log("need to relocate");
                foreach (var trafficCarView in _enemies)
                {
                    _tempPositions.Add(trafficCarView.Transform.position);
                }
            }

            _transform.position = backPosition.Change(y: offsetPositionY);

            if (_tempPositions.Count > 0)
            {
                for (var i = 0; i < _enemies.Count; i++)
                {
                    _enemies[i].Transform.position = _tempPositions[i];
                }
                _tempPositions.Clear();
            }
        }
    }
}