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

        public FishPageViewModel()
        {

            var assemblyPath = Assembly.GetExecutingAssembly().Location;
            assemblyPath = Path.GetDirectoryName(assemblyPath);
            Debug.Assert(assemblyPath != null, nameof(assemblyPath) + " != null");
            var saveFilePath = Path.Combine(assemblyPath, "Settings.json");
            var dbFilePath = Path.Combine(assemblyPath, "Database.json");

            fishList.LoadDatabase(dbFilePath);

            for (int i = 0; i < 5; i++)
            {
                fishList.Add(new FishRecord
                {
                    Name = $"Fish {i.ToString()}",
                    Probability = i / 100
                });
            }


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

            var fish = new FishRecord { Name = EntryName, Probability = EntryProbability };

            var exists = FishList.Any(x =>
            {
                x.Probability = fish.Probability;
                return x.Name.Equals(fish.Name);
            });

            if (exists)
            {
                await Shell.Current.DisplayAlert("Error", $"{fish.Name} already exists. Updating probability.", "OK");
            }
            else
            {
                FishList.Add(fish);
            }
        }


        [ObservableProperty]
        ObservableCollection<FishRecord> fishList = new();


        [ObservableProperty] private string defaultFishName = "Goldfish";
        [ObservableProperty] private string fishPageTitle = "Fish list";
    }
}
