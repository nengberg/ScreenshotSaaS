using System.Collections.Generic;
using System.Threading.Tasks;

namespace Screenshot.Domain
{
    public interface IGetScreenshotsQuery
    {
        Task<IEnumerable<Screenshot>> Execute();
    }
}