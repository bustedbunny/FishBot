using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace FishBot;


public enum DeserizationResult
{
    Success,
    NoFile,
    FileIsNull,
    Empty
}
public static class FishingDatabaseUtility
{
    public static DeserizationResult LoadDatabase(this ICollection<FishRecord> fishList, string filePath)
    {
        if (File.Exists(filePath))
        {
            var rawJson = File.ReadAllText(filePath);
            var data = JsonConvert.DeserializeObject(rawJson, fishList.GetType()) as ICollection<FishRecord>;

            if (data != null)
            {
                if (data.Count == 0)
                {
                    return DeserizationResult.Empty;
                }

                fishList.Clear();

                foreach (var item in data)
                {
                    fishList.Add(item);
                }

                return DeserizationResult.Success;
            }
            return DeserizationResult.FileIsNull;
        }
        return DeserizationResult.NoFile;
    }
}


[Serializable]
public class FishingDatabase
{
    public ObservableCollection<FishRecord> fishList = new();

    public void Save(string filePath)
    {
        File.WriteAllText(filePath, JsonConvert.SerializeObject(this, Formatting.Indented));
    }

    public void AddRecord(string name, float probability)
    {
        for (int i = 0; i < fishList.Count; i++)
        {
            if (fishList[i].Name.Equals(name))
            {
                fishList[i] = new FishRecord { Name = name, Probability = probability };
                Console.WriteLine("Fish with that name already exists. Overriding probability");
                return;
            }
        }

        fishList.Add(new FishRecord
        {
            Name = name,
            Probability = probability
        });

        Console.WriteLine($"Added {name} with {probability} probability successfully.");
    }





}