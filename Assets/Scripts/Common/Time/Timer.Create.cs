using System;

namespace RushingMachine.Common.Time
{
    public partial class Timer
    {
        public static TimersController TimersController => lazy.Value;
            
        private static readonly Lazy<TimersController> lazy =
            new Lazy<TimersController>(() => new TimersController());
        
        /// <summary>
        /// Create new timer from duration in seconds
        /// </summary>
        /// <param name="duration">seconds</param>
        /// <param name="isRepeatable">start timer after complete</param>
        /// <param name="autoStart">start timer after create</param>
        public static ITimer Create(float duration, bool isRepeatable, bool autoStart = true)
        {
            var timer = new Timer();
            timer.Initialize(duration, isRepeatable);
            TimersController.Add(timer);
            
            if(autoStart)
                timer.Start();
            
            return timer;
        }
    }
}