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
        public Cube(int numInColumn, int numInRow, int value, int coef, Cell cell) : base(numInColumn, numInRow, value, coef, cell) 
        {
            base.numInColumn = numInColumn;
            base.numInRow = numInRow;

            base.value = value;
            base.shape = new Rectangle();
            base.cell = cell;

            shape.Width = cell.Size.Width/coef;
            shape.Height = cell.Size.Heigth/coef;
            //shape.Margin = new System.Windows.Thickness(base.cell.Point.X + (base.numInRow * base.cell.Size.Width + base.numInRow * base.cell.Size.Width), base.cell.Point.Y + (base.numInColumn * base.cell.Size.Heigth + base.numInColumn * base.cell.Size.Heigth), 0, 0);
            shape.Fill = System.Windows.Media.Brushes.Blue;
            shape.Stroke = System.Windows.Media.Brushes.Black;
            shape.StrokeThickness = 2;
        }
    }
}
