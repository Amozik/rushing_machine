using System;

namespace RushingMachine.Common.Time
{
    public partial class Timer : ITimer
    {
        public event Action<ITimer> CompletedEvent;
        public event Action TickEvent;
        public float TimeLeft { get; private set; }
        
        private float _duration; //in seconds
        private bool _isRepeatable;
        private bool _isStarted;

        private void Initialize(float duration, bool isRepeatable)
        {
            _isRepeatable = isRepeatable;
            _duration = duration;
        }

        public void Start()
        {
            _isStarted = true;
            TimeLeft = _duration;
        }

        public void Tick(float deltaTime)
        {
            if (!_isStarted)
                return;
            
            TimeLeft -= deltaTime;

            TickEvent?.Invoke();
            if (TimeLeft <= double.Epsilon)
            {
                Complete();
            }
        }

        public void Release()
        {
            CompletedEvent = null;
            TickEvent = null;

            _isStarted = false;
            TimersController.RemoveTimer(this);
        }

        private void Complete()
        {
            TimeLeft = 0;
            _isStarted = false;
            
            CompletedEvent?.Invoke(this);
            TryRepeat();
        }

        private void TryRepeat()
        {
            if (!_isRepeatable)
                return;
            
            Start();
        }
    }
}