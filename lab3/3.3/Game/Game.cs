using _3._3.Input;
using Silk.NET.Maths;
using Silk.NET.Windowing;

namespace _3._3.Game;

public class Game
{
    private IWindow _window;
    private InputHandler _inputHandler;

    public Game()
    {
        WindowOptions options = WindowOptions.Default with
        {
            Size = new Vector2D<int>( 1280, 720 ),
            Title = "Tetris",
        };

        _window = Window.Create( options );

        _window.Load += OnLoad;
        _window.Update += OnUpdate;
        _window.Render += OnRender;
    }

    private void OnLoad()
    {

    }

    private void OnUpdate( double deltaTime )
    {

    }

    private void OnRender( double deltaTime )
    {

    }
}
