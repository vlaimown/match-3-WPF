using System;
using System.Collections.Generic;
using System.Windows.Shapes;

namespace Match3
{
    public class Item
    {
        protected Cell cell;
        protected Shape shape;

        protected int coef;

        protected int value;

        public Item(int value, int coef, Cell cell, Shape shape = null) 
        {
            this.value = value;
            this.coef = coef;
            this.shape = shape;
            this.cell = cell;
        }
        public Cell Cell { get { return cell; } set { cell = value; } }
        public Shape Shape { get { return shape; } set { shape = value; } }
        public int Value { get { return value; } }
    }
}
