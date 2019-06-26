namespace Screenshot.Infrastructure
{
    public interface IMessageBroker
    {
        void Publish<TMessage>(TMessage message) where TMessage : IMessage;
        void Subscribe<TMessage>() where TMessage : IMessage;
    }
}