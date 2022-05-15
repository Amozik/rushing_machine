namespace RushingMachine.Controllers.Interfaces
{
    public interface IFixedUpdate : IController
    {
        void FixedUpdate(float deltaTime);
    }
}