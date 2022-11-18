using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FishBot;
using FishBotMAUI.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FishBotMAUI.ViewModel
{
    public partial class FishPageViewModel : ObservableObject
    {
        private readonly FishingDatabase _database;
        public FishPageViewModel(FishingDatabase fishingDatabase)
        {
            _database = fishingDatabase;
            fishList = fishingDatabase.Content;
        }

        [ObservableProperty]
        private string entryName;
        [ObservableProperty]
        private float entryProbability;

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


        [ObservableProperty]
        ObservableCollection<FishRecord> fishList = new();


        [ObservableProperty] private string defaultFishName = "Goldfish";
        [ObservableProperty] private string fishPageTitle = "Fish list";
    }
}
