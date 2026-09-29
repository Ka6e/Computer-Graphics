using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using _1.Letters;

namespace _1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Canvas canvas = new Canvas()
            {
                Background = Brushes.White,
            };

            Content = canvas;


            Letter v = new LetterV( 575, 250, Brushes.Blue );
            Letter i = new LetterI( 650, 250, Brushes.Green );
            Letter a = new LetterA( 750, 250, Brushes.Red );

            canvas.Children.Add( v.Visual );
            canvas.Children.Add( i.Visual );
            canvas.Children.Add( a.Visual );

            v.Jump(0);
            i.Jump(1.0);
            a.Jump(2.3);
        }
    }
}