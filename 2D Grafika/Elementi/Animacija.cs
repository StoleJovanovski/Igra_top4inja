using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace _2D_Grafika.Elementi
{
    public class Animacija : ElementProst, PoseduvaVidlivost
    {
        public _2D_Grafika.Animacija Anim { get; set; }
        public HomKoord2D XYA { get; set; }

        public Animacija(HomKoord2D xya, _2D_Grafika.Animacija anim, bool DimenziiSlika = true)
        {
            XYA = xya;
            Anim = anim;

            if (DimenziiSlika) XYA.SkalDim(anim.Sirina, anim.Visina);
        }

        public void Crtaj(Pogled p, long segaVreme)
        {
            HomKoord2D XYA = p.XYA * this.XYA;

            PointF[] pp = new PointF[3] {
                new PointF(XYA.X, XYA.Y),
                new PointF(XYA.X + XYA.Xx, XYA.Y + XYA.Xy),
                new PointF(XYA.X + XYA.Yx, XYA.Y + XYA.Yy)
            };

            p.Grafik.DrawImage(Anim.SlikaVoMoment(segaVreme), pp);
        }
    }
}
