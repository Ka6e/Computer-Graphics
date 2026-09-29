using System.Windows;
using System.Windows.Input;
using _1._2.Models;

namespace _1._2;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private bool _isDragging = false;
    private CompositeShape _house;
    private Point _position;
    private HouseView _view;

    public MainWindow()
    {
        InitializeComponent();

        _house = HouseFactory.CreateHouse();

        _view = new HouseView()
        {
            Shape = _house,
        };

        Content = _view;

        _view.MouseDown += OnMouseDown;
        _view.MouseUp += OnMouseUp;
        _view.MouseMove += OnMouseMove;
    }

    private void OnMouseDown( object sender, MouseEventArgs e )
    {
        if ( _house.GetBounds().Contains( e.GetPosition( _view ) ) )
        {
            _isDragging = true;
            _position = e.GetPosition( _view );
            _view.CaptureMouse();
        }
    }

    private void OnMouseMove( object sender, MouseEventArgs e )
    {
        if ( !_isDragging )
        {
            return;
        }

        Point current = e.GetPosition( _view );
        double dx = current.X - _position.X;
        double dy = current.Y - _position.Y;

        Rect bounds = _house.GetBounds();

        if ( bounds.Left + dx < 0 || bounds.Right + dx > _view.ActualWidth )
        {
            dx = 0;
        }
        if ( bounds.Top + dy < 0 || bounds.Bottom + dy > _view.ActualHeight )
        {
            dy = 0;
        }

        _house.Move( dx, dy );
        _position = current;
        _view.InvalidateVisual();
    }

    private void OnMouseUp( object sender, MouseEventArgs e )
    {
        _isDragging = false;
        _view.ReleaseMouseCapture();
    }
}