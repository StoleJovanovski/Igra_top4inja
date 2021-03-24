using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2D_Grafika.Elementi;
using System.Drawing;
using _2D_Grafika;

namespace Igra_so_topk4inja
{
    public class TopceVidlivo : ElementProst, PoseduvaVidlivost
    {
        public const float _R = 20;

        public float X {
            get { return izgled.XYA.X  + R; }
            set { izgled.XYA.X = value - R; }
        }
        public float Y {
            get { return izgled.XYA.Y  + R; }
            set { izgled.XYA.Y = value - R; }
        }
        public float R {
            get { return izgled.XYA.Xx  / 2; }
            set {
                izgled.XYA.X += R;
                izgled.XYA.Y += R;
                izgled.XYA.Xx = value * 2;
                izgled.XYA.Yy = value * 2;
                izgled.XYA.X -= value;
                izgled.XYA.Y -= value;
            }
        }

        public static int nn;

        private static readonly Image[] toptop;
        static TopceVidlivo()
        {
            toptop = new Image[6];
            toptop[0] = new Bitmap("TopceVioletovo.png");
            toptop[1] = new Bitmap("TopceSino.png");
            toptop[2] = new Bitmap("TopceZeleno.png");
            toptop[3] = new Bitmap("TopceZolto.png");
            toptop[4] = new Bitmap("TopcePortokalovo.png");
            toptop[5] = new Bitmap("TopceCrno.png");
        }

        public Slika izgled;
        public TopceVidlivo(float x, float y, float r, Image slk)
        {
            izgled = new Slika(new HomKoord2D(x-r, y-r, r+r, r+r), slk, false);
        }
        public TopceVidlivo(float x, float y, bool crno = false)
            : this(x,y, _R, toptop[crno ? 5 : OpsteRabote.rand.Next(nn)]) { }

        public void Crtaj(_2D_Grafika.Pogled p, long segaVreme)
        {
            izgled.Crtaj(p, segaVreme);
        }
    }
}
// *