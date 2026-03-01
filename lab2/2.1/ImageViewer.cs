using System.Drawing.Imaging;

namespace _2._1;

public partial class ImageViewer : Form
{
    private Bitmap _checkerBoard;
    private Bitmap _cachedImage;
    private Image _image;
    private OpenFileDialog _openFileDialog;
    private int _imageWidth = 400;
    private int _imageHeight = 300;
    private Point _imageLocation;
    private bool _isDragging = false;
    private Point _startPoint;

    public ImageViewer()
    {
        InitializeComponent();
        DoubleBuffered = true;

        var menuStrip = new MenuStrip();
        var fileMenu = new ToolStripMenuItem( "File" );
        var openMenuItem = new ToolStripMenuItem( "Open", null, OnOpenClicked );
        fileMenu.DropDownItems.Add( openMenuItem );
        menuStrip.Items.Add( fileMenu );

        MainMenuStrip = menuStrip;
        Controls.Add( menuStrip );

        _openFileDialog = new OpenFileDialog();
        _openFileDialog.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp";
    }

    private void PositionImage()
    {
        if ( _image == null )
        {
            return;
        }

        _imageLocation = new Point(
            ( ClientSize.Width - _imageWidth ) / 2,
            ( ClientSize.Height - MainMenuStrip.Height - _imageHeight ) / 2 + MainMenuStrip.Height
        );
    }

    private void OnOpenClicked( object sender, EventArgs e )
    {
        if ( _openFileDialog.ShowDialog() == DialogResult.OK )
        {
            _image?.Dispose();
            _cachedImage?.Dispose();
            _image = Image.FromFile( _openFileDialog.FileName );

            CreateCheckerBoard( _imageWidth, _imageHeight );
            UpdateCachedImage();

            PositionImage();
            Invalidate();
        }
    }

    protected override void OnMouseDown( MouseEventArgs e )
    {
        base.OnMouseDown( e );
        if ( _image == null )
        {
            return;
        }

        var rect = new Rectangle( _imageLocation.X, _imageLocation.Y, _imageWidth, _imageHeight );
        if ( rect.Contains( e.Location ) )
        {
            _isDragging = true;
            _startPoint = e.Location;
        }
    }

    protected override void OnMouseMove( MouseEventArgs e )
    {
        base.OnMouseMove( e );
        if ( !_isDragging )
        {
            return;
        }

        int dx = e.X - _startPoint.X;
        int dy = e.Y - _startPoint.Y;

        _imageLocation.X += dx;
        _imageLocation.Y += dy;

        _startPoint = e.Location;

        ConstrainImage();
        Invalidate();
    }

    protected override void OnMouseUp( MouseEventArgs e )
    {
        base.OnMouseUp( e );
        _isDragging = false;
    }

    private void ConstrainImage()
    {
        int topOffset = MainMenuStrip?.Height ?? 0;
        int minX = Math.Min( 0, ClientSize.Width - _imageWidth );
        int maxX = Math.Max( 0, ClientSize.Width - _imageWidth );
        int minY = Math.Min( topOffset, ClientSize.Height - _imageHeight );
        int maxY = Math.Max( topOffset, ClientSize.Height - _imageHeight );

        _imageLocation.X = Math.Clamp( _imageLocation.X, minX, maxX );
        _imageLocation.Y = Math.Clamp( _imageLocation.Y, minY, maxY );
    }

    protected override void OnPaint( PaintEventArgs e )
    {
        base.OnPaint( e );
        if ( _image == null )
        {
            return;
        }

        var g = e.Graphics;

        if ( _cachedImage != null )
        {
            g.DrawImage( _cachedImage, _imageLocation );
        }
        else
        {
            var destRect = new Rectangle( _imageLocation.X, _imageLocation.Y, _imageWidth, _imageHeight );
            if ( HasAlphaChannel( _image ) && _checkerBoard != null )
            {
                g.DrawImage( _checkerBoard, _imageLocation );
            }

            g.DrawImage( _image, destRect );
        }
    }

    private void CreateCheckerBoard( int width, int height )
    {
        _checkerBoard?.Dispose();
        _checkerBoard = new Bitmap( width, height );
        using ( var g = Graphics.FromImage( _checkerBoard ) )
        {
            DrawCheckerBoard( g, new Rectangle( 0, 0, width, height ) );
        }
    }

    private void UpdateCachedImage()
    {
        if ( _image == null || _checkerBoard == null )
        {
            return;
        }

        _cachedImage?.Dispose();
        _cachedImage = new Bitmap( _imageWidth, _imageHeight );

        using ( var g = Graphics.FromImage( _cachedImage ) )
        {
            g.DrawImage( _checkerBoard, 0, 0, _imageWidth, _imageHeight );
            g.DrawImage( _image, 0, 0, _imageWidth, _imageHeight );
        }
    }

    private bool HasAlphaChannel( Image img )
    {
        return ( img.PixelFormat & PixelFormat.Alpha ) != 0 ||
               ( img.PixelFormat & PixelFormat.PAlpha ) != 0;
    }

    private void DrawCheckerBoard( Graphics g, Rectangle area )
    {
        int cellSize = 20;
        for ( int y = area.Top; y < area.Bottom; y += cellSize )
        {
            for ( int x = area.Left; x < area.Right; x += cellSize )
            {
                bool isDark = ( ( x / cellSize ) + ( y / cellSize ) ) % 2 == 0;
                g.FillRectangle( isDark ? Brushes.LightGray : Brushes.Gray, x, y, cellSize, cellSize );
            }
        }
    }
}
