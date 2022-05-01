using System.Text.Json;
using Serilog;
using TheScribe.Application.Model;

namespace TheScribe.Application;

public class ConfigurationManager
{
    private const string _gamesLocation = "./Config/SupportedGames.json";
    private Games _games;

    private string _settingsLocation;
    private Settings _settings;

    private string _userHomeDir;
    
    public ConfigurationManager()
    {
        InitGamesList();

        GetUserHomeDir();
        GetUserSettingsLocation();
        InitUserSettings();
    }

    #region Init
    private void InitGamesList()
    {
        string fullPath = Path.GetFullPath(_gamesLocation);
        if (!File.Exists(fullPath))
        {
            throw new ApplicationException($"Unable to locate SupportedGames.json: {fullPath}");
        }
        
        string gamesJson = File.ReadAllText(fullPath);
        _games = JsonSerializer.Deserialize<Games>(gamesJson) 
                 ?? throw new ApplicationException($"There was a problem reading SupportedGames.json: {fullPath}");
    }

    private void GetUserHomeDir()
    {
        _userHomeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        Log.Information($"Set user home dir to: {_userHomeDir}");
    }

    private void GetUserSettingsLocation()
    {
        const string dirName = "TheScribe/";
        string? xdgHome = Environment.GetEnvironmentVariable("XDG_CONFIG_HOME");

        string settingsDir = xdgHome is null 
            ? Path.Combine(Path.Combine(_userHomeDir, "Documents/"), dirName)
            : Path.Combine(Path.GetFullPath(xdgHome), dirName);

        if (!Directory.Exists(settingsDir))
        {
            Directory.CreateDirectory(settingsDir);
        }

        _settingsLocation = Path.Combine(settingsDir, "settings.json");
    }

    private void InitUserSettings()
    {
        if (File.Exists(_settingsLocation))
        {
            string settingsJson = File.ReadAllText(_settingsLocation);
            _settings = JsonSerializer.Deserialize<Settings>(settingsJson)
                        ?? throw new ApplicationException($"There was a problem reading settings: {_settingsLocation}");
            
            Log.Information($"Settings loaded from: {_settingsLocation}");
        }
        else
        {
            const string dirName = "TheScribe/backups/";
            string? xdgData = Environment.GetEnvironmentVariable("XDG_DATA_HOME");
            string backupLocation = xdgData is null 
                ? Path.Combine(Path.Combine(_userHomeDir, "Documents/"), dirName)
                : Path.Combine(Path.GetFullPath(xdgData), dirName);

            // Create setting with default values
            _settings = new Settings
            {
                BackupLocation = backupLocation
            };
            
            // Save the settings
            File.WriteAllText(_settingsLocation, JsonSerializer.Serialize(_settings));
            
            Log.Information($"Settings initialized at: {_settingsLocation}");
            Log.Information($"Set default backup location to: {backupLocation}");
        }
    }
    #endregion

    public Settings GetSettings()
    {
        return _settings;
    }

    public void SaveSettings(Settings settings)
    {
        _settings = settings;
        File.WriteAllText(_settingsLocation, JsonSerializer.Serialize(_settings));
    }
}