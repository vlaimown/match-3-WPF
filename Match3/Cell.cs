using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Match3
{
    public class Cell
    {
        private Point point;
        private Size size;
        private Item item;
        private Button btn;

        private int rowNum;
        private int colNum;
        //private System.Windows.Shapes.Rectangle shape;

        public Cell(int numRow, int numColumn, Button button, Point point, Size size/*System.Windows.Shapes.Rectangle rectangle,*/)
        {
            this.rowNum = numRow;
            this.colNum = numColumn;

            this.btn = button;
            //this.shape = rectangle;
            this.point = point;
            this.size = size;

            btn.Width = size.Width;
            btn.Height = size.Heigth;
            //btn.Margin = new System.Windows.Thickness(this.point.X + (rowNum * size.Width + rowNum * size.Width), this.point.Y + (colNum * size.Heigth + colNum * size.Heigth), 0, 0);
            //btn.Margin = new System.Windows.Thickness(this.point.X + (rowNum * size.Width), this.point.Y + (colNum * size.Heigth), 0, 0);
            btn.Background = System.Windows.Media.Brushes.White;
            btn.BorderBrush = System.Windows.Media.Brushes.Red;

            //Canvas.SetLeft(btn, this.point.X);
            //Canvas.SetTop(btn, this.point.Y);

            //shape.Width = size.Width;
            //shape.Height = size.Heigth;
            //shape.Margin = new System.Windows.Thickness(this.point.X + (numInRow * size.Width + numInRow * size.Width), this.point.Y + (numInColumn * size.Heigth + numInColumn * size.Heigth), 0, 0);
            //shape.Fill = System.Windows.Media.Brushes.White;
            //shape.Stroke = System.Windows.Media.Brushes.Red;
            //shape.StrokeThickness = 2;
        }

        //public System.Windows.Shapes.Rectangle Shape
        //{
        //    get { return shape; }
        //}

        public Size Size
        {
            get { return size; }
            set { size = value; }
        }

        public Point Point
        {
            get { return point; }
            set { point = value; }
        }

        public Item Item
        {
            get { return item; }
            set { item = value; }
        }

        public Button Button { get { return btn; } set { btn = value; } }

        public int RowNum 
        { 
            get { return rowNum; } 
            set { rowNum = value; } 
        }
        public int ColNum 
        { 
            get { return colNum; } 
            set { colNum = value; } 
        }
    }
}
