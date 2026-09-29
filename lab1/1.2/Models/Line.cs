using System.Windows;
using System.Windows.Media;

namespace _1._2.Models;
public class Line : Shape
{
    Point _start;
    Point _end;
    public Line( Point start, Point end, Brush color ) : base( color )
    {
        _start = start;
        _end = end;
    }

    public override void Draw( DrawingContext drawingContext )
    {
        drawingContext.DrawLine( new Pen( _color, 1 ), _start, _end );
    }

    public override Rect GetBounds()
    {
        double minX = Math.Min( _start.X, _end.X );
        double maxX = Math.Max( _start.X, _end.X );
        double minY = Math.Min( _start.Y, _end.Y );
        double maxY = Math.Max( _start.Y, _end.Y );

        return new Rect( minX, minY, maxX - minX, maxY - minY );
    }

    public override void Move( double x, double y )
    {
        _start = new Point( _start.X + x, _start.Y + y );
        _end = new Point( _end.X + x, _end.Y + y );
    }
}
