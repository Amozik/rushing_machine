using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using RushingMachine.Common.Pool;
using RushingMachine.Data;
using RushingMachine.Entities.Interfaces;
using RushingMachine.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RushingMachine.Entities.Enemies
{
    public class TrafficSpawner : ISpawner
    {
        public event Action<TrafficCarInfo, TrafficCarView> OnSpawnEnemy;
        public event Action<TrafficCarInfo, TrafficCarView> OnDeactivateEnemy;
        
        private readonly List<TrafficCarInfo> _enemies;
        private readonly ViewCache<TrafficCarType, TrafficCarView> _viewCache;
        private readonly GameObject _road;
        
        private bool _canSpawn = true;
        private CancellationTokenSource _ctSource;
        
        private float _spawnPeriod;

        private Tracks _tracks;

        public TrafficSpawner(IEnumerable<TrafficCarInfo> enemies, IEnumerable<Vector2> spawnPositions, float worldSpeed, GameObject road)
        {
            _road = road;
            _enemies = enemies.ToList();
            _viewCache = new ViewCache<TrafficCarType, TrafficCarView>(_enemies.Count);
            _spawnPeriod = 40f / Math.Abs(worldSpeed);
            _ctSource = new CancellationTokenSource();
            _tracks = new Tracks(spawnPositions);
        }

        public async UniTask StartSpawn()
        {
            if (!_canSpawn)
            {
                _canSpawn = true;
                return;
            }
            
            for (var i = 0; i < _tracks.Length - 1; i++)
            {
                var startDelay = TimeSpan.FromSeconds(Random.Range(.5f, 1f));
                await UniTask.Delay(startDelay, cancellationToken: _ctSource.Token);
                
                SpawnEnemyAsync(i).Forget();
                
                // var trafficCarView = Object.Instantiate(trafficCarInfo.EnemyPrefab);
                // AddMove(trafficCarView, trafficCarInfo.Speed);
                // _enemies.Add(trafficCarView);
            }
        }

        public void StopSpawn()
        {
            _canSpawn = false;
        }

        private async UniTask SpawnEnemyAsync(int roadIndex)
        {
            while (_canSpawn)
            {
                var randomCarInfo = _enemies.GetRandomItemWithWeight(item => item.Probability);
                var newTrack = _tracks.GetNewTrack();
                //var randomCarInfo = _enemies[Mathf.Clamp(roadIndex, 0, _enemies.Count - 1)];

                var delay = newTrack.LastCarInfo?.Speed / randomCarInfo.Speed * _spawnPeriod ?? 0;
                UnityEngine.Debug.Log($"Spawn Period : {delay}; track {newTrack.Index} \n {randomCarInfo.Type} <- {newTrack.LastCarInfo?.Type}");
                
                //TODO: исключить наезд машин друг на друга. Происходит, если машина медленне едет, чем та, что за ней
                //Пробовал исключить за счет деления скорости текущего на предыдущий, получилось не очень
                await SpawnCarOnTrackWithDelay(randomCarInfo, newTrack, Mathf.Max(0,delay - _spawnPeriod));

                await UniTask.Delay(TimeSpan.FromSeconds(_spawnPeriod), cancellationToken: _ctSource.Token);
            }
        }

        private async UniTask<TrafficCarView> SpawnCarOnTrackWithDelay(TrafficCarInfo carInfo, Track track, float delay)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: _ctSource.Token);
            
            track.LastCarInfo = carInfo;
            
            var trafficCarView = _viewCache.Create(carInfo.Type, carInfo.EnemyPrefab);
            trafficCarView.Transform.position = track.SpawnPosition;
            trafficCarView.Transform.rotation = Quaternion.identity;
            trafficCarView.Transform.parent = _road.transform;

            track.LastCarView = trafficCarView;
            
            void Handler(TrafficCarView gameObject)
            {
                trafficCarView.BecameInvisibleEvent -= Handler;
                track.LastCarInfo = null;
                track.LastCarView = null;
                //trafficCarView.Transform.parent = trafficCarView.Transform.parent.parent;
                DeactivateEnemy(carInfo, gameObject);
            }

            trafficCarView.BecameInvisibleEvent += Handler;
            OnSpawnEnemy?.Invoke(carInfo, trafficCarView);
            
            return trafficCarView;
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

    public class Tracks
    {
        public int Length => _tracks.Length;
        
        private readonly Track[] _tracks;
        private readonly List<int> _spawnedTracks;

        public Tracks(IEnumerable<Vector2> spawnPositions)
        {
            var spawnPositionsList = spawnPositions.ToList();
            _tracks = new Track[spawnPositionsList.Count];
            _spawnedTracks = new List<int>(spawnPositionsList.Count);
            
            for (var index = 0; index < spawnPositionsList.Count; index++)
            {
                var spawnPosition = spawnPositionsList[index];
                _tracks[index] = new Track(spawnPosition)
                {
                    Index = index,
                };
            }
        }

        public Track GetNewTrack()
        {
            if (_spawnedTracks.Count == 0)
            {
                return GenerateStartTrack();
            }
            
            var lastTrack = _spawnedTracks.Last();
            //UnityEngine.Debug.Log("Previous position: " + lastPosition);
            if (_spawnedTracks.Count == _tracks.Count())
            {
                _spawnedTracks.Clear();
                _spawnedTracks.Add(lastTrack);
            }
            
            var newTrack = _tracks.GetRandomItemExcept(_spawnedTracks, out var trackIndex);
            _spawnedTracks.Add(trackIndex);
            //UnityEngine.Debug.Log("Spawn position: " + trackIndex + "\n");
            return newTrack;
        }

        private Track GenerateStartTrack()
        {
            var track =  _tracks.GetRandomItem(out var index);
            
            _spawnedTracks.Add(index);

            return track;
        }
    }

    public class Track
    {
        public Track(Vector2 spawnPosition)
        {
            SpawnPosition = spawnPosition;
        }

        public Vector2 SpawnPosition { get; }
        public TrafficCarInfo LastCarInfo { get; set; }
        public TrafficCarView LastCarView { get; set; }
        public int Index { get; set; }
    }
}