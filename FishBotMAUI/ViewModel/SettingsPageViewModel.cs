using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FishBotMAUI.Services.FishBot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishBotMAUI.ViewModel
{
    public partial class SettingsPageViewModel : ObservableObject
    {
        public SettingsPageViewModel(CredentialSettings credentialSettings)
        {
            _credentialSettings = credentialSettings;
            UserName = credentialSettings?.Credentials?.TwitchUsername ?? string.Empty;
        }

        [ObservableProperty]
        private string _userName;
        [ObservableProperty]
        private string _password;
        private readonly CredentialSettings _credentialSettings;

        [RelayCommand]
        private void Save()
        {
            var pass = _password;
            Password = string.Empty;

            _credentialSettings.Set(_userName, pass);
        }
    }
}
