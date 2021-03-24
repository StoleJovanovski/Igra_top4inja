using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2D_Grafika;
using _2D_Grafika.Elementi;
using System.Drawing;
using _2D_Grafika.KorisnickiInterfejs;

namespace Igra_so_topk4inja
{
    public class ProveriZatvori : ElementProst, PoseduvaNastan
    {
        private bool sigurnoPrekini, prikazano;
        private Prostor prostor;

        ElementSlozen ell;

        public ProveriZatvori(Prostor prostor)
        {
            this.prostor = prostor;

            sigurnoPrekini = prikazano = false;
            ell = new ElementSlozen();

            Panel4agol pp = new Panel4agol(new HomKoord2D(0, 0, 900, 600), b:new SolidBrush(Color.FromArgb(100, Color.Black)));
            pp.priGluvceDvizi += new Spravuvac.SpravuvacGluvce.FunkcijaSpravuvacGluvce(delegate(object povikuvac, System.Windows.Forms.MouseEventArgs arg, PointF T) { return true; });
            pp.priGluvceKlik += new EventHandler(Negiraj);
            ell.Elementi.Unesi(pp);

            KopceAnimacisko<_2D_Grafika.Oblik.Krug> kk = new KopceAnimacisko<_2D_Grafika.Oblik.Krug>(
                new HomKoord2D(375, 225, 150, 150), new _2D_Grafika.Oblik.Krug(450, 300, 70), null,
                new _2D_Grafika.Animacija(new Image[] { new Bitmap("Kopce1.png") }, 10),
                new _2D_Grafika.Animacija(new Image[] { new Bitmap("Kopce2.png") }, 10),
                new _2D_Grafika.Animacija(new Image[] { new Bitmap("Kopce3.png") }, 10), false
            );
            kk.priGluvceKlik += new EventHandler(Potvrdi);
            ell.Elementi.Unesi(kk);
        }

        private void Prikazi()
        {
            if (!prikazano) {
                prikazano = true;
                prostor.Dodadi(ell);
            }
        }
        private void Otstrani()
        {
            if (prikazano) {
                prikazano = false;
                prostor.Izbrisi(ell);
            }
        }

        void Potvrdi(object sender, EventArgs e)
        {
            Otstrani();
            sigurnoPrekini = true;
            ((Spravuvac.SpravuvacZatvori)prostor.spravuvaci.VratiSpravuvac(typeof(Spravuvac.SpravuvacZatvori))).ObidZatvori(this, new System.Windows.Forms.FormClosingEventArgs(System.Windows.Forms.CloseReason.None, true));
        }
        void Negiraj(object sender, EventArgs e)
        {
            Otstrani();
            sigurnoPrekini = false;
        }

        public bool probajZatvori(object povikuvac, object arg)
        {
            if (sigurnoPrekini) return true;
            if (ProstorIgra.Zvuk) System.Media.SystemSounds.Asterisk.Play();
            Prikazi();
            return false;
        }

        public void DodadiSpravuvac(Spravuvaci sprav)
        {
            ((Spravuvac.SpravuvacZatvori)prostor.spravuvaci.VratiSpravuvac(typeof(Spravuvac.SpravuvacZatvori))).DodadiObidZatvori(probajZatvori);
            ((Spravuvac.SpravuvacTastatura)prostor.spravuvaci.VratiSpravuvac(typeof(Spravuvac.SpravuvacTastatura))).DodadiKopcePritisnato(tast);
        }
        public void IzbrisiSpravuvac(Spravuvaci sprav)
        {
            ((Spravuvac.SpravuvacZatvori)prostor.spravuvaci.VratiSpravuvac(typeof(Spravuvac.SpravuvacZatvori))).IzbrisiObidZatvori(probajZatvori);
            ((Spravuvac.SpravuvacTastatura)prostor.spravuvaci.VratiSpravuvac(typeof(Spravuvac.SpravuvacTastatura))).IzbrisiKopcePritisnato(tast);
        }

        private bool tast(object povikuvac, System.Windows.Forms.KeyEventArgs arg)
        {
            if (!prikazano) return false;

            if (arg.KeyData == System.Windows.Forms.Keys.Enter ) Potvrdi(this, arg);
            if (arg.KeyData == System.Windows.Forms.Keys.Escape) Negiraj(this, arg);
            
            return true;
        }
    }
}
