using _3._2.Models;

namespace _3._2.Game;

public static class BubbleManager
{
    public static List<Bubble> Bubbles = new List<Bubble>();

    public static void SpawnBubble( float x, float y )
    {
        Bubbles.Add( new Bubble( x, y ) );
    }

    public static void UpdateAll( float deltaTime )
    {
        for ( int i = Bubbles.Count - 1; i >= 0; i-- )
        {
            Bubbles[ i ].Update( deltaTime );
            if ( Bubbles[ i ].Position.Y > 1.0f )
            {
                Bubbles.RemoveAt( i );
            }
        }
    }
}
