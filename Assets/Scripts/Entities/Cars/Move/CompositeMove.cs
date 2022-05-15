using System.Collections.Generic;
using RushingMachine.Entities.Interfaces;
using UnityEngine;

namespace RushingMachine.Entities.Cars.Move
{
    public class CompositeMove : IMove
    {
        private readonly List<IMove> _moves = new List<IMove>();
        
        public void AddUnit(IMove unit)
        {
            _moves.Add(unit);
        }

        public void RemoveUnit(IMove unit)
        {
            _moves.Remove(unit);
        }
        
        public Vector2 Move(float horizontal, float vertical, float deltaTime)
        {
            for (var i = 0; i < _moves.Count; i++)
            {
                _moves[i].Move(horizontal, vertical, deltaTime);
            }

            return Vector2.zero;
        }
    }
}