// See https://aka.ms/new-console-template for more information


using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using FishBot;
using Newtonsoft.Json;

var assemblyPath = Assembly.GetExecutingAssembly().Location;
assemblyPath = Path.GetDirectoryName(assemblyPath);
Debug.Assert(assemblyPath != null, nameof(assemblyPath) + " != null");
var saveFilePath = Path.Combine(assemblyPath, "Settings.json");

Settings? settings = null;
if (File.Exists(saveFilePath))
{
    settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(saveFilePath));
}

if (settings == null)
{
    settings = new Settings();
    Console.WriteLine($"Write username");
    settings.userName = Console.ReadLine() ?? throw new Exception($"User name is incorrect");

    Console.WriteLine($"Write token");
    settings.token = Console.ReadLine() ?? throw new Exception($"User name is incorrect");

    File.WriteAllText(saveFilePath, JsonConvert.SerializeObject(settings));
}

var dbFilePath = Path.Combine(assemblyPath, "Database.json");
var bot = new FishBot.FishBot(settings, FishingDatabase.LoadOrCreate(dbFilePath));
if (bot == null)
{
    throw new NullReferenceException("Bot is null");
}

while (true)
{
    var command = Console.ReadLine();
    Debug.Assert(command != null, nameof(command) + " != null");
    if (command.Equals("Add"))
    {
        AddNewFish();
    }
    else if (command.Equals("Exit"))
    {
        break;
    }
    else
    {
        Console.WriteLine("Unknown command");
    }
}

void AddNewFish()
{
    Console.WriteLine("Enter fish name");
    var name = Console.ReadLine();
    Console.WriteLine($"Enter {name}'s probability. Must be between 0 and 1.0");
    var probability =
        float.Parse(Console.ReadLine() ?? throw new InvalidOperationException("Couldn't parse such probability"),
            CultureInfo.InvariantCulture);
    if (probability is < 0f or > 1f)
    {
        Console.WriteLine("Incorrect probability. Must be between 0 and 1.0");
        return;
    }

    Debug.Assert(name != null, nameof(name) + " != null");
    bot.db.AddRecord(name, probability);
}