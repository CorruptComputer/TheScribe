using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using TheScribe.Application;
using TheScribe.Application.Model;

namespace TheScribe.GUI.Views
{
    public partial class MainWindow : Window
    {
        private readonly StackPanel _warningPanel;
        private readonly TextBlock _warningReason;
        private readonly TextBox _backupLocationSetting;

        private ConfigurationManager _configurationManager;
        
        public MainWindow()
        {
            InitializeComponent();

            _warningPanel = this.Find<StackPanel>("WarningPanel");
            _warningReason = this.Find<TextBlock>("WarningReason");
            _backupLocationSetting = this.Find<TextBox>("BackupLocationSetting");

            _configurationManager = new ConfigurationManager();
        }

        #region Backup Tab
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