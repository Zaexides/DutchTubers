using System.Collections;
using System.Text;
using DutchTubers.Server.Models;
using DutchTubers.Server.Utils;
using TwitchLib.Api;
using TwitchLib.Api.Helix.Models.Users.GetUsers;
using TwitchLib.Api.Interfaces;
using Stream = TwitchLib.Api.Helix.Models.Streams.GetStreams.Stream;

namespace DutchTubers.Server.Services
{
    public class TwitchService : ITwitchService
    {
        private static readonly TimeSpan VTuberDataLifespan = new TimeSpan(hours: 0, minutes: 5, seconds: 0);

        private readonly ITwitchAPI _twitchApi;
        private readonly IVTuberListProvider _vtuberListProvider;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ICacheProvider _cacheProvider;

        public TwitchService(ITwitchAPI twichApi, ISecretProvider secretProvider, IVTuberListProvider vtuberListProvider, IDateTimeProvider dateTimeProvider, ICacheProvider cacheProvider)
        {
            _twitchApi = twichApi;
            _vtuberListProvider = vtuberListProvider;
            _dateTimeProvider = dateTimeProvider;
            _cacheProvider = cacheProvider;
            _twitchApi.Settings.ClientId = secretProvider.GetTwitchClientID();
            _twitchApi.Settings.Secret = secretProvider.GetTwitchSecret();
        }

        public async Task<IEnumerable<VTuberDTO>> GetVTubersAsync()
        {
            CachedData<IEnumerable<VTuberDTO>> cachedData;
            _cacheProvider.TryGetCachedDataFor(out cachedData);

            if (cachedData == null || cachedData.DataElapsedLifespan)
            {
                var vtuberData = await DownloadVTuberDataAsync();
                if (cachedData == null)
                {
                    cachedData = new CachedData<IEnumerable<VTuberDTO>>(VTuberDataLifespan, vtuberData, _dateTimeProvider);
                    _cacheProvider.Store(cachedData);
                }
                else
                {
                    cachedData.Store(vtuberData);
                }
            }

            return cachedData.Retrieve();
        }

        public ICacheMeta? GetCacheMeta()
        {
            if (_cacheProvider.TryGetCachedDataFor<IEnumerable<VTuberDTO>>(out var cachedData))
            {
                return cachedData.GetMeta();
            }

            return null;
        }

        private async Task<IEnumerable<VTuberDTO>> DownloadVTuberDataAsync()
        {
            var vtuberNames = _vtuberListProvider.GetVTuberList();
            var users = await _twitchApi.Helix.Users.GetUsersAsync(logins: vtuberNames);
            var streams = await _twitchApi.Helix.Streams.GetStreamsAsync(userLogins: vtuberNames);

            var vtubers = new List<VTuberDTO>();
            foreach (var user in users.Users)
            {
                var dto = new VTuberDTO()
                {
                    Username = user.DisplayName,
                    Description = user.Description,
                    ProfileImage = user.ProfileImageUrl.Replace("300x300", "150x150"),
                    StreamInfo = MapStreamInfo(user, streams.Streams)
                };
                vtubers.Add(dto);
            }

            return vtubers.ToArray();
        }

        private StreamInfoDTO? MapStreamInfo(User user, IEnumerable<Stream> streams)
        {
            var stream = streams.SingleOrDefault((stream) => stream.UserId == user.Id);
            if (stream == null)
            {
                return null;
            }
            else
            {
                return new StreamInfoDTO()
                {
                    Title = stream.Title,
                    Game = stream.GameName
                };
            }
        }
    }
}
