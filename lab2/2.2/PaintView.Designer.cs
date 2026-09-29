using System.ComponentModel;

namespace _2._2
{
    partial class PaintView
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fileMenu = new ToolStripMenuItem();
            newMenu = new ToolStripMenuItem();
            openMenu = new ToolStripMenuItem();
            saveAsMenu = new ToolStripMenuItem();
            colorMenu = new ToolStripMenuItem();
            colorPanel = new ColorDialog();
            pictureBox = new PictureBox();
            menuStrip1.SuspendLayout();
            ((ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size( 20, 20 );
            menuStrip1.Items.AddRange( new ToolStripItem[] { fileMenu, colorMenu } );
            menuStrip1.Location = new Point( 0, 0 );
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size( 1222, 28 );
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileMenu.DropDownItems.AddRange( new ToolStripItem[] { newMenu, openMenu, saveAsMenu } );
            fileMenu.Name = "fileToolStripMenuItem";
            fileMenu.Size = new Size( 46, 24 );
            fileMenu.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            newMenu.Name = "newToolStripMenuItem";
            newMenu.Size = new Size( 224, 26 );
            newMenu.Text = "New";
            //newToolStripMenuItem.Click +=  newToolStripMenuItem_Click ;
            // 
            // openToolStripMenuItem
            // 
            openMenu.Name = "openToolStripMenuItem";
            openMenu.Size = new Size( 224, 26 );
            openMenu.Text = "Open";
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsMenu.Name = "saveAsToolStripMenuItem";
            saveAsMenu.Size = new Size( 224, 26 );
            saveAsMenu.Text = "Save As";
            // 
            // drawToolStripMenuItem
            // 
            colorMenu.Name = "drawToolStripMenuItem";
            colorMenu.Size = new Size( 59, 24 );
            colorMenu.Text = "Color";
            //drawToolStripMenuItem.Click +=  drawToolStripMenuItem_Click ;
            //
            // PictureBox
            //
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Location = new Point( 0, 30 );
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size( 782, 523 );
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox.TabIndex = 2;
            pictureBox.TabStop = false;
            // 
            // PaintView
            // 
            AutoScaleDimensions = new SizeF( 8F, 20F );
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size( 1222, 529 );
            Controls.Add( menuStrip1 );
            Controls.Add( pictureBox );
            MainMenuStrip = menuStrip1;
            Name = "PaintView";
            Text = "Paint";
            menuStrip1.ResumeLayout( false );
            menuStrip1.PerformLayout();
            ((ISupportInitialize)(pictureBox)).EndInit();
            ResumeLayout( false );
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileMenu;
        private ToolStripMenuItem openMenu;
        private ToolStripMenuItem newMenu;
        private ToolStripMenuItem saveAsMenu;
        private ToolStripMenuItem colorMenu;
        private PictureBox pictureBox;
        private ColorDialog colorPanel;
    }
}
