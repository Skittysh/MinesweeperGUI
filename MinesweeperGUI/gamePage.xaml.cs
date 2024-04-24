using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MinesweeperGUI
{
    public partial class GamePage : Page
    {
        private Game game;

        public GamePage(int mines, int width, int height)
        {
            game = new Game(mines, width, height);
            InitializeComponent();
            DrawSquares(width, height);
        }

        private bool ifCanClick = true;

        public void DrawSquares(int height, int width)
        {
            int squareSize = 30;
            int spacing = 5;
           int totalHeight = height * squareSize + (height - 1) * spacing + 1;
            int totalWidth = width * squareSize + (width - 1) * spacing + 1;
            int startRow = (int)((myCanvas.Height - totalHeight) / 2);
            int startCol = (int)((myCanvas.Width - totalWidth) / 2);

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    var rectangle = new CellRectangle()
                    {
                        x = row, y = col,
                    };

                    var textBlock = new TextBlock
                    {
                        Uid = row + " " + col,
                        Foreground = Brushes.White,
                        FontSize = 16,
                        FontWeight = FontWeights.UltraBlack,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    
                        rectangle.cell.MouseLeftButtonDown += (sender, e) =>
                        {
                            if (ifCanClick)
                            {
                            if (!game.board.isRevealed(rectangle.x, rectangle.y))
                            {
                                game.ExecuteReveal(rectangle.x, rectangle.y);
                                ((Rectangle)sender).Fill = Brushes.OrangeRed;
                                textBlock.Text = (game.getMines(rectangle.x, rectangle.y).ToString());
                                if (game.EmptyCell(rectangle.x, rectangle.y))
                                {
                                    for (int board_row = 0; board_row < height; board_row++)
                                    {
                                        for (int board_col = 0; board_col < width; board_col++)
                                        {
                                            if (game.EmptyCell(board_row, board_col) &&
                                                (game.board.GetMinesAround(rectangle.x, rectangle.y) == 0))
                                            {
                                                var rectangle2 = myCanvas.Children.OfType<Rectangle>()
                                                    .First(r => r.Uid == board_row + " " + board_col);
                                                if (game.getMines(board_row, board_col) == 0)
                                                    rectangle2.Fill = Brushes.Orange;
                                                if (game.getMines(board_row, board_col) != 0)
                                                    rectangle2.Fill = Brushes.OrangeRed;


                                                var textBlock2 = myCanvas.Children.OfType<TextBlock>()
                                                    .First(r => r.Uid == board_row + " " + board_col);
                                                textBlock2.Text = (game.getMines(board_row, board_col).ToString());
                                            }
                                        }
                                    }
                                }

                                Console.WriteLine(game.board.FieldsToReveal);
                                if (game.board.FieldsToReveal == 0)
                                {
                                    var mainWindow = (MainWindow)Application.Current.MainWindow;
                                    mainWindow.mainFrame.Navigate(new WinBoard());
                                    mainWindow.mainFrame.NavigationService.RemoveBackEntry();
                                }
                            }
                            }
                            if (game.board.hasMine(rectangle.x, rectangle.y) && ifCanClick)
                            {
                                ifCanClick = false;
                                for(int board_row = 0; board_row < height; board_row++)
                                {
                                    for(int board_col = 0; board_col < width; board_col++)
                                    {
                                        if (game.board.hasMine(board_row, board_col))
                                        {
                                            var rectangle2 = myCanvas.Children.OfType<Rectangle>()
                                                .First(r => r.Uid == board_row + " " + board_col);
                                            ImageBrush bombBrush = new ImageBrush();
                                            bombBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/bomber.png",
                                                UriKind.RelativeOrAbsolute));
                                            rectangle2.Fill = bombBrush;
                                            var textBlock2 = myCanvas.Children.OfType<TextBlock>()
                                                .First(r => r.Uid == board_row+ " " + board_col);
                                            textBlock2.Visibility = Visibility.Collapsed;
                                        }
                                    }
                                }
                                DoubleAnimation fadeOutAnimation = new DoubleAnimation
                                {
                                    From = 1.0,
                                    To = 0.0,
                                    Duration = new Duration(TimeSpan.FromSeconds(3)) // duration of 2 seconds

                                };

                                fadeOutAnimation.Completed += (s, e) =>
                                {
                                    var mainWindow = (MainWindow)Application.Current.MainWindow;
                                    mainWindow.mainFrame.Navigate(new LostBoard());
                                    mainWindow.mainFrame.NavigationService.RemoveBackEntry();
                                };

                                this.BeginAnimation(Page.OpacityProperty, fadeOutAnimation);
                            }
                        };
                    

                    rectangle.cell.MouseRightButtonDown += (sender, e) =>
                    {
                        if (ifCanClick)
                        {
                            if (!game.board.hasFlag(rectangle.x, rectangle.y))
                            {
                                game.board.ToggleFlag(rectangle.x, rectangle.y);
                                ImageBrush flagBrush = new ImageBrush();
                                flagBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/flaggy.png",
                                    UriKind.RelativeOrAbsolute));
                                ((Rectangle)sender).Fill = flagBrush;
                            }
                            else if (game.board.hasFlag(rectangle.x, rectangle.y))
                            {
                                if (game.board.isRevealed(rectangle.x, rectangle.y))
                                {
                                    if (game.board.GetMinesAround(rectangle.x, rectangle.y) == 0)
                                        ((Rectangle)sender).Fill = Brushes.Orange;
                                    else if (game.board.GetMinesAround(rectangle.x, rectangle.y) != 0)
                                        ((Rectangle)sender).Fill = Brushes.OrangeRed;
                                }
                                else if (game.board.hasFlag(rectangle.x, rectangle.y))
                                {
                                    ((Rectangle)sender).Fill = Brushes.Blue;
                                }

                                game.board.ToggleFlag(rectangle.x, rectangle.y);
                            }
                            // ((Rectangle)sender).Fill = Brushes.Green;
                        }
                    };

                    Canvas.SetLeft(rectangle.cell, startRow + col * (squareSize + spacing));
                    Canvas.SetTop(rectangle.cell, startCol + row * (squareSize + spacing));

                    rectangle.cell.Uid = row + " " + col;
                    Console.WriteLine(rectangle.cell.Uid);
                    myCanvas.Children.Add(rectangle.cell);

                    Canvas.SetLeft(textBlock,
                        startRow + col * (squareSize + spacing) + squareSize / 2 - textBlock.ActualWidth / 2);
                    Canvas.SetTop(textBlock,
                        startCol + row * (squareSize + spacing) + squareSize / 2 - textBlock.ActualHeight / 2);

                    myCanvas.Children.Add(textBlock);
                }
            }
        }
    }
}