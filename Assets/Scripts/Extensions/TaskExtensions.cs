using System.Threading.Tasks;

namespace RushingMachine.Extensions
{
    public static class TaskExtensions
    {
        public static void Forget(this Task task) { }
    }
}