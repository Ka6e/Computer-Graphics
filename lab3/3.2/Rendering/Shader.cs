using System.Net.NetworkInformation;
using Silk.NET.OpenGL;

namespace _3._2.Rendering;

public class Shader
{
    private GL _gl;
    public uint Handle;

    public Shader( GL gL, string vertexPath, string fragmentPath )
    {
        _gl = gL;

        string vertexSourse = File.ReadAllText( vertexPath );
        string fragmentSourse = File.ReadAllText( fragmentPath );

        uint v = _gl.CreateShader( GLEnum.VertexShader );
        _gl.ShaderSource( v, vertexSourse );
        _gl.CompileShader( v );
        _gl.GetShader( v, ShaderParameterName.CompileStatus, out int vStatus );
        if ( vStatus != ( int )GLEnum.True )
        {
            throw new Exception( "Vertex shader failed to compile: " + _gl.GetShaderInfoLog( v ) );
        }


        uint f = _gl.CreateShader ( GLEnum.FragmentShader );
        _gl.ShaderSource ( f, fragmentSourse );
        _gl.CompileShader( f );
        _gl.GetShader( f, ShaderParameterName.CompileStatus, out int fStatus );
        if ( fStatus != ( int )GLEnum.True )
        {
            throw new Exception( "Fragment shader failed to compile: " + _gl.GetShaderInfoLog( f ) );
        }

        Handle = _gl.CreateProgram();
        _gl.AttachShader( Handle, v );
        _gl.AttachShader( Handle, f );
        _gl.LinkProgram( Handle );
        _gl.GetProgram( Handle, ProgramPropertyARB.LinkStatus, out int lStatus );
        if ( lStatus != ( int )GLEnum.True )
        {
            throw new Exception( "Program failed to link: " + _gl.GetProgramInfoLog( Handle ) );
        }

        _gl.DetachShader( Handle, v );
        _gl.DetachShader( Handle, f );
        _gl.DeleteShader( v );
        _gl.DeleteShader( f );
    }

    public void Use()
    {
        _gl.UseProgram( Handle );
    }
}
