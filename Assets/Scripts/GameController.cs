using System;
using RushingMachine.Controllers;
using RushingMachine.Controllers.Game;
using RushingMachine.Data;
using UnityEngine;

namespace RushingMachine
{
    public class GameController : MonoBehaviour, IDisposable
    {
        [SerializeField] 
        private GameConfig _gameConfig;

        [SerializeField] 
        private GameObject _back;

        private CompositeController _controllersHandler;

        private void Awake()
        {
            _controllersHandler = new CompositeController();

            _gameConfig.back = _back; //TODO instantiate in BackgroundController
            var gameInitialization = new GameInitialization(_controllersHandler, _gameConfig);
        }

        private void Start()
        {
            _controllersHandler.Initialization();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _controllersHandler.Update(deltaTime);
        }

        private void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;
            _controllersHandler.FixedUpdate(deltaTime);
        }

        private void OnDestroy()
        {
            Dispose();
        }

        public void Dispose()
        {
            _controllersHandler.Cleanup();
        }
    }
}


