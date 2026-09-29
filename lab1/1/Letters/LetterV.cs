using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _1.Letters;
public sealed class LetterV : Letter
{
    public LetterV( double x, double y, Brush color ) : base( x, y, color )
    {
    }
    //дублирование полукруга
    protected override void Build()
    {
        canvas.Children.Add( new Rectangle { Width = 20, Height = 100, Fill = color } );

        var topArc = new Path()
        {
            Stroke = color,
            StrokeThickness = 20,
            Fill = Brushes.Transparent,
            Data = new PathGeometry()
            {
                Figures = new PathFigureCollection()
                {
                    new PathFigure()
                    {
                        StartPoint = new Point(20, 10),
                        Segments = new PathSegmentCollection()
                        {
                            new ArcSegment()
                            {
                                Point = new Point(20, 50),
                                Size = new Size(10, 10),
                                SweepDirection = SweepDirection.Clockwise,
                                IsLargeArc = true,
                            }
                        }
                    }
                }
            }
        };
        canvas.Children.Add( topArc );

        var bottomArc = new Path()
        {
            Stroke = color,
            StrokeThickness = 20,
            Fill = Brushes.Transparent,
            Data = new PathGeometry()
            {
                Figures = new PathFigureCollection()
                {
                    new PathFigure()
                    {
                        StartPoint= new Point(20, 50),
                        Segments = new PathSegmentCollection()
                        {
                            new ArcSegment()
                            {
                                Point = new Point(20, 90),
                                Size = new Size(10, 10),
                                SweepDirection = SweepDirection.Clockwise,
                                IsLargeArc = true,
                            } 
                        }
                    }
                }
            }
        };
        canvas.Children.Add( bottomArc );
    }
}
