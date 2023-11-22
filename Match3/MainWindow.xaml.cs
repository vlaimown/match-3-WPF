using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Threading;

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
        public MainWindow()
        {
            InitializeComponent();
            Time.Visibility = System.Windows.Visibility.Hidden;
            Score.Visibility = System.Windows.Visibility.Hidden;

            //Polygon polygon = new Polygon();
            //polygon.Points = new PointCollection() { new System.Windows.Point(400, 400), new System.Windows.Point(200, 200), new System.Windows.Point(400, 0), new System.Windows.Point(600, 200) };
            //shape.Width = cell.Size.Width / coef;
            //shape.Height = cell.Size.Heigth / coef;
            //shape.Margin = new System.Windows.Thickness(cell.Point.X + (numInRow * cell.Size.Width + numInRow * cell.Size.Width), cell.Point.Y + (numInColumn * cell.Size.Heigth + numInColumn * cell.Size.Heigth), 0, 0);
            //polygon.Fill = System.Windows.Media.Brushes.Red;
            //shape.Stroke = System.Windows.Media.Brushes.Black;
            //shape.StrokeThickness = 2;
            //RootGrid.Children.Add(polygon);
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
                    Point point = new Point(-1000, -500);
                    Size size = new Size(50, 50);
                    Button button = new Button();
                    button.Foreground = System.Windows.Media.Brushes.Black;
                    button.Click += new RoutedEventHandler(TestButton_Click);

                    Cell cell = new Cell(button, point, size, /*new Rectangle(),*/ i, j);
                    field.GameField[i, j] = cell;
                    //field.GameFieldButtons[i,j] = button;
                    RootGrid.Children.Add(field.GameField[i, j].Button);
                }
            }

            for (int i = 0; i < field.GameField.GetLength(0); i++)
            {
                for (int j = 0; j < field.GameField.GetLength(1); j++)
                {
                    Cube cube = new Cube(i, j, 2, 2, field.GameField[i, j]);
                    Circle circle = new Circle(2, 2, field.GameField[i, j], i, j);
                    Figure2 figure2 = new Figure2(2, 2, field.GameField[i, j], i, j);
                    Figure3 figure3 = new Figure3(2, 2, field.GameField[i, j], i, j);
                    Figure_4 figure4 = new Figure_4(2, 2, field.GameField[i, j], i, j);
                    //Rhomb rhomb = new Rhomb(1, 1, field.GameField[i, j], i, j);

                    Item[] arr = { cube, circle, figure2, figure3, figure4/*, pentagon, rhomb, triangle*/ };
                    Item item = arr[random.Next(0, arr.Length)];

                    field.GameField[i, j].Item = item;

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

            txt.Text = ($"{curCell.Item.Shape.GetType()}");
        }
    }
}