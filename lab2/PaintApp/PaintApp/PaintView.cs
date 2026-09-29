using System.Drawing;
using System;
using System.Windows.Forms;


namespace PaintApp
{
    public partial class PaintView : Form, IPaintView
    {
        private Bitmap _canvas;
        public Bitmap Canvas
        {
            get { return _canvas; }
            set { _canvas = value; pictureBox.Image = _canvas; }
        }
        public Pen Pen { get; set; } = new Pen( Color.Black, 3 );

        public event EventHandler SaveFile;
        public event MouseEventHandler StartDrawing;
        public event MouseEventHandler Draw;
        public event MouseEventHandler StopDrawing;
        public event EventHandler NewFile;
        public event EventHandler OpenFile;
        public event EventHandler OpenColorDialog;
        public int CanvasWidth => ClientSize.Width;
        public int CanvasHeight => ClientSize.Height;

        public PaintView()
        {
            InitializeComponent();

            DoubleBuffered = true;

            pictureBox.Image = Canvas;

            pictureBox.MouseDown += ( s, e ) => StartDrawing( s, e );
            pictureBox.MouseMove += ( s, e ) => Draw( s, e );
            pictureBox.MouseUp += ( s, e ) => StopDrawing( s, e );

            openMenu.Click += ( s, e ) => OpenFile( s, e );
            newMenu.Click += ( s, e ) => NewFile( s, e );
            saveAsMenu.Click += ( s, e ) => SaveFile( s, e );
            colorMenu.Click += ( s, e ) => OpenColorDialog( s, e );
        }
    }
}
