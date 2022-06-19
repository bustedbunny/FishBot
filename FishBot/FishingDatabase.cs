using Newtonsoft.Json;

namespace FishBot;

[Serializable]
public class FishingDatabase
{
    public Dictionary<string, float> fishList;
    private Random _random;

    private string _filePath;

    public FishingDatabase(string filePath)
    {
        _filePath = filePath;
        _random = new Random();
        fishList = new Dictionary<string, float>();
    }

    public static FishingDatabase LoadOrCreate(string filePath)
    {
        FishingDatabase? db = null;
        if (File.Exists(filePath))
        {
            var rawJson = File.ReadAllText(filePath);
            db = JsonConvert.DeserializeObject<FishingDatabase>(rawJson);
        }

        if (db != null)
        {
            db._random = new Random();
            if (db.fishList.Count == 0)
            {
                Console.WriteLine("There is no fish in the sea. To add new type 'Add'.");
            }
        }
        else
        {
            db = new FishingDatabase(filePath);
        }

        db._filePath = filePath;

        return db;
    }

    public void AddRecord(string name, float probability)
    {
        if (fishList.ContainsKey(name))
        {
            Console.WriteLine("Fish with that name already exists. Overriding probability");
            fishList[name] = probability;
        }
        else
        {
            fishList.Add(name, probability);
            Console.WriteLine($"Added {name} with {probability} probability successfully.");
        }

        File.WriteAllText(_filePath, JsonConvert.SerializeObject(this, Formatting.Indented));
    }

    public string GrabRandom()
    {
        var shuffledList = fishList.ToList();
        shuffledList.Shuffle();

        var rng = _random.NextDouble();
        Console.WriteLine(rng);
        double cumulativeProbability = 0f;
        foreach (var fishRecord in shuffledList)
        {
            cumulativeProbability += fishRecord.Value;
            if (rng <= cumulativeProbability)
            {
                return fishRecord.Key;
            }
        }

        return "Sorry, no fish :(";
    }
}