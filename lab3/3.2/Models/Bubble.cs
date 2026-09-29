using Silk.NET.Maths;

namespace _3._2.Models;

public class Bubble
{
    public Vector2D<float> Position;
    public float Size;
    public float Speed;
    private readonly Random _random = new Random();

    public Bubble( float x, float y )
    {
        Position = new Vector2D<float>( x, y );
        Size = 0.01f + ( float )_random.NextDouble() * 0.01f;
        Speed = 0.001f + ( float )_random.NextDouble() * 0.002f;
    }

    public void Update( float deltaTime )
    {
        Position.Y += Speed * deltaTime * 60f;
    }
}
