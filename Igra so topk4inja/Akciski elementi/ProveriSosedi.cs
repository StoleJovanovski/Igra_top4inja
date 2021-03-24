using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2D_Grafika.Elementi;
using System.Drawing;
using PodatocneStrukture.Liste;
using PodatocneStrukture.Redice;

namespace Igra_so_topk4inja.AkciskiElementi
{
    public class ProveriSosedi : ElementAkciski
    {
        private GlavnaIgra glIg;
        private Matrica mat;
        private Point p;

        private TopceVidlivo top;

        private static WMPLib.WindowsMediaPlayer zvukBrs, zvukFiks;
        static ProveriSosedi()
        {
            (zvukBrs = new WMPLib.WindowsMediaPlayer() { URL = "TopceBrisi.wav" }).controls.stop();
            (zvukFiks = new WMPLib.WindowsMediaPlayer() { URL = "TopceFiks.wav" }).controls.stop();
        }

        public ProveriSosedi(Point p, GlavnaIgra gli, ElementAkciski roditel)
            : base(roditel)
        {
            this.glIg = gli;
            this.mat = gli.matrica;
            this.p = p;

            top = mat.A[p.Y, p.X];
        }

        public override Lista<ElementAkciski> Akcija(_2D_Grafika.Prostor p, long dt, float dT)
        {
            int k = 1;
            RedicaNiza<Point> rr = new RedicaNiza<Point>();
            rr.Dodadi(this.p);
            ListaNiza<Point> ll = new ListaNiza<Point>();
            ll.Unesi(rr.ZaVadenje());

            while (!rr.Prazno)
                foreach (Point pe in VratiSosedi(rr.Izvadi()))
                    if (DaliOdgovara(pe) && !ll.SodrziLi(pe)) {
                        rr.Dodadi(pe);
                        ll.Unesi(pe);
                        if (++k >= 3) {
                            this.Kompletiraj();
                            
                            if (ProstorIgra.Zvuk) zvukBrs.controls.play();
                            
                            ListaNiza<ElementAkciski> l = new ListaNiza<ElementAkciski>();
                            l.Unesi(new IzbrisiIsti(this.p, glIg, Roditel));
                            return l;
                        }
                    }
            
            if (ProstorIgra.Zvuk && k<3) zvukFiks.controls.play();

            this.NeuspZavrsi();
            return null;
        }

        private static Point[] VratiSosedi(Point p)
        {
            return new Point[6] {
                new Point(p.X+1, p.Y),
                new Point(p.X-1, p.Y),
                new Point(p.X, p.Y+1),
                new Point(p.X, p.Y-1),
                new Point(p.X + (p.Y%2!=0 ? +1:-1), p.Y+1),
                new Point(p.X + (p.Y%2!=0 ? +1:-1), p.Y-1)
            };
        }

        private bool DaliOdgovara(Point p)
        {
            int i = p.Y;
            int j = p.X;

            return 0 <= i && i < mat.vi  &&
                   0 <= j && j < mat.si  &&
                   mat.A[i, j] != null   &&
                  (top == null || top.izgled.slika == mat.A[i,j].izgled.slika);
        }
    }
}
