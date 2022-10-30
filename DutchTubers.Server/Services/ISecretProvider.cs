namespace DutchTubers.Server.Services
{
    public interface ISecretProvider
    {
        string GetTwitchClientID();

        string GetTwitchSecret();
    }
}
