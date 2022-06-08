using RushingMachine.Controllers.Interfaces;
using RushingMachine.Data;
using RushingMachine.Entities.Cars.Move;
using RushingMachine.Entities.Enemies;
using RushingMachine.Entities.Interfaces;
using UnityEngine;

namespace RushingMachine.Controllers.Enemies
{
    public class PoliceInitialization : IInitialization
    {
        private readonly PoliceView _police;
        private IMove _move;
        
        public IMove Move => _move;

        public PoliceInitialization(PoliceConfig policeConfig)
        {
            _police = Object.Instantiate(policeConfig.view);
            
            _move = new MoveTransform(_police.transform, 5f);
        }
        public void Initialization()
        {
            
        }
    }
}