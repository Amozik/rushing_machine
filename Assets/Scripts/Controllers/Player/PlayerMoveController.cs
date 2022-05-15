using System;
using System.ComponentModel.Design.Serialization;
using RushingMachine.Controllers.Interfaces;
using RushingMachine.Entities.Interfaces;
using RushingMachine.Extensions;
using RushingMachine.UserInput.Interfaces;
using UnityEngine;

namespace RushingMachine.Controllers.Player
{
    public class PlayerMoveController : IUpdate, ICleanup
    {
        private readonly IMove _move;
        private float _horizontal;
        private float _vertical;
        private IUserInputProxy _horizontalInputProxy;
        private IUserInputProxy _verticalInputProxy;
        
        private float _smoothTime = 0.01f;
        private float _xVelocity;
        
        private float[] _roads = {1.95f, 5.85f};
        private float _roadsOffset = 3.9f;
        
        private bool _isMoving = false;
        private Vector2 _targetPosition;
        private float _direction;


        public PlayerMoveController(IMove move, (IUserInputProxy inputHorizontal, IUserInputProxy inputVertical) input)
        {
            _move = move;
            _horizontalInputProxy = input.inputHorizontal;
            _verticalInputProxy = input.inputVertical;
            _targetPosition = _move.Move(0, 0, 0);
            
            _horizontalInputProxy.AxisOnChange += HorizontalOnAxisOnChange;
            //_verticalInputProxy.AxisOnChange += VerticalOnAxisOnChange;
        }

        public void Update(float deltaTime)
        {
            if (!_isMoving)
                return;

            //_horizontal = _direction * 0.8f;
            _horizontal = Mathf.SmoothDamp(_horizontal, _direction * 1f, ref _xVelocity, _smoothTime);
            var position = _move.Move(_horizontal, _vertical, deltaTime);

            if (_horizontal > 0 && position.x >= _targetPosition.x ||
                _horizontal < 0 &&  position.x <= _targetPosition.x)
            {
                _isMoving = false;
            }
        }

        public void Cleanup()
        {
            _horizontalInputProxy.AxisOnChange -= HorizontalOnAxisOnChange;
            //_verticalInputProxy.AxisOnChange -= VerticalOnAxisOnChange;
        }
        
        private void VerticalOnAxisOnChange(float value)
        {
            _vertical = value;
        }

        private void HorizontalOnAxisOnChange(float value)
        {
            if (value == 0) 
                return;

            _direction = Mathf.Sign(value);
            var newPosition = _targetPosition.x + _direction * _roadsOffset;
            
            if (Math.Abs(newPosition) > _roads[1] + _roadsOffset/2)
                return;
            
            _isMoving = true;
            _horizontal = 0;
            _targetPosition = _targetPosition.Change(newPosition);
        }
    }
}