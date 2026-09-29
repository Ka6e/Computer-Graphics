using System.Windows;
using System.Windows.Media;
using _1._2.Models;

namespace _1._2;
public static class HouseFactory
{
    public static CompositeShape CreateHouse()
    {
        Rectangle wall = new Rectangle( new Rect( 150, 220, 200, 150 ), Brushes.Bisque );
        Rectangle door = new Rectangle( new Rect( 270, 270, 50, 100 ), Brushes.SaddleBrown );
        Triangle roof = new Triangle(
            new Point( 130, 220 ),
            new Point( 250, 140 ),
            new Point( 370, 220 ),
            Brushes.Red );
        var fence = CreateFence();
        var windwo = CreateWindow();
        var chimney = new Polygon( new PointCollection()
        {
            new Point(310, 180),
            new Point(330, 194),
            new Point(330, 150),
            new Point(310, 150)
        }, Brushes.Gray );


        List<Shape> shapes = new List<Shape>()
        {
            wall, door, roof, fence, windwo, chimney
        };

        return new CompositeShape( shapes );
    }

    private static CompositeShape CreateFence()
    {
        List<Shape> shapes = new List<Shape>();
        for ( int i = 0; i < 8; i++ )
        {
            shapes.Add( new Rectangle( new Rect( 100 + i * 40, 320, 20, 60 ), Brushes.Peru ) );
        }

        return new CompositeShape( shapes );
    }

    private static CompositeShape CreateWindow()
    {

        var window = new Rectangle( new Rect( 180, 250, 60, 60 ), Brushes.LightBlue );

        var verticalLine = new Line(
            new Point( 210, 250 ),
            new Point( 210, 310 ),
            Brushes.Black );

        var horizontalLine = new Line(
            new Point( 180, 280 ),
            new Point( 240, 280 ),
            Brushes.Black );

        List<Shape> shapes = new List<Shape>()
        {
            window, horizontalLine, verticalLine
        };

        return new CompositeShape( shapes );
    }
}
