using System.Windows;
using System.Windows.Media;

namespace _1._2.Models;
public class Triangle : Shape
{
    private PointCollection _points;
    public Triangle( Point p1, Point p2, Point p3, Brush color ) : base( color )
    {
        _points = new PointCollection()
        {
            p1, p2, p3
        };
    }

    public override void Draw( DrawingContext drawingContext )
    {
        var geometry = new StreamGeometry();

        using ( var ctx = geometry.Open() )
        {
            ctx.BeginFigure( _points[ 0 ], true, true );
            ctx.LineTo( _points[ 1 ], true, false );
            ctx.LineTo( _points[ 2 ], true, false );
        }

        geometry.Freeze(); // повышает производительность

        drawingContext.DrawGeometry(
            _color,
            new Pen( Brushes.Black, 1 ),
            geometry );
        //StreamGeometry geometry = new StreamGeometry();
        //using ( var ctx = geometry.Open() )
        //{
        //    ctx.BeginFigure( _points[ 0 ], true, true );
        //    ctx.PolyLineTo( _points.Skip( 1 ).ToList(), true, true );
        //}

        //drawingContext.DrawGeometry( _color, new Pen( Brushes.Black, 1 ), geometry );
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
