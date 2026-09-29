using _3._3.Enum;

namespace _3._3.Models;

public class Shape
{
    private const int Size = 4;

    public int[,] Matrix { get; private set; } = new int[ Size, Size ];
    public ShapeType _shapeType { get; private set; }
    public ColorType _color { get; private set; }

    public Shape()
    {
        Matrix = ShapeFactory.GetRandomShape();
        _color = GetRandomColor();
    }

    public void Rotate()
    {
        int[,] rotated = new int[ Size, Size ];
        for ( int y = 0; y < Size; y++ )
        {
            for ( int x = 0; x < Size; x++ )
            {
                rotated[ x, Size - 1 - y ] = Matrix[ y, x ];
            }
        }

        Matrix = rotated;
    }

    private ColorType GetRandomColor()
    {
        Random rand = new Random();
        return ( ColorType )rand.Next( 0, 7 );
    }
}
