using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FishBot;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace FishBotMAUI.ViewModel
{
    public partial class FishPageViewModel : ObservableObject
    {
        private readonly FishingDatabase _database;
        public FishPageViewModel(FishingDatabase fishingDatabase)
        {
            _database = fishingDatabase;
            _fishList = fishingDatabase.Content;
        }

        [ObservableProperty]
        private string _entryName;
        [ObservableProperty]
        private float _entryProbability;

        [RelayCommand]
        private async void AddFish()
        {
            if (string.IsNullOrEmpty(EntryName))
            {
                await Shell.Current.DisplayAlert("Error", "Invalid fish name.", "OK");
                return;
            }

            _database.AddRecord(EntryName, EntryProbability);
        }
        [RelayCommand]
        private void RemoveFish(FishRecord fishRecord)
        {
            _database.RemoveRecord(fishRecord);
        }


        [RelayCommand]
        private void OpenDatabaseFolder()
        {

            ProcessStartInfo lPsi = new()
            {
                FileName = "explorer",
                Arguments = string.Format("/root,{0}", _database.DatabaseFolder),
                UseShellExecute = true
            };

            Process lNewProcess = new()
            {
                StartInfo = lPsi
            };
            lNewProcess.Start();
        }

        [ObservableProperty]
        ObservableCollection<FishRecord> _fishList = new();


        [ObservableProperty] private string _defaultFishName = "Goldfish";
        [ObservableProperty] private string _fishPageTitle = "Fish list";
    }
}
