using RushingMachine.Common.UI.Views;
using RushingMachine.Data;
using UnityEngine;

namespace RushingMachine.Controllers.UI
{
    public class UiInitialization
    {
        private Canvas _canvas;

        public GameObject EndGame { get; private set; }
        public GameObject Score { get; private set; }
        public CircleTimer CircleTimer { get; private set; }

        public UiInitialization(UiConfig config)
        {
            _canvas = Object.FindObjectOfType<Canvas>();
            Score = Object.Instantiate(config.score, _canvas.transform);
            CircleTimer = Object.Instantiate(config.circleTimer, _canvas.transform);
            //EndGame = Object.Instantiate(config.endGame, _canvas.transform);
        }
    }
}