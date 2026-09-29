using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _1._3;
public class CircleDrawer
{
    private Canvas _canvas;

    public CircleDrawer( Canvas canvas )
    {
        _canvas = canvas;
    }

    public void DrawCircle( int xc, int yc, int r, Color lineColor, int thickness = 1, Color? fillColor = null )
    {
        int x0 = xc - r - 1;
        int x1 = xc + r + 1;
        int y0 = yc - r - 1;
        int y1 = yc + r + 1;

        for ( int y = y0; y <= y1; y++ )
        {
            for ( int x = x0; x <= x1; x++ )
            {
                double dx = x - xc;
                double dy = y - yc;
                double distance = Math.Sqrt( dx * dx + dy * dy );

                if ( fillColor.HasValue && distance <= r )
                {
                    DrawPixel( x, y, fillColor.Value, 1.0 );
                }

                if ( distance >= r - thickness / 2.0 && distance <= r + thickness / 2.0 )
                {
                    double diff = Math.Abs( distance - r );
                    double alpha = 1.0 - diff / ( thickness / 2.0 );
                    alpha = Math.Max( 0, Math.Min( 1, alpha ) );
                    DrawPixel( x, y, lineColor, alpha );
                }
            }
        }
    }

    private void DrawPixel( int x, int y, Color color, double alpha )
    {
        if ( x < 0 || y < 0 || x >= _canvas.Width || y >= _canvas.Height )
        {
            return;
        }
        // дороого
        var brush = new SolidColorBrush( Color.FromArgb( ( byte )( alpha * 255 ), color.R, color.G, color.B ) );

        var rect = new Rectangle()
        {
            Width = 1,
            Height = 1,
            Fill = brush,
        };

        Canvas.SetLeft( rect, x );
        Canvas.SetTop( rect, y );
        _canvas.Children.Add( rect );
    }
}
