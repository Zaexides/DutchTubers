namespace DutchTubers.Server.Services
{
    public class SecretProvider : ISecretProvider
    {
        private readonly EnvironmentVariableTarget _environmentVariableTarget;

        public SecretProvider(IWebHostEnvironment env)
        {
            _environmentVariableTarget = env.IsDevelopment() ? EnvironmentVariableTarget.User : EnvironmentVariableTarget.Process;
        }

        public string GetTwitchClientID()
        {
            return Environment.GetEnvironmentVariable("TTV_CLIENT_ID", _environmentVariableTarget);
        }

        public string GetTwitchSecret()
        {
            return Environment.GetEnvironmentVariable("TTV_SECRET", _environmentVariableTarget);
        }
    }
}
