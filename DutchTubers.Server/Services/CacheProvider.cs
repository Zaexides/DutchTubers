using DutchTubers.Server.Utils;

namespace DutchTubers.Server.Services
{
    public class CacheProvider : ICacheProvider
    {
        private IDictionary<Type, object> _storedCaches = new Dictionary<Type, object>();

        public bool TryGetCachedDataFor<TData>(out CachedData<TData>? cachedData)
        {
            if (_storedCaches.TryGetValue(typeof(TData), out var abstractStoredData) && abstractStoredData is CachedData<TData> concrecteStoredData)
            {
                cachedData = concrecteStoredData;
                return true;
            }

            cachedData = null;
            return false;
        }

        public void Store<TData>(CachedData<TData> cachedData)
        {
            cachedData = cachedData ?? throw new ArgumentNullException(nameof(cachedData));
            _storedCaches[typeof(TData)] = cachedData;
        }
    }
}
