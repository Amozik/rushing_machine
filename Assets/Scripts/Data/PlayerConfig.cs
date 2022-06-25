using RushingMachine.Entities.Cars.Weapon;
using RushingMachine.Entities.Player;
using UnityEngine;

namespace RushingMachine.Data
{
    [CreateAssetMenu(fileName = nameof(PlayerConfig), menuName = "Configs/" + nameof(PlayerConfig), order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        public PlayerView view;
        public MineView mineView;
    }
}