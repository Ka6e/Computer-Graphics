using _3._2.Models;
using _3._2.Rendering;

namespace _3._2.Strategies.FishDrawingStrategy;

public abstract class FishDrawingStrategy
{
    protected Fish Fish = null!;

    public void Draw( Fish fish )
    {
        Fish = fish;
    }

    public abstract void DrawBody( Fish fish, Renderer renderer );
    public abstract void DrawTail( Fish fish, Renderer renderer );
    public abstract void DrawEye( Fish fish, Renderer renderer );

    //public void Draw( Fish fish, Renderer renderer )
    //{
    //    DrawBody( fish, renderer );
    //    DrawTail( fish, renderer );
    //    DrawEye( fish, renderer );
    //}
}
