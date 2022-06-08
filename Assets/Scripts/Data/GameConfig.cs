using UnityEngine;
using UnityEngine.Serialization;

namespace RushingMachine.Data
{
    [CreateAssetMenu(fileName = nameof(GameConfig),  menuName = "Configs/" + nameof(GameConfig), order = 0)]
    public class GameConfig : ScriptableObject
    {
        public PlayerConfig playerConfig;
        public PoliceConfig policeConfig;
        public TrafficConfig trafficConfig;
        public UiConfig uiConfig;
        public GameObject back;
        public float worldSpeed = -10f;
    }
}