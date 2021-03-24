using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace _2D_Grafika.Elementi
{
    public class _4agolnik : ElementProst, PoseduvaVidlivost
    {
        public Pen      pen { get; set; }
        public Brush    brs { get; set; }
        public HomKoord2D XYA { get; set; }

        public _4agolnik(HomKoord2D xya, Pen p, Brush b)
        {
            XYA = xya;
            pen = p;
            brs = b;
        }

        public void Crtaj(Pogled p, long segaVreme)
        {
            HomKoord2D XYA = p.XYA * this.XYA;

            PointF[] pp = new PointF[4] {
                new PointF(XYA.X, XYA.Y),
                new PointF(XYA.X + XYA.Xx, XYA.Y + XYA.Xy),
                new PointF(XYA.X + XYA.Xx + XYA.Yx, XYA.Y + XYA.Xy + XYA.Yy),
                new PointF(XYA.X + XYA.Yx, XYA.Y + XYA.Yy)
            };
            p.Grafik.FillPolygon(brs, pp);
            p.Grafik.DrawPolygon(pen, pp);
        }
    }
}
