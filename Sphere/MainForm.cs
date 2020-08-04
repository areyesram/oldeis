using System;
using System.Drawing;
using System.Windows.Forms;

namespace areyesram
{
    public partial class MainForm : Form
    {
        private readonly Random _rnd = new Random();

        public MainForm()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            var g = Graphics.FromHwnd(Handle);
            var x0 = _rnd.Next(ClientRectangle.Width);
            var y0 = _rnd.Next(ClientRectangle.Height);
            var r = 10 + _rnd.Next(20);
            var r2 = r * r;
            var rgb = _rnd.Next(7) + 1;
            for (var y = -r; y <= r; y++)
            {
                for (var x = -r; x <= r; x++)
                {
                    var z = x * x + y * y;
                    if (z < r2)
                    {
                        var c = 255 - z * 256 / r2;
                        var brush = new SolidBrush(
                            Color.FromArgb(
                                (rgb & 1) != 0 ? c : 0,
                                (rgb & 2) != 0 ? c : 0,
                                (rgb & 4) != 0 ? c : 0)
                        );
                        g.FillRectangle(brush, x0 + x, y0 + y, 1, 1);
                    }
                }
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Application.Exit();
        }
    }
}

