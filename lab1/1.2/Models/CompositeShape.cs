using System.Windows;
using System.Windows.Media;

namespace _1._2.Models;
public class CompositeShape : Shape
{
    private readonly List<Shape> _shapes;

    public CompositeShape( List<Shape> shapes ) : base( Brushes.Transparent )
    {
        _shapes = shapes;
    }

    public override void Draw( DrawingContext drawingContext )
    {
        foreach ( var shape in _shapes )
        {
            shape.Draw( drawingContext );
        }
    }

    public override Rect GetBounds()
    {
        Rect bounds = _shapes[ 0 ].GetBounds();
        foreach ( var shape in _shapes )
        {
            bounds.Union( shape.GetBounds() );
        }

        return bounds;
    }

    public override void Move( double x, double y )
    {
        foreach ( var shape in _shapes )
        {
            shape.Move( x, y );
        }
    }
}
