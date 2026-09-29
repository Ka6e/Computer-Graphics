namespace _3._3.Models;

public static class ShapeFactory
{
    private static readonly Random rand = new Random();

    public static readonly List<int[,]> Shapes = new()
    {
        //|
        new int[,]
        {
            {0,0,0,0},
            {1,1,1,1},
            {0,0,0,0},
            {0,0,0,0}
        },

        //Z
        new int [,]
        {
            {1,1,0,0},
            {0,1,1,0},
            {0,0,0,0},
            {0,0,0,0}
        },
        //T
        new int [,]
        {
            {0,1,0,0},
            {1,1,1,0},
            {0,0,0,0},
            {0,0,0,0}
        },

        //O
        new int [,]
        {
            {0,0,0,0},
            {0,1,1,0},
            {0,1,1,0},
            {0,0,0,0}
        },

        //L
        new int[,]
        {
           {0,1,0,0},
           {0,1,0,0},
           {0,1,1,0},
           {0,0,0,0}
        }
    };

    public static int[,] GetRandomShape()
    {
        int index = rand.Next( Shapes.Count );
        return Shapes[ index ];
    }
}
