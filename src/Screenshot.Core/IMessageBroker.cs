using System.Threading.Tasks;

using Screenshot.Core.BuildingBlocks;

namespace Screenshot.Core
{
    public interface IMessageBroker
    {
        Task Publish<TMessage>(TMessage message) where TMessage : IMessage;

        void Subscribe<TMessage, TMessageHandler>()
            where TMessage : IMessage
            where TMessageHandler : IMessageHandler<TMessage>;
    }
}