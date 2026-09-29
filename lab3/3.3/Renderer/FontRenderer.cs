using Silk.NET.OpenGL;

namespace _3._3.Renderer;

public class FontRenderer
{
    private const int CharacterSize = 8;
    private const int CharGap = 2;
    private const int LineGap = 4;

    private readonly GL _gl;

    public FontRenderer(GL gl)
    {
        _gl = gl;
    }
}
