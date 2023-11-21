using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Match3
{
    public class Cube : Item
    {
        public Cube(int numInColumn, int numInRow, int value, int coef, Cell cell) : base(value, coef, cell) 
        {
            base.value = value;
            base.shape = new Rectangle();
            base.cell = cell;

            shape.Width = cell.Size.Width/coef;
            shape.Height = cell.Size.Heigth/coef;
            shape.Margin = new System.Windows.Thickness(cell.Point.X + (numInRow * cell.Size.Width + numInRow * cell.Size.Width), cell.Point.Y + (numInColumn * cell.Size.Heigth + numInColumn * cell.Size.Heigth), 0, 0);
            shape.Fill = System.Windows.Media.Brushes.Blue;
            shape.Stroke = System.Windows.Media.Brushes.Red;
            shape.StrokeThickness = 2;
        }
    }
}
