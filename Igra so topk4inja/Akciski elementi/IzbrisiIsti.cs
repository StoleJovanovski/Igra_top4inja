using System.Drawing;
using _2D_Grafika.Elementi;
using PodatocneStrukture.Liste;
using PodatocneStrukture.Redice;

namespace Igra_so_topk4inja.AkciskiElementi
{
    public class IzbrisiIsti : ElementAkciski
    {
        public const float _DOCNI = 0.05F;

        private GlavnaIgra glIg;
        private Matrica mat;

        private float vreme;

        private TopceVidlivo top;
        private RedicaNiza<Point> rr, rr1;
        private ListaNiza<Point> ll;

        private static WMPLib.WindowsMediaPlayer zvukOs;
        static IzbrisiIsti()
        {
            (zvukOs = new WMPLib.WindowsMediaPlayer() { URL = "Oslobodi.wav" }).controls.stop();
        }

        public IzbrisiIsti(Point p, GlavnaIgra gli, ElementAkciski roditel)
            : base(roditel)
        {
            glIg = gli;
            mat = gli.matrica;

            top = mat.A[p.Y, p.X];

            rr1 = new RedicaNiza<Point>();
            rr = new RedicaNiza<Point>();
            rr.Dodadi(p);

            ll = new ListaNiza<Point>();
            ll.Unesi(rr.ZaVadenje());

            vreme = 0;
        }

        public override Lista<ElementAkciski> Akcija(_2D_Grafika.Prostor p, long dt, float dT)
        {
            if ((vreme -= dT) > 0) return null;
            vreme += _DOCNI;

            Lista<ElementAkciski> l = new ListaNiza<ElementAkciski>();

            Point pp;
            while (!rr.Prazno) {
                pp = rr.Izvadi();
                l.Unesi(BrisiTopce(pp));
                foreach (Point pe in VratiSosedi(pp)) 
                    if (DaliOdgovara(pe) && !ll.SodrziLi(pe)) {
                        rr1.Dodadi(pe);
                        ll.Unesi(pe);
                    }
                }

            OpsteRabote.Razmeni(ref rr, ref rr1);
            if (rr.Prazno) {
                this.Kompletiraj();
                Zavrsno(l);
            }
            return l;
        }

        private ElementAkciski BrisiTopce(Point p)
        {
            glIg.BrPoeni += 173;
            return new BrisiTopce(p, mat, base.Roditel);
        }

        private void Zavrsno(Lista<ElementAkciski> l)
        {
            Point p;

            for (int i = 0, j = 0; j < mat.si; ++j)
                if (mat.A[0,j] == null || this.ll.SodrziLi(new Point(j,0)))
                    if (++i >= 6) {
                        for (i = 0; i < mat.vi; ++i)
                            for (j = 0; j < mat.si; ++j)
                                if (mat.A[i, j] != null && !this.ll.SodrziLi(p = new Point(j,i)))
                                    l.Unesi(new SlobodnoTopce(glIg, p, Roditel));
                        
                        if (ProstorIgra.Zvuk) zvukOs.controls.play();
                        
                        glIg.streliste.KrajUspesno();
                        return;
                    }
            
            top = null;
            ListaNiza<Point> ll = new ListaNiza<Point>();
            for (int j = 0; j < mat.si; ++j)
                Prebaraj(new Point(j, 0), ll);

            
            for (int j, i = 0; i < mat.vi; ++i)
                for (j = 0; j < mat.si; ++j)
                    if (mat.A[i, j] != null && !ll.SodrziLi(p = new Point(j, i)) && !this.ll.SodrziLi(p))
                        //l.Unesi(new _2D_Grafika.Elementi.Kasni(0.3F, BrisiTopce(p)));
                        l.Unesi(new SlobodnoTopce(glIg, p, Roditel));
        }
        private void Prebaraj(Point p, ListaNiza<Point> ll)
        {
            if (DaliOdgovara(p) && !ll.SodrziLi(p) && !this.ll.SodrziLi(p)) {
                ll.Unesi(p);
                foreach (Point pe in VratiSosedi(p))
                    Prebaraj(pe, ll);
            }
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
