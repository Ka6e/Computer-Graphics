using System.Drawing;
using System.Windows.Forms;
using System;
using System.Drawing.Imaging;

namespace PaintApp
{
    public class PaintPresenter
    {
        private PaintModel _model;
        private IPaintView _view;
        private Point _lastPoint;
        private bool _isDrawing;

        public PaintPresenter( IPaintView view, PaintModel model )
        {
            _view = view;
            _model = model;
            _view.OpenFile += OpenFile;
            _view.NewFile += NewFile;
            _view.StartDrawing += StartDrawing;
            _view.Draw += Draw;
            _view.StopDrawing += StopDrawing;
            _view.SaveFile += SaveAsFile;
            _view.OpenColorDialog += OpenColorPicker;
        }

        private void NewFile( object sender, EventArgs e )
        {
            _model.InitializeDrawing( _view.CanvasWidth, _view.CanvasHeight );
            _view.Canvas = _model.Drawing;
        }

        private void OpenFile( object sender, EventArgs e )
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.png;*.bmp;*.jpg"
            };
            if ( openFileDialog.ShowDialog() == DialogResult.OK )
            {
                _model.AddImage( openFileDialog.FileName );
                _view.Canvas = _model.Drawing;
            }
        }

        private void SaveAsFile( object sender, EventArgs e )
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PNG Image|*.png|Bitmap Image|*.bmp|JPEG Image|*.jpg"
            };
            if ( saveFileDialog.ShowDialog() == DialogResult.OK )
            {
                ImageFormat format = ImageFormat.Png;
                string extension = System.IO.Path.GetExtension( saveFileDialog.FileName ).ToLower();
                if ( extension == ".bmp" ) format = ImageFormat.Bmp;
                else if ( extension == ".jpg" ) format = ImageFormat.Jpeg;
                _model.SaveImage( saveFileDialog.FileName, format );
            }
        }

        private void StartDrawing( object sender, MouseEventArgs e )
        {
            if ( _model.Drawing == null )
                return;
            if ( e.Button == MouseButtons.Left )
            {
                _isDrawing = true;
                _lastPoint = e.Location;
            }
        }

        private void Draw( object sender, MouseEventArgs e )
        {
            if ( _isDrawing )
            {
                Pen pen = _view.Pen;
                _model.DrawLine( pen, _lastPoint, e.Location );
                _lastPoint = e.Location;
                _view.Canvas = _model.Drawing;
            }
        }

        private void StopDrawing( object sender, MouseEventArgs e )
        {
            if ( e.Button == MouseButtons.Left )
            {
                _isDrawing = false;
            }
        }

        private void OpenColorPicker( object sender, EventArgs e )
        {
            ColorDialog colorDialog = new ColorDialog();
            if ( colorDialog.ShowDialog() == DialogResult.Cancel )
                return;

            _model.CurrentColor = colorDialog.Color;
            _view.Pen.Color = colorDialog.Color;
        }
    }
}
