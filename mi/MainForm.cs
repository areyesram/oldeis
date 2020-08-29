using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace areyesram
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _bounds = ClientRectangle;
            _bounds.Inflate(-(_bounds.Width / 4), -(_bounds.Height / 4));
            timer.Start();
        }

        private State _state = State.CountDown;
        private int _count = 6;
        private List<PointF> _pixels;
        private List<PointF> _speed;
        private Rectangle _bounds;

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var w = ClientRectangle.Width;
            var h = ClientRectangle.Height;
            var r = new Random();
            Bitmap bmp;
            if (_state == State.CountDown)
            {
                _count--;
                g.DrawString($"Este mensaje se autodestruirá en {_count} segundos.",
                   Font, Brushes.White, _bounds);
                if (_count == 0)
                {
                    bmp = new Bitmap(w, h);
                    DrawToBitmap(bmp, ClientRectangle);
                    _pixels = new List<PointF>();
                    var back = bmp.GetPixel(0, 0);
                    for (var x = 0; x < bmp.Width; x++)
                    {
                        for (var y = 0; y < bmp.Height; y++)
                        {
                            if (!bmp.GetPixel(x, y).Equals(back))
                                _pixels.Add(new Point(x, y));
                        }
                    }
                    _speed = _pixels.Select(p => new PointF(
                            (float)r.NextDouble() * 40 - 20,
                            (float)r.NextDouble() * 40 - 20))
                        .ToList();
                    _state = State.Explode;
                    timer.Interval = 15;
                }
            }
            else
            {
                _pixels = _pixels.Select((p, i) => new PointF(p.X + _speed[i].X, p.Y + _speed[i].Y)).ToList();
                bmp = new Bitmap(w, h);
                var f = false;
                foreach (var p in _pixels)
                {
                    if (p.X > 0 && p.X < bmp.Width && p.Y > 0 && p.Y < bmp.Height)
                    {
                        bmp.SetPixel((int)p.X, (int)p.Y, Color.White);
                        f = true;
                    }
                }
                if (!f) Application.Exit();
                g.DrawImageUnscaled(bmp, ClientRectangle);
                _speed = _speed.Select(p => new PointF(p.X, p.Y + 0.5f)).ToList();
            }
        }

        private enum State
        {
            CountDown,
            Explode
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
