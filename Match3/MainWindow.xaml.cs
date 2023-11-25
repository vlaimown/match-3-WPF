using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Shapes;
using System.IO;
using System.Drawing;
using System.Windows.Documents;

namespace Match3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random random = new Random();

        private int currentTime = 0;
        private int maxTime = 5000;

        private DispatcherTimer dt = new DispatcherTimer();

        private Field field = new Field(new Cell[8, 8]);

        private Cell selectedCell1 = null;
        private Cell selectedCell2 = null;

        private int selectedCount = 0;

        private int rowNum = 0;
        private int colNum = 0;

        private Score score = new Score();
        public MainWindow()
        {
            InitializeComponent();

            Time.Visibility = System.Windows.Visibility.Hidden;
            Score.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Timer_Start()
        {
            if (dt == null)
                dt = new DispatcherTimer();

            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += dtTicker;
            dt.Start();
        }

        private void dtTicker(object sender, EventArgs e)
        {
            if (currentTime > 0)
            {
                currentTime--;
                Time.Text = $"TIME: {currentTime}";
            }
            else
            {
                dt.Stop();
                dt = null;

                if (MessageBox.Show("Game Over", "", MessageBoxButton.OK, MessageBoxImage.Stop) == MessageBoxResult.OK)
                {
                    for (int i = 0; i < field.GameField.GetLength(0); i++)
                    {
                        for (int j = 0; j < field.GameField.GetLength(1); j++)
                        {
                            PlayButton.Visibility = System.Windows.Visibility.Visible;

                            Time.Visibility = System.Windows.Visibility.Hidden;
                            Score.Visibility = System.Windows.Visibility.Hidden;

                            RootGrid.Children.Remove(field.GameField[i, j].Button);
                            RootGrid.Children.Remove(field.GameField[i, j].Item.Shape);
                        }
                    }
                }
            }
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            Time.Text = $"TIME: {maxTime}";
            currentTime = maxTime;
            Timer_Start();

            PlayButton.Visibility = System.Windows.Visibility.Hidden;

            Time.Visibility = System.Windows.Visibility.Visible;
            Score.Visibility = System.Windows.Visibility.Visible;

            for (int i = 0; i < field.GameField.GetLength(0); i++)
            {
                for (int j = 0; j < field.GameField.GetLength(1); j++)
               {
                    Point point = new Point(100, 100);
                    Size size = new Size(25, 25);
                    Button button = new Button();

                    button.Click += new RoutedEventHandler(TestButton_Click);

                    Cell cell = new Cell(i, j, button, point, size);
                    field.GameField[i, j] = cell;

                    point = SetPointCell(field.GameField[i, j]);

                    SetCanvasPosition(cell.Button, point.X, point.Y);

                    RootGrid.Children.Add(field.GameField[i, j].Button);
                }
            }

            for (int i = 0; i < field.GameField.GetLength(0); i++)
            {
                for (int j = 0; j < field.GameField.GetLength(1); j++)
                {
                    Cube cube = new Cube(2, 2, field.GameField[i, j]);
                    Circle circle = new Circle(2, 2, field.GameField[i, j]);
                    Figure2 figure2 = new Figure2(2, 2, field.GameField[i, j]);
        //            /*Figure3 figure3 = new Figure3(i, j, 2, 2, field.GameField[i, j]);
        //            Figure_4 figure4 = new Figure_4(i, j, 2, 2, field.GameField[i, j]);*/
        //            //Rhomb rhomb = new Rhomb(1, 1, field.GameField[i, j], i, j);

                    Item[] arr = { cube, circle, figure2, /*figure3, figure4/*, pentagon, rhomb, triangle*/ };
                    Item item = arr[random.Next(0, arr.Length)];

                    field.GameField[i, j].Item = item;

                    Point point = SetPointCell(field.GameField[i, j]);

                    SetCanvasPosition(item.Shape, point.X, point.Y);

                    RootGrid.Children.Add(item.Shape);
                }
            }
            RandomItemsFill();
        }

        private void RandomItemsFill()
        {
            int horizontalMatch = 0;
            Item curItem = null;

            //int upItem = 0;
            //int downItem = 0;
            //int rightItem = 0;

            for (int i = 0; i < field.GameField.GetLength(0); i++)
            {
                for (int j = 0; j < field.GameField.GetLength(1); j++)
                {
                    //curItem = field.GameField[i, j].Item;

                    if (i == 0 || i == field.GameField.GetLength(0) - 1)
                    {
                        if (j + 1 < field.GameField.GetLength(1))
                        {
                            if (field.GameField[i, j].Item.GetType() == field.GameField[i, j + 1].Item.GetType())
                            {
                                horizontalMatch++;
                                //txt.Text = $"{horizontalMatch}";
                                if (horizontalMatch >= 2)
                                {
                                    RandomDontMatchItem(i, j, 0, 1);
                                    horizontalMatch = 0;
                                }
                            }
                        }
                    }

                    if (j == 0 && i > 0 && i < field.GameField.GetLength(0) - 1)
                    {
                        Item currentItem = field.GameField[i, j].Item;
                        if (currentItem.GetType() == field.GameField[i, j + 1].Item.GetType() && currentItem.GetType() == field.GameField[i + 1, j].Item.GetType() && currentItem.GetType() == field.GameField[i - 1, j].Item.GetType())
                            RandomDontMatchItem(i, j, 0, 1);

                        if (currentItem.GetType() == field.GameField[i + 1, j].Item.GetType() && currentItem.GetType() == field.GameField[i - 1, j].Item.GetType())
                            RandomDontMatchItem(i, j, -1, 0);

                        //txt.Text = $"работает!";
                    }

                    if (j == field.GameField.GetLength(0) - 1 && i > 0 && i < field.GameField.GetLength(0) - 1)
                    {
                        Item currentItem = field.GameField[i, j].Item;
                        if (currentItem.GetType() == field.GameField[i, j - 1].Item.GetType() && currentItem.GetType() == field.GameField[i + 1, j].Item.GetType() && currentItem.GetType() == field.GameField[i - 1, j].Item.GetType())
                            RandomDontMatchItem(i, j, 0, -1);

                        if (currentItem.GetType() == field.GameField[i + 1, j].Item.GetType() && currentItem.GetType() == field.GameField[i - 1, j].Item.GetType())
                            RandomDontMatchItem(i, j, -1, 0);
                        //txt.Text = $"{horizontalMatch}";
                    }
                }
                horizontalMatch = 0;
            }
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = e.Source as Button;
            Cell curCell = null;

            for (int i = 0; i < field.GameField.GetLength(0); i++)
            {
                for (int j = 0; j < field.GameField.GetLength(1); j++)
                {
                    if (field.GameField[i, j].Button == btn)
                    {
                        curCell = field.GameField[i, j];
                        break;
                    }
                }
            }

            if (selectedCount == 0)
            {
                selectedCell1 = curCell;
                selectedCount = 1;
            }
            else if (selectedCount == 1)
            {
                if (curCell.ColNum == selectedCell1.ColNum - 1 && curCell.RowNum == selectedCell1.RowNum ||
                    curCell.ColNum == selectedCell1.ColNum + 1 && curCell.RowNum == selectedCell1.RowNum ||
                    curCell.ColNum == selectedCell1.ColNum && curCell.RowNum == selectedCell1.RowNum + 1 ||
                    curCell.ColNum == selectedCell1.ColNum && curCell.RowNum == selectedCell1.RowNum - 1)
                {
                    selectedCell2 = curCell;
                    selectedCount = 2;
                    txt.Text = selectedCount.ToString();
                }
                else
                {
                    selectedCount = 0;
                    selectedCell1.Button.Background = System.Windows.Media.Brushes.White;
                    selectedCell1 = null;
                    curCell.Button.Background = System.Windows.Media.Brushes.White;
                }

                if (selectedCell1 == curCell)
                {
                    selectedCount = 0;
                    selectedCell1.Button.Background = System.Windows.Media.Brushes.White;
                    selectedCell1 = null;
                    curCell.Button.Background = System.Windows.Media.Brushes.White;
                }
            }

            if (selectedCount == 1)
            {
                curCell.Button.Background = System.Windows.Media.Brushes.Cyan;
                txt.Text = $"{curCell.Item}";
            }

            if (selectedCount == 2)
            {
                Point point = null;
                selectedCount = 0;

                curCell.Button.Background = System.Windows.Media.Brushes.Cyan;

                var tmp1 = selectedCell1;
                var tmp2 = selectedCell2;

                var tmp = selectedCell1.Item;
                selectedCell1.Item = selectedCell2.Item;
                selectedCell2.Item = tmp;

                RootGrid.Children.Remove(tmp1.Item.Shape);
                RootGrid.Children.Remove(tmp2.Item.Shape);

                point = SetPointCell(selectedCell1);
                SetCanvasPosition(selectedCell1.Item.Shape, point.X, point.Y, selectedCell1.Size.Width / 2, selectedCell1.Size.Heigth / 2);
                RootGrid.Children.Add(selectedCell1.Item.Shape);

                point = SetPointCell(selectedCell2);
                SetCanvasPosition(selectedCell2.Item.Shape, point.X, point.Y, selectedCell2.Size.Width / 2, selectedCell2.Size.Heigth / 2);
                RootGrid.Children.Add(selectedCell2.Item.Shape);

                selectedCell1.Button.Background = System.Windows.Media.Brushes.White;
                selectedCell2.Button.Background = System.Windows.Media.Brushes.White;

                int directionX = selectedCell1.ColNum - selectedCell2.ColNum;
                int directionY = selectedCell1.RowNum - selectedCell2.RowNum;

                List<Item> matchedItems1 = new List<Item>();
                List<Item> matchedItems2 = new List<Item>();
                List<Item> matchedItems3 = new List<Item>();

                rowNum = selectedCell2.RowNum;
                colNum = selectedCell2.ColNum;

                //if (directionX != 0) 
                //{
                //    if (selectedCell2.Item.GetType() == field.GameField[nextRow + 1, colNum].Item.GetType())
                //    {
                //        FindMatch(rowNum, colNum, matchedItems1, 1, 0);
                //    }

                //    if (selectedCell2.Item.GetType() == field.GameField[prevRow - 1, colNum].Item.GetType())
                //    {
                //        FindMatch(rowNum, colNum, matchedItems2, -1, 0);
                //    }

                //    if (selectedCell2.Item.GetType() == field.GameField[rowNum, colNum + directionX].Item.GetType())
                //    {
                //        FindMatch(rowNum, colNum, matchedItems3, 0, directionX);
                //    }
                //}
                if (directionY != 0)
                {
                    if (colNum + 1 < field.GameField.GetLength(1))
                        if (selectedCell2.Item.GetType() == field.GameField[rowNum, colNum + 1].Item.GetType())
                            FindMatch(rowNum, colNum, matchedItems1, 0, 1);
                    
                    if (colNum - 1 >= 0)
                        if (selectedCell2.Item.GetType() == field.GameField[rowNum, colNum - 1].Item.GetType())
                            FindMatch(rowNum, colNum, matchedItems2, 0, -1);

                    if (rowNum + directionY >= 0 && rowNum + directionY < field.GameField.GetLength(0))
                        if (selectedCell2.Item.GetType() == field.GameField[rowNum + directionY, colNum].Item.GetType())
                            FindMatch(rowNum, colNum, matchedItems3, directionY, 0);
                }

                txt.Text = $"{matchedItems1.Count + matchedItems2.Count + matchedItems3.Count}";

                //if (matchedItems1.Count >= 2)
                //    Match(matchedItems1);

                //if (matchedItems2.Count >= 2)
                //    Match(matchedItems2);

                //if (matchedItems3.Count >= 2)
                //    Match(matchedItems3);

                //        if (matchedItems1.Count == 0 && matchedItems2.Count == 0 && matchedItems3.Count == 0)
                //        {
                //            if (directionX == 0)
                //            {
                //                if (selectedCell2.Item.GetType() == field.GameField[rowNum, colNum + 1].Item.GetType())
                //                    FindMatch(0, 1, matchedItems1);

                //                if (selectedCell2.Item.GetType() == field.GameField[rowNum, colNum - 1].Item.GetType())
                //                    FindMatch(0, -1, matchedItems2);
                //            }

                //            else if (directionY == 0)
                //            {
                //                if (selectedCell2.Item.GetType() == field.GameField[rowNum + 1, colNum].Item.GetType())
                //                    FindMatch(1, 0, matchedItems1);

                //                if (selectedCell2.Item.GetType() == field.GameField[rowNum - 1, colNum].Item.GetType())
                //                    FindMatch(-1, 0, matchedItems2);
                //            }
                //        }
            }
        }

        private void Match(List<Item> matchedList)
        {
            foreach (var item in matchedList)
            {
                RootGrid.Children.Remove(item.Shape);
                score.Value += item.Value;
                item.Cell.Item = null;
            }
            score.Value += selectedCell2.Item.Value;
            RootGrid.Children.Remove(selectedCell2.Item.Shape);
            selectedCell2.Item = null;
            Score.Text = $"Score: {score.Value}";
        }

        private void FindMatch(int row, int col, List<Item> matchedItems, int increaseRow = 0, int increaseCol = 0)
        {
            Item curItem = selectedCell2.Item;

            row += increaseRow;
            col += increaseCol;

            if (row >= 0 && col >= 0 && row < field.GameField.GetLength(0) && col < field.GameField.GetLength(1))

            while (curItem.GetType() == field.GameField[row, col].Item.GetType())
            {
                matchedItems.Add(field.GameField[row, col].Item);
                curItem = field.GameField[row, col].Item;

                if ((row + increaseRow >= 0 && row + increaseRow < field.GameField.GetLength(0)) &&
                    (row + increaseCol >= 0 && col + increaseCol < field.GameField.GetLength(1)))
                {
                    row += increaseRow;
                    col += increaseCol;
                }
                else
                    break;
            }
        }

        //private void FindMatch(int increaseRow, int increaseCol, List<Item> matchedItems)
        //{
        //    Item curItem = selectedCell2.Item;

        //    while (curItem.GetType() == field.GameField[rowNum + increaseRow, colNum + increaseCol].Item.GetType())
        //    {
        //        matchedItems.Add(field.GameField[rowNum + increaseRow, colNum + increaseCol].Item);
        //        curItem = field.GameField[rowNum + increaseRow, colNum + increaseCol].Item;

        //        if ((rowNum + increaseRow >= 0 && rowNum + increaseRow < field.GameField.GetLength(0)) &&
        //            (colNum + increaseCol >= 0 && colNum + increaseCol < field.GameField.GetLength(1)))
        //        {
        //            increaseRow += increaseRow;
        //            increaseCol += increaseCol;
        //        }
        //        else
        //            break;
        //    }
        //}

        //private void TestButton_Click_1(object sender, RoutedEventArgs e)
        //{

        //}

        //private void SwitchElements()
        //{

        //}

        //private void SwitchElementsBack()
        //{

        //}

        private void SetCanvasPosition(UIElement element, int x, int y, int offsetX = 0, int offsetY = 0)
        {
            Canvas.SetLeft(element, x);
            Canvas.SetTop(element, y);
        }

        private Point SetPointCell(Cell cell)
        {
            int x = cell.Point.X + (cell.ColNum * cell.Size.Width);
            int y = cell.Point.Y + (cell.RowNum * cell.Size.Heigth);
            Point point = new Point(x, y);

            return point;
        }

        private void RandomDontMatchItem(int row, int col, int rowOffset = 0, int colOffset = 0)
        {
            Item randomItem = RandomItem(row + rowOffset, col + colOffset);

            while (field.GameField[row + rowOffset, col + colOffset].Item.GetType() == randomItem.GetType())
                randomItem = RandomItem(row + rowOffset, col + colOffset);

            RootGrid.Children.Remove(field.GameField[row + rowOffset, col + colOffset].Item.Shape);

            Point point = SetPointCell(field.GameField[row + rowOffset, col + colOffset]);
            SetCanvasPosition(randomItem.Shape, point.X, point.Y);

            field.GameField[row + rowOffset, col + colOffset].Item = randomItem;
            RootGrid.Children.Add(randomItem.Shape);
        }

        private Item RandomItem(int row, int col)
        {
            Cube cube = new Cube(2, 2, field.GameField[row, col]);
            Circle circle = new Circle(2, 2, field.GameField[row, col]);
            Figure2 figure2 = new Figure2(2, 2, field.GameField[row, col]);
            //            /*Figure3 figure3 = new Figure3(i, j, 2, 2, field.GameField[i, j]);
            //            Figure_4 figure4 = new Figure_4(i, j, 2, 2, field.GameField[i, j]);*/
            //            //Rhomb rhomb = new Rhomb(1, 1, field.GameField[i, j], i, j);

            Item[] arr = { cube, circle, figure2, /*figure3, figure4/*, pentagon, rhomb, triangle*/ };
            Item item = arr[random.Next(0, arr.Length)];

            return item;
        }
    }
}