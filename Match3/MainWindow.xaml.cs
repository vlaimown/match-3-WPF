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

                    Cell cell = new Cell(i, j, button, point, size /*new Rectangle(),*/);
                    field.GameField[i, j] = cell;

                    point = SetPointCell(field.GameField[i, j]);

                    Canvas.SetLeft(cell.Button, point.X);
                    Canvas.SetTop(cell.Button, point.Y);

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
                    /*Figure3 figure3 = new Figure3(i, j, 2, 2, field.GameField[i, j]);
                    Figure_4 figure4 = new Figure_4(i, j, 2, 2, field.GameField[i, j]);*/
                    //Rhomb rhomb = new Rhomb(1, 1, field.GameField[i, j], i, j);

                    Item[] arr = { cube, circle, figure2, /*figure3, figure4/*, pentagon, rhomb, triangle*/ };
                    Item item = arr[random.Next(0, arr.Length)];

                    field.GameField[i, j].Item = item;

                    Point point = SetPointCell(field.GameField[i, j]);

                    Canvas.SetLeft(item.Shape, point.X);
                    Canvas.SetTop(item.Shape, point.Y);

                    RootGrid.Children.Add(item.Shape);
                }
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
                    if (field.GameField[i,j].Button == btn)
                    {
                        curCell = field.GameField[i,j];
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
                txt.Text = selectedCount.ToString();
            }

            if (selectedCount == 2)
            {
                Point point = null;

                curCell.Button.Background = System.Windows.Media.Brushes.Cyan;
                txt.Text = selectedCount.ToString();

                var tmp1 = selectedCell1;
                var tmp2 = selectedCell2;

                var tmp = selectedCell1.Item;
                selectedCell1.Item = selectedCell2.Item;
                selectedCell2.Item = tmp;

                RootGrid.Children.Remove(tmp1.Item.Shape);
                RootGrid.Children.Remove(tmp2.Item.Shape);

                selectedCount = 0;
                txt.Text = $"первый элемент - {selectedCell1.Item.GetType()}, второй - {selectedCell2.Item.GetType()}";

                point = SetPointCell(selectedCell1);

                Canvas.SetLeft(selectedCell1.Item.Shape, point.X);
                Canvas.SetTop(selectedCell1.Item.Shape, point.Y);

                RootGrid.Children.Add(selectedCell1.Item.Shape);

                point = SetPointCell(selectedCell2);

                Canvas.SetLeft(selectedCell2.Item.Shape, point.X);
                Canvas.SetTop(selectedCell2.Item.Shape, point.Y);

                RootGrid.Children.Add(selectedCell2.Item.Shape);

                selectedCell1.Button.Background = System.Windows.Media.Brushes.White;
                selectedCell2.Button.Background = System.Windows.Media.Brushes.White;

                int match = 0;

                int directionX = selectedCell1.ColNum - selectedCell2.ColNum;
                int directionY = selectedCell1.RowNum - selectedCell2.RowNum;

                rowNum = selectedCell2.RowNum;
                colNum = selectedCell2.ColNum;

                txt.Text = $"X - {directionX} Y - {directionY}";

                if (directionX == 0)
                {
                    if (directionY == 1) 
                    {
                        if (selectedCell2.Item.GetType() == field.GameField[rowNum + 1, colNum].Item.GetType())
                            match++;
                    }
                    if (directionY == -1)
                    {
                        if (selectedCell2.Item.GetType() == field.GameField[rowNum - 1, colNum].Item.GetType())
                            match++;
                    }
                    if (selectedCell2.Item.GetType() == field.GameField[rowNum, colNum + 1].Item.GetType())
                        match++;
                    if (selectedCell2.Item.GetType() == field.GameField[rowNum, colNum - 1].Item.GetType())
                        match++;
                }
                else if (directionY == 0)
                {
                    if (directionX == 1)
                    {
                        if (selectedCell2.Item.GetType() == field.GameField[rowNum, colNum + 1].Item.GetType())
                            match++;
                    }
                    if (directionX == -1)
                    {
                        if (selectedCell2.Item.GetType() == field.GameField[rowNum, colNum - 1].Item.GetType())
                            match++;
                    }
                    if (selectedCell2.Item.GetType() == field.GameField[rowNum + 1, colNum].Item.GetType())
                        match++;
                    if (selectedCell2.Item.GetType() == field.GameField[rowNum - 1, colNum].Item.GetType())
                        match++;
                }

                if (match >= 2)
                {
                    txt.Text = "ogo!!";
                }
            }
        }



        private void TestButton_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void SwitchElements()
        {

        }

        private void SwitchElementsBack()
        {
        }

        private Point SetPointCell(Cell cell)
        {
            int x = cell.Point.X + (cell.ColNum * cell.Size.Width);
            int y = cell.Point.Y + (cell.RowNum * cell.Size.Heigth);
            Point point = new Point(x, y);

            return point;
        }
    }
}