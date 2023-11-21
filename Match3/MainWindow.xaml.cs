using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Match3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random random = new Random();

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

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            Field field = new Field(new Cell[4, 4]);

            PlayButton.Visibility = System.Windows.Visibility.Hidden;
            Time.Visibility = System.Windows.Visibility.Visible;
            Score.Visibility = System.Windows.Visibility.Visible;

            for (int i = 0; i < field.GameField.GetLength(0); i++)
            {
                for (int j = 0; j < field.GameField.GetLength(1); j++)
                {
                    Point point = new Point(-750, 250);
                    Size size = new Size(50, 50);
                    Cell cell = new Cell(point, size, new Rectangle(), i, j);
                    field.GameField[i, j] = cell;
                    RootGrid.Children.Add(cell.Shape);
                    //d.Text += $"{field.GameField[i, j].Size.Width} ";
                }
            }

            for (int i = 0; i < field.GameField.GetLength(0); i++)
            {
                for (int j = 0; j < field.GameField.GetLength(1); j++)
                {
                    Cube cube = new Cube(i, j, 2, 2, field.GameField[i, j]);
                    Circle circle = new Circle(2, 2, field.GameField[i, j], i, j);
                    Rhomb rhomb = new Rhomb(1, 1, field.GameField[i, j], i, j);

                    Item[] arr = { cube, circle, rhomb/*, pentagon, rhomb, triangle*/ };
                    Item item = arr[random.Next(0, arr.Length)];

                    field.GameField[i, j].Item = item;

                    RootGrid.Children.Add(rhomb.Shape);
                }
            }
        }
    }
}
