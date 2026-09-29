using Silk.NET.Maths;

namespace _3._2.Models;

public class Plant
{
    public Vector2D<float> Position { get; set; }
    public float Height { get; set; }
    public Vector3D<float> Color { get; set; }

    public uint Vao { get; set; }
    public int VertexCount { get; set; }

    public Plant( Vector2D<float> position, float height, Vector3D<float> color )
    {
        Position = position;
        Height = height;
        Color = color;
    }
}
