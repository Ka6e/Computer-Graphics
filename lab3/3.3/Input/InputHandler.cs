using Silk.NET.Input;

namespace _3._3.Input;

public class InputHandler
{
    private IKeyboard _keyboard;

    public bool IsLeftPressed { get; private set; }
    public bool IsRightPressed { get; private set; }
    public bool IsUpPressed { get; private set; }
    public bool IsDownPressed { get; private set; }

    public bool IsPausePressed { get; private set; }

    public InputHandler( IInputContext inputContext )
    {
        _keyboard = inputContext.Keyboards[ 0 ];

        _keyboard.KeyDown += OnKeyDown;
        _keyboard.KeyUp += OnKeyUp;
    }


    private void OnKeyDown( IKeyboard keyboard, Key key, int keyCode )
    {
        switch ( key )
        {
            case Key.Left:
                IsLeftPressed = true;
                break;
            case Key.Right:
                IsRightPressed = true;
                break;
            case Key.Up:
                IsUpPressed = true;
                break;
            case Key.Down:
                IsDownPressed = true;
                break;
            case Key.P:
                IsPausePressed = true;
                break;
        }
    }

    private void OnKeyUp( IKeyboard keyboard, Key key, int keyCode )
    {
        switch ( key )
        {
            case Key.Left:
                IsLeftPressed = false;
                break;
            case Key.Right:
                IsRightPressed = false;
                break;
            case Key.Up:
                IsUpPressed = false;
                break;
            case Key.Down:
                IsDownPressed = false;
                break;
            case Key.P:
                IsPausePressed = false;
                break;
        }
    }
}
