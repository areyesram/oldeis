using System;
using System.Drawing;
using System.Windows.Forms;

namespace areyesram
{
    public partial class MainForm : Form
    {
        private static int _offset = 1;

        public MainForm()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            var bitmap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            for (var y = 0; y < ClientRectangle.Height; y++)
            {
                for (var x = 0; x < ClientRectangle.Width; x++)
                {
                    var i = (x * x + y * y + _offset) & 255;
                    var c = Color.FromArgb(i, i, i);
                    bitmap.SetPixel(x, y, c);
                }
            }
            BackgroundImage = bitmap;
            _offset += 4;
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Application.Exit();
        }
    }
}

