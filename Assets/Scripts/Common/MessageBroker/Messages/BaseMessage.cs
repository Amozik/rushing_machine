using System;

namespace RushingMachine.Common.MessageBroker.Messages
{
    public abstract class BaseMessage<TMessage> where TMessage : BaseMessage<TMessage>, new()
    {
        public static void Publish(string id)
        {
            SimpleMessageBroker.Publish(id,new TMessage());
        }
        
        public static void Publish()
        {
            SimpleMessageBroker.Publish(new TMessage());
        }
        
        public static void Subscribe(string id, Action<TMessage> action)
        {
            SimpleMessageBroker.Subscribe<TMessage>(id, action);
        }
        
        public static void Subscribe(Action<TMessage> action)
        {
            SimpleMessageBroker.Subscribe<TMessage>(action);
        }
        
        public static void Unsubscribe(string id, Action<TMessage> action)
        {
            SimpleMessageBroker.Unsubscribe<TMessage>(id, action);
        }
        
        public static void Unsubscribe(Action<TMessage> action)
        {
            SimpleMessageBroker.Unsubscribe<TMessage>(action);
        }
    }
    
    public abstract class BaseMessage<TMessage, TModel> where TMessage : BaseMessage<TMessage, TModel>, new()
    {
        public TModel Model { get; private set; }
        
        public static void Publish(string id, in TModel model)
        {
            SimpleMessageBroker.Publish(id,new TMessage { Model = model });
        }
        
        public static void Publish(in TModel model)
        {
            SimpleMessageBroker.Publish(new TMessage { Model = model });
        }

        public static void Subscribe(string id, Action<TMessage> action)
        {
            SimpleMessageBroker.Subscribe<TMessage>(id, action);
        }
        
        public static void Subscribe(Action<TMessage> action)
        {
            SimpleMessageBroker.Subscribe<TMessage>(action);
        }
        
        public static void Unsubscribe(string id, Action<TMessage> action)
        {
            SimpleMessageBroker.Unsubscribe<TMessage>(id, action);
        }
        
        public static void Unsubscribe(Action<TMessage> action)
        {
            SimpleMessageBroker.Unsubscribe<TMessage>(action);
        }
    }
}