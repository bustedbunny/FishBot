using CommunityToolkit.Mvvm.ComponentModel;
using FishBot;
using FishBotMAUI.Services.Logging;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Events;
using TwitchLib.Communication.Models;

namespace FishBotMAUI.Services.FishBot
{
    public class FishBotController
    {

        public readonly TwitchClient client;
        private readonly Logger _logger;
        private readonly CredentialSettings _credentialSettings;
        private readonly FishingDatabase _fishingDatabase;

        public FishBotController(Logger logger, CredentialSettings credentialSettings, FishingDatabase fishingDatabase)
        {
            _credentialSettings = credentialSettings;
            _fishingDatabase = fishingDatabase;
            var clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };
            var customClient = new WebSocketClient(clientOptions);
            client = new TwitchClient(customClient);

            _logger = logger;

            client.OnMessageReceived += OnMessageRecieved;
        }

        private void OnMessageRecieved(object sender, OnMessageReceivedArgs e)
        {
            if (e.ChatMessage.Message.StartsWith("!fish"))
            {
                var message = _fishingDatabase.GetRandom();

                if (message is not null)
                {
                    message = message.Replace("{user}", "@" + e.ChatMessage.DisplayName);
                }
                else
                {
                    message = "Sorry, no fish :(";
                }

                _logger.Message(message);

                client.SendMessage(e.ChatMessage.Channel, message);
            }
        }

        public void Connect()
        {
            if (_credentialSettings.Credentials is null)
            {
                _logger.Message("Cannot connect. Check credentials");
                return;
            }
            client.Initialize(_credentialSettings.Credentials, _credentialSettings.Credentials.TwitchUsername);
            if (!client.IsInitialized)
            {
                _logger.Message("Cannot connect uninitialized. Check credentials");
            }
            else
            {
                _logger.Message("Attempt to connect...");
                try
                {
                    client.Connect();
                }
                catch (Exception ex)
                {
                    _logger.Message($"{ex.Message}");
                }

            }
        }
        public void Disconnect()
        {
            client.Disconnect();
            _logger.Message("Disconnecting...");
        }
    }
}
