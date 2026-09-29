using _1._2;

namespace _2._2
{
    public partial class PaintView : Form, IPaintView
    {
        private Bitmap _canvas;
        public Bitmap Canvas
        {
            get { return _canvas; }
            set { _canvas = value; pictureBox.Image = _canvas; }
        }
        public int CanasWidth => ClientSize.Width;
        public int CanvasHeight => ClientSize.Height;

        public event EventHandler NewFile;
        public event EventHandler SaveFile;
        public event EventHandler OpenFile;
        public event EventHandler OpenColorDialog;
        public event MouseEventHandler StartDraw;
        public event MouseEventHandler Draw;
        public event MouseEventHandler StopDraw;

        public PaintView()
        {
            InitializeComponent();
            DoubleBuffered = true;

            pictureBox.MouseDown += ( s, e ) => StartDraw( s, e );
            pictureBox.MouseMove += ( s, e ) => Draw( s, e );
            pictureBox.MouseUp += ( s, e ) => StopDraw( s, e );

            openMenu.Click += ( s, e ) => OpenFile( s, e );
            newMenu.Click += ( s, e ) => NewFile( s, e );
            saveAsMenu.Click += ( s, e ) => SaveFile( s, e );
            colorMenu.Click += ( s, e ) => OpenColorDialog( s, e );
        }


        //private void InitializePictureBox()
        //{
        //    _pictureBox = new PictureBox();
        //    _pictureBox.Size = new Size( 400, 300 );
        //    _pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        //    _pictureBox.Location = new Point( ( ClientSize.Width - _pictureBox.Width ) / 2, ( ClientSize.Height - _pictureBox.Height ) / 2 );
        //    _pictureBox.Anchor = AnchorStyles.None;

        //    Bitmap bpm = new Bitmap( _pictureBox.Width, _pictureBox.Height );
        //    _pictureBox.Image = bpm;

        //    _graphics = Graphics.FromImage( bpm );
        //    _graphics.Clear( Color.White );
        //    Controls.Add( _pictureBox );
        //}

        //private void drawToolStripMenuItem_Click( object sender, EventArgs e )
        //{
        //    if ( colorPanel.ShowDialog() == DialogResult.OK )
        //    {
        //        _pen.Color = colorPanel.Color;
        //    }
        //}

        //private void OnMouseDown( object sender, MouseEventArgs e )
        //{
        //    if ( e.Button == MouseButtons.Left )
        //    {
        //        _isDrawing = true;
        //        _previousPoint = e.Location;
        //    }
        //}

        //private void OnMouseMove( object sender, MouseEventArgs e )
        //{
        //    if ( _isDrawing )
        //    {
        //        _graphics.DrawLine( _pen, _previousPoint, e.Location );
        //        _previousPoint = e.Location;

        //        _pictureBox.Invalidate();
        //    }
        //}

        //private void OnMouseUp( object sender, MouseEventArgs e )
        //{
        //    if ( e.Button == MouseButtons.Left )
        //    {
        //        _isDrawing = false;
        //    }
        //}

        //private void newToolStripMenuItem_Click( object sender, EventArgs e )
        //{
        //    InitializePictureBox();
        //}
    }
}
