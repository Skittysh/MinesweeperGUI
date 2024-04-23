using System.Windows;

namespace MinesweeperGUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private void StartGameButton_Click(object sender, RoutedEventArgs e)
{
    int size = int.Parse(sizeInputField.Text);
    int mines = int.Parse(minesInputField.Text);

    GamePage gamePage = new GamePage(size, mines);
    startGameButton.Visibility = Visibility.Collapsed;
    sizeInputField.Visibility = Visibility.Collapsed;
    minesInputField.Visibility = Visibility.Collapsed;
    mainFrame.Navigate(gamePage);
}

    public MainWindow()
    {
        InitializeComponent();
    }
}