using System.Windows.Forms;
using System;
using System.Drawing;


namespace PaintApp
{
    public interface IPaintView
    {
        event MouseEventHandler StartDrawing;
        event MouseEventHandler Draw;
        event MouseEventHandler StopDrawing;
        event EventHandler NewFile;
        event EventHandler OpenFile;
        event EventHandler SaveFile;
        event EventHandler OpenColorDialog;
        Pen Pen { get; set; }
        Bitmap Canvas { get; set; }
        int CanvasWidth { get; }
        int CanvasHeight { get; }
    }
}
