namespace DutchTubers.Server.Services
{
    public interface IDateTimeProvider
    {
        public DateTime Now { get; }
    }
}
