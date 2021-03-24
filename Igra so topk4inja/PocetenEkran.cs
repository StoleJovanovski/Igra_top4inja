using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2D_Grafika.Elementi;
using _2D_Grafika.KorisnickiInterfejs;
using _2D_Grafika.Oblik;
using _2D_Grafika;
using System.Drawing;

namespace Igra_so_topk4inja
{
    public class PocetenEkran : ElementSlozen
    {
        public const float _SS = 120;
        public const float _VV = 33;
        public const float _XX = (900-_SS) * 0.5F;
        public const float _YY1 = 250;
        public const float _YY2 = 330;
        public const float _YY3 = 410;

        public Slika pozadina;
        public Kopce4agol kopIskluci, kopZvuk, kopNova;
        public Tekst tekKopI, tekKopZvuk, tekKopNov;

        public PocetenEkran(ProstorIgra pr)
        {
            pozadina = new Slika(new HomKoord2D(), new Bitmap("PozadinaP.jpg"));
            
            Font ff = new System.Drawing.Font("Monotype Corsiva", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            
            kopNova = new Kopce4agol(new HomKoord2D(_XX, _YY1, _SS, _VV));
            kopNova.priGluvceKlik += new EventHandler(delegate(object sender, EventArgs e) {
                pr.Mapa();
            });
            tekKopNov = new Tekst(new HomKoord2D(_XX+5, _YY1+3), ff, "Нова игра", new SolidBrush(Color.Cyan));

            kopZvuk = new Kopce4agol(new HomKoord2D(_XX, _YY2, _SS, _VV));
            kopZvuk.priGluvceKlik += new EventHandler(delegate(object sender, EventArgs e) {
                if (ProstorIgra.Zvuk = !ProstorIgra.Zvuk) tekKopZvuk.ispoln = Brushes.GreenYellow;
                else                                      tekKopZvuk.ispoln = Brushes.Red;
            });
            tekKopZvuk = new Tekst(new HomKoord2D(_XX + 30, _YY2 + 3), ff, "Звук", new SolidBrush(Color.Red));

            kopIskluci = new Kopce4agol(new HomKoord2D(_XX, _YY3, _SS, _VV));
            kopIskluci.priGluvceKlik += new EventHandler(delegate(object sender, EventArgs e) {
                ((Spravuvac.SpravuvacZatvori)pr.spravuvaci.VratiSpravuvac(typeof(Spravuvac.SpravuvacZatvori))).ObidZatvori(this, new System.Windows.Forms.FormClosingEventArgs(System.Windows.Forms.CloseReason.None, true));
            });
            tekKopI = new Tekst(new HomKoord2D(_XX+15, _YY3+3), ff, "Исклучи", new SolidBrush(Color.Cyan));

            OpsteRabote.PostaviKopceZvuk(kopNova);
            OpsteRabote.PostaviKopceZvuk(kopZvuk);
            OpsteRabote.PostaviKopceZvuk(kopIskluci);

            Elementi.Unesi(pozadina);

            Elementi.Unesi(kopNova);
            Elementi.Unesi(kopZvuk);
            Elementi.Unesi(kopIskluci);

            Elementi.Unesi(tekKopNov);
            Elementi.Unesi(tekKopZvuk);
            Elementi.Unesi(tekKopI);
        }
    }
}
