using Silk.NET.OpenGL;

namespace _3._2.Rendering;

public class Renderer
{
    private GL _gL;
    private Shader _shader;

    public Renderer( GL gl, string vertexShaderPath, string fragmentShaderPath )
    {
        _gL = gl;
        _shader = new Shader( _gL, vertexShaderPath, fragmentShaderPath );
    }

    public (uint, uint) CreateVaoVbo( float[] vertices )
    {
        uint vao = _gL.GenVertexArray();
        uint vbo = _gL.GenBuffer();

        _gL.BindVertexArray( vao );
        _gL.BindBuffer( BufferTargetARB.ArrayBuffer, vbo );

        unsafe
        {
            fixed ( float* ptr = vertices )
            {
                _gL.BufferData( BufferTargetARB.ArrayBuffer,
                               ( nuint )( vertices.Length * sizeof( float ) ),
                               ptr,
                               BufferUsageARB.StaticDraw );
            }
        }

        _gL.VertexAttribPointer( 0, 2, VertexAttribPointerType.Float, false, 2 * sizeof( float ), 0 );
        _gL.EnableVertexAttribArray( 0 );

        return (vao, vbo);
    }

    public void Draw( uint vao, int vertexCount, float offsetX, float offsetY, float r, float g, float b)
    {
        //uint vao = _gL.GenVertexArray();
        //uint vbo = _gL.GenBuffer();

        //_gL.BindVertexArray( vao );
        //_gL.BindBuffer( BufferTargetARB.ArrayBuffer, vbo );

        //unsafe
        //{
        //    fixed ( float* ptr = vertices )
        //    {
        //        _gL.BufferData( BufferTargetARB.ArrayBuffer,
        //                       ( nuint )( vertices.Length * sizeof( float ) ),
        //                       ptr,
        //                       BufferUsageARB.StaticDraw );
        //    }
        //}

        //_gL.VertexAttribPointer( 0, 2, VertexAttribPointerType.Float, false, 2 * sizeof( float ), 0 );
        //_gL.EnableVertexAttribArray( 0 );

        _shader.Use();

        int offsetLoc = _gL.GetUniformLocation( _shader.Handle, "offset" );
        _gL.Uniform2( offsetLoc, offsetX, offsetY );

        int colorLoc = _gL.GetUniformLocation( _shader.Handle, "color" );
        _gL.Uniform3( colorLoc, r, g, b );

        //int rotationLoc = _gL.GetUniformLocation( _shader.Handle, "rotation" );
        //_gL.Uniform1( rotationLoc, rotation );

        _gL.BindVertexArray( vao );
        _gL.DrawArrays( ( GLEnum )PrimitiveType.Triangles, 0, ( uint )vertexCount);

        //_gL.DeleteBuffer( vbo );
        //_gL.DeleteVertexArray( vao );
    }
}
