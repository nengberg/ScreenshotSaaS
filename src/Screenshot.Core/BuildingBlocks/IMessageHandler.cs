using System.Threading.Tasks;

namespace Screenshot.Core.BuildingBlocks
{
    public interface IMessageHandler<in TMessage> where TMessage : IMessage
    {
        Task Handle(TMessage message);
    }
}