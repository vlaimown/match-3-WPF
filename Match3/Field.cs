using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3
{
    public class Field
    {
        private Cell[,] gameField;
        public Field(Cell[,] field)
        {
            this.gameField = field;
        }

        public Cell[,] GameField 
        {
            get { return gameField; }
            set { gameField = value; }
        }
    }
}
