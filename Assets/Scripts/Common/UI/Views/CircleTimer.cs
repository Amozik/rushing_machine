using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RushingMachine.Common.UI.Views
{
    public class CircleTimer : UiView
    {
        [SerializeField] 
        private Image _timerImage;

        [SerializeField] 
        private TMP_Text _timerText;

        private float _duration;
        private bool _decreasing;
        private bool _withText;
        
        public void Initialize(float duration, bool decreasing = false, bool withText = false)
        {
            _duration = duration;
            _withText = withText;
            _timerText.gameObject.SetActive(_withText);
            
            _timerText.text = _duration.ToString();
            _decreasing = decreasing;
        }

        public void UpdateTime(float timeLeft)
        {
            var currentTime = _decreasing ? timeLeft : _duration - timeLeft;
            _timerImage.fillAmount = currentTime / _duration;
            _timerText.text = Mathf.RoundToInt(currentTime).ToString();
        }
    }
}