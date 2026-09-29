using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _1.Letters;
public sealed class LetterA : Letter
{
    public LetterA( double x, double y, Brush color ) : base( x, y, color )
    { }

    protected override void Build()
    {
        canvas.Children.Add( new Polygon
        {
            Fill = color,
            Points = new PointCollection { new Point( 0, 100 ), new Point( 20, 0 ), new Point( 40, 0 ), new Point( 20, 100 ) }
        } );

        canvas.Children.Add( new Polygon
        {
            Fill = color,
            Points = new PointCollection { new Point(60, 100), new Point(40,0), new Point(60, 0), new Point(80, 100) }
        } );

        canvas.Children.Add( new Rectangle
        {
            Width = 40,
            Height = 20,
            Fill = color,
            Margin = new Thickness( 20, 50, 0, 0 ),
        } );
    }
}
