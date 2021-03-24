using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace _2D_Grafika
{
    public class Pogled
    {
        public Graphics   Grf { get; set; }
        public HomKoord2D XYA { get; set; }

        private Bitmap  bafer  { get; set; }
        public Graphics Grafik { get; set; }

        public Pogled(Graphics g, HomKoord2D xya)
        {
            Grf = g;
            XYA = xya;
        }

        public void Kalibriraj()
        {
            if (bafer == null || bafer.Size != Grf.VisibleClipBounds.Size && Grf.VisibleClipBounds.Size != Size.Empty)
                Grafik = Graphics.FromImage(bafer = new Bitmap((int)Grf.VisibleClipBounds.Size.Width, (int)Grf.VisibleClipBounds.Size.Height));
        }
        public void Prikazi()
        {
            Grf.DrawImageUnscaled(bafer, Point.Empty);
        }
    }
}
