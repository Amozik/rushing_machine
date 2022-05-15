using UnityEngine;

namespace RushingMachine.Entities.Views
{
    public class BaseView : MonoBehaviour
    {
        [SerializeField]
        private Transform _transform;

        public Transform Transform => _transform;
    }
}