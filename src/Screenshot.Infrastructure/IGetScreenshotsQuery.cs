using System.Collections.Generic;
using System.Threading.Tasks;

namespace Screenshot.Infrastructure
{
    public interface IGetScreenshotsQuery
    {
        Task<IEnumerable<Screenshot>> Execute();
    }
}