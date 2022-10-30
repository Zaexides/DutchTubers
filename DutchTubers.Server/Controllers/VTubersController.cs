using DutchTubers.Server.Models;
using DutchTubers.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace DutchTubers.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VTubersController : ControllerBase
    {
        private ITwitchService _twitch;
        private IRandomProvider _randomProvider;

        public VTubersController(ITwitchService twitch, IRandomProvider randomProvider)
        {
            _twitch = twitch;
            _randomProvider = randomProvider;
        }

        [HttpGet]
        public async Task<IEnumerable<VTuberDTO>> Get()
        {
            var vtubers = await _twitch.GetVTubersAsync();

            return vtubers
                .OrderBy((vtuber) => vtuber.StreamInfo == null)
                .ThenBy((_) => _randomProvider.Next());
        }

        [HttpGet("cache")]
        public CacheMetaDTO GetCache()
        {
            var cacheMeta = _twitch.GetCacheMeta();

            return new CacheMetaDTO()
            {
                Id = cacheMeta?.Id ?? Guid.Empty,
                IsOutdated = cacheMeta?.IsOutdated ?? true
            };
        }
    }
}
