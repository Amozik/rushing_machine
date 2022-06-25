using System;

namespace RushingMachine.Common.Time
{
    public interface ITimer
    {
        event Action<ITimer> CompletedEvent;
        event Action TickEvent;
        float TimeLeft { get; }
        
        void Start();
        void Tick(float deltaTime);
        void Release();
    }
}