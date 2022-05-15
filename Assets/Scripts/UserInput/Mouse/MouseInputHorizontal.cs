using System;
using RushingMachine.UserInput.Interfaces;
using UnityEngine;

namespace RushingMachine.UserInput.Mouse
{
    public class MouseInputHorizontal : IUserInputProxy
    {
        public event Action<float> AxisOnChange;
        
        public void GetAxis()
        {
            var axisValue = 0f;
            
            if (Input.GetButtonDown("Fire1"))
            {
                if (Input.mousePosition.x > Screen.width / 2f)
                {
                    axisValue = 1;
                }
                else
                {
                    axisValue = -1;
                }
                
                AxisOnChange?.Invoke(axisValue);
            }
        }
    }
}