namespace DutchTubers.Server.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now
        {
            get
            {
                return DateTime.UtcNow;
            }
        }
    }
}
