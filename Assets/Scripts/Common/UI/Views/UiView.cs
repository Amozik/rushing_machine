using UnityEngine;

namespace RushingMachine.Common.UI.Views
{
    public class UiView : MonoBehaviour
    {
        [SerializeField] 
        private RectTransform _rectTransform;

        public RectTransform RectTransform => _rectTransform;
    }
}