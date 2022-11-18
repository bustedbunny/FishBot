using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FishBotMAUI.Pages;
using FishBotMAUI.Services.FishBot;
using FishBotMAUI.Services.Logging;
using System.Collections.ObjectModel;
using TwitchLib.Client.Events;
using TwitchLib.Communication.Events;
using TwitchLib.Communication.Interfaces;

namespace FishBotMAUI.ViewModel
{
    public partial class StatusPageViewModel : ObservableObject
    {
        private readonly FishBotController _fishBot;
        private readonly Logger _logger;
        [ObservableProperty] private ObservableCollection<string> _logs;

        public StatusPageViewModel(Logger logger, FishBotController fishBot)
        {
            _logger = logger;
            _logs = logger.Messages;
            _fishBot = fishBot;

            _fishBot.client.OnConnected += OnConnected;
            _fishBot.client.OnConnectionError += OnConnectionError;
            _fishBot.client.OnDisconnected += OnDisconnected;
        }


        [RelayCommand]
        private void Connect()
        {
            _fishBot.Connect();
        }
        [RelayCommand]
        private void Disconnect()
        {
            _fishBot.Disconnect();
        }

        private void OnConnected(object sender, OnConnectedArgs e)
        {
            _logger.Message($"Connected.");
            OnPropertyChanged(nameof(IsConnected));
            OnPropertyChanged(nameof(IsNotConnected));

        }
        private void OnConnectionError(object sender, OnConnectionErrorArgs e)
        {
            _logger.Message($"Couldn't connect. {e.Error.ToString()}");
            OnPropertyChanged(nameof(IsConnected));
            OnPropertyChanged(nameof(IsNotConnected));
        }

        private void OnDisconnected(object sender, OnDisconnectedEventArgs e)
        {
            _logger.Message($"Disconnected.");
            OnPropertyChanged(nameof(IsConnected));
            OnPropertyChanged(nameof(IsNotConnected));
        }

        public bool IsConnected => _fishBot.client.IsConnected;
        public bool IsNotConnected => !IsConnected;

    }
}
