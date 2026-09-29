using System.Numerics;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

using InputMouseButton = Silk.NET.Input.MouseButton;

namespace _3._1;

public class Program
{
    private static IWindow _window;
    private static IInputContext _input;
    private static GL _gL;

    private static Vector2 p0 = new Vector2( -0.9f, -0.5f );
    private static Vector2 p1 = new Vector2( -0.4f, 0.8f );
    private static Vector2 p2 = new Vector2( 1.0f, 0.8f ); 
    private static Vector2 p3 = new Vector2( 0.8f, -0.5f );

    private static int? draggedPointIndex = null;
    private static readonly float pointClickRadius = 0.07f;
    private static Vector2 lastMousePos;

    static List<Vector2> curvePoints = new();

    private static uint curveVAO;
    private static uint curveVBO;

    private static uint pointsVAO;
    private static uint pointsVBO;

    static void Main( string[] args )
    {
        WindowOptions options = WindowOptions.Default with
        {
            Size = new Vector2D<int>( 1280, 720 ),
            Title = "Bezier curve"
        };

        _window = Window.Create( options );

        _window.Load += OnLoad;
        _window.Render += OnRender;

        _window.Run();
    }

    private static void OnLoad()
    {
        _gL = _window.CreateOpenGL();

        _input = _window.CreateInput();

        var mouse = _input.Mice[ 0 ];

        mouse.MouseDown += OnMouseDown;
        mouse.MouseUp += OnMouseUp;
        mouse.MouseMove += OnMouseMove;

        curveVAO = _gL.GenVertexArray();
        curveVBO = _gL.GenBuffer();

        _gL.BindVertexArray( curveVAO );
        _gL.BindBuffer( BufferTargetARB.ArrayBuffer, curveVBO );

        GenerateCurve();
        UpdateCurveVBO();

        _gL.VertexAttribPointer( 0, 2, VertexAttribPointerType.Float, false, 2 * sizeof( float ), 0 );
        _gL.EnableVertexAttribArray( 0 );

        _gL.BindVertexArray( 0 );

        pointsVAO = _gL.GenVertexArray();
        pointsVBO = _gL.GenBuffer();

        _gL.BindVertexArray( pointsVAO );
        _gL.BindBuffer( BufferTargetARB.ArrayBuffer, pointsVBO );
        UpdatePointsVBO();

        _gL.VertexAttribPointer( 0, 2, VertexAttribPointerType.Float, false, 2 * sizeof( float ), 0 );
        _gL.EnableVertexAttribArray( 0 );

        _gL.BindVertexArray( 0 );

        _gL.ClearColor( 0f, 0f, 0f, 1f );
    }

    private static void OnMouseDown( IMouse mouse, InputMouseButton button )
    {
        if ( button != InputMouseButton.Left )
            return;

        Vector2 mousePosNDC = MouseToNDC( mouse.Position.X, mouse.Position.Y );

        Vector2[] points = { p0, p1, p2, p3 };

        for ( int i = 0; i < points.Length; i++ )
        {
            float dist = Vector2.Distance( points[ i ], mousePosNDC );
            if ( dist < pointClickRadius )
            {
                draggedPointIndex = i;
                return;
            }
        }
    }

    private static void OnMouseMove( IMouse mouse, Vector2 position )
    {
        if ( draggedPointIndex == null )
            return;

        Vector2 current = MouseToNDC( position.X, position.Y );

        switch ( draggedPointIndex.Value )
        {
            case 0:
                p0 = current;
                break;
            case 1:
                p1 = current;
                break;
            case 2:
                p2 = current;
                break;
            case 3:
                p3 = current;
                break;
        }

        GenerateCurve();
        UpdateCurveVBO();
        UpdatePointsVBO();
    }

    private static void OnMouseUp( IMouse mouse, InputMouseButton button )
    {
        if ( button == InputMouseButton.Left )
        {
            draggedPointIndex = null;
        }
    }

    private static Vector2 MouseToNDC( float mouseX, float mouseY )
    {
        var size = _window.Size;

        float ndcX = ( mouseX / size.X ) * 2f - 1f;
        float ndcY = 1f - ( mouseY / size.Y ) * 2f;

        return new Vector2( ndcX, ndcY );
    }

    private static void OnRender( double deltaTime )
    {
        _gL.Clear( ClearBufferMask.ColorBufferBit );

        DrawAxes();

        _gL.BindVertexArray( curveVAO );
        _gL.LineWidth( 2f );
        _gL.DrawArrays( PrimitiveType.LineStrip, 0, ( uint )curvePoints.Count );
        _gL.BindVertexArray( 0 );

        DrawDashedLine( p0, p1 );
        DrawDashedLine( p1, p2 );
        DrawDashedLine( p2, p3 );

        _gL.BindVertexArray( pointsVAO );
        _gL.PointSize( 10f );
        _gL.DrawArrays( PrimitiveType.Points, 0, 4 );
        _gL.BindVertexArray( 0 );
    }

