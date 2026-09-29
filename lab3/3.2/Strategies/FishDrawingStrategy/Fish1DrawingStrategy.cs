//using System.Numerics;
//using System.Reflection.PortableExecutable;
//using Silk.NET.OpenGL;

//namespace _3._2.Strategies.FishDrawingStrategy;

//public class Fish1DrawingStrategy : FishDrawingStrategy
//{

//    private uint _shaderProgram;

//    public Fish1DrawingStrategy( GL gL ) : base( gL )
//    {

//    }

//    public override void DrawBody( Matrix4x4 model, float rx, float ry )
//    {
//        var bodyModel = model * Matrix4x4.CreateScale( rx, ry, 1f );
//        int modelLoc = _gl.GetUniformLocation( _shaderProgram, "model" );

//        float[] bodyArray =
//        {
//            bodyModel.M11, bodyModel.M12, bodyModel.M13, bodyModel.M14,
//            bodyModel.M21, bodyModel.M22, bodyModel.M23, bodyModel.M24,
//            bodyModel.M31, bodyModel.M32, bodyModel.M33, bodyModel.M34,
//            bodyModel.M41, bodyModel.M42, bodyModel.M43, bodyModel.M44,
//        };

//        // Используем fixed для передачи указателя
//        unsafe
//        {
//            fixed ( float* ptr = bodyArray )
//            {
//                _gl.UniformMatrix4( modelLoc, 1, false, ptr );
//            }
//        }
//        //_gl.UniformMatrix4( modelLoc, 1, false, bodyModel );

//        _gl.DrawArrays( GLEnum.Triangles, 0, 6 );
//    }


//    public override void DrawEye( Matrix4x4 model )
//    {
//        throw new NotImplementedException();
//    }

//    public override void DrawTail( Matrix4x4 model, float rx )
//    {
//        var tailModel = model * Matrix4x4.CreateTranslation( -rx, 0f, 0f )
//            * Matrix4x4.CreateScale( rx / 2, rx / 2, 1f );


//    }
//}
