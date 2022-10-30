using DutchTubers.Server.Models;
using DutchTubers.Server.Utils;

namespace DutchTubers.Server.Services
{
    public interface ITwitchService
    {
        Task<IEnumerable<VTuberDTO>> GetVTubersAsync();

        ICacheMeta? GetCacheMeta();
    }
}
