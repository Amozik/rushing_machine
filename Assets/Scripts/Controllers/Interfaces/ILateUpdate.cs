namespace RushingMachine.Controllers.Interfaces
{
    public interface ILateUpdate : IController
    {
        void LateUpdate(float deltaTime);
    }
}