using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace FishBot;

public class FishBot
{
    public readonly TwitchClient client;
    public readonly FishingDatabase db;

    public FishBot(Settings settings, FishingDatabase db)
    {
        this.db = db;

        ConnectionCredentials credentials = new ConnectionCredentials(settings.userName, settings.token);
        var clientOptions = new ClientOptions
        {
            MessagesAllowedInPeriod = 750, ThrottlingPeriod = TimeSpan.FromSeconds(30)
        };
        WebSocketClient customClient = new WebSocketClient(clientOptions);
        client = new TwitchClient(customClient);
        client.Initialize(credentials, settings.userName);

        client.OnMessageReceived += Client_OnMessageReceived;

        if (client.Connect())
        {
            Console.WriteLine("Successfully connected");
        }
    }

    private void Client_OnMessageReceived(object? sender, OnMessageReceivedArgs e)
    {
        if (e.ChatMessage.Message.StartsWith("!fish"))
        {
            var message = db.fishList.GetRandom();

            if (message is not null)
            {
                message.Replace("{user}", "@" + e.ChatMessage.DisplayName);
            }
            else
            {
                message = "Sorry, no fish :(";
            }

            client.SendMessage(e.ChatMessage.Channel, message);
        }
    }
}