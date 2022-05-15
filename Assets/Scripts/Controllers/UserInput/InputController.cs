using RushingMachine.Controllers.Interfaces;
using RushingMachine.UserInput.Interfaces;

namespace RushingMachine.Controllers.UserInput
{
    public class InputController : IUpdate
    {
        private readonly IUserInputProxy _horizontal;
        private readonly IUserInputProxy _vertical;

        public InputController((IUserInputProxy inputHorizontal, IUserInputProxy inputVertical) input)
        {
            _horizontal = input.inputHorizontal;
            _vertical = input.inputVertical;
        }

        public void Update(float deltaTime)
        {
            _horizontal.GetAxis();
            //_vertical.GetAxis();
        }
    }
}