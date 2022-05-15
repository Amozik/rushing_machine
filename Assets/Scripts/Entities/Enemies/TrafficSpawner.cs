using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RushingMachine.Common.Pool;
using RushingMachine.Data;
using RushingMachine.Entities.Interfaces;
using RushingMachine.Extensions;
using Object = UnityEngine.Object;

namespace RushingMachine.Entities.Enemies
{
    public class TrafficSpawner : ISpawner
    {
        public event Action<TrafficCarInfo, TrafficCarView> OnSpawnEnemy;
        public event Action<TrafficCarInfo, TrafficCarView> OnDeactivateEnemy;
        
        private readonly List<TrafficCarInfo> _enemies;
        private readonly ViewCache<TrafficCarType, TrafficCarView> _viewCache;
        
        private bool _canSpawn = true;
        private CancellationTokenSource _ctSource;
        
        public TrafficSpawner(IEnumerable<TrafficCarInfo> enemies)
        {
            _enemies = enemies.ToList();
            _viewCache = new ViewCache<TrafficCarType, TrafficCarView>(_enemies.Count);
            _ctSource = new CancellationTokenSource();
        }

        public void StartSpawn()
        {
            if (!_canSpawn)
            {
                _canSpawn = true;
                return;
            }
            
            foreach (var trafficCarInfo in _enemies)
            {
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
                //var trafficCarView = Object.Instantiate(carInfo.EnemyPrefab);
                var trafficCarView = _viewCache.Create(carInfo.Type, carInfo.EnemyPrefab);
                trafficCarView.transform.position = carInfo.EnemyPrefab.transform.position; //нужно переделать

                void Handler(TrafficCarView gameObject)
                {
                    trafficCarView.BecameInvisibleEvent -= Handler;
                    DeactivateEnemy(carInfo, gameObject);
                }

                trafficCarView.BecameInvisibleEvent += Handler;
                OnSpawnEnemy?.Invoke(carInfo, trafficCarView);
                
                await Task.Delay(TimeSpan.FromSeconds(10), _ctSource.Token);
            }
        }

        private void DeactivateEnemy(TrafficCarInfo carInfo, TrafficCarView gameObject)
        {
            _viewCache.Destroy(carInfo.Type, gameObject);
            OnDeactivateEnemy?.Invoke(carInfo, gameObject);
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