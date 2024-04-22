using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MinesweeperGUI
{
    public partial class GamePage : Page
    {
        private Game game;

        public GamePage(int n)
        {
            game = new Game(n, n);
            InitializeComponent();
            DrawSquares(n);
            Console.WriteLine("debby");
        }


        public void DrawSquares(int n)
        {
            int squareSize = 50;
            int spacing = 10;
            int totalWidth = n * squareSize + (n - 1) * spacing;
            Console.WriteLine("debbysqre");
            int startX = (int)((myCanvas.Width - totalWidth) / 2);
            int startY = (int)((myCanvas.Height - totalWidth) / 2);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    var rectangle = new CellRectangle()
                    {
                        x = i, y = j,
                    };

                    var textBlock = new TextBlock
                    {
                        Uid = i + " " + j,
                        Text = (i * n + j + 1).ToString(),
                        Foreground = Brushes.White,
                        FontSize = 16,
                        FontWeight = FontWeights.UltraBlack,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    rectangle.cell.MouseLeftButtonDown += (sender, e) =>
                    {
                        Console.WriteLine("Click count: " + rectangle.x + " i" + rectangle.y);
                        game.ExecuteReveal(rectangle.x, rectangle.y);
                        ((Rectangle)sender).Fill = Brushes.Red;
                        textBlock.Text = (game.getMines(rectangle.x, rectangle.y).ToString());
                        if (game.EmptyCell(rectangle.x, rectangle.y))
                        {
                            for (int iterator_x = 0; iterator_x < n; iterator_x++)
                            {
                                for (int iterator_y = 0; iterator_y < n; iterator_y++)
                                {
                                    if (game.EmptyCell(iterator_x, iterator_y))
                                    {
                                        var rectangle2 = myCanvas.Children.OfType<Rectangle>()
                                            .First(r => r.Uid == iterator_x + " " + iterator_y);
                                        rectangle2.Fill = Brushes.Yellow;
                                        var textBlock2 = myCanvas.Children.OfType<TextBlock>()
                                            .First(r => r.Uid == iterator_x + " " + iterator_y);
                                        textBlock2.Text = "0";
                                    }
                                }
                            }
                        }
                    };

                    rectangle.cell.MouseRightButtonDown += (sender, e) => { ((Rectangle)sender).Fill = Brushes.Green; };

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