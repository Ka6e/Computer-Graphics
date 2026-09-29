using System.Windows;
using System.Windows.Media;
using _1._2.Models;

namespace _1._2;
public class HouseView : FrameworkElement
{
    public Shape Shape { get; set; }

    protected override void OnRender( DrawingContext dc )
    {
        base.OnRender( dc );
        Shape?.Draw( dc );
    }
}
