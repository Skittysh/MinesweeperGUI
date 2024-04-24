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

        public GamePage(int n, int d)
        {
            game = new Game(n, d);
            InitializeComponent();
            int m = 5;
            DrawSquares(n,m);
        }

        private bool ifCanClick = true;

        public void DrawSquares(int n, int m)
        {
            int squareSize = 30;
            int spacing = 5;
            int totalWidth = n * squareSize + (n - 1) * spacing;
            int startX = (int)((myCanvas.Width - totalWidth) / 2);
            int startY = (int)((myCanvas.Height - totalWidth) / 2);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    var rectangle = new CellRectangle()
                    {
                        x = i, y = j,
                    };

                    var textBlock = new TextBlock
                    {
                        Uid = i + " " + j,
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
                                    for (int iterator_x = 0; iterator_x < n; iterator_x++)
                                    {
                                        for (int iterator_y = 0; iterator_y < m; iterator_y++)
                                        {
                                            if (game.EmptyCell(iterator_x, iterator_y) &&
                                                (game.board.GetMinesAround(rectangle.x, rectangle.y) == 0))
                                            {
                                                var rectangle2 = myCanvas.Children.OfType<Rectangle>()
                                                    .First(r => r.Uid == iterator_x + " " + iterator_y);
                                                if (game.getMines(iterator_x, iterator_y) == 0)
                                                    rectangle2.Fill = Brushes.Orange;
                                                if (game.getMines(iterator_x, iterator_y) != 0)
                                                    rectangle2.Fill = Brushes.OrangeRed;


                                                var textBlock2 = myCanvas.Children.OfType<TextBlock>()
                                                    .First(r => r.Uid == iterator_x + " " + iterator_y);
                                                textBlock2.Text = (game.getMines(iterator_x, iterator_y).ToString());
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
                                for(int i = 0; i < n; i++)
                                {
                                    for(int j = 0; j < m; j++)
                                    {
                                        if (game.board.hasMine(i, j))
                                        {
                                            var rectangle2 = myCanvas.Children.OfType<Rectangle>()
                                                .First(r => r.Uid == i + " " + j);
                                            ImageBrush bombBrush = new ImageBrush();
                                            bombBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/bomber.png",
                                                UriKind.RelativeOrAbsolute));
                                            rectangle2.Fill = bombBrush;
                                            var textBlock2 = myCanvas.Children.OfType<TextBlock>()
                                                .First(r => r.Uid == i+ " " + j);
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

                    Canvas.SetLeft(rectangle.cell, startX + j * (squareSize + spacing));
                    Canvas.SetTop(rectangle.cell, startY + i * (squareSize + spacing));

                    rectangle.cell.Uid = i + " " + j;
                    Console.WriteLine(rectangle.cell.Uid);
                    myCanvas.Children.Add(rectangle.cell);

                    Canvas.SetLeft(textBlock,
                        startX + j * (squareSize + spacing) + squareSize / 2 - textBlock.ActualWidth / 2);
                    Canvas.SetTop(textBlock,
                        startY + i * (squareSize + spacing) + squareSize / 2 - textBlock.ActualHeight / 2);

                    myCanvas.Children.Add(textBlock);
                }
            }
        }
    }
}