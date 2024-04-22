using System.Windows.Media;
using System.Windows.Shapes;

namespace MinesweeperGUI;

public class CellRectangle
{
    public int x;
    public int y;

    public Rectangle cell = new Rectangle
    {
        Width = 50,
        Height = 50,
        Fill = Brushes.Blue 
    };
}