    private static void DrawAxes()
    {
        DrawLine( new Vector2( -1f, 0f ), new Vector2( 1f, 0f ) );
        DrawLine( new Vector2( 0f, -1f ), new Vector2( 0f, 1f ) );

        float arrowSize = 0.03f;

        DrawLine( new Vector2( 1f, 0f ), new Vector2( 1f - arrowSize, arrowSize ) );
        DrawLine( new Vector2( 1f, 0f ), new Vector2( 1f - arrowSize, -arrowSize ) );

        DrawLine( new Vector2( 0f, 1f ), new Vector2( arrowSize, 1f - arrowSize ) );
        DrawLine( new Vector2( 0f, 1f ), new Vector2( -arrowSize, 1f - arrowSize ) );

        DrawTicks();
    }

    private static void DrawTicks()
    {
        int tickCount = 10;
        float step = 2f / tickCount;
        float tickSize = 0.02f;
 
        for ( int i = -tickCount / 2; i <= tickCount / 2; i++ )
        {
            float x = i * step;
            float y = i * step;

            DrawLine(
                new Vector2( x, -tickSize ),
                new Vector2( x, tickSize )
            );

            DrawLine(
                new Vector2( -tickSize, y ),
                new Vector2( tickSize, y )
            );
        }
    }
    private static Vector2 Bezier( Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t )
    {
        float u = 1 - t;
        float t2 = t * t;
        float u2 = u * u;
        float u3 = u2 * u;
        float t3 = t2 * t;

        Vector2 p = u3 * p0;
        p += 3 * u2 * t * p1;
        p += 3 * u * t2 * p2;
        p += t3 * p3;

        return p;
    }

    private static void GenerateCurve()
    {
        curvePoints.Clear();
        int segments = 100;
        for ( int i = 0; i <= segments; i++ )
        {
            float t = i / ( float )segments;
            curvePoints.Add( Bezier( p0, p1, p2, p3, t ) );
        }
    }

    private static unsafe void UpdateCurveVBO()
    {
        float[] vertices = new float[ curvePoints.Count * 2 ];
        for ( int i = 0; i < curvePoints.Count; i++ )
        {
            vertices[ i * 2 ] = curvePoints[ i ].X;
            vertices[ i * 2 + 1 ] = curvePoints[ i ].Y;
        }

        _gL.BindBuffer( BufferTargetARB.ArrayBuffer, curveVBO );
        fixed ( float* v = vertices )
        {
            _gL.BufferData( BufferTargetARB.ArrayBuffer, ( nuint )( vertices.Length * sizeof( float ) ), v, BufferUsageARB.StaticDraw );
        }
    }

    private static unsafe void UpdatePointsVBO()
    {
        Vector2[] points = { p0, p1, p2, p3 };
        float[] vertices = new float[ points.Length * 2 ];
        for ( int i = 0; i < points.Length; i++ )
        {
            vertices[ i * 2 ] = points[ i ].X;
            vertices[ i * 2 + 1 ] = points[ i ].Y;
        }

        _gL.BindBuffer( BufferTargetARB.ArrayBuffer, pointsVBO );
        fixed ( float* v = vertices )
        {
            _gL.BufferData( BufferTargetARB.ArrayBuffer, ( nuint )( vertices.Length * sizeof( float ) ), v, BufferUsageARB.StaticDraw );
        }
    }

    private static void DrawDashedLine( Vector2 a, Vector2 b, int segments = 10 )
    {
        for ( int i = 0; i < segments; i++ )
        {
            if ( i % 2 == 0 )
            {
                float t1 = i / ( float )segments;
                float t2 = ( i + 1 ) / ( float )segments;

                Vector2 p1 = Vector2.Lerp( a, b, t1 );
                Vector2 p2 = Vector2.Lerp( a, b, t2 );

                DrawLine( p1, p2 );
            }
        }
    }

    private static void DrawLine( Vector2 a, Vector2 b )
    {
        float[] vertices = { a.X, a.Y, b.X, b.Y };

        uint vao = _gL.GenVertexArray();
        uint vbo = _gL.GenBuffer();

        _gL.BindVertexArray( vao );
        _gL.BindBuffer( BufferTargetARB.ArrayBuffer, vbo );

        unsafe
        {
            fixed ( float* ptr = vertices )
            {
                _gL.BufferData( BufferTargetARB.ArrayBuffer, ( nuint )( vertices.Length * sizeof( float ) ), ptr, BufferUsageARB.DynamicDraw );
            }
        }

        _gL.VertexAttribPointer( 0, 2, VertexAttribPointerType.Float, false, 2 * sizeof( float ), 0 );
        _gL.EnableVertexAttribArray( 0 );

        _gL.DrawArrays( PrimitiveType.Lines, 0, 2 );

        _gL.DeleteBuffer( vbo );
        _gL.DeleteVertexArray( vao );
    }
}