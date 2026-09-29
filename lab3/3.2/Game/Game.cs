using System.Drawing;
using _3._2.Models;
using _3._2.Rendering;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace _3._2.Game;

public class Game
{
    private IWindow _window;
    private GL _gL;
    private Renderer _renderer;

    private List<Fish> _fishes = new();
    private List<Stone> _stones = new();
    private List<Plant> _plants = new();

    private uint _fishVao, _fishVbo;
    private uint _stoneVao, _stoneVbo;
    private uint _plantVao, _plantVbo;
    private uint _bubbleVao, _bubbleVbo;


    private float _time = 0f;

    private float[] _fishVertices = { -0.05f, -0.03f, 0.05f, 0f, -0.05f, 0.03f };

    private float[] _stoneVertices = {
            -0.05f, -0.05f, 0.05f, -0.05f, 0.05f, 0.05f,
            -0.05f, -0.05f, 0.05f, 0.05f, -0.05f, 0.05f
        };

    private float[] _plantVertices = {
            0f, 0f, 0.02f, 0f, 0.02f, 1f,
            0f, 0f, 0.02f, 1f, 0f, 1f
        };
    private float[] _unitCircle;


    public Game()
    {
        WindowOptions options = WindowOptions.Default with
        {
            Size = new Vector2D<int>( 1280, 960 ),
            Title = "Aquarium",
        };

        _window = Window.Create( options );

        _window.Load += OnLoad;
        _window.Update += OnUpdate;
        _window.Render += OnRender;
        _window.Resize += ( size ) =>
        {
            _gL.Viewport( 0, 0, ( uint )size.X, ( uint )size.Y );
        };
    }
    public void Run() => _window.Run();

    private void OnLoad()
    {
        _gL = GL.GetApi( _window );
        _gL.ClearColor( Color.CornflowerBlue );
        _renderer = new Renderer( _gL, "Rendering/vertex.glsl", "Rendering/fragment.glsl" );

        _fishes.Add( new Fish( new Vector2D<float>( -0.5f, 0f ), new Vector3D<float>( 1f, 0.5f, 0.2f ) ) );
        _fishes.Add( new Fish( new Vector2D<float>( 0.5f, -0.3f ), new Vector3D<float>( 1f, 0.7f, 0.2f ) ) );

        _stones.Add( new Stone( new Vector2D<float>( -0.6f, -0.8f ), new Vector3D<float>( 0.5f, 0.5f, 0.5f ) ) );
        _stones.Add( new Stone( new Vector2D<float>( 0.3f, -0.85f ), new Vector3D<float>( 0.6f, 0.4f, 0.2f ) ) );

        _plants.Add( new Plant( new Vector2D<float>( -0.8f, -0.7f ), 0.4f, new Vector3D<float>( 0f, 1f, 0f ) ) );
        _plants.Add( new Plant( new Vector2D<float>( 0.7f, -0.75f ), 0.5f, new Vector3D<float>( 0f, 0.8f, 0f ) ) );

        foreach ( var fish in _fishes )
        {
            var vertices = GenerateFishVertices(
                width: 0.05f,
                height: 0.03f,
                tailLength: 0.02f,
                eyeRadius: 0.005f,
                eyeOffsetX: -0.02f,
                eyeOffsetY: 0.01f
            );
            (fish.Vao, fish.Vbo) = _renderer.CreateVaoVbo( vertices );
            fish.VertexCount = vertices.Length / 2;
        }

        //(_fishVao, _fishVbo) = _renderer.CreateVaoVbo( _fishVertices );
        (_stoneVao, _stoneVbo) = _renderer.CreateVaoVbo( _stoneVertices );

        foreach ( var plant in _plants )
        {
            var vertices = GeneratePlant( plant.Height, 0f );
            var (vao, vbo) = _renderer.CreateVaoVbo( vertices );

            plant.Vao = vao;
            plant.VertexCount = vertices.Length / 2;
        }

        _unitCircle = GenerateCircle( 0.01f, 20 );
        (_bubbleVao, _bubbleVbo) = _renderer.CreateVaoVbo( _unitCircle );
    }

    private void OnUpdate( double deltaTime )
    {
        float delta = ( float )deltaTime;

        foreach ( var fish in _fishes )
        {
            FishBehaviour.Swim( fish, delta );
        }

        BubbleManager.UpdateAll( delta );
    }

