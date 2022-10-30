namespace DutchTubers.Server.Utils
{
    public interface ICacheMeta
    {
        Guid Id { get; }

        bool IsOutdated { get; }
    }
}
