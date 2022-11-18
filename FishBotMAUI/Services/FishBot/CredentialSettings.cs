using TwitchLib.Client.Models;

namespace FishBotMAUI.Services.FishBot
{
    public class CredentialSettings
    {
        private const string UserName = nameof(UserName);
        private const string Token = nameof(Token);

        public CredentialSettings()
        {
            LoadSaved();
        }

        private async void LoadSaved()
        {
            var userName = await SecureStorage.Default.GetAsync(UserName);
            var token = await SecureStorage.Default.GetAsync(Token);
            if (userName is not null && token is not null)
            {
                Set(userName, token);
            }
        }
        public ConnectionCredentials Credentials { get; private set; }

        public void Set(string username, string password)
        {
            Credentials = new ConnectionCredentials(username, password);

            SecureStorage.Default.SetAsync(UserName, username);
            SecureStorage.Default.SetAsync(Token, password);
        }
    }
}
