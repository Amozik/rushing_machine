using RushingMachine.Controllers.Interfaces;
using UnityEngine;

namespace RushingMachine.Controllers.Game
{
    public class CameraController : IUpdate
    {
        private Camera _camera;
        private readonly float _initialSize;
        private readonly float _targetAspect;
        private readonly Vector3 _cameraPosition;
        
#if UNITY_EDITOR
        private Vector2 _resolution;
#endif
        
        public CameraController(Camera camera)
        {
            _camera = camera;
            _cameraPosition = _camera.transform.position;
            _initialSize = _camera.orthographicSize;
            _targetAspect = (float) 1080 / 1920;

            CalculateCamera();
#if UNITY_EDITOR
            _resolution = new Vector2(Screen.width, Screen.height);
#endif
        }

        public void Update(float deltaTime) //TODO переделать на OnRectTransformDimensionsChange
        {
#if UNITY_EDITOR
            if (_resolution.x != Screen.width || _resolution.y != Screen.height)
            {
                CalculateCamera();
     
                _resolution.x = Screen.width;
                _resolution.y = Screen.height;
            }
#endif
        }

        private void CalculateCamera()
        {
            _camera.orthographicSize = _initialSize * (_targetAspect / _camera.aspect);
            _camera.transform.position = new Vector3(_cameraPosition.x,
                _cameraPosition.y - 1 * (_initialSize - _camera.orthographicSize), _cameraPosition.z);
        }
    }
}