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
    int size = int.Parse(sizeInputField.Text);
    int mines = int.Parse(minesInputField.Text);
        if (size == 1 || size == 2 || size == 3)
        {
            OneSize oneSize = new OneSize();
            mainFrame.Navigate(oneSize);
            startGameButton.Visibility = Visibility.Collapsed;
            sizeInputField.Visibility = Visibility.Collapsed;
            minesInputField.Visibility = Visibility.Collapsed;
            Size.Visibility = Visibility.Collapsed;
            Mines.Visibility = Visibility.Collapsed;
            return;
        }
        
        if ( size > 20)
        {
            MessageBox.Show("Size must be less than 20");
            return;
        }
    GamePage gamePage = new GamePage(size, mines);
    startGameButton.Visibility = Visibility.Collapsed;
    sizeInputField.Visibility = Visibility.Collapsed;
    minesInputField.Visibility = Visibility.Collapsed;
    Size.Visibility = Visibility.Collapsed;
    Mines.Visibility = Visibility.Collapsed;

    mainFrame.Navigate(gamePage);
}

    public MainWindow()
    {
        InitializeComponent();
    }
}