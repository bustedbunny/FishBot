using FishBotMAUI.Services.Logging;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace FishBot;


[Serializable]
public class FishingDatabase
{
    public readonly string DatabaseName = "FishDatabase.json";
    public readonly string DatabasePath;
    public readonly string DatabaseFolder;
    private readonly Logger _logger;


    private int _version;
    public int Version => _version;
    public FishingDatabase(Logger logger)
    {
        _logger = logger;




        //DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FishBot");
        //DatabasePath = Path.Combine(DatabasePath, DatabaseName);

        DatabaseFolder = FileSystem.AppDataDirectory;

        DatabasePath = Path.Combine(FileSystem.AppDataDirectory, DatabaseName);


        _logger.Message(DatabasePath);

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

    public void RemoveRecord(string name)
    {
        for (int i = 0; i < Content.Count; i++)
        {
            if (Content[i].Name.Equals(name))
            {
                Content.RemoveAt(i);
                _logger.Message($"Removing fish named '{name}'.");
                return;
            }
        }
    }
    public void RemoveRecord(FishRecord record)
    {
        Content.Remove(record);
    }
}