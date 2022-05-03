using Serilog;

namespace TheScribe.Application;

public static class Backups
{
    public static List<string> GetBackupListForGame(string selectedGame, string backupLocation)
    {
        if (string.IsNullOrWhiteSpace(selectedGame))
        {
            Log.Information($"No backups to populate, no game selected.");
            return new();
        }
        
        string gameBackupFolder = Path.Combine(backupLocation, selectedGame);
        if (!Directory.Exists(gameBackupFolder))
        {
            Log.Information($"No backups to populate, game '{selectedGame}' has no backup directory created yet.");
            return new();
        }

        var backupFiles = Directory.GetFiles(gameBackupFolder).Where(x => x.EndsWith(".scribe"));

        List<string> backupNames = new();
        foreach (string file in backupFiles)
        {
            string backupName = Path.GetFileNameWithoutExtension(file);
            backupNames.Add(backupName);
        }
        
        Log.Information($"Loaded {backupNames.Count} backups for selected game '{selectedGame}'.");
        return backupNames;
    }
}