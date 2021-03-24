using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2D_Grafika.Elementi;
using PodatocneStrukture.Liste;
using System.Drawing;

namespace Igra_so_topk4inja
{
    public class Kazani : ElementProst, PoseduvaVidlivost
    {
        public Lista<TopceVidlivo> ll;
        public TopceFizika[] rabKazani;

        private Slika slc;

        public static readonly Image slcKaz1, slcKaz2;
        static Kazani()
        {
            slcKaz1 = new Bitmap("Cauldron1.png");
            slcKaz2 = new Bitmap("Cauldron2.png");
        }

        public Kazani()
        {
            ll = new ListaNiza<TopceVidlivo>();

            float x = 20;
            float dx = (GlavnaIgra._SS - x - x) / 5;
            x += GlavnaIgra._XX;
            //*
            rabKazani = new TopceFizika[6] {
                new TopceFizika(new TopceVidlivo(x + dx*0, 520, true){R=7}, 0, 0),
                new TopceFizika(new TopceVidlivo(x + dx*1, 520, true){R=7}, 0, 0),
                new TopceFizika(new TopceVidlivo(x + dx*2, 520, true){R=7}, 0, 0),
                new TopceFizika(new TopceVidlivo(x + dx*3, 520, true){R=7}, 0, 0),
                new TopceFizika(new TopceVidlivo(x + dx*4, 520, true){R=7}, 0, 0),
                new TopceFizika(new TopceVidlivo(x + dx*5, 520, true){R=7}, 0, 0)
            }; /*/
            rabKazani = new TopceFizika[6] {
                new TopceFizika(x + dx*0, 520, 7),
                new TopceFizika(x + dx*1, 520, 7),
                new TopceFizika(x + dx*2, 520, 7),
                new TopceFizika(x + dx*3, 520, 7),
                new TopceFizika(x + dx*4, 520, 7),
                new TopceFizika(x + dx*5, 520, 7),
            };//*/

            dx *= 1.125F;
            slc = new Slika(new _2D_Grafika.HomKoord2D(0,0, dx, dx), null, false);
        }

        public void Resetiraj()
        {
            ll.Isprazni();
        }

        public void Crtaj(_2D_Grafika.Pogled p, long segaVreme)
        {
            slc.slika = slcKaz2;
            for (int i = 0; i+1 < rabKazani.Length; ++i) {
                slc.XYA.Pozicioniraj(rabKazani[i].X-5, rabKazani[i].Y-17);
                slc.Crtaj(p, segaVreme);
            }
               
            foreach (TopceVidlivo el in ll)
                el.Crtaj(p, segaVreme);

            slc.slika = slcKaz1;
            for (int i = 0; i+1 < rabKazani.Length; ++i) {
                slc.XYA.Pozicioniraj(rabKazani[i].X-5, rabKazani[i].Y-17);
                slc.Crtaj(p, segaVreme);
            }

            //for (int i = 0; i + 2 < rabKazani.Length; ++i)
            //    rabKazani[i].top.Crtaj(p, segaVreme);
        }
    }
}
