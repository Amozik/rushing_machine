using System.Collections.Generic;
using RushingMachine.Controllers.Interfaces;

namespace RushingMachine.Common.Time
{
    public class TimersController : IUpdate
    {
        private readonly List<ITimer> _timers = new List<ITimer>();

        public void Add(ITimer timer)
        {
            _timers.Add(timer);
        }

        public void RemoveTimer(ITimer timer)
        {
            _timers.Remove(timer);
        }

        public void Update(float deltaTime)
        {
            foreach (var timer in _timers)
            {
                timer.Tick(deltaTime);
            }
        }
    }
}