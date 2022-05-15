using System.Collections.Generic;
using RushingMachine.Controllers.Interfaces;
using RushingMachine.Data;
using RushingMachine.Entities.Cars.Move;
using RushingMachine.Entities.Enemies;
using RushingMachine.Entities.Interfaces;
using RushingMachine.Extensions;
using UnityEngine;

namespace RushingMachine.Controllers.Enemies
{
    public class TrafficInitialization : IInitialization, ICleanup
    {
        private readonly List<TrafficCarView> _enemies;
        private readonly CompositeMove _enemyMove;
        private readonly TrafficSpawner _trafficSpawner;

        public TrafficInitialization(TrafficConfig config)
        {
            _enemies = new List<TrafficCarView>();
            _enemyMove = new CompositeMove();

            _trafficSpawner = new TrafficSpawner(config.Enemies.Values, config.trafficSpawnPositions);
            _trafficSpawner.OnSpawnEnemy += OnSpawnEnemy;
            _trafficSpawner.OnDeactivateEnemy += OnDeactivateEnemy;
        }

        public void Initialization()
        {
            _trafficSpawner.StartSpawn().Forget();
        }
        
        public IMove GetTrafficMove()
        {
            return _enemyMove;
        }
        
        public IEnumerable<TrafficCarView> GetEnemies()
        {
            foreach (var enemy in _enemies)
            {
                yield return enemy;
            }
        }
        
        private void AddMove(TrafficCarView enemy, float speed)
        {
            var rigidbody = enemy.GetComponent<Rigidbody2D>();

            IMove move;
            
            if (rigidbody)
            {
                move = new MoveRigidbody(rigidbody, speed); 
            }
            else
            {
                move = new MoveTransform(enemy.transform, speed);
            }
            
            _enemyMove.AddUnit(move);
            
            void Handler(TrafficCarView gameObject)
            {
                enemy.BecameInvisibleEvent -= Handler;
                _enemyMove.RemoveUnit(move);
            }

            enemy.BecameInvisibleEvent += Handler;
        }

        private void OnSpawnEnemy(TrafficCarInfo carInfo, TrafficCarView carView)
        {
            AddMove(carView, carInfo.Speed);
            _enemies.Add(carView);
        }        
        
        private void OnDeactivateEnemy(TrafficCarInfo carInfo, TrafficCarView carView)
        {
            //_enemyMove.AddUnit(move);
            _enemies.Remove(carView);
        }

        public void Cleanup()
        {
            if (_trafficSpawner != null)
            {
                _trafficSpawner.OnSpawnEnemy -= OnSpawnEnemy;
                _trafficSpawner.OnDeactivateEnemy -= OnDeactivateEnemy;
                _trafficSpawner.Cleanup();
            }
        }
    }
}