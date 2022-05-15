using UnityEngine;
using UnityEngine.Serialization;

namespace RushingMachine.Data
{
    [CreateAssetMenu(fileName = nameof(GameConfig),  menuName = "Configs/" + nameof(GameConfig), order = 0)]
    public class GameConfig : ScriptableObject
    {
        public PlayerConfig playerConfig;
        public TrafficConfig trafficConfig;
        public GameObject back;
    }
}