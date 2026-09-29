using System.Windows;
using System.Windows.Media;

namespace _1._2.Models;
public class Rectangle : Shape
{
    private Rect _rect;

    public Rectangle( Rect rect, Brush color ) : base( color )
    {
        _rect = rect;
    }

    public override void Draw( DrawingContext drawingContext )
    {
        drawingContext.DrawRectangle(_color, new Pen(Brushes.Black, 1), _rect);
    }

    public override Rect GetBounds() => _rect;

    public override void Move( double x, double y )
    {
        _rect.Offset( x, y );
    }
}
