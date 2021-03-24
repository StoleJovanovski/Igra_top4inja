using System;
using System.Drawing;
using _2D_Grafika;
using _2D_Grafika.Elementi;
using PodatocneStrukture.Liste;
using System.Media;

namespace Igra_so_topk4inja.AkciskiElementi
{
    public class SlobodnoTopce : ElementAkciski
    {
        public const float _VxMax = 100;
        public const float _VyMax = 100;
        public const float _VV = 400;
        public const float _AA = 40;

        private GlavnaIgra glIg;
        private Kazani kaz;
        private HomKoord2D XYA;
        private TopceFizika topF;

        private static WMPLib.WindowsMediaPlayer zvukTopce, zvukGljup;
        static SlobodnoTopce()
        {
            (zvukTopce = new WMPLib.WindowsMediaPlayer() { URL = "TopceUdar.wav" }).controls.stop();
            (zvukGljup = new WMPLib.WindowsMediaPlayer() { URL = "Gljup.wav" }).controls.stop();
        }

        public SlobodnoTopce(GlavnaIgra gli, Point p, ElementAkciski roditel)
            : base(roditel)
        {
            glIg = gli;
            kaz = gli.kazani;
            XYA = gli.rabotnaPovrsina.XYA;

            topF = new TopceFizika(
                gli.matrica.A[p.Y, p.X],
                _VxMax * (1 - 2*(float)OpsteRabote.rand.NextDouble()),
                _VyMax * (1 - 2*(float)OpsteRabote.rand.NextDouble())
            );
            gli.matrica.IzbrisiTopce(p.Y, p.X);
            gli.kazani.ll.Unesi(topF.top);
        }

        public SlobodnoTopce(GlavnaIgra gli, TopceVidlivo top, ElementAkciski roditel)
            : base(roditel)
        {
            glIg = gli;
            kaz = gli.kazani;
            XYA = gli.rabotnaPovrsina.oblik.XYA;

            double a = (1-2*OpsteRabote.rand.NextDouble()) * _AA * Math.PI / 180;
            topF = new TopceFizika(top, _VV * (float)Math.Sin(a), -_VV * (float)Math.Cos(a));
            gli.kazani.ll.Unesi(topF.top);
        }

        public override Lista<ElementAkciski> Akcija(_2D_Grafika.Prostor p, long dt, float dT)
        {
            topF.Vreme(dT);
            topF.Pridvizi();
            topF.Gravitacija();
            topF.Interakcija(XYA);
            if (
                topF.Interakcija(kaz.rabKazani[0]) ||
                topF.Interakcija(kaz.rabKazani[1]) ||
                topF.Interakcija(kaz.rabKazani[2]) ||
                topF.Interakcija(kaz.rabKazani[3]) ||
                topF.Interakcija(kaz.rabKazani[4]) ||
                topF.Interakcija(kaz.rabKazani[5])) if (ProstorIgra.Zvuk) zvukTopce.controls.play();
            topF.Postavi();

            if (KontaktKazani())
                base.Kompletiraj();
        
            return null;
        }

        private bool KontaktKazani()
        {
            if (topF.Y < 530) return false;

            if (topF.X > kaz.rabKazani[2].X && topF.X < kaz.rabKazani[3].X) glIg.BrPoeni += 512;   else
            if (topF.X > kaz.rabKazani[1].X && topF.X < kaz.rabKazani[4].X) glIg.BrPoeni += 357;   else
            glIg.BrPoeni += 222;

            if (ProstorIgra.Zvuk) zvukGljup.controls.play();

            kaz.ll.Izbrisi(topF.top);
            return true;
        }
    }
}
