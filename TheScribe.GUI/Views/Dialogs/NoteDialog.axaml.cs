using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace TheScribe.GUI.Views;

public partial class NoteDialog : Window
{
    public NoteDialog(string message) : this()
    {
        this.Find<TextBlock>("DialogText").Text = message;
    }
    
    public NoteDialog()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    private void OkClicked(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}