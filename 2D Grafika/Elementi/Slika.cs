using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace _2D_Grafika.Elementi
{
    public class Slika : ElementProst, PoseduvaVidlivost
    {
        public Image slika { get; set; }
        public HomKoord2D XYA { get; set; }

        public Slika(HomKoord2D xya, Image slk, bool dimenziiSlika = true)
        {
            XYA = xya;
            slika = slk;

            if (dimenziiSlika) XYA.SkalDim(slika.Width, slika.Height);
        }
        
        public void Crtaj(Pogled p, long segaVreme)
        {
            HomKoord2D XYA = p.XYA * this.XYA;

            PointF[] pp = new PointF[3] {
                new PointF(XYA.X, XYA.Y),
                new PointF(XYA.X + XYA.Xx, XYA.Y + XYA.Xy),
                new PointF(XYA.X + XYA.Yx, XYA.Y + XYA.Yy)
            };

            p.Grafik.DrawImage(slika, pp);
        }
    }
}
