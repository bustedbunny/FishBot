using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FishBotMAUI.Pages;

namespace FishBotMAUI.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty] private string defaultFishName;

        [ObservableProperty] private string fishPageName = nameof(FishPage);
        [ObservableProperty] private string usersPageName = nameof(FishPage);
        [ObservableProperty] private string settingsPageName = nameof(FishPage);

        public MainPageViewModel()
        {
            
        }
    }
}
