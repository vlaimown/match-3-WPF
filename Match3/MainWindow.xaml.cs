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
        Random random = new Random();

        private int decrement = 4;
        private int time = 6000;
        DispatcherTimer dt = new DispatcherTimer();

        Field field = new Field(new Cell[8, 8]);

        Cell selectedCell1 = null;
        Cell selectedCell2 = null;

        int selectedCount = 0;
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
            if (decrement > 0)
            {
                decrement--;
                Time.Text = $"TIME: {decrement}";
            }
            else
            {
                dt.Stop();
                dt = null;
                if (MessageBox.Show("Game Over", "", MessageBoxButton.OK, MessageBoxImage.Question) == MessageBoxResult.OK)
                {
                    for (int i = 0; i < field.GameField.GetLength(0); i++)
                    {
                        for (int j = 0; j < field.GameField.GetLength(1); j++)
                        {
                            PlayButton.Visibility = System.Windows.Visibility.Visible;

                            Time.Visibility = System.Windows.Visibility.Hidden;
                            Score.Visibility = System.Windows.Visibility.Hidden;

                            RootGrid.Children.Remove(field.GameField[i,j].Button);
                            RootGrid.Children.Remove(field.GameField[i,j].Item.Shape);
                        }
                    }
                }
            }
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            Time.Text = $"TIME: {time}";
            decrement = time;
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
                    button.Foreground = System.Windows.Media.Brushes.Black;
                    button.Click += new RoutedEventHandler(TestButton_Click);

                    Cell cell = new Cell(button, point, size, /*new Rectangle(),*/ i, j);
                    field.GameField[i, j] = cell;

                    int posX = cell.Point.X + (cell.RowNum * cell.Size.Width);
                    int posY = cell.Point.Y + (cell.ColNum * cell.Size.Heigth);
                    Canvas.SetLeft(cell.Button, posX);
                    Canvas.SetTop(cell.Button, posY);

                    RootGrid.Children.Add(field.GameField[i, j].Button);
                }
            }

            for (int i = 0; i < field.GameField.GetLength(0); i++)
            {
                for (int j = 0; j < field.GameField.GetLength(1); j++)
                {
                    Cube cube = new Cube(i, j, 2, 2, field.GameField[i, j]);
                    Circle circle = new Circle(i, j, 2, 2, field.GameField[i, j]);
                    Figure2 figure2 = new Figure2(i, j, 2, 2, field.GameField[i, j]);
                    Figure3 figure3 = new Figure3(i, j, 2, 2, field.GameField[i, j]);
                    Figure_4 figure4 = new Figure_4(i, j, 2, 2, field.GameField[i, j]);
                    //Rhomb rhomb = new Rhomb(1, 1, field.GameField[i, j], i, j);

                    Item[] arr = { cube, circle, figure2, figure3, figure4/*, pentagon, rhomb, triangle*/ };
                    Item item = arr[random.Next(0, arr.Length)];

                    field.GameField[i, j].Item = item;

                    int posX = field.GameField[i, j].Point.X + (field.GameField[i, j].RowNum * field.GameField[i, j].Size.Width);
                    int posY = field.GameField[i, j].Point.Y + (field.GameField[i, j].ColNum * field.GameField[i, j].Size.Heigth);

                    Canvas.SetLeft(item.Shape, posX);
                    Canvas.SetTop(item.Shape, posY);

                    RootGrid.Children.Add(item.Shape);
                }
            }
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = e.Source as Button;
            Cell curCell = null;

            int rowNum = 0;
            int colNum = 0;

            for (int i = 0; i < field.GameField.GetLength(0); i++)
            {
                for (int j = 0; j < field.GameField.GetLength(1); j++)
                {
                    if (field.GameField[i,j].Button == btn)
                    {
                        colNum = i;
                        rowNum = j;
                        curCell = field.GameField[i,j];
                        break;
                    }
                }
            }

            if (selectedCount == 0)
            {
                selectedCell1 = curCell;
                selectedCount = 1;
                txt.Text = selectedCount.ToString();
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
                txt.Text = $"первый элемент - {selectedCell1.Item.Shape}, второй - {selectedCell2.Item.Shape}";

                int posX = selectedCell1.Point.X + (selectedCell1.RowNum * selectedCell1.Size.Width);
                int posY = selectedCell1.Point.Y + (selectedCell1.ColNum * selectedCell1.Size.Heigth);

                Canvas.SetLeft(selectedCell1.Item.Shape, posX);
                Canvas.SetTop(selectedCell1.Item.Shape, posY);

                RootGrid.Children.Add(selectedCell1.Item.Shape);

                posX = selectedCell2.Point.X + (selectedCell2.RowNum * selectedCell2.Size.Width);
                posY = selectedCell2.Point.Y + (selectedCell2.ColNum * selectedCell2.Size.Heigth);

                Canvas.SetLeft(selectedCell2.Item.Shape, posX);
                Canvas.SetTop(selectedCell2.Item.Shape, posY);

                RootGrid.Children.Add(selectedCell2.Item.Shape);

                selectedCell1.Button.Background = System.Windows.Media.Brushes.White;
                selectedCell2.Button.Background = System.Windows.Media.Brushes.White;
            }
        }



        private void TestButton_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void SwitchElements()
        {

        }
    }
}