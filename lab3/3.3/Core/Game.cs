using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace _3._3.Core;

public class Game
{
    private IWindow _window;
    private GL _gL;


    public Game(IWindow window)
    {
        WindowOptions options = new WindowOptions() with
        {
            Title = "Tetris",
            Size = new Vector2D<int>( 1280, 720 ),
        };

        _window = Window.Create(options);

        _window.Load += OnLoad;
        _window.Update += OnUpdate;
        _window.Render += OnRender;

        _window.Run();
    }

    public void OnLoad()
    {

    }

    public void OnUpdate( double deltaTime )
    {

    }

    public void OnRender( double deltaTime )
    {

    }
}
