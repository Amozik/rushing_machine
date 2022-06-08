using RushingMachine.Entities.Enemies;
using UnityEngine;

namespace RushingMachine.Data
{
    [CreateAssetMenu(fileName = nameof(PoliceConfig), menuName = "Configs/" + nameof(PoliceConfig), order = 0)]
    public class PoliceConfig : ScriptableObject
    {
        public PoliceView view;
    }
}