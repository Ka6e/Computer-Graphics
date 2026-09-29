using Silk.NET.Maths;

namespace _3._2.Models;

public class Stone
{
    public Vector2D<float> Position { get; set; }
    public Vector2D<float> Size { get; set; }
    public Vector3D<float> Color { get; set; }

    public Stone( Vector2D<float> position, Vector3D<float> color )
    {
        Position = position;
        Color = color;
    }
}