using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Match3
{
    public class Figure3 : Item
    {
        public Figure3(/*int numInRow, int numInColumn,*/ int value, int coef, Cell cell) : base(/*numInRow, numInColumn,*/ value, coef, cell)
        {
            //base.numInRow = numInRow;
            //base.numInColumn = numInColumn;

            base.value = value;
            base.shape = new Ellipse();
            base.cell = cell;

            shape.Width = cell.Size.Width / coef;
            shape.Height = cell.Size.Heigth / coef;
            //shape.Margin = new System.Windows.Thickness(base.cell.Point.X + (base.numInRow * base.cell.Size.Width + base.numInRow * base.cell.Size.Width), base.cell.Point.Y + (base.numInColumn * base.cell.Size.Heigth + base.numInColumn * base.cell.Size.Heigth), 0, 0);
            shape.Fill = System.Windows.Media.Brushes.Green;
            shape.Stroke = System.Windows.Media.Brushes.Black;
            shape.StrokeThickness = 2;
        }
    }
}
