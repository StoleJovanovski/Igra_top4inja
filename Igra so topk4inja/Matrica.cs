using _2D_Grafika;
using _2D_Grafika.Elementi;

namespace Igra_so_topk4inja
{
    public class Matrica : ElementProst, PoseduvaVidlivost
    {
        public const float _XX = GlavnaIgra._XX + TopceVidlivo._R + 2;
        public const float _YY = GlavnaIgra._YY + TopceVidlivo._R + 2;

        public TopceVidlivo[,] A;
        public int vi, si;

        public Matrica()
        {
            A = new TopceVidlivo[vi = 10, si = 11];
        }

        public void Resetiraj() {
            for (int j, i = 0; i < vi; ++i)
                for (j = 0; j < si; ++j)
                    A[i,j] = null;

            for (int j, i = 0; i < 7; ++i)
                for (j = 0; j < si + (i%2==0?0:-1); ++j)
                    if (i > 0 && OpsteRabote.rand.Next(10) == 0)
                        DodadiTopce(i,j,new TopceVidlivo(0,0, true));
                    else
                        DodadiTopce(i, j, new TopceVidlivo(0,0));
        }

        public void DodadiTopce(int i, int j, TopceVidlivo tt)
        {
            tt.X = _XX + 2*TopceVidlivo._R * j + (i%2 == 0 ? 0 : TopceVidlivo._R);
            tt.Y = _YY + 1.732F*TopceVidlivo._R * i;
            A[i,j] = tt;
        }
        public void IzbrisiTopce(int i, int j)
        {
            A[i,j] = null;
        }

        public void Crtaj(Pogled p, long segaVreme)
        {
            foreach (TopceVidlivo el in A)
                if (el != null)
                    el.Crtaj(p, segaVreme);
        }
    }
}
