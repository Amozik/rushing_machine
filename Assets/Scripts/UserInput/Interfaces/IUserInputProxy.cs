using System;

namespace RushingMachine.UserInput.Interfaces
{
    public interface IUserInputProxy
    {
        event Action<float> AxisOnChange;
        void GetAxis();
    }
}