    private void OnRender( double deltaTime )
    {
        _gL.Clear( ClearBufferMask.ColorBufferBit );

        foreach ( var fish in _fishes )
        {
            //    _renderer.Draw( fish.Vao,
            //                   fish.VertexCount,
            //                   fish.Position.X,
            //                   fish.Position.Y,
            //                   fish.Color.X,
            //                   fish.Color.Y,
            //                   fish.Color.Z);
            //_renderer.Draw( _fishVao, _fishVertices.Length / 2, fish.Position.X, fish.Position.Y, fish.Color.X, fish.Color.Y, fish.Color.Z );
        }

        foreach ( var stone in _stones )
        {
            _renderer.Draw( _stoneVao, _stoneVertices.Length / 2, stone.Position.X, stone.Position.Y, stone.Color.X, stone.Color.Y, stone.Color.Z );
        }

        foreach ( var plant in _plants )
        {
            _renderer.Draw(
                    plant.Vao,
                    plant.VertexCount,
                    plant.Position.X,
                    plant.Position.Y,
                    plant.Color.X,
                    plant.Color.Y,
                    plant.Color.Z
                );
            //_renderer.Draw( _plantVao, _plantVertices.Length / 2, plant.Position.X, plant.Position.Y, plant.Color.X, plant.Color.Y, plant.Color.Z );
        }

        foreach ( var bubble in BubbleManager.Bubbles )
        {
            _renderer.Draw( _bubbleVao, _unitCircle.Length / 2, bubble.Position.X, bubble.Position.Y, 0.8f, 0.9f, 1f );
        }
    }

    private float[] GenerateFishVertices( float width, float height, float tailLength, float eyeRadius, float eyeOffsetX, float eyeOffsetY )
    {
        int bodySegments = 20;
        int eyeSegments = 12;

        float[] vertices = new float[ bodySegments * 6 + 6 + eyeSegments * 6 ];
        int index = 0;

        for ( int i = 0; i < bodySegments; i++ )
        {
            float theta1 = 2 * MathF.PI * i / bodySegments;
            float theta2 = 2 * MathF.PI * ( i + 1 ) / bodySegments;

            vertices[ index++ ] = 0f;
            vertices[ index++ ] = 0f;

            vertices[ index++ ] = width * MathF.Cos( theta1 );
            vertices[ index++ ] = height * MathF.Sin( theta1 );

            vertices[ index++ ] = width * MathF.Cos( theta2 );
            vertices[ index++ ] = height * MathF.Sin( theta2 );
        }

        vertices[ index++ ] = width;
        vertices[ index++ ] = 0f;

        vertices[ index++ ] = width + tailLength;
        vertices[ index++ ] = height / 2;

        vertices[ index++ ] = width + tailLength;
        vertices[ index++ ] = -height / 2;

        for ( int i = 0; i < eyeSegments; i++ )
        {
            float theta1 = 2 * MathF.PI * i / eyeSegments;
            float theta2 = 2 * MathF.PI * ( i + 1 ) / eyeSegments;

            vertices[ index++ ] = eyeOffsetX;
            vertices[ index++ ] = eyeOffsetY;

            vertices[ index++ ] = eyeOffsetX + eyeRadius * MathF.Cos( theta1 );
            vertices[ index++ ] = eyeOffsetY + eyeRadius * MathF.Sin( theta1 );

            vertices[ index++ ] = eyeOffsetX + eyeRadius * MathF.Cos( theta2 );
            vertices[ index++ ] = eyeOffsetY + eyeRadius * MathF.Sin( theta2 );
        }

        return vertices;
    }

    private float[] GenerateCircle( float radius, int segments = 20 )
    {
        float[] vertices = new float[ segments * 6 ];
        for ( int i = 0; i < segments; i++ )
        {
            float theta1 = 2 * MathF.PI * i / segments;
            float theta2 = 2 * MathF.PI * ( i + 1 ) / segments;

            vertices[ i * 6 + 0 ] = 0f;
            vertices[ i * 6 + 1 ] = 0f;
            vertices[ i * 6 + 2 ] = radius * MathF.Cos( theta1 );
            vertices[ i * 6 + 3 ] = radius * MathF.Sin( theta1 );
            vertices[ i * 6 + 4 ] = radius * MathF.Cos( theta2 );
            vertices[ i * 6 + 5 ] = radius * MathF.Sin( theta2 );
        }
        return vertices;
    }

    private float[] GeneratePlant( float height, float time, int segments = 20, float amplitude = 0.02f )
    {
        float[] vertices = new float[ segments * 12 ];
        for ( int i = 0; i < segments; i++ )
        {
            float y1 = height * i / segments;
            float y2 = height * ( i + 1 ) / segments;

            float x1 = MathF.Sin( y1 * 8f + time ) * amplitude;
            float x2 = MathF.Sin( y2 * 8f + time ) * amplitude;

            float width = 0.02f;

            int index = i * 12;

            vertices[ index + 0 ] = x1;
            vertices[ index + 1 ] = y1;

            vertices[ index + 2 ] = x1 + width;
            vertices[ index + 3 ] = y1;

            vertices[ index + 4 ] = x2 + width;
            vertices[ index + 5 ] = y2;

            vertices[ index + 6 ] = x1;
            vertices[ index + 7 ] = y1;

            vertices[ index + 8 ] = x2 + width;
            vertices[ index + 9 ] = y2;

            vertices[ index + 10 ] = x2;
            vertices[ index + 11 ] = y2;
        }

        return vertices;
    }
}
