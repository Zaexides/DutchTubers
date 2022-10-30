using DutchTubers.Server.Services;
using Microsoft.AspNetCore.Components;

namespace DutchTubers.Server.Utils
{
    public class CachedData<TData>
    {
        private readonly TimeSpan _lifespan;
        private readonly IDateTimeProvider _dateTimeProvider;
        private TData _storedData;
        private DateTime _storeDateTime;
        private Guid _id;

        public bool DataElapsedLifespan => _dateTimeProvider.Now - _storeDateTime >= _lifespan;

        public CachedData(TimeSpan lifespan, TData data, IDateTimeProvider dateTimeProvider)
        {
            _lifespan = lifespan;
            _dateTimeProvider = dateTimeProvider;
            Store(data);
        }

        public void Store(TData data)
        {
            _storedData = data;
            _storeDateTime = _dateTimeProvider.Now;
            _id = Guid.NewGuid();
        }

        public TData Retrieve()
        {
            return _storedData;
        }

        public ICacheMeta GetMeta()
        {
            return new Meta()
            {
                Id = _id,
                IsOutdated = DataElapsedLifespan
            };
        }

        private class Meta : ICacheMeta
        {
            public Guid Id { get; init; }

            public bool IsOutdated { get; init; }
        }
    }
}
