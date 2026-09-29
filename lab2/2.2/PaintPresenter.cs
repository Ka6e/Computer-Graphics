namespace _1._2;

public class PaintPresenter
{
    private PaintModel _model;
    private IPaintView _view;
    private Point _lastPoint;
    private bool _isDrawing = false;


    public PaintPresenter( IPaintView view, PaintModel model )
    {
        _view = view;
        _model = model;

        _view.OpenFile += OpenFile;
        _view.NewFile += NewFile;
        _view.SaveFile += SaveAsFile;
        _view.StartDraw += OnMouseDown;
        _view.Draw += OnMouseMove;
        _view.StopDraw += OnMouseUp;
    }

    private void OpenFile( object sender, EventArgs e )
    {
        OpenFileDialog openFileDialog = new OpenFileDialog()
        {
            Filter = "Image Files|*.png;*.bpm;*.jpg"
        };
        if ( openFileDialog.ShowDialog() == DialogResult.OK )
        {
            _model.AddImage( openFileDialog.FileName );
            _view.Canvas = _model.Bitmap;
        }
    }

    private void SaveAsFile( object sender, EventArgs e )
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog()
        {
            Filter = "PNG Image|*.png|Bitmap Image|*.bmp|JPEG Image|*.jpg"
        };
        if ( saveFileDialog.ShowDialog() == DialogResult.OK )
        {

        }
    }

    private void NewFile( object sender, EventArgs e )
    {
        _model.InitializeBitmap( _view.CanasWidth, _view.CanvasHeight );
        _view.Canvas = _model.Bitmap;
    }

    private void OnMouseDown( object sender, MouseEventArgs e )
    {
        if ( _model.Bitmap != null )
        {
            return;
        }
        if ( e.Button == MouseButtons.Left )
        {
            _isDrawing = true;
            _lastPoint = e.Location;
        }
    }

    private void OnMouseMove( object sender, MouseEventArgs e )
    {
        if ( _isDrawing )
        {
            _model.DrawLine( _lastPoint, e.Location );
            _lastPoint = e.Location;
            _view.Canvas = _model.Bitmap;
        }
    }

    private void OnMouseUp( object sender, MouseEventArgs e )
    {
        if ( e.Button == MouseButtons.Left )
        {
            _isDrawing = false;
        }
    }
}
