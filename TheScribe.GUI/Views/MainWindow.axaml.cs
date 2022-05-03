using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Serilog;
using TheScribe.Application;
using TheScribe.Application.Model;

namespace TheScribe.GUI.Views
{
    public partial class MainWindow : Window
    {
        private readonly StackPanel _warningPanel;
        private readonly TextBlock _warningReason;
        private readonly TextBox _backupLocationSetting;
        private readonly ComboBox _gamesSelector;
        private readonly ListBox _backupsList;

        private ConfigurationManager _configurationManager;
        
        public MainWindow()
        {
            InitializeComponent();

            _warningPanel = this.Find<StackPanel>("WarningPanel");
            _warningReason = this.Find<TextBlock>("WarningReason");
            _backupLocationSetting = this.Find<TextBox>("BackupLocationSetting");
            _gamesSelector = this.Find<ComboBox>("GameSelect");
            _backupsList = this.Find<ListBox>("BackupsList");

            _configurationManager = new ConfigurationManager();

            PopulateGamesList();
        }

        #region Backup Tab
        private void PopulateGamesList()
        {
            // TODO: There is probably a better way to do this.
            List<string> names = new();
            
            foreach (Game game in _configurationManager.GetGames().SupportedGames)
            {
                names.Add(game.Name);
            }
            
            _gamesSelector.Items = names;
            Log.Information($"Loaded {names.Count} games from configuration.");
        }
        
        private void OnGameSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            string selectedGame = (string) _gamesSelector.SelectedItem;
            string backupLocation = _configurationManager.GetSettings().BackupLocation;
            Backups.GetBackupListForGame(selectedGame, backupLocation);
        }
        
        private void OnBackupTabFocused(object? sender, GotFocusEventArgs e)
        {
            // Change items shown in backups list
        }
        
        private void ShowWarning(string message)
        {
            _warningPanel.IsVisible = true;
            _warningReason.Text = message;
        }

        private void ClearWarning()
        {
            _warningPanel.IsVisible = false;
            _warningReason.Text = "You shouldn't see this!";
        }
        
        private void BackupButtonClicked(object sender, RoutedEventArgs e)
        {
            ShowWarning("Test!");
        }
        
        private void RestoreButtonClicked(object sender, RoutedEventArgs e)
        {
            ClearWarning();
        }
        #endregion

        #region Settings Tab
        private void OnSettingsTabFocused(object? sender, GotFocusEventArgs e)
        {
            Settings settings = _configurationManager.GetSettings();
            _backupLocationSetting.Text = settings.BackupLocation;
        }

        private void SaveSettingsButtonClicked(object? sender, RoutedEventArgs e)
        {
            Settings newSettings = new Settings()
            {
                BackupLocation = _backupLocationSetting.Text
            };
            
            _configurationManager.SaveSettings(newSettings);
        }
        #endregion
    }
}