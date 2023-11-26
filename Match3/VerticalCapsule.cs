using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Match3
{
    public class VerticalCapsule : Item
    {
        private int widthOffset;
        private int heigthOffset;
        public VerticalCapsule(int value, int coef, Cell cell) : base(value, coef, cell)
        {
            base.value = value;
            base.shape = new Ellipse();
            base.cell = cell;

            widthOffset = coef * 2 - 1;
            heigthOffset = coef * 2;

            shape.Width = cell.Size.Width / coef - widthOffset;
            shape.Height = cell.Size.Heigth / coef + heigthOffset;
            shape.Margin = new System.Windows.Thickness(shape.Width / coef + widthOffset, shape.Height / coef - heigthOffset, 0, 0);
            //shape.Margin = new System.Windows.Thickness(base.cell.Point.X + (base.numInRow * base.cell.Size.Width + base.numInRow * base.cell.Size.Width), base.cell.Point.Y + (base.numInColumn * base.cell.Size.Heigth + base.numInColumn * base.cell.Size.Heigth), 0, 0);
            shape.Fill = System.Windows.Media.Brushes.Green;
            shape.Stroke = System.Windows.Media.Brushes.Black;
            shape.StrokeThickness = 2;
        }
    }
}
