using DutchTubers.Server.Utils;

namespace DutchTubers.Server.Services
{
    public interface ICacheProvider
    {
        bool TryGetCachedDataFor<TData>(out CachedData<TData>? cachedData);

        void Store<TData>(CachedData<TData> cachedData);
    }
}
