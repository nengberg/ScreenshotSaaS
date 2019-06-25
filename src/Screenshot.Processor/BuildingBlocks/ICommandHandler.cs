using System.Threading.Tasks;

namespace Screenshot.Processor.BuildingBlocks
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task Handle(TCommand command);
    }
}