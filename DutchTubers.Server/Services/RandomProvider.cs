namespace DutchTubers.Server.Services
{
    public class RandomProvider : IRandomProvider
    {
        private readonly Random _random;

        public RandomProvider()
        {
            _random = new Random();
        }

        public int Next()
        {
            return _random.Next();
        }
    }
}
