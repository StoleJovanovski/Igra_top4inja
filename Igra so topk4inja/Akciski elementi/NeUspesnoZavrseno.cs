using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2D_Grafika.Elementi;
using System.Drawing;

namespace Igra_so_topk4inja.AkciskiElementi
{
    class NeUspesnoZavrseno : ElementAkciski
    {
        public const float _TT1 = 1;
        public const float _TT2 = 4;

        private GlavnaIgra glavIgr;

        private float k;
        private bool b;

        private float Xp1, Xp2, Yp1, Yp2;
        private Color b1, b2;

        private WMPLib.WindowsMediaPlayer zvuk;
        private static WMPLib.WindowsMediaPlayer zvukUs, zvukNeus;
        static NeUspesnoZavrseno()
        {
            (zvukUs = new WMPLib.WindowsMediaPlayer() { URL = "KrajUsp.wav" }).controls.stop();
            (zvukNeus = new WMPLib.WindowsMediaPlayer() { URL = "KrajNeUsp.wav" }).controls.stop();
        }

        public NeUspesnoZavrseno(GlavnaIgra glavIgr, bool usp, ElementAkciski roditel)
            : base(roditel)
        {
            this.glavIgr = glavIgr;

            k = 0;
            b = true;

            Xp1 = glavIgr.poenPozd.XYA.X;
            Yp1 = glavIgr.poenPozd.XYA.Y;
            Xp2 = (900-glavIgr.poenPozd.XYA.Xx) * 0.5F;
            Yp2 = (600-glavIgr.poenPozd.XYA.Yy) * 0.5F;

            b1 = ((SolidBrush)glavIgr.poenPozd.brs).Color;
            if (usp) {  b2 = Color.Blue; zvuk = zvukUs;  }
            else     {  b2 = Color.Red;  zvuk = zvukNeus;  }
            zvuk.controls.stop();
        }

        public override PodatocneStrukture.Liste.Lista<ElementAkciski> Akcija(_2D_Grafika.Prostor p, long dt, float dT)
        {
            if (b)
                if ((k += dT / _TT1) < 1) {
                    glavIgr.poenPozd.brs = new SolidBrush(OpsteRabote.Mesaj(b1, b2, k));
                    glavIgr.poenPozd.XYA.X = Xp1 + (Xp2 - Xp1) * k;
                    glavIgr.poenPozd.XYA.Y = Yp1 + (Yp2 - Yp1) * k;
                    glavIgr.poenTekst.XYA.X = glavIgr.poenPozd.XYA.X + 11;
                    glavIgr.poenTekst.XYA.Y = glavIgr.poenPozd.XYA.Y + 11;
                } else {
                    k = 0;
                    b = false;
                    if (ProstorIgra.Zvuk) zvuk.controls.play();
                }
            else
                if ((k += dT / _TT2) >= 1) {
                    ((ProstorIgra)p).Mapa();
                    base.Kompletiraj();
                }
            return null;
        }
    }
}
