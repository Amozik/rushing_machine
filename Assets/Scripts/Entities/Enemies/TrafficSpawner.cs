using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RushingMachine.Common.Pool;
using RushingMachine.Data;
using RushingMachine.Entities.Interfaces;
using RushingMachine.Extensions;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace RushingMachine.Entities.Enemies
{
    public class TrafficSpawner : ISpawner
    {
        public event Action<TrafficCarInfo, TrafficCarView> OnSpawnEnemy;
        public event Action<TrafficCarInfo, TrafficCarView> OnDeactivateEnemy;
        
        private readonly List<TrafficCarInfo> _enemies;
        private readonly List<Vector2> _spawnPositions;
        private readonly ViewCache<TrafficCarType, TrafficCarView> _viewCache;
        private readonly GameObject _road;
        
        private bool _canSpawn = true;
        private CancellationTokenSource _ctSource;
        private List<Vector2> _spawnedPositions;
        private float _spawnPeriod;

        public TrafficSpawner(IEnumerable<TrafficCarInfo> enemies, IEnumerable<Vector2> spawnPositions, float worldSpeed, GameObject road)
        {
            _road = road;
            _spawnPositions = spawnPositions.ToList();
            _spawnedPositions = new List<Vector2>(_spawnPositions.Count);
            _enemies = enemies.ToList();
            _viewCache = new ViewCache<TrafficCarType, TrafficCarView>(_enemies.Count);
            _spawnPeriod = 40f / Math.Abs(worldSpeed);
            _ctSource = new CancellationTokenSource();
        }

        public async Task StartSpawn()
        {
            if (!_canSpawn)
            {
                _canSpawn = true;
                return;
            }
            
            _spawnedPositions.Add(_spawnPositions.GetRandomItem());
            foreach (var trafficCarInfo in _enemies)
            {
                var startDelay = TimeSpan.FromSeconds(Random.Range(.5f, 1f));
                await Task.Delay(startDelay, _ctSource.Token);
                
                SpawnEnemyAsync(trafficCarInfo).Forget();
                
                // var trafficCarView = Object.Instantiate(trafficCarInfo.EnemyPrefab);
                // AddMove(trafficCarView, trafficCarInfo.Speed);
                // _enemies.Add(trafficCarView);
            }
        }

        public void StopSpawn()
        {
            _canSpawn = false;
        }

        private async Task SpawnEnemyAsync(TrafficCarInfo carInfo)
        {
            while (_canSpawn)
            {
                //TODO: исключить наезд машин друг на друга. Происходит, если машина медленне едет, чем та, что за ней
                var trafficCarView = _viewCache.Create(carInfo.Type, carInfo.EnemyPrefab);
                trafficCarView.Transform.position = GetNewPosition();
                trafficCarView.Transform.rotation = Quaternion.identity;
                trafficCarView.Transform.parent = _road.transform;
                
                void Handler(TrafficCarView gameObject)
                {
                    trafficCarView.BecameInvisibleEvent -= Handler;
                    trafficCarView.Transform.parent = trafficCarView.Transform.parent.parent;
                    DeactivateEnemy(carInfo, gameObject);
                }

                trafficCarView.BecameInvisibleEvent += Handler;
                OnSpawnEnemy?.Invoke(carInfo, trafficCarView);

                await Task.Delay(TimeSpan.FromSeconds(_spawnPeriod), _ctSource.Token);
            }
        }

        private void DeactivateEnemy(TrafficCarInfo carInfo, TrafficCarView gameObject)
        {
            _viewCache.Destroy(carInfo.Type, gameObject);
            OnDeactivateEnemy?.Invoke(carInfo, gameObject);
        }

        private Vector2 GetNewPosition()
        {
            var lastPosition = _spawnedPositions.Last();
            //Debug.Log("Previous position: " + lastPosition.x);
            if (_spawnedPositions.Count == _spawnPositions.Count())
            {
                _spawnedPositions.Clear();
                _spawnedPositions.Add(lastPosition);
            }
            
            var newPosition = _spawnPositions.GetRandomItemExcept(_spawnedPositions);
            _spawnedPositions.Add(newPosition);
            //Debug.Log("Spawn position: " + newPosition.x);
            return newPosition;
        }

        public void Cleanup()
        {
            _canSpawn = false;
            if (_ctSource != null)
            {
                _ctSource.Cancel();
                _ctSource.Dispose();
                _ctSource = null;
            }
        }
    }
}