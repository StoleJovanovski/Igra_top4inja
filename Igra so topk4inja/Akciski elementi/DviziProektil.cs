using System;
using System.Drawing;
using _2D_Grafika;
using _2D_Grafika.Elementi;
using PodatocneStrukture.Liste;

namespace Igra_so_topk4inja.AkciskiElementi
{
    public class DviziProektil : ElementAkciski
    {
        public const float _V = 533;

        private GlavnaIgra glIg;
        private HomKoord2D XYA;
        private Streliste strl;
        private Matrica matrica;
        private TopceFizika topF;

        public DviziProektil(GlavnaIgra gli, TopceVidlivo top, PointF V, ElementAkciski roditel)
            : base(roditel)
        {
            glIg = gli;
            strl = gli.streliste;
            XYA = gli.rabotnaPovrsina.XYA;
            matrica = gli.matrica;
            
            topF = new TopceFizika(top, V.X * _V, V.Y * _V);
        }

        public override Lista<ElementAkciski> Akcija(_2D_Grafika.Prostor p, long dt, float dT)
        {
            topF.Vreme(dT);
            topF.Pridvizi();
            topF.Interakcija(XYA);
            topF.Postavi();

            if (KontaktMatrica(matrica)) {
                if (i < matrica.vi)
                    matrica.DodadiTopce(i,j, topF.top);

                strl.OslobodiProektil();
                this.Kompletiraj();
                
                if (i < matrica.vi) {
                    ListaNiza<ElementAkciski> l = new ListaNiza<ElementAkciski>();
                    l.Unesi(new ProveriSosedi(new Point(j,i), glIg, Roditel));
                    return l;
                }
            }

            return null;
        }

        private int i, j;

        private bool KontaktMatrica(Matrica matrica)
        {
            if (topF.Y < Matrica._YY - 0.5F*TopceVidlivo._R) {
                i = 0;
                j = (int)(0.5F * (topF.X-Matrica._XX) / TopceVidlivo._R + 0.5F);
                if (matrica.A[i,j] == null)
                    return true;
            }
            for (int j, i = 0; i < matrica.vi; ++i)
                for (j = 0; j < matrica.si; ++j)
                    if (KontaktTop(matrica.A[i, j], topF.top)) {
                        float x, y, k;
                        do {
                            topF.Vreme(-0.01F);
                            topF.Pridvizi();
                            x = topF.X - matrica.A[i,j].X;
                            y = topF.Y - matrica.A[i,j].Y;
                            k = (float)Math.Sqrt(x*x + y*y);
                        } while (k < 2*TopceVidlivo._R);

                        //topF.Postavi();

                        x /= k;
                        y /= k;

                        if (y < -0.5F) {
                            this.i = i - 1;
                            if (x > 0)  this.j = j + (i%2!=0 ? 1:0);
                            else        this.j = j + (i%2!=0 ? 0:-1);
                        }
                        else
                        if (y > 0.5F) {
                            this.i = i + 1;
                            if (x > 0)  this.j = j + (i%2!=0 ? 1:0);
                            else        this.j = j + (i%2!=0 ? 0:-1);
                        }
                        else {
                            this.i = i;
                            this.j = j + (x>0 ? +1 : -1);
                        }

                        if (this.j < 0) {
                            this.i = i+1;
                            this.j = 0;
                        }
                        else
                        if (this.j >= matrica.si     ||
                            this.j >= matrica.si -1  && this.i % 2 != 0) {
                            this.i = i + 1;
                            this.j = matrica.si - (this.i % 2 == 0 ? 1:2);
                        }
                       // if (this.i < matrica.vi && matrica.A[this.i, this.j] != null) ++this.i;
                        return true;
                    }

            return false;
        }

        private static bool KontaktTop(TopceVidlivo TT1, TopceVidlivo TT2)
        {
            return TT1 != null &&
                   TT2 != null &&
            (TT1.X-TT2.X)*(TT1.X-TT2.X) + (TT1.Y-TT2.Y)*(TT1.Y-TT2.Y) < 0.33F * (TT1.R+TT2.R)*(TT1.R+TT2.R);
        }
    }
}
