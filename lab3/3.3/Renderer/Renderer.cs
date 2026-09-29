using System.Numerics;
using _3._3.Enum;
using Silk.NET.OpenGL;

namespace _3._3.Renderer;

public class Renderer
{
    private GL _gl;

    public Renderer( GL gl )
    {
        _gl = gl;
    }


    public void DrawBlock()

    public void Clear()
    {
        _gl.Clear( ClearBufferMask.ColorBufferBit );
    }

    private Vector3 GetColor( ColorType color )
    {
        return color switch
        {
            ColorType.Red => new Vector3( 1f, 0f, 0f ),
            ColorType.Green => new Vector3( 0f, 1f, 0f ),
            ColorType.Yellow => new Vector3( 1f, 1f, 0f ),
            ColorType.Blue => new Vector3( 0f, 0f, 1f ),
            ColorType.Orange => new Vector3( 1f, 0.5f, 0f ),
            ColorType.Purple => new Vector3( 1f, 0f, 1f ),
            _ => new Vector3( 0f, 0f, 0f ),
        };
    }

}
