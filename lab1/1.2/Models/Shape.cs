using System.Windows;
using System.Windows.Media;

namespace _1._2.Models;
public abstract class Shape
{
    protected readonly Brush _color;

    protected Shape( Brush color )
    {
        _color = color;
    }

    public abstract void Draw(DrawingContext drawingContext);

    public abstract void Move( double x, double y );

    public abstract Rect GetBounds();
}
