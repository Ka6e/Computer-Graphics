using Silk.NET.Maths;

namespace _3._2.Models;

public class Fish
{
    public Vector2D<float> Position { get; set; }
    public float Size { get; set; } = 1.0f;
    public Vector2D<float> Velocity { get; set; } = new Vector2D<float>( 0, 0 );
    public float DirectionTimer { get; set; } = 0;
    public Vector3D<float> Color { get; set; }
    public uint Vao { get; set; }
    public uint Vbo { get; set; }
    public int VertexCount { get; set; }
    public float OscillationPhase { get; set; } = 0f;
    public float BubbleTimer { get; set; } = 0f;

    public Fish( Vector2D<float> position, Vector3D<float> color )
    {
        Position = position;
        Color = color;
    }
}
