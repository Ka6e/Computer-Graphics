namespace _3._3.Models;

public class Board
{
    public const int Width = 10;
    public const int Height = 20;

    public int[,] Grid;

    public Board()
    {
        Grid = new int[Width, Height];
    }

    public bool IsInside(int x, int y)
    {
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }

    public void ClearLines()
    {
        
    }

    //public void Clear()
    //{
        
    //}

    //public bool IsGameOver()
    //{

    //}
}
