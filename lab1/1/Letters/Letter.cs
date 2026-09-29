using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace _1.Letters;
public abstract class Letter
{
    protected Canvas canvas;
    protected Brush color;

    public UIElement Visual => canvas;

    protected Letter( double x, double y, Brush color )
    {
        this.color = color;

        canvas = new Canvas();
        Canvas.SetLeft( canvas, x );
        Canvas.SetTop( canvas, y );

        Build();
    }

    protected abstract void Build();

    public async void Jump( double delay )
    {
        await Task.Delay( TimeSpan.FromSeconds( delay ) );

        var transform = new TranslateTransform();
        Visual.RenderTransform = transform;

        double v0 = 350;
        double g = 980;
        double duration = 2 * v0 / g;

        double t = 0;
        DateTime lastTime = DateTime.Now;

        EventHandler handler;

        handler = ( s, e ) =>
        {
            var now = DateTime.Now;
            double deltaTime = ( now - lastTime ).TotalSeconds;
            lastTime = now;

            t += deltaTime;

            if ( t > duration )
            {
                t = 0;
            }

            double y = -v0 * t + ( g * t * t ) / 2;

            transform.Y = y;
        };

        CompositionTarget.Rendering += handler;
    }
}

