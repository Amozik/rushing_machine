using RushingMachine.Common.MessageBroker.Messages;
using RushingMachine.Controllers.Interfaces;
using TMPro;
using UnityEngine;

namespace RushingMachine.Controllers.UI
{
    public class ScoreController : IUpdate, ICleanup
    {
        private TMP_Text _text;
        private int _score;
        private int _frameCount;

        public ScoreController(GameObject scoreElement)
        {
            _text = scoreElement.GetComponentInChildren<TMP_Text>();
            _text.text = _score.ToString();
            
            Message.Enemy.Destroy.Subscribe(OnEnemyDestroy);
        }

        public void Update(float deltaTime)
        {
            _frameCount++;
            
            if (_frameCount % 50 == 0)
            {
                _score += 1;
                _text.text = _score.ToString();
                _frameCount = 0;
            }
        }
        
        private void OnEnemyDestroy(Message.Enemy.Destroy message)
        {
            _score += message.Points;
            //UnityEngine.Debug.Log($"Receive {message.Points} points");
        }

        public void Cleanup()
        {
            Message.Enemy.Destroy.Unsubscribe(OnEnemyDestroy);
        }
    }
}