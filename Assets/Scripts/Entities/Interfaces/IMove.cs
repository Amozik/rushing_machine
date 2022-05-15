using UnityEngine;

namespace RushingMachine.Entities.Interfaces
{
    public interface IMove
    {
        Vector2 Move(float horizontal, float vertical, float deltaTime);
    }
}