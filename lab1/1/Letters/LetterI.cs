using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _1.Letters;
public sealed class LetterI : Letter
{
    public LetterI( double x, double y, Brush color ) : base( x, y, color )
    {
    }

    protected override void Build()
    {
        canvas.Children.Add( new Rectangle { Width = 20, Height = 100, Fill = color } );

        canvas.Children.Add( new Polygon
        {
            Fill = color,
            Points = new PointCollection { new Point( 0, 85 ), new Point( 60, 5 ), new Point( 80, 10 ), new Point( 20, 95 ) }
        } );

        canvas.Children.Add( new Rectangle
        {
            Width = 20,
            Height = 100,
            Fill = color,
            Margin = new Thickness( 60, 0, 0, 0 )
        } );
    }
}
