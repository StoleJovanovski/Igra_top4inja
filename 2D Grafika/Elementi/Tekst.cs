using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace _2D_Grafika.Elementi
{
    public class Tekst : ElementProst, PoseduvaVidlivost
    {
        public HomKoord2D XYA { get; set; }
        public Brush ispoln { get; set; }
        public bool DimenziiTekst { get; set; }

        private Font f;
        public Font font {
            get { return f; }
            set {
                f = value;
                promenaGolemina();
            }
        }

        private String s;
        public String tekst {
            get { return s; }
            set {
                s = value;
                promenaGolemina();
            }
        }

        private SizeF tekDim;

        public Tekst(HomKoord2D XYA, Font font, String tekst, Brush ispoln, bool dimenziiTekst = true)
        {
            this.XYA = XYA;
            this.ispoln = ispoln;

            this.f = font;
            this.s = tekst;

            this.DimenziiTekst = dimenziiTekst;

            tekDim = new SizeF(1,1);
            promenaGolemina();
        }

        public void Crtaj(Pogled p, long segaVreme)
        {
            HomKoord2D XYA = p.XYA * this.XYA;
            XYA.SkalDim(1/tekDim.Width, 1/tekDim.Height);
            p.Grafik.Transform = new System.Drawing.Drawing2D.Matrix(XYA.Xx, XYA.Xy,
                                                                     XYA.Yx, XYA.Yy,
                                                                     XYA.X , XYA.Y );
            p.Grafik.DrawString(tekst, font, ispoln, 0, 0);
            p.Grafik.ResetTransform();
        }

        private void promenaGolemina()
        {
            SizeF sz = Graphics.FromImage(new Bitmap(1, 1)).MeasureString(tekst, font);
            if (sz.Width != 0 && sz.Height != 0) {
                if (DimenziiTekst) XYA.SkalDim(1/tekDim.Width, 1/tekDim.Height);
                tekDim = sz;
                if (DimenziiTekst) XYA.SkalDim(tekDim.Width, tekDim.Height);
            }
        }
    }
}
