using System.Threading.Tasks;

namespace Screenshot.Domain
{
    public interface ISaveScreenshotCommand
    {
        Task Execute(Screenshot screenshot);
    }
}