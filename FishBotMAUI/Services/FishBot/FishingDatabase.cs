using FishBotMAUI.Services.Logging;
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
    private readonly string DatabaseName = "FishDatabase.json";
    private readonly string DatabasePath;
    private readonly Logger _logger;


    private int _version;
    public int Version => _version;
    public FishingDatabase(Logger logger)
    {
        _logger = logger;

        DatabasePath = Path.Combine(FileSystem.AppDataDirectory, DatabaseName);

#if DEBUG
        _logger.Message(DatabasePath);
#endif

        if (File.Exists(DatabasePath))
        {
            var rawJson = File.ReadAllText(DatabasePath);
            Content = JsonConvert.DeserializeObject<ObservableCollection<FishRecord>>(rawJson);

            _logger.Message($"Loaded database. It's count is {Content.Count}.");
        }
        else
        {
            Content = new();

            _logger.Message("No database found, creating new empty.");
        }
    }

    public readonly ObservableCollection<FishRecord> Content;

    public void Save()
    {
        File.WriteAllText(DatabasePath, JsonConvert.SerializeObject(Content, Formatting.Indented));
    }

    public void AddRecord(string name, float probability)
    {
        for (int i = 0; i < Content.Count; i++)
        {
            if (Content[i].Name.Equals(name))
            {
                Content[i] = new FishRecord { Name = name, Probability = probability };
                _logger.Message("Fish with that name already exists. Overriding probability");
                return;
            }
        }

        Content.Add(new FishRecord
        {
            Name = name,
            Probability = probability
        });

        _logger.Message($"Added {name} with {probability} probability successfully.");

        _version++;

        Save();
    }
}