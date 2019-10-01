using System.Threading;
using System.Threading.Tasks;

namespace Screenshot.Infrastructure
{
    public interface IMessageHandler<in TMessage> where TMessage : IMessage
    {
        Task Handle(TMessage message, CancellationToken cancellationToken);
    }
}