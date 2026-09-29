using _3._2.Game;
using Silk.NET.Maths;

namespace _3._2.Models;

public static class FishBehaviour
{
    private static Random _rnd = new Random();
    public static void Swim( Fish fish, float deltaTime )
    {
        fish.DirectionTimer -= deltaTime;

        if ( fish.DirectionTimer <= 0 )
        {
            fish.DirectionTimer = 1f + ( float )_rnd.NextDouble() * 2f; // 1–3 секунды до следующей смены направления

            float angle = ( float )_rnd.NextDouble() * MathF.PI * 2f; // случайный угол
            float speed = 0.1f; // скорость рыбы
            fish.Velocity = new Vector2D<float>(
                MathF.Cos( angle ) * speed,
                MathF.Sin( angle ) * speed
            );
        }

        fish.Position += fish.Velocity * deltaTime;

        fish.BubbleTimer += deltaTime;
        if ( fish.BubbleTimer >= 1.5f )
        {
            BubbleManager.SpawnBubble( fish.Position.X, fish.Position.Y );
            fish.BubbleTimer = 0f;
        }
        //float speed = 0.05f;
        //float dx = ( ( float )_rnd.NextDouble() - 0.5f ) * speed;
        //float dy = ( ( float )_rnd.NextDouble() - 0.5f ) * speed;

        //var pos = fish.Position;
        //pos.X += dx * deltaTime;
        //pos.Y += dy * deltaTime;
        //fish.Position = pos;

        //fish.BubbleTimer += deltaTime;
        //if ( fish.BubbleTimer >= 1.5f )
        //{
        //    BubbleManager.SpawnBubble( fish.Position.X, fish.Position.Y );
        //    fish.BubbleTimer = 0;
        //}
        //fish.OscillationPhase += deltaTime;
        //var pos = fish.Position;
        //pos.X += ( float )Math.Sin( DateTime.Now.Ticks * 1e-7 ) * 0.001f;
        //fish.Position = pos;

        //fish.BubbleTimer += deltaTime;
        //if ( fish.BubbleTimer >= 1.5f ) 
        //{
        //    BubbleManager.SpawnBubble( fish.Position.X, fish.Position.Y );
        //    fish.BubbleTimer = 0f;
        //}
    }
}
