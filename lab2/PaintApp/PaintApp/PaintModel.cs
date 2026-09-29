using System.Drawing;
using System.Drawing.Imaging;


namespace PaintApp
{
    public class PaintModel
    {
        public Bitmap Drawing { get; set; }
        public Graphics Graphics { get; set; }
        public Pen Pen { get; set; }
        public Color CurrentColor { get; set; } = Color.Black;
        public float PenWidth { get; set; } = 3;

        public void DrawLine( Pen pen, Point lastPoint, Point Location )
        {
            Graphics.DrawLine( pen, lastPoint, Location );
        }

        public void SaveImage( string path, ImageFormat format )
        {
            Drawing.Save( path, format );
        }

        public void AddImage( string path )
        {
            Drawing = new Bitmap( path );
            Graphics = Graphics.FromImage( Drawing );
        }

        public void InitializeDrawing( int canvasWidth, int canvasHeight )
        {
            Drawing = new Bitmap( canvasWidth, canvasHeight );
            Graphics = Graphics.FromImage( Drawing );
            Graphics.Clear( Color.White );
        }
    }
}
