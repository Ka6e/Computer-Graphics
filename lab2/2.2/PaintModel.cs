using System.Drawing.Imaging;

namespace _1._2;

public class PaintModel
{
    public Bitmap Bitmap { get; set; }
    public Graphics Graphics { get; set; }
    public float PenWidth { get; set; } = 3f;
    public Pen Pen { get; set; }

    public PaintModel()
    {
        Pen = new Pen( Color.Black, PenWidth );
    }

    public void SaveImage( string path, ImageFormat imageFormat )
    {
        Bitmap.Save( path, imageFormat );
    }

    public void DrawLine( Point lastPoint, Point currentPoin )
    {
        Graphics.DrawLine( Pen, lastPoint, currentPoin );
    }

    public void AddImage( string path )
    {
        Bitmap = new Bitmap( path );
        Graphics = Graphics.FromImage( Bitmap );
    }

    public void InitializeBitmap( int widht, int height )
    {
        Bitmap = new Bitmap( widht, height );
        Graphics = Graphics.FromImage( Bitmap );
        Graphics.Clear( Color.White );
    }
}
