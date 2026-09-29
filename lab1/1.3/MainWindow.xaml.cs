using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace _1._3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var canvas = new Canvas()
            {
                Width = 1240,
                Height = 576,
                Background = Brushes.White,
            };
            Content = canvas;

            var drawer = new CircleDrawer( canvas );

            drawer.DrawCircle( 200, 200, 100, Colors.Red, 10, Colors.Green);
        }
    }
}