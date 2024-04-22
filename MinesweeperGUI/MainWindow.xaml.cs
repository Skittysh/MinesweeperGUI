using System.Windows;

namespace MinesweeperGUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private void DrawButton_Click(object sender, RoutedEventArgs e)
    {
        int n = int.Parse(inputField.Text);

        GamePage gamePage = new GamePage(n);
        drawButton.Visibility = Visibility.Collapsed;
        inputField.Visibility = Visibility.Collapsed;
        mainFrame.Navigate(gamePage);
    }

    public MainWindow()
    {
        InitializeComponent();
    }
}