using RushingMachine.Controllers.Interfaces;
using RushingMachine.UserInput.Interfaces;
using RushingMachine.UserInput.Mouse;

namespace RushingMachine.Controllers.UserInput
{
    public class InputInitialization : IInitialization
    {
        private IUserInputProxy _inputHorizontal;
        private IUserInputProxy _inputVertical;

        public InputInitialization()
        {
            _inputHorizontal = new MouseInputHorizontal();
        }
        
        public void Initialization()
        {
            
        }
        
        public (IUserInputProxy inputHorizontal, IUserInputProxy inputVertical) GetInput()
        {
            (IUserInputProxy inputHorizontal, IUserInputProxy inputVertical) result = (_inputHorizontal, _inputVertical);
            return result;
        }
    }
}