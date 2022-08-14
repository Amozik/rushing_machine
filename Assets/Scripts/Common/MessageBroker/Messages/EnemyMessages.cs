namespace RushingMachine.Common.MessageBroker.Messages
{
    public static partial class Message
    {
        public static class Enemy {
            public sealed class Destroy : BaseMessage<Destroy, int>
            {
                public int Points => Model;
            }
        }
    }
}