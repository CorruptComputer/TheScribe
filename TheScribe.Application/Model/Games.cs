using System.Text.Json.Serialization;

namespace TheScribe.Application.Model;

[Serializable]
public class Games
{
    [JsonPropertyName("SupportedGames")]
    public List<Game> SupportedGames { get; set; }
}

[Serializable]
public class Game
{
    [JsonPropertyName("Name")]
    public string Name { get; set; }
    
    [JsonPropertyName("SaveLocations")]
    public List<string> SaveLocations { get; set; }
}