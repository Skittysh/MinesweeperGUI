using System.Reflection.Emit;
using System.Windows;

namespace MinesweeperGUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private void StartGameButton_Click(object sender, RoutedEventArgs e)
{
    int height = int.Parse(heightInputField.Text);
    int width = int.Parse(widthInputField.Text);
    int mines = int.Parse(minesInputField.Text);
        if (height == 1 || height == 2 || height == 3 || width == 1 || width == 2 || width == 3 || mines == 0)
        {
            OneSize oneSize = new OneSize();
            mainFrame.Navigate(oneSize);
            startGameButton.Visibility = Visibility.Collapsed;
            heightInputField.Visibility = Visibility.Collapsed;
            widthInputField.Visibility = Visibility.Collapsed;
            minesInputField.Visibility = Visibility.Collapsed;
            Height.Visibility = Visibility.Collapsed;
            Width.Visibility = Visibility.Collapsed;
            Mines.Visibility = Visibility.Collapsed;
            return;
        }
        
        if ( height > 25)
        {
            MessageBox.Show("Size must be less than 25");
            return;
        }
    GamePage gamePage = new GamePage(mines, height, width);
    startGameButton.Visibility = Visibility.Collapsed;
    heightInputField.Visibility = Visibility.Collapsed;
    widthInputField.Visibility = Visibility.Collapsed;
    minesInputField.Visibility = Visibility.Collapsed;
    Height.Visibility = Visibility.Collapsed;
    Width.Visibility = Visibility.Collapsed;
    Mines.Visibility = Visibility.Collapsed;

    mainFrame.Navigate(gamePage);
}

    public MainWindow()
    {
        InitializeComponent();
    }
}