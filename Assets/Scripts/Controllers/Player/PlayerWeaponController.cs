using RushingMachine.Common.Time;
using RushingMachine.Common.UI.Views;
using RushingMachine.Controllers.Interfaces;
using RushingMachine.Entities.Interfaces;

namespace RushingMachine.Controllers.Player
{
    public class PlayerWeaponController : IInitialization, ICleanup
    {
        private readonly IWeapon _weapon;
        private readonly CircleTimer _timerView;
        private ITimer _shootTimer;
        private float _cooldownTime = 10; //seconds //TODO: move to weapon config

        public PlayerWeaponController(IWeapon weapon, CircleTimer timerView)
        {
            _weapon = weapon;
            _timerView = timerView;
            _shootTimer = Timer.Create(_cooldownTime, true);
            _shootTimer.CompletedEvent += OnComplete;
            _shootTimer.TickEvent += OnTick;
        }

        public void Initialization()
        {
            _timerView.Initialize(_cooldownTime, true);
        }

        private void OnComplete(ITimer timer)
        {
            _weapon.Shoot();
        }

        private void OnTick()
        {
            _timerView.UpdateTime(_shootTimer.TimeLeft);
        }

        public void Cleanup()
        {
            _shootTimer.Release();
        }
    }
}