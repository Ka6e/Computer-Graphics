using System.Windows;
using System.Windows.Media;

namespace _1._2.Models;
public class Polygon : Shape
{
    private PointCollection _points;
    public Polygon(PointCollection points, Brush color ) : base( color )
    {
        _points = points;
    }

    public override void Draw( DrawingContext drawingContext )
    {
        StreamGeometry geometry = new StreamGeometry();
        using ( var ctx = geometry.Open() )
        {
            ctx.BeginFigure( _points[ 0 ], true, true );
            ctx.PolyLineTo( _points.Skip( 1 ).ToList(), true, true );
        }

        drawingContext.DrawGeometry( _color, new Pen( Brushes.Black, 1 ), geometry );
    }

    public override Rect GetBounds()
    {
        double minX = _points.Min( p => p.X );
        double maxX = _points.Max( p => p.X );
        double minY = _points.Min( p => p.Y );
        double maxY = _points.Max( p => p.Y );

        return new Rect( new Point( minX, minY ), new Point( maxX, maxY ) );
    }

    public override void Move( double x, double y )
    {
        for ( int i = 0; i < _points.Count; i++ )
        {
            _points[ i ] = new Point( _points[ i ].X + x, _points[ i ].Y + y );
        }
    }
}
