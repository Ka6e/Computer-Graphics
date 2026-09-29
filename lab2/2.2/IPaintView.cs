namespace _1._2;

public interface IPaintView
{
    event MouseEventHandler StartDraw;
    event MouseEventHandler Draw;
    event MouseEventHandler StopDraw;
    event EventHandler NewFile;
    event EventHandler SaveFile;
    event EventHandler OpenFile;
    event EventHandler OpenColorDialog;

    Bitmap Canvas {  get; set; }
    
    int CanasWidth { get; }
    int CanvasHeight { get;}
}
