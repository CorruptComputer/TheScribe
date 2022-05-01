using System.Text.Json.Serialization;

namespace TheScribe.Application.Model;

[Serializable]
public class Settings
{
    [JsonPropertyName("BackupLocation")]
    public string BackupLocation { get; set; }
}