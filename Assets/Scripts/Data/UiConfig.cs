using RushingMachine.Common.UI.Views;
using UnityEngine;

namespace RushingMachine.Data
{
    [CreateAssetMenu(fileName = "UiConfig", menuName = "Configs/UiConfig", order = 0)]
    public class UiConfig : ScriptableObject
    {
        public GameObject score;
        public CircleTimer circleTimer;
        public GameObject endGame;
        public GameObject pauseMenu;
    }
}