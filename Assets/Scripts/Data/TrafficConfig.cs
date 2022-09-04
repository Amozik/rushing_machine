using System;
using System.Collections.Generic;
using System.Linq;
using RushingMachine.Entities.Enemies;
using UnityEngine;

namespace RushingMachine.Data
{
    [Serializable] 
    public class TrafficCarInfo
    {
        public TrafficCarType Type;
        public TrafficCarView EnemyPrefab;
        public float Probability;
        public int HP;
        public float Speed;
        public int Points = 1;
    }

    [CreateAssetMenu(fileName = nameof(TrafficConfig), menuName = "Configs/" + nameof(TrafficConfig), order = 0)]
    public class TrafficConfig : ScriptableObject
    {
        [SerializeField] 
        private List<TrafficCarInfo> _enemies;

        public Vector2[] trafficSpawnPositions;
        
        public Dictionary<TrafficCarType, TrafficCarInfo> Enemies =>
            _enemies.ToDictionary(item => item.Type, item => item);
    }
}