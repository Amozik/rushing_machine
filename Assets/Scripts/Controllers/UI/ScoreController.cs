using RushingMachine.Controllers.Interfaces;
using TMPro;
using UnityEngine;

namespace RushingMachine.Controllers.UI
{
    public class ScoreController : IUpdate
    {
        private TMP_Text _text;
        private int _score;
        private int _frameCount;

        public ScoreController(GameObject scoreElement)
        {
            _text = scoreElement.GetComponentInChildren<TMP_Text>();
            _text.text = _score.ToString();
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
    }
}