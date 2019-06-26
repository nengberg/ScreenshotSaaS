using System.Threading.Tasks;

namespace Screenshot.Infrastructure
{
    public interface ISaveScreenshotCommand
    {
        Task Execute(Screenshot screenshot);
    }
}