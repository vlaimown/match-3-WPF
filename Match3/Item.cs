using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Match3
{
    public class Item
    {
        //Graphics graphics;

        protected Cell cell;
        protected Shape shape;

        protected int coef;

        //Rectangle rectangle = new Rectangle();
        //DrawingVisual drawingVisual = new DrawingVisual();
        //protected Image image;

        protected int value;

        public Item(int value, int coef, Cell cell, Shape shape = null) 
        {
            this.value = value;
            this.coef = coef;
            this.shape = shape;
            this.cell = cell;
        }

        public Shape Shape { get { return shape; } }
    }
}
