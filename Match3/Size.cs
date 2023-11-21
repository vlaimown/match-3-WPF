using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3
{
    public class Size
    {
        private int width = 0;
        private int heigth = 0;

        public Size(int width, int heigth)
        {
            this.width = width;
            this.heigth = heigth;
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Heigth
        {
            get { return heigth; }
            set { heigth = value; }
        }
    }
}
