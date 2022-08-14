using System.Collections.Generic;
using RushingMachine.Controllers.Interfaces;

namespace RushingMachine.Controllers
{
    internal sealed class CompositeController : IInitialization, IUpdate, IFixedUpdate, ICleanup
    {
        private readonly List<IInitialization> _initializeControllers;
        private readonly List<IUpdate> _updateControllers;
        private readonly List<IFixedUpdate> _fixedUpdateControllers;
        private readonly List<ILateUpdate> _lateUpdateControllers;
        private readonly List<ICleanup> _cleanupControllers;

        public CompositeController()
        {
            _initializeControllers = new List<IInitialization>();
            _updateControllers = new List<IUpdate>();
            _fixedUpdateControllers = new List<IFixedUpdate>();
            _lateUpdateControllers = new List<ILateUpdate>();
            _cleanupControllers = new List<ICleanup>();
        }

        public void Add(IController controller)
        {
            if (controller is IInitialization initializeController)
            {
                _initializeControllers.Add(initializeController);
            }

            if (controller is IUpdate executeController)
            {
                _updateControllers.Add(executeController);
            }

            if (controller is IFixedUpdate fixedExecuteController)
            {
                _fixedUpdateControllers.Add(fixedExecuteController);
            }
            
            if (controller is ILateUpdate lateExecuteController)
            {
                _lateUpdateControllers.Add(lateExecuteController);
            }

            if (controller is ICleanup cleanupController)
            {
                _cleanupControllers.Add(cleanupController);
            }
        }

        public void Initialization()
        {
            for (var index = 0; index < _initializeControllers.Count; ++index)
            {
                _initializeControllers[index].Initialization();
            }
        }

        public void Update(float deltaTime)
        {
            for (var index = 0; index < _updateControllers.Count; ++index)
            {
                _updateControllers[index].Update(deltaTime);
            }
        }

        public void FixedUpdate(float deltaTime)
        {
            for (var index = 0; index < _fixedUpdateControllers.Count; ++index)
            {
                _fixedUpdateControllers[index].FixedUpdate(deltaTime);
            }
        }

        public void LateUpdate(float deltaTime)
        {
            for (var index = 0; index < _lateUpdateControllers.Count; ++index)
            {
                _lateUpdateControllers[index].LateUpdate(deltaTime);
            }
        }

        public void Cleanup()
        {
            for (var index = 0; index < _cleanupControllers.Count; ++index)
            {
                _cleanupControllers[index].Cleanup();
            }
        }
    }